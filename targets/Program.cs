using Targets;
using static Bullseye.Targets;
using static SimpleExec.Command;

const string commonArgs = "--configuration Release --nologo";

Target("restore", () => RunAsync("dotnet", "restore --locked-mode"));

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
    () => RunAsync("dotnet", $"test --no-build {commonArgs}"));

Target("update-upstream", Update.Upstream);

Target("default", ["format", "test", "pack"]);

await RunTargetsAndExitAsync(args, ex => ex is SimpleExec.ExitCodeException);
