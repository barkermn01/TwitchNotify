# Twitchy
The Twitch windows desktop notification system.

## Not affiliated with Twitch or Amazon

Please note that currently this is a BETA as such it does not add it's self to windows start up if you wish to add this to start up for now you can just create a shortcut to the `Twitch Notify.exe` in the directory `C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup`,

This will be fixed when this project moves out of Beta only.

## Installation
 Twitch Notify Requires [.NET 6 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.13-windows-x64-installer).

## How to use
**How to Exit:**

To exit you will have a Notification Icon / Tray icon where you can quit the application.

![Showing windows Notification Icon Area also known as the Tray Area](https://user-images.githubusercontent.com/37368/215226270-52d596d5-7811-4389-a761-3aabad7da360.png)

**What it looks like:**

![Example of the Notification](https://user-images.githubusercontent.com/37368/215226419-0ec644f1-bd2b-498f-93ed-a48acd3c824c.png)

## Development

Want to contribute? Great!

Project is built using Visual Studios 2022,

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
