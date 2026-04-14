# DailyRoutineApp

Minimal ASP.NET Core MVC app to create simple customized daily routines.

Requirements: .NET SDK (10.0 installed or compatible)

Run locally:

```powershell
cd F:\CAPSTONE\DailyRoutineApp
dotnet restore
dotnet run --urls http://localhost:5000
```

Open http://localhost:5000 in your browser.

Notes:
- Data is stored in-memory (singleton service). Restarting the app clears saved routines.
- The Create form allows up to 6 activities.
 
Database:
- The app now uses SQLite (`DailyRoutine.db` created beside the app) to persist routines.
 - The database file is created automatically on first run.
