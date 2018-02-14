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

Task("GetDependencies")
    .Does(() =>
{
    if (string.IsNullOrEmpty(kspPath) || !DirectoryExists(kspPath)) {
        throw new Exception("kspPath must be set to a valid directory.");
    }

    string jebNetPath = $"JebNet/bin/{configuration}/JebNet.dll";
    string csharpDllPath = $"{kspPath}/KSP_Data/Managed/Assembly-CSharp.dll";
    string unityEngineDllPath = $"{kspPath}/KSP_Data/Managed/UnityEngine.dll";

    if (!FileExists(csharpDllPath)) {
        throw new Exception($"Assembly-CSharp.dll could not be found at this path: '{csharpDllPath}'.");
    }
    if (!FileExists(unityEngineDllPath)) {
        throw new Exception($"UnityEngine.dll could not be found at this path: '{unityEngineDllPath}'.");
    }
    Information("KSP dependencies found.");

    string dependenciesDirectory = "JebNet/Dependencies/";
    string csharpDllTargetPath = $"{dependenciesDirectory}Assembly-CSharp.dll";
    string unityEngineDllTargetPath = $"{dependenciesDirectory}UnityEngine.dll";

    if (FileExists(csharpDllTargetPath)) {
        DeleteFile(csharpDllTargetPath);
        Information($"Deleted old dll at '{csharpDllTargetPath}'.");
    }
    CopyFile(csharpDllPath, csharpDllTargetPath);
    Information($"Copied dll to '{csharpDllTargetPath}'.");

    if (FileExists(unityEngineDllTargetPath)) {
        DeleteFile(unityEngineDllTargetPath);
        Information($"Deleted old dll at '{unityEngineDllTargetPath}'.");
    }
    CopyFile(unityEngineDllPath, unityEngineDllTargetPath);
    Information($"Copied dll to '{unityEngineDllTargetPath}'.");
});

Task("Build")
    .IsDependentOn("GetDependencies")
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
    else {
        throw new Exception("Unsupported operation system.");
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

Task("RunKsp")
    .IsDependentOn("Deploy")
    .Does(() =>
{
    if (string.IsNullOrEmpty(kspPath) || !DirectoryExists(kspPath)) {
        throw new Exception("kspPath must be set to a valid directory.");
    }

    string kspExePath = $"{kspPath}/KSP.exe";

    if (!FileExists(kspExePath)) {
        throw new Exception($"KSP.exe could not be found at this path: '{kspExePath}'.");
    }
    Information($"KSP.exe found at '{kspExePath}'.");
    StartProcess(kspExePath, new ProcessSettings{ WorkingDirectory = kspPath });
    Information($"KSP exited.");
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
