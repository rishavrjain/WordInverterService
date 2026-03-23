# WordInverterService

A .NET 8 REST API that inverts the words in a sentence and persists every request/response pair to a database.

## What it does

Send a sentence — every word is reversed character by character and the result is returned and stored.

```
"Hello World" → "olleH dlroW"
```
The only separator currently supported is ' '. 


## Getting Started

```bash
dotnet run
```

The application runs migrations automatically on startup. No manual database setup is required.

Swagger UI is available at `http://localhost:5154/swagger` once the app is running.

## API

Refer to `WordInverterService.http` in the project root for ready-to-run request examples covering all three endpoints.

| Method | Route | Description |
|--------|-------|-------------|
| `POST` | `/api/WordInverter` | Invert a sentence |
| `GET` | `/api/WordInverter` | Return all stored request/response pairs |
| `GET` | `/api/WordInverter/findByWord?word=` | Find pairs containing a specific whole word |

## Database

The project uses **SQLite** by default. A pre-populated database file (`WordInverter.db`) is included in the project folder with some existing entries so you can test the `GET` endpoints immediately without making any `POST` requests first.

To switch to SQL Server, update `appsettings.json`:

```json
{
  "DatabaseProvider": "SqlServer",
  "ConnectionStrings": {
    "SqlServer": "Server=localhost;Database=WordInverterDb;Integrated Security=true;TrustServerCertificate=true;"
  }
}
```

> Note: SQL Server requires its own migration to be generated. See the Migrations section below.

## Migrations

Migrations for SQLite are already included under `Migrations/Sqlite/` and are applied automatically on startup.

To add a new migration after changing the data model:

```bash
# SQLite
dotnet ef migrations add <MigrationName> --output-dir Migrations/Sqlite

# SQL Server
dotnet ef migrations add <MigrationName> --output-dir Migrations/SqlServer
```
