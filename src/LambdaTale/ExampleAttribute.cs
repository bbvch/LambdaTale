using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace LambdaTale;

/// <summary>
/// Provides example values for a scenario passed as arguments to the scenario method.
/// This attribute is designed as a synonym of <see cref="Xunit.InlineDataAttribute"/>,
/// which is the most commonly used data attribute, but you can also use any type of attribute derived from
/// <see cref="DataAttribute"/> to provide a data source for a scenario.
/// E.g. <see cref="Xunit.InlineDataAttribute"/> or
/// <see cref="Xunit.MemberDataAttribute"/>.
/// </summary>
/// /// <param name="data">The data values to pass to the scenario.</param>
[DataDiscoverer("Xunit.Sdk.InlineDataDiscoverer", "xunit.core")]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public sealed class ExampleAttribute(params object[] data) : DataAttribute
{
    /// <inheritdoc/>
    public override IEnumerable<object[]> GetData(MethodInfo testMethod) => [data];
}
