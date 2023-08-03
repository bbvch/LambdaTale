# bbv.LambdaTale

LambdaTale is an [xUnit.net](https://github.com/xunit/xunit) extension for describing each step in a test with natural language.

> [!NOTE]
> This is a fork of [xBehave.net](https://github.com/adamralph/xbehave.net). Adam, thank you very much!

_[![Build status](https://github.com/bbvch/LambdaTale/workflows/.github/workflows/ci.yml/badge.svg)](https://github.com/bbvch/LambdaTale/actions)_
_[![CodeQL analysis](https://github.com/bbvch/LambdaTale/workflows/.github/workflows/codeql-analysis.yml/badge.svg)](https://github.com/bbvch/LambdaTale/actions?query=workflow%3A.github%2Fworkflows%2Fcodeql-analysis.yml)_
_[![Lint](https://github.com/bbvch/LambdaTale/workflows/.github/workflows/lint.yml/badge.svg)](https://github.com/bbvch/LambdaTale/actions?query=workflow%3A.github%2Fworkflows%2Flint.yml)_
_[![Spell check](https://github.com/bbvch/LambdaTale/workflows/.github/workflows/spell-check.yml/badge.svg)](https://github.com/bbvch/LambdaTale/actions?query=workflow%3A.github%2Fworkflows%2Fspell-check.yml)_

Platform support: [.NET Standard 2.0 and upwards](https://docs.microsoft.com/en-us/dotnet/standard/net-standard).

## Packages

The [`LambdaTale` package](https://www.nuget.org/packages/bbv.LambdaTale) depends on the [`xunit.core` package](https://www.nuget.org/packages/xunit.core/). That means you get only the minimum dependencies required to write and execute LambdaTale scenarios.

## Versions

LambdaTale follows the versioning scheme of xUnit.net. xUnit.net and LambdaTale do not follow SemVer and may introduce breaking changes in minor (or even patch) versions. Each minor version of LambdaTale is linked to the equivalent minor version of xUnit.net. The LambdaTale patch version is incremented to the next multiple of 100 each time it is linked to a new xUnit.net patch version. For example, LambdaTale 2.5.0 is linked with xUnit.net 2.5.0, and LambdaTale 2.5.100 is linked with xUnit 2.5.1.

A given LambdaTale patch version may introduce new features, fix bugs, or both.
