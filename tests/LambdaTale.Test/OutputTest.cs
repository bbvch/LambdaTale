using System;
using System.Runtime.CompilerServices;
using Xunit.Abstractions;

namespace LambdaTale.Test;

public sealed class OutputTest : IDisposable
{
    private static int seq;
    private readonly ITestOutputHelper outputHelper;

    [Background]
    public void Background() => this.Log();

    public OutputTest(ITestOutputHelper outputHelper)
    {
        this.outputHelper = outputHelper;
        this.Log();
    }

    [Scenario]
    public void Foo()
    {
        this.Log("before");
        "Bar".x(() => this.Log("Bar"));
        this.Log("in-between");
        "Baz".x(() => this.Log("Baz"));
        this.Log("after");
    }

    public void Dispose() => this.Log();

    private void Log(string message = "", [CallerMemberName] string caller = null) =>
        this.outputHelper.WriteLine("{0} [{1}]: {2}", ++seq, caller, message);
}
