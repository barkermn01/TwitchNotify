# Twitch Notify
## not affiliated with Twitch or Amazon

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

## Installation
 Twitch Notify Requires [.NET 6 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.13-windows-x64-installer).


## Development

Want to contribute? Great!

Project is built using Visual Studios 2022, must have Windows 10.0.17763 SDK installed

You need to create Application to obtain a ID and Secret on [Twitch Developer Console](https://dev.twitch.tv/console) once you have them
Open the project in Visual Studio 2022 go to the Developer Powershell and then run the following commands, remembering to replace the `{You Twitch Applciations Client ID}` and `{You Twitch Applciations Client Secret}` with the appropriate information from your [Twitch Developer Console](https://dev.twitch.tv/console)
```pwsh
cd TwitchDesktopNotifications
dotnet user-secrets init
dotnet user-secret set TwitchClientID {You Twitch Applciation's Client ID}
dotnet user-secret set TwitchClientSecret {You Twitch Applciation's Client Secret}
```

## License

MIT
**Free Software, Hell Yeah!**
