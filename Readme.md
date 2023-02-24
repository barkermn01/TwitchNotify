# Twitchy
The Twitch windows desktop notification system.

## Not affiliated with Twitch or Amazon

## Installation
 Twitch Notify Requires [.NET 6 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.13-windows-x64-installer).
 As of RC-1 There is now a Setup that will install into auto run aswell 

## How to use
**How to Manage Ignores:**
1. Click "Manage Ignores" from the right click notification area icon

![Image of Context Menu from Notification / Tray Icon ](https://user-images.githubusercontent.com/37368/220464000-3aa632e0-1eb8-4be0-8d06-613bf9ea96f6.png)

2, the following form will open where you can manage streamers that should be ignored

![image of manage ignores UI](https://user-images.githubusercontent.com/37368/220468049-70bb423d-e16c-49ff-a9e7-4135df9ebc15.png)

_Something to note:_
Making changes inside the form is saved automatically
Streamers won't appear in the form until they have been seen 

**How to Exit:**

To exit you will have a Notification Icon / Tray icon where you can quit the application.

![Image of Context Menu from Notification / Tray Icon ](https://user-images.githubusercontent.com/37368/220464000-3aa632e0-1eb8-4be0-8d06-613bf9ea96f6.png)

## Development

Want to contribute? Great!

Project is built using Visual Studios 2022, please make sure you have added support for .NET 6, You will also requrie the Windows SDK (10.0.17763.0)

If your unable to code sign you can build configuration to "Debug" or "Release" mode the signing script will only run if set to "ReleaseSign".

You need to create Application to obtain a ID and Secret on [Twitch Developer Console](https://dev.twitch.tv/console)
Add a new C# Class to the project named `TwitchDetails.cs` add the following code with your ID and Secret
```cs
namespace TwitchDesktopNotifications
{
    static public class TwitchDetails
    {
        public static string TwitchClientID = "";
        public static string TwitchClientSecret = "";
    }
}
```

### CommunityToolkit 8.0.0 Pre-release
Project Requests `CommunityToolkit-MainLatest` NuGET Package Source

1. Tool > NuGET Package Manager > Package Manager Settings
2. Click on Package Source (just below the select General in the Left hand column
3. Click the + icon top right
5. 4. Enter the Name `CommunityToolkit-MainLatest` and Source `https://pkgs.dev.azure.com/dotnet/CommunityToolkit/_packaging/CommunityToolkit-MainLatest/nuget/v3/index.json`
6. Click Update
7. Click Ok



## License

MIT
**Free Software, Hell Yeah!**
