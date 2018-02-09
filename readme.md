# JebNet

This project is a mod for Kerbal Space Program that creates an API for controlling craft from a separate process.

The mod works by creating a new command module that has code for creating a web server. When added to a craft, normal input is disabled. Instead, Kerbal Space Program starts an internal web server that listens for JSON POST requests which contain flight parameters. These are then passed to the craft.

The final goal is to create a mod that allows language agnostic flight AI. It should be possible to write an autopilot in C#, Javascript, Python or any other language. It should also be possible to control a craft from any other program (Twitch plays KSP?).

## Building

To build the mod you will need Visual Studio Community (other editions should work too) and KSP installed somewhere.

Clone this repository and build in Visual Studio. It is likely references to Assembly-CSharp.dll and UnityEngine.dll will need to be updated depending on where KSP is installed.
To do this, right click on the References section in the Visual Studio solution explorer and point to the DLLs that will have a file path similar to the following locations:

* {SteamDirectory}/SteamApps/common/Kerbal Space Program/KSP_Data/Managed/Assembly-CSharp.dll
* {SteamDirectory}/SteamApps/common/Kerbal Space Program/KSP_Data/Managed/UnityEngine.dll

The project should now build.

## Installing

First, the edited command module needs to be added to the parts directory. The entire [probeCoreOcto2NetworkSlave directory](https://github.com/RichTeaMan/JebNet/tree/master/JebNet/probeCoreOcto2NetworkSlave) needs to be copied to the parts command directory. The path will look something like:

* {SteamDirectory}/SteamApps/common/Kerbal Space Program/GameData/Squad/Parts/Command

Secondly, the JebNet.dll built earlier from the JebNet project needs to be copied to the KSP plugin directory. The path will look something like:

* {SteamDirectory}/SteamApps/common/Kerbal Space Program/GameData/Squad/Plugins

That should be it. Restart KSP and you should see the new part in the KSP catalogue. When added to a craft and deployed to the launch pad it should be controllable via the API.

## Testing

When the module is added and the craft is in a playable state (such as on the launch pad) it should respond to API calls. Currently there are two endpoints, 'status' and  'command'.

Status is GET endpoint and can be accessed at 'http://localhost:2001/status'. This will return a JSON object of the current flight parameters. This object is defined by the [Vessel](https://github.com/RichTeaMan/JebNet/blob/master/JebNet.Controller/Integration/Domain/Vessel.cs) class.

Command is POST endpoint and it affects the control of the craft. It can be accessed at 'http://localhost:2001/command'. The contents of the POST should be a JSON object conforming to the definition
of the [ControlState](https://github.com/RichTeaMan/JebNet/blob/master/JebNet.Controller/Integration/Domain/ControlState.cs) class. The server will then respond with a Vessel object, exactly the same as the status endpoint.

## Next Steps

The mod is in very early stages. Currently, when the command module is destroyed in game (this will happen a lot) the server also is also disposed, so any API clients are left to timeout. Ideally there would be a singleton server that can handle many crafts. This will require a bit more investment in learning how the Unity engine creates long running services.

Additionally, some of the finer points of control vectors need to be worked out. This is mostly a trial and error thing as very little of this is documented.

## Useful to know

Note that Unity is a cross platform technology that doesn't run .NET as most Windows programmers would know. Instead, it uses [Mono](http://www.mono-project.com/). It is for this reason the server project is locked into .NET 3.5. The main difference is the standard library isn't entirely implemented and many Nuget libraries (eg, Json.NET) won't work. Be careful when selecting dependencies.

The default Unity JSON serialiser does not support members (only public fields) or nested objects. Other serialisers don't work for the aforementioned compatibility reasons.
