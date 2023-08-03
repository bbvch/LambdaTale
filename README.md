<!-- markdownlint-disable-next-line MD041 -->
<img src="assets/xbehave_256x256.png" width="128" />

# xBehave.net

> [!WARNING]
> This is a fork of [xBehave.net](https://github.com/adamralph/xbehave.net). Adam, thank you very much!
> After many years of maintenance, Adam has decided to invest his energy elsewhere.
> This repo is the birthplace for a new (renamed, new NuGet) version of xBehave.
> The main goal is to maintain versions used internally. Notably this means that support for old .NET / dotnet versions can be dropped anytime.

_[![Build status](https://github.com/bbvch/xbehave.net/workflows/.github/workflows/ci.yml/badge.svg)](https://github.com/bbvch/xbehave.net/actions)_
_[![CodeQL analysis](https://github.com/bbvch/xbehave.net/workflows/.github/workflows/codeql-analysis.yml/badge.svg)](https://github.com/bbvch/xbehave.net/actions?query=workflow%3A.github%2Fworkflows%2Fcodeql-analysis.yml)_
_[![Lint](https://github.com/bbvch/xbehave.net/workflows/.github/workflows/lint.yml/badge.svg)](https://github.com/bbvch/xbehave.net/actions?query=workflow%3A.github%2Fworkflows%2Flint.yml)_
_[![Spell check](https://github.com/bbvch/xbehave.net/workflows/.github/workflows/spell-check.yml/badge.svg)](https://github.com/bbvch/xbehave.net/actions?query=workflow%3A.github%2Fworkflows%2Fspell-check.yml)_

xBehave.net is an [xUnit.net](https://github.com/xunit/xunit) extension, available in [full](https://www.nuget.org/packages/Xbehave) or [minimal](https://www.nuget.org/packages/Xbehave.Core) form, for describing each step in a test with natural language.

Platform support: [.NET Standard 2.0 and upwards](https://docs.microsoft.com/en-us/dotnet/standard/net-standard).

## Packages

The [full `Xbehave` package](https://www.nuget.org/packages/Xbehave) depends on the [`xunit` package](https://www.nuget.org/packages/xunit/). That means you get the full suite of xUnit.net dependencies such as [`xunit.assert`](https://www.nuget.org/packages/xunit.assert/) and [`xunit.analyzers`](https://www.nuget.org/packages/xunit.analyzers/).

The [minimal `Xbehave.Core` package](https://www.nuget.org/packages/Xbehave.Core) depends on the [`xunit.core` package](https://www.nuget.org/packages/xunit/). That means you get only the minimum dependencies required to write and execute xBehave.net scenarios.

## Versions

xBehave.net follows the versioning scheme of xUnit.net. xUnit.net and xBehave.net do not follow SemVer and may introduce breaking changes in minor versions. Each minor version of xBehave.net is linked to the equivalent minor version of xUnit.net. The xBehave.net patch version is incremented to the next multiple of 100 each time it is linked to a new xUnit.net patch version. For example, xBehave.net 2.4.0 is linked with xUnit.net 2.4.0, and xBehave.net 2.4.100 is linked with xUnit 2.4.1.

A given xBehave.net patch version may introduce new features, fix bugs, or both.

---

<sub>xBehave.net logo designed by [Vanja Pakaski](https://github.com/vanpak).</sub>
