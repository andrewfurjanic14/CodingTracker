# CodingTracker
CodingTracker is a simple .NET console application for tracking coding sessions. It lets you log when you start and stop working, record the duration, and review or update your previous sessions in a clean interactive menu.
## Features
- Log a coding session with start time, end time, and duration
- View all saved sessions in a table
- Filter sessions by date
- Update an existing session
- Delete a saved session
- Persist data in SQLite using Dapper
- Friendly terminal UI powered by Spectre.Console
## Tech Stack
- C# / .NET
- SQLite
- Dapper ORM
- Spectre.Console
## Project Structure
- `CodingTracker/Program.cs` - application entry point
- `CodingTracker/UI/` - console menu and terminal prompts
- `CodingTracker/Data/` - database access and session models
- `CodingTracker/appsettings.json` - database connection configuration
## Getting Started
### Prerequisites
- .NET SDK 10.0 or newer
### Run the app
1. Clone the repository
2. Open a terminal in the project root
3. Restore packages and run the app:
```bash
dotnet restore
dotnet run --project CodingTracker
```
On first launch, the app will create the SQLite database file automatically if it does not already exist.
## Database
The app uses SQLite and reads its connection string from `CodingTracker/appsettings.json`.
```json
{
  "ConnectionStrings": {
    "CodingTrackerDb": "Data Source=codingtracker.db"
  }
}
```
This stores session records in a table named `CodingSessions` with the following fields:
- `Id`
- `StartTime`
- `EndTime`
- `DurationMinutes`
- `Date`
## Usage
When the app starts, a menu appears with actions such as:
- View sessions
- Log session
- Delete session
- Update session
- Exit
Follow the prompts to manage your coding history.
