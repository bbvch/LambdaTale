# LambdaTale

_[![LambdaTale NuGet version](https://img.shields.io/nuget/v/bbv.LambdaTale?label=bbv.LambdaTale)](https://www.nuget.org/packages/bbv.LambdaTale)_ is a [xUnit.net](https://github.com/xunit/xunit) extension for describing each step in a test with natural language.

> [!NOTE]
> This is a fork of [xBehave.net](https://github.com/adamralph/xbehave.net). Adam, thank you very much!

[![CI](https://github.com/bbvch/LambdaTale/actions/workflows/ci.yml/badge.svg)](https://github.com/bbvch/LambdaTale/actions/workflows/ci.yml)
[![CodeQL](https://github.com/bbvch/LambdaTale/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/bbvch/LambdaTale/actions/workflows/codeql-analysis.yml)
[![Lint](https://github.com/bbvch/LambdaTale/actions/workflows/lint.yml/badge.svg)](https://github.com/bbvch/LambdaTale/actions/workflows/lint.yml)
[![Spell check](https://github.com/bbvch/LambdaTale/actions/workflows/spell-check.yml/badge.svg)](https://github.com/bbvch/LambdaTale/actions/workflows/spell-check.yml)

Platform support: [.NET 8](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8) and [.NET 4.7.2](https://learn.microsoft.com/en-us/dotnet/framework/whats-new) upwards.

## Usage

Install the [`bbv.LambdaTale` NuGet package](https://www.nuget.org/packages/bbv.LambdaTale) and start using LambdaTale in your tests.
LambdaTale can be used in two different ways: Through a string extension method or a static `Spec()` method. Both usages are described below.

### String Extension Method

An example of using LambdaTale with the `x()` extension method is given below.

```csharp
using System.Threading.Tasks;
using LambdaTale;

namespace Your.Tests;

public class SomeFeature
{
    public class OracleService
    {
        public Task<int> Run() => Task.FromResult(42);
    }

    [Scenario]
    public void SimpleDemo(int answer, OracleService sut)
    {
        "Given a magic oracle"
            .x(() => sut = new OracleService());

        "the answer"
            .x(async () => answer = await sut.Run());

        "is always known"
            .x(() => Xunit.Assert.Equal(42, answer));
    }
}
```

### Static Spec Method

An example of using LambdaTale with a [C# 6.0 `using static` directive](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-directive#static-modifier) and the static `Spec()` method is given below.

```csharp
using System.Threading.Tasks;
using LambdaTale;
using static LambdaTale.Specifications;

namespace Your.Tests;

public class SomeFeature
{
    public class OracleService
    {
        public Task<int> Run() => Task.FromResult(42);
    }

    [Scenario]
    public void SimpleDemo(int answer, OracleService sut)
    {
        Spec("Given a magic oracle", () => sut = new OracleService());

        Spec("the answer", async () => answer = await sut.Run());

        Spec("is always known", () => Xunit.Assert.Equal(42, answer));
    }
}
```

### Gotchas

The `ITestOutputHelper` will only write to test steps. Messages written before the first step (e.g. in the constructor, `[Background]`, outside step definitions) will be logged as part of the first step, messages written after the last step (e.g. inside `Dispose`) will be logged as part of the last step.

## Packages

The [`LambdaTale` package](https://www.nuget.org/packages/bbv.LambdaTale) depends on the [`xunit.core` package](https://www.nuget.org/packages/xunit.core/). That means you get only the minimum dependencies required to write and execute LambdaTale scenarios.

## Versions

LambdaTale follows the versioning scheme of xUnit.net. xUnit.net and LambdaTale do not follow SemVer and may introduce breaking changes in minor (or even patch) versions. Each minor version of LambdaTale is linked to the equivalent minor version of xUnit.net. The LambdaTale patch version is incremented to the next multiple of 100 each time it is linked to a new xUnit.net patch version. For example, LambdaTale 2.5.0 is linked with xUnit.net 2.5.0, and LambdaTale 2.5.100 is linked with xUnit 2.5.1.

A given LambdaTale patch version may introduce new features, fix bugs, or both.
