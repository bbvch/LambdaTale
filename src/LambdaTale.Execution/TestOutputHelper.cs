using System;
using System.Collections.Concurrent;
using System.Globalization;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LambdaTale.Execution;

public class TestOutputHelper(IMessageBus messageBus) : ITestOutputHelper
{
    private readonly ConcurrentQueue<string> messages = new();
    private ITest test;

    public void SetScope(ITest t)
    {
        this.test = t;
        while (this.messages.TryDequeue(out var message))
        {
            this.Queue(t, message);
        }
    }

    public void WriteLine(string message)
    {
        if (this.test is { } t)
        {
            this.Queue(t, message);
        }
        else
        {
            this.messages.Enqueue(message);
        }
    }

    public void WriteLine(string format, params object[] args) =>
        this.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));

    private void Queue(ITest t, string message) =>
        messageBus.QueueMessage(new TestOutput(t, message + Environment.NewLine));
}
