using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using LambdaTale.Sdk;
using Xunit.Sdk;

namespace LambdaTale.Execution;

public class StepTestRunner(
    IStepContext stepContext,
    Func<IStepContext, Task> body,
    IMessageBus messageBus,
    Type scenarioClass,
    object[] constructorArguments,
    MethodInfo scenarioMethod,
    object[] scenarioMethodArguments,
    string skipReason,
    IReadOnlyList<BeforeAfterTestAttribute> beforeAfterAttributes,
    ExceptionAggregator aggregator,
    CancellationTokenSource cancellationTokenSource)
    : XunitTestRunner(stepContext.Step,
        messageBus,
        scenarioClass,
        constructorArguments,
        scenarioMethod,
        scenarioMethodArguments,
        skipReason,
        beforeAfterAttributes,
        aggregator,
        cancellationTokenSource)
{
    protected override Task<decimal> InvokeTestMethodAsync(ExceptionAggregator aggregator) =>
        new StepInvoker(stepContext, body, aggregator, this.CancellationTokenSource).RunAsync();
}
