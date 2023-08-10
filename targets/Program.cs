using static Bullseye.Targets;
using static SimpleExec.Command;

const string commonArgs = "--configuration Release --nologo";

Target("restore", () => RunAsync("dotnet", "restore --locked-mode"));

Target(
    "build",
    DependsOn("restore"),
    () => RunAsync("dotnet", $"build --no-restore {commonArgs}"));

Target(
    "format",
    DependsOn("restore"),
    () => RunAsync("dotnet", "format --no-restore --verify-no-changes"));

Target(
    "pack",
    DependsOn("build"),
    () => RunAsync(
        "dotnet",
        $"pack src/LambdaTale --no-build {commonArgs}",
        configureEnvironment: env => env.Add("NUSPEC_FILE", "LambdaTale.nuspec")));

Target(
    "test",
    DependsOn("build"),
    () => RunAsync("dotnet", $"test --no-build {commonArgs}"));

Target("default", DependsOn("format", "test", "pack"));

await RunTargetsAndExitAsync(args, ex => ex is SimpleExec.ExitCodeException);
