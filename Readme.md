# Twitch Notify
## Not affiliated with Twitch or Amazon

## Installation
 Twitch Notify Requires [.NET 6 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.13-windows-x64-installer).


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
