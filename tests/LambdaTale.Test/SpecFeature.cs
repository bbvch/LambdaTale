using System.Threading.Tasks;
using Xunit;
using static LambdaTale.Specifications;

namespace LambdaTale.Test;

public class SpecFeature
{
    [Scenario]
    public void SpecScenario()
    {
        Spec("Async spec works", async () =>
        {
            await Task.Delay(1);
            Assert.True(true);
        });

        Spec("Sync spec works", () =>
        {
            Task.Delay(1);
            Assert.True(true);
        });
    }
}
