using System;
using System.Diagnostics.CodeAnalysis;

namespace LambdaTale;

/// <summary>
/// Applied to a method to indicate a background for each scenario defined in the same feature class.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
#pragma warning disable IDE0079 // Remove unnecessary suppression
[SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Designed for extensibility.")]
#pragma warning restore IDE0079 // Remove unnecessary suppression
[IgnoreXunitAnalyzersRule1013]
public class BackgroundAttribute : Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    private class IgnoreXunitAnalyzersRule1013Attribute : Attribute
    {
    }
}
