using System;
using System.Threading.Tasks;
using LambdaTale.Sdk;

namespace LambdaTale;

public static class Specifications
{
    public static IStepDefinition Spec(string text, Func<Task> body)
    {
        var stepDefinition = new StepDefinition
        {
            Text = text,
            Body = body == null ? null : _ => body()
        };

        CurrentThread.Add(stepDefinition);
        return stepDefinition;
    }

    public static IStepDefinition Spec(string text, Action body)
    {
        var stepDefinition = new StepDefinition
        {
            Text = text,
            Body = body == null ? null : _ =>
            {
                body();
                return Task.CompletedTask;
            }
        };

        CurrentThread.Add(stepDefinition);
        return stepDefinition;
    }
}
