using System;

namespace LambdaTale;

/// <summary>
/// Applied to a method to indicate a background for each scenario defined in the same feature class.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
[IgnoreXunitAnalyzersRule1013]
public class BackgroundAttribute : Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    private sealed class IgnoreXunitAnalyzersRule1013Attribute : Attribute;
}
