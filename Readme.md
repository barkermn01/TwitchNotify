# Twitch Notify
## not affiliated with Twitch or Amazon

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

## Installation
 Twitch Notify Requires [.NET 6 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-6.0.13-windows-x64-installer).


## Development

Want to contribute? Great!

Project is built using Visual Studios 2022, must have Windows 10.0.17763 SDK installed

You must create a `App.config` file inside `TwitchDesktopNotifications` project.
```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
	<appSettings>
		<add key="TwitchClientID" value="" />
		<add key="TwitchClientSecret" value="" />
	</appSettings>
</configuration>
```

## License

MIT
**Free Software, Hell Yeah!**
