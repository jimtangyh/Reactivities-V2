# Reactivities Client

React + TypeScript client for the Reactivities API.

## Prerequisites
- Node.js 20+ and npm

## Run
1. Install deps: `npm install`
2. Start dev server: `npm run dev`
   - Default URL: `http://localhost:3000` or `https://localhost:3000` (this repo includes `vite-plugin-mkcert`).

## API Base URL
The activities request is currently hard-coded to `https://localhost:5001/api/activities` in `src/App.tsx`.

## Scripts
- `npm run dev` — start Vite dev server
- `npm run build` — typecheck + build
- `npm run lint` — run ESLint
- `npm run preview` — preview production build
