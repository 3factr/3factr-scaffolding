var currentVersion = "2.0.0";

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var nugetFeed = EnvironmentVariable("NUGET_FEED");
var nugetApiKey = EnvironmentVariable("NUGET_API_KEY");
var verbosityArg = Argument("verbosity", "Minimal");
var verbosity = Verbosity.Minimal;

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

using System.Text.RegularExpressions;

var solutionName = "MvxScaffolding";
var outputDirNuGet = new DirectoryPath("./artifacts/NuGet");
var nuspecFile = new FilePath("./nuspec/MvxScaffolding.Templates.nuspec");

SemVer.Version versionInfo = null;

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(context => 
{
    var cakeVersion = typeof(ICakeContext).Assembly.GetName().Version.ToString();

    versionInfo = new SemVer.Version(currentVersion);

    Information("Building version {0}, ({1}, {2}) using version {3} of Cake.",
        versionInfo.ToString(),
        configuration,
        target,
        cakeVersion);

    verbosity = (Verbosity) Enum.Parse(typeof(Verbosity), verbosityArg, true);
});

Teardown(ctx =>
{
    Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean").Does(() =>
{
    Information("Cleaning common files...");
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
    CleanDirectories(outputDirNuGet.FullPath);

    EnsureDirectoryExists(outputDirNuGet);
});

Task("Build-NuGet-Package").Does(() =>
{
    var nuGetPackSettings = new NuGetPackSettings
      {
          OutputDirectory = outputDirNuGet,
          Version = versionInfo.ToString()
      };

    NuGetPack(nuspecFile, nuGetPackSettings);
});

Task("Restore-NuGet-Packages").Does(() =>
{
    Information("Restoring solution...");
    NuGetRestore("./MvxScaffolding.sln");
});

Task("Publish-NuGet-Package").Does(() => 
{
    Information("Publishing NuGet package...");
    var path = outputDirNuGet.FullPath + "/*.nupkg";
    var results = GetFiles(path);
    var package = results.First();
    NuGetPush(package, new NuGetPushSettings
    {
        Source = nugetFeed,
        ApiKey = nugetApiKey
    });
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build-NuGet-Package");

Task("Release")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build-NuGet-Package")
    .IsDependentOn("Publish-NuGet-Package");

RunTarget(target);
