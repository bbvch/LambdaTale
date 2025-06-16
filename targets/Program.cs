using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Targets;
using static Bullseye.Targets;
using static SimpleExec.Command;

const string commonArgs = "--configuration Release --nologo";
string lockedMode = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "--locked-mode" : "";

Target("restore", () => RunAsync("dotnet", $"restore {lockedMode}"));

Target(
    "build",
    ["restore"],
    () => RunAsync("dotnet", $"build --no-restore {commonArgs} /p:ContinuousIntegrationBuild=true"));

Target(
    "format",
    ["restore"],
    () => RunAsync("dotnet", "format --no-restore --verify-no-changes"));

Target(
    "pack",
    ["build"],
    () => RunAsync(
        "dotnet",
        $"pack src/LambdaTale --no-build {commonArgs}",
        configureEnvironment: env => env.Add("NUSPEC_FILE", "LambdaTale.nuspec")));

Target(
    "test",
    ["build"],
    forEach: TargetFrameworks(),
    tfm => RunAsync("dotnet", $"test -f {tfm} --no-build {commonArgs}"));

Target("update-upstream", Update.Upstream);

Target("default", ["format", "test", "pack"]);

await RunTargetsAndExitAsync(args, ex => ex is SimpleExec.ExitCodeException);

static IEnumerable<string> TargetFrameworks()
{
    var data = Assembly.GetExecutingAssembly().GetCustomAttributes<AssemblyMetadataAttribute>();
    yield return "netcoreapp3.1";
    yield return data.Single(d => d.Key == "SupportedDotnetVersion").Value;
    yield return data.Single(d => d.Key == "LatestDotnetVersion").Value;
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        yield return data.Single(d => d.Key == "SupportedFrameworkVersion").Value;
    }
}
