# LambdaTale

_[![LambdaTale NuGet version](https://img.shields.io/nuget/v/bbv.LambdaTale?label=bbv.LambdaTale
)](https://www.nuget.org/packages/bbv.LambdaTale)_ is a [xUnit.net](https://github.com/xunit/xunit) extension for describing each step in a test with natural language.

> [!NOTE]
> This is a fork of [xBehave.net](https://github.com/adamralph/xbehave.net). Adam, thank you very much!

_[![CI](https://github.com/bbvch/LambdaTale/workflows/.github/workflows/ci.yml/badge.svg)](https://github.com/bbvch/LambdaTale/actions?query=workflow%3A.github%2Fworkflows%2Fci.yml)_
_[![CodeQL](https://github.com/bbvch/LambdaTale/workflows/.github/workflows/codeql-analysis.yml/badge.svg)](https://github.com/bbvch/LambdaTale/actions?query=workflow%3A.github%2Fworkflows%2Fcodeql-analysis.yml)_
_[![Lint](https://github.com/bbvch/LambdaTale/workflows/.github/workflows/lint.yml/badge.svg)](https://github.com/bbvch/LambdaTale/actions?query=workflow%3A.github%2Fworkflows%2Flint.yml)_
_[![Spell check](https://github.com/bbvch/LambdaTale/workflows/.github/workflows/spell-check.yml/badge.svg)](https://github.com/bbvch/LambdaTale/actions?query=workflow%3A.github%2Fworkflows%2Fspell-check.yml)_

Platform support: [.NET Standard 2.0 and upwards](https://docs.microsoft.com/en-us/dotnet/standard/net-standard).

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

## Packages

The [`LambdaTale` package](https://www.nuget.org/packages/bbv.LambdaTale) depends on the [`xunit.core` package](https://www.nuget.org/packages/xunit.core/). That means you get only the minimum dependencies required to write and execute LambdaTale scenarios.

## Versions

LambdaTale follows the versioning scheme of xUnit.net. xUnit.net and LambdaTale do not follow SemVer and may introduce breaking changes in minor (or even patch) versions. Each minor version of LambdaTale is linked to the equivalent minor version of xUnit.net. The LambdaTale patch version is incremented to the next multiple of 100 each time it is linked to a new xUnit.net patch version. For example, LambdaTale 2.5.0 is linked with xUnit.net 2.5.0, and LambdaTale 2.5.100 is linked with xUnit 2.5.1.

A given LambdaTale patch version may introduce new features, fix bugs, or both.
