# Reactivities V2

Full-stack sample with a .NET 10 Web API and a React 19 + Vite client. The API uses EF Core with SQLite, seeds demo activities at startup, and exposes them to the client.

## Stack
- Backend: ASP.NET Core 10, EF Core Sqlite, minimal hosting
- Frontend: React + TypeScript, Vite, fetches activities from the API

## Project Structure
- `API/` — Web API (`/api/activities`, `appsettings.Development.json` for connection string)
- `Persistence/` — EF Core DbContext, migrations, seeding (`reactivities.db`)
- `Domain/` — Activity entity
- `Client/` — React app (Vite)

## Prerequisites
- .NET 10 SDK
- Node.js 20+ and npm
- Dev HTTPS certificate trusted locally: `dotnet dev-certs https --trust`

## Backend (API)
1. Restore/build (from repo root): `dotnet restore`
2. Run the API: `dotnet run --project API/API.csproj`
   - HTTPS: `https://localhost:5001` (HTTP: `http://localhost:5000`)
   - Applies EF Core migrations and seeds `reactivities.db` automatically on startup.
3. Key endpoints:
   - `GET /api/activities` — list activities
   - `GET /api/activities/{id}` — activity by id
   - `GET /weatherforecast` — sample endpoint
4. DB config: `API/appsettings.Development.json` (`DefaultConnection` points to local SQLite).

## Frontend (Client)
1. Install deps: `cd Client && npm install`
2. Start dev server: `npm run dev -- --host --port 3000`
   - The app fetches from `https://localhost:5001/api/activities`.
   - CORS is enabled for `http://localhost:3000` and `https://localhost:3000` in `API/Program.cs`.

## Data Model
Activity fields: `id`, `title`, `date`, `description`, `category`, `isCancelled`, `city`, `venue`, `latitude`, `longitude`.

## Migrations & DB
- Add migration: `dotnet ef migrations add <Name> --project Persistence --startup-project API`
- Apply migrations: run the API; it will migrate on startup.
- Move DB/change connection: update `DefaultConnection` in `API/appsettings.Development.json`.

## Troubleshooting
- HTTPS errors: trust dev cert (`dotnet dev-certs https --trust`) or switch client fetch URL to `http://localhost:5000` and match CORS origins.
- Empty list: ensure the API is running, DB file exists, and the client is pointing to the correct base URL.
