using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LambdaTale.Execution.Extensions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LambdaTale.Execution;

public class ScenarioOutlineTestCaseRunner : XunitTestCaseRunner
{
    private static readonly object[] noArguments = [];

    private readonly IMessageSink diagnosticMessageSink;
    private readonly ExceptionAggregator cleanupAggregator = new();
    private readonly List<ScenarioRunner> scenarioRunners = new();
    private readonly List<IDisposable> disposables = new();
    private Exception dataDiscoveryException;

    public ScenarioOutlineTestCaseRunner(
        IMessageSink diagnosticMessageSink,
        IXunitTestCase scenarioOutline,
        string displayName,
        string skipReason,
        object[] constructorArguments,
        IMessageBus messageBus,
        ExceptionAggregator aggregator,
        CancellationTokenSource cancellationTokenSource)
        : base(
            scenarioOutline,
            displayName,
            skipReason,
            constructorArguments,
            noArguments,
            messageBus,
            aggregator,
            cancellationTokenSource) => this.diagnosticMessageSink = diagnosticMessageSink;

    protected override async Task AfterTestCaseStartingAsync()
    {
        await base.AfterTestCaseStartingAsync();

        try
        {
            var dataAttributes = this.TestCase.TestMethod.Method.GetCustomAttributes(typeof(DataAttribute)).ToList();
            foreach (var dataAttribute in dataAttributes)
            {
                var discovererAttribute = dataAttribute.GetCustomAttributes(typeof(DataDiscovererAttribute)).First();
                var discoverer =
                    ExtensibilityPointFactory.GetDataDiscoverer(this.diagnosticMessageSink, discovererAttribute);

                foreach (var dataRow in discoverer.GetData(dataAttribute, this.TestCase.TestMethod.Method))
                {
                    this.disposables.AddRange(dataRow.OfType<IDisposable>());

                    var info = new ScenarioInfo(this.TestCase.TestMethod.Method, dataRow, this.DisplayName);
                    var test = new Scenario(this.TestCase, info.ScenarioDisplayName);
                    var skipReason = this.SkipReason ?? dataAttribute.GetNamedArgument<string>(nameof(DataAttribute.Skip));
                    var runner = new ScenarioRunner(
                        test,
                        this.MessageBus,
                        this.TestClass,
                        this.ConstructorArguments,
                        info.MethodToRun,
                        info.ConvertedDataRow.ToArray(),
                        skipReason,
                        this.BeforeAfterAttributes,
                        this.Aggregator,
                        this.CancellationTokenSource);
                    this.scenarioRunners.Add(runner);
                }
            }

            if (!this.scenarioRunners.Any())
            {
                var info = new ScenarioInfo(this.TestCase.TestMethod.Method, noArguments, this.DisplayName);
                var test = new Scenario(this.TestCase, info.ScenarioDisplayName);
                var runner = new ScenarioRunner(
                    test,
                    this.MessageBus,
                    this.TestClass,
                    this.ConstructorArguments,
                    info.MethodToRun,
                    info.ConvertedDataRow.ToArray(),
                    this.SkipReason,
                    this.BeforeAfterAttributes,
                    this.Aggregator,
                    this.CancellationTokenSource);
                this.scenarioRunners.Add(runner);
            }
        }
#pragma warning disable CA1031 // Do not catch general exception types
        catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
        {
            this.dataDiscoveryException = ex;
        }
    }

    protected override async Task<RunSummary> RunTestAsync()
    {
        if (this.dataDiscoveryException != null)
        {
            this.MessageBus.Queue(
                new XunitTest(this.TestCase, this.DisplayName),
                test => new TestFailed(test, 0, null, this.dataDiscoveryException.Unwrap()),
                this.CancellationTokenSource);

            return new RunSummary { Total = 1, Failed = 1 };
        }

        var summary = new RunSummary();
        foreach (var scenarioRunner in this.scenarioRunners)
        {
            summary.Aggregate(await scenarioRunner.RunAsync());
        }

        // Run the cleanup here so we can include cleanup time in the run summary,
        // but save any exceptions so we can surface them during the cleanup phase,
        // so they get properly reported as test case cleanup failures.
        var timer = new ExecutionTimer();
        foreach (var disposable in this.disposables)
        {
            timer.Aggregate(() => this.cleanupAggregator.Run(() => disposable.Dispose()));
        }

        summary.Time += timer.Total;
        return summary;
    }

    protected override Task BeforeTestCaseFinishedAsync()
    {
        this.Aggregator.Aggregate(this.cleanupAggregator);

        return base.BeforeTestCaseFinishedAsync();
    }
}
