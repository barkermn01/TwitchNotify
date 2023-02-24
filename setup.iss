; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Twitchy"
#define MyAppVersion "0.4.0"
#define MyAppExeName "Twitchy.exe"

#ifndef MyAppTargetFramework
  #define MyAppTargetFramework "net6.0-windows"
#endif

#ifndef SourceDir
  #define SourceDir "TwitchDesktopNotifications\bin\Release\net6.0-windows10.0.17763.0\"
#endif

#define public Dependency_NoExampleSetup
#include "CodeDependencies.iss"

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{7774CEEE-6785-48B9-BA35-6B842F947A21}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
DefaultDirName={autopf}\{#MyAppName}
DisableProgramGroupPage=yes
LicenseFile=.\TwitchDesktopNotifications\Lisence.rtf
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=.\SetupFiles
OutputBaseFilename=TwitchySetup
SetupIconFile=.\TwitchDesktopNotifications\Assets\icon.ico
UninstallDisplayIcon=.\TwitchDesktopNotifications\Assets\icon.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern
ArchitecturesInstallIn64BitMode=x64
SignedUninstaller=yes
SignTool=signtool
SourceDir=.\

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "{#SourceDir}{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}Microsoft.Toolkit.Uwp.Notifications.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}Microsoft.Windows.SDK.NET.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}Twitchy.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}Twitchy.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}Twitchy.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}WinRT.Runtime.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#SourceDir}Assets\icon.ico"; DestDir: "{app}\Assets"; Flags: ignoreversion
Source: "{#SourceDir}Assets\twitch.png"; DestDir: "{app}\Assets"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files
Source: "netcorecheck.exe"; Flags: dontcopy noencryption
Source: "netcorecheck_x64.exe"; Flags: dontcopy noencryption

#expr SaveToFile("Preprocessed.iss")

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: {app}\{cm:AppName}.exe; Description: {cm:LaunchProgram,{cm:AppName}}; Flags: nowait postinstall skipifsilent

[Registry]
Root: HKCU; Subkey: "SOFTWARE\Microsoft\Windows\CurrentVersion\Run"; ValueType: string; ValueName: "Twitchy"; ValueData: """{app}\{#MyAppExeName}"""; Flags: uninsdeletevalue

[CustomMessages]
AppName=Twitchy
LaunchProgram=Start Twitchy after finishing installation

[Code]
function InitializeSetup: Boolean;
begin
  Dependency_AddDotNet60Desktop;
  Result := True;
end;