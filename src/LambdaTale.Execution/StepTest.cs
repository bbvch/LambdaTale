using System;
using LambdaTale.Sdk;
using Xunit;
using Xunit.Abstractions;

namespace LambdaTale.Execution;

public class StepTest : LongLivedMarshalByRefObject, IStep
{
    public StepTest(IScenario scenario, string displayName)
    {
        this.Scenario = scenario ?? throw new ArgumentNullException(nameof(scenario));
        this.DisplayName = displayName;
    }

    public IScenario Scenario { get; }

    public string DisplayName { get; }

    public ITestCase TestCase => this.Scenario.ScenarioOutline;
}
