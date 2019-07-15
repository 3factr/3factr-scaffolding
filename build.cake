///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var verbosityArg = Argument("verbosity", "Minimal");
var verbosity = Verbosity.Minimal;

//////////////////////////////////////////////////////////////////////
// TOOLS / ADDINS
//////////////////////////////////////////////////////////////////////

#addin nuget:?package=Cake.Figlet&version=1.2.0
#addin nuget:?package=Cake.Npx&version=1.3.0
#addin nuget:?package=SemanticVersioning&version=1.2.0

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

using System.Text.RegularExpressions;

var solutionName = "MvxScaffolding";
var outputDirNuGet = new DirectoryPath("./artifacts/NuGet");
var nuspecFile = new FilePath("./nuspec/MvxScaffolding.Templates.nuspec");

var isRunningOnAzurePipelines = BuildSystem.IsRunningOnAzurePipelines ;
SemVer.Version versionInfo = null;

Information(Figlet(solutionName));

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(context => 
{
    var cakeVersion = typeof(ICakeContext).Assembly.GetName().Version.ToString();

    string[] redirectedStandardOutput = null;

    Npx("standard-version",
        args => args.Append("--dry-run"),
        out redirectedStandardOutput);
        
    Regex regex = new Regex(@"(?<=\[).+?(?=\])");
    Match match = regex.Match(redirectedStandardOutput[3]);

    if (!match.Success)
        throw new InvalidOperationException ("Can not parse a build version number.");

    versionInfo = new SemVer.Version(match.Value);

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
    NuGetRestore("./MvxScaffolding.Vsix.sln");
});

Task("Build-Release").Does(() => 
{
    Information("Bumping version and updating changelog...");
    Npx("standard-version");
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build-NuGet-Package");

Task("Release")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build-NuGet-Package")
    .IsDependentOn("Build-Release");

RunTarget(target);
