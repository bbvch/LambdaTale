using System;
using System.Threading.Tasks;
using LambdaTale.Sdk;

namespace LambdaTale;

/// <summary>
/// Provides access to step definition methods.
/// </summary>
public static class Specifications
{
    /// <summary>
    /// Defines a step in the current scenario.
    /// </summary>
    /// <param name="text">The step text.</param>
    /// <param name="body">The action that will perform the step.</param>
    /// <returns>
    /// An instance of <see cref="IStepBuilder"/>.
    /// </returns>
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

    /// <summary>
    /// Defines a step in the current scenario.
    /// </summary>
    /// <param name="text">The step text.</param>
    /// <param name="body">The action that will perform the step.</param>
    /// <returns>
    /// An instance of <see cref="IStepBuilder"/>.
    /// </returns>
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
