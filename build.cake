//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var sln = "Nordic4HCamp.sln";

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Restore-NuGet-Packages")
    .Does(() =>
{
	var settings = new DotNetCoreRestoreSettings 
    {
    };

    DotNetCoreRestore(sln, settings);
});

Task("Build")
	.IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
	var settings = new DotNetCoreBuildSettings
    {
		Configuration = configuration,
		NoRestore = true
    };

	DotNetCoreBuild(sln, settings);
});

Task("Test")
	.IsDependentOn("Build")
    .Does(() =>
{
	var projects = GetFiles("./tests/**/*.csproj");
	foreach(var project in projects)
	{
		DotNetCoreTest(
			project.FullPath,
			new DotNetCoreTestSettings()
			{
				Configuration = configuration,
				NoBuild = true
			});
	}
});


Task("Publish")
	.IsDependentOn("Test") // Publish implicit builds
	.Does(() =>
{
	var settings = new DotNetCorePublishSettings
    {
		Configuration = configuration,
		NoRestore = true,
    };

	DotNetCorePublish(sln, settings);
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Publish");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);