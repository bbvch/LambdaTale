using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LambdaTale.Execution.Extensions;
using LambdaTale.Sdk;
using Xunit.Sdk;

namespace LambdaTale.Execution;

public class ScenarioRunner
{
    private readonly IScenario scenario;
    private readonly IMessageBus messageBus;
    private readonly Type scenarioClass;
    private readonly object[] constructorArguments;
    private readonly MethodInfo scenarioMethod;
    private readonly object[] scenarioMethodArguments;
    private readonly string skipReason;
    private readonly IReadOnlyList<BeforeAfterTestAttribute> beforeAfterScenarioAttributes;
    private readonly ExceptionAggregator parentAggregator;
    private readonly CancellationTokenSource cancellationTokenSource;

    public ScenarioRunner(
        IScenario scenario,
        IMessageBus messageBus,
        Type scenarioClass,
        object[] constructorArguments,
        MethodInfo scenarioMethod,
        object[] scenarioMethodArguments,
        string skipReason,
        IReadOnlyList<BeforeAfterTestAttribute> beforeAfterScenarioAttributes,
        ExceptionAggregator aggregator,
        CancellationTokenSource cancellationTokenSource)
    {
        this.scenario = scenario ?? throw new ArgumentNullException(nameof(scenario));
        this.messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
        this.scenarioClass = scenarioClass;
        this.constructorArguments = constructorArguments;
        this.scenarioMethod = scenarioMethod;
        this.scenarioMethodArguments = scenarioMethodArguments;
        this.skipReason = skipReason;
        this.beforeAfterScenarioAttributes = beforeAfterScenarioAttributes;
        this.parentAggregator = aggregator ?? throw new ArgumentNullException(nameof(aggregator));
        this.cancellationTokenSource = cancellationTokenSource;
    }

    public async Task<RunSummary> RunAsync()
    {
        if (!string.IsNullOrEmpty(this.skipReason))
        {
            this.messageBus.Queue(
                this.scenario, test => new TestSkipped(test, this.skipReason), this.cancellationTokenSource);

            return new RunSummary { Total = 1, Skipped = 1 };
        }
        else
        {
            var summary = new RunSummary();
            var childAggregator = new ExceptionAggregator(this.parentAggregator);
            if (!childAggregator.HasExceptions)
            {
                summary.Aggregate(await childAggregator.RunAsync(() => this.InvokeScenarioAsync(childAggregator)));
            }

            var exception = childAggregator.ToException();
            if (exception != null)
            {
                summary.Total++;
                summary.Failed++;
                this.messageBus.Queue(
                    this.scenario,
                    test => new TestFailed(test, summary.Time, string.Empty, exception),
                    this.cancellationTokenSource);
            }
            else if (summary.Total == 0)
            {
                summary.Total++;
                this.messageBus.Queue(
                    this.scenario, test => new TestPassed(test, summary.Time, string.Empty), this.cancellationTokenSource);
            }

            return summary;
        }
    }

    private async Task<RunSummary> InvokeScenarioAsync(ExceptionAggregator aggregator) =>
        await new ScenarioInvoker(
                this.scenario,
                this.messageBus,
                this.scenarioClass,
                this.constructorArguments,
                this.scenarioMethod,
                this.scenarioMethodArguments,
                this.beforeAfterScenarioAttributes,
                aggregator,
                this.cancellationTokenSource)
            .RunAsync();
}
