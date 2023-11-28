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
    public static IStepBuilder Spec(string text, Func<Task> body) => text.x(body);

    /// <summary>
    /// Defines a step in the current scenario.
    /// </summary>
    /// <param name="text">The step text.</param>
    /// <param name="body">The action that will perform the step.</param>
    /// <returns>
    /// An instance of <see cref="IStepBuilder"/>.
    /// </returns>
    public static IStepBuilder Spec(string text, Action body) => text.x(body);
}
