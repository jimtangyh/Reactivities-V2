# Reactivities V2

Full-stack sample with a .NET 10 Web API and a React 19 + Vite client. The API uses EF Core with SQLite, seeds demo activities at startup, and exposes them to the client.

## Quick Start
1. Start API: `dotnet run --project API/API.csproj`
2. Start client: `cd Client && npm install && npm run dev`

## Stack
- Backend: ASP.NET Core 10, EF Core Sqlite, minimal hosting, MediatR (CQRS-style requests/handlers), AutoMapper
- Frontend: React + TypeScript, Vite, fetches activities from the API

## Project Structure
- `API/` — Web API (`/api/activities`, `appsettings.Development.json` for connection string)
- `Application/` — application layer (MediatR requests/handlers)
- `Persistence/` — EF Core DbContext, migrations, seeding
- `Domain/` — Activity entity
- `Client/` — React app (Vite)
- `docs/` — project notes and examples
- `Reactivities.slnx` — solution file

## Docs
- MediatR request examples (this repo): `docs/MediatR-Request-Examples.md`

## Prerequisites
- .NET 10 SDK
- Node.js 20+ and npm
- Dev HTTPS certificate trusted locally: `dotnet dev-certs https --trust`

## Backend (API)
1. Restore/build (from repo root): `dotnet restore`
2. Run the API: `dotnet run --project API/API.csproj`
   - HTTPS: `https://localhost:5001`
   - Applies EF Core migrations and seeds `reactivities.db` automatically on startup.
3. Key endpoints:
   - `GET /api/activities` — list activities
   - `GET /api/activities/{id}` — activity by id
   - `POST /api/activities` — create activity (returns created `id`)
   - `PUT /api/activities` — edit activity (returns `204 No Content`)
   - `GET /weatherforecast` — sample endpoint
4. DB config: `API/appsettings.Development.json` (`DefaultConnection` points to local SQLite).

## Frontend (Client)
1. Install deps: `cd Client && npm install`
2. Start dev server: `npm run dev`
   - Vite runs on port `3000` (see `Client/vite.config.ts`).
   - This repo uses `vite-plugin-mkcert`, so the dev server may run on `https://localhost:3000`.
   - The app fetches from `https://localhost:5001/api/activities` (currently hard-coded in `Client/src/App.tsx`).
   - CORS is enabled for `http://localhost:3000` and `https://localhost:3000` in `API/Program.cs`.

## Data Model
Activity fields: `id`, `title`, `date`, `description`, `category`, `isCancelled`, `city`, `venue`, `latitude`, `longitude`.

## Migrations & DB
- Add migration: `dotnet ef migrations add <Name> --project Persistence --startup-project API`
- Apply migrations: run the API; it will migrate on startup.
- Move DB/change connection: update `DefaultConnection` in `API/appsettings.Development.json`.

## Troubleshooting
- HTTPS errors: trust the ASP.NET dev cert (`dotnet dev-certs https --trust`) and, if using the Vite HTTPS dev server, ensure `mkcert` can be installed/run by `vite-plugin-mkcert`.
- Empty list: ensure the API is running, DB file exists, and the client is pointing to the correct base URL.
