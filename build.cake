#tool nuget:?package=vswhere
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var kspPath = Argument("kspPath", string.Empty);

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectories("./**/bin/**");
});

Task("Build")
    .Does(() =>
{
    if(IsRunningOnWindows())
    {

        DirectoryPath vsLatest  = VSWhereLatest();
        FilePath msBuildPathX64 = (vsLatest==null)
                                ? null
                                : vsLatest.CombineWithFilePath("./MSBuild/15.0/Bin/MSBuild.exe");

        Information($"MS Build Path: {msBuildPathX64}");
        MSBuild("JebNet.sln", new MSBuildSettings {
        Verbosity = Verbosity.Minimal,
        Configuration = configuration,
        PlatformTarget = PlatformTarget.MSIL,
        ToolPath = msBuildPathX64
        });
    }
});

Task("Deploy")
    .IsDependentOn("Build")
    .Does(() =>
{
    if (string.IsNullOrEmpty(kspPath) || !DirectoryExists(kspPath)) {
        throw new Exception("kspPath must be set to a valid directory.");
    }

    string jebNetPath = $"JebNet/bin/{configuration}/JebNet.dll";
    string targetJebNetPath = $"{kspPath}/GameData/Squad/Plugins/JebNet.dll";

    if (!FileExists(jebNetPath)) {
        throw new Exception($"JebNet.dll could not be found at this path: '{jebNetPath}'.");
    }
    Information($"Built plugin found at '{jebNetPath}'.");

    if (FileExists(targetJebNetPath)) {
        DeleteFile(targetJebNetPath);
        Information($"Deleted old plugin at '{targetJebNetPath}'.");
    }
    CopyFile(jebNetPath, targetJebNetPath);
    Information($"Copied plugin to '{targetJebNetPath}'.");
});



//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Deploy");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
