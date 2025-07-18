using System;
using Xunit;
using Xunit.Sdk;

namespace LambdaTale;

/// <summary>
/// Applied to a method to indicate the definition of a scenario.
/// A scenario can also be fed examples from a data source, mapping to parameters on the scenario method.
/// If the data source contains multiple rows,
/// then the scenario method is executed multiple times (once with each data row).
/// Examples can be fed to the scenario by applying one or more instances of <see cref="ExampleAttribute"/>
/// or any other attribute inheriting from <see cref="Xunit.Sdk.DataAttribute"/>.
/// E.g. <see cref="Xunit.InlineDataAttribute"/> or
/// <see cref="Xunit.MemberDataAttribute"/>.
/// </summary>
[XunitTestCaseDiscoverer("LambdaTale.Execution.ScenarioDiscoverer", "bbv.LambdaTale.Execution")]
[AttributeUsage(AttributeTargets.Method)]
public class ScenarioAttribute : FactAttribute;
