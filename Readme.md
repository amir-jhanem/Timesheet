# Desktop cross platform timesheet app by .Net core and Avalonia

Timesheet is a desktop cross platform app. GUI implemented by avalonia and backend by asp.net core.

# Solution projects:
- Timesheet.Contracts.
	- Contains shared contracts among presentation and api layers.
- Timesheet.Backend.
	- Asp.net core web api project manages authentications and the business logic.
- Timesheet.GUI.
	- Desktop app cross platform by Avalonia. 

# Screenshoots

- Login : https://imgur.com/V0Jc11d
- Register : https://imgur.com/OlklG5G
- Attendance : https://imgur.com/KV976JF

## Get The Requirements

1. Get mysql: https://www.mysql.com/downloads.
2. Get .NET Core 3.1 SDK: https://www.microsoft.com/net/download.

## Configure Timesheet
1. Execute `timesheet.sql` database script file attached.
2. Run `timesheet.backend` on IISExpress.
3. Configure backend hostname in `app.config.json`.

## Run Timesheet
1. Build `timesheet.GUI`.
2. Open `timesheet.GUI.exe`.