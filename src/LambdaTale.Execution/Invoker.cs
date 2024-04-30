using System;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace LambdaTale.Execution;

public static class Invoker
{
    public static async Task Invoke(this Func<Task> action, ExceptionAggregator aggregator, ExecutionTimer timer)
    {
        var oldSyncContext = SynchronizationContext.Current;
        try
        {
            var asyncSyncContext = new AsyncTestSyncContext(oldSyncContext);
            SynchronizationContext.SetSynchronizationContext(asyncSyncContext);

            await aggregator?.RunAsync(
                () => timer.AggregateAsync(
                    async () =>
                    {
                        await action();
                        var ex = await asyncSyncContext.WaitForCompletionAsync();
                        if (ex != null)
                        {
                            aggregator.Add(ex);
                        }
                    }));
        }
        finally
        {
            SynchronizationContext.SetSynchronizationContext(oldSyncContext);
        }
    }
}
