using LambdaTale.Sdk;
using Xunit;

namespace LambdaTale.Test;

public class MetadataFeature
{
    [Scenario]
    [Example("abc")]
    public void UsingMetadata(string text, IStepContext stepContext, IStep step, IScenario scenario)
    {
        "When I execute a step"
            .x(c => stepContext = c)
            .Teardown(c => Assert.Same(stepContext, c));

        "Then the step context contains metadata about the step"
            .x(() => Assert.EndsWith("UsingMetadata(text: \"abc\") [01] When I execute a step", (step = stepContext.Step)?.DisplayName));

        "And the step contains metadata about the scenario"
            .x(() => Assert.EndsWith("UsingMetadata(text: \"abc\")", (scenario = step.Scenario)?.DisplayName));

        "And the step contains metadata about the scenario outline"
            .x(() => Assert.EndsWith("UsingMetadata", scenario.ScenarioOutline?.DisplayName));
    }
}
