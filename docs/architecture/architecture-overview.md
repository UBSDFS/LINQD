# Architecture Overview — Proposed
## Client
.NET MAUI (C# + XAML), Shell for routes and navigation. Organize source into `Views`, `Models`, `Services`, and later `ViewModels` as complexity grows.

## Prototype data
MockDataService supplies hard-coded users/events; UserSessionService tracks authenticated/guest state in memory. Avoid embedding credentials and datasets across individual pages.

## Future persistence
SQLite for local app data/caching where appropriate. An authenticated backend/API should be the authority for shared events, invitations, and RSVPs across devices; local SQLite alone cannot synchronize multiple users.

## Future backend
A separately versioned API is an option (Python/FastAPI or ASP.NET Core, decision pending). Never treat an external event API key as a secure secret when shipped in the mobile client.

## Navigation
Use Shell routes for pages and explicit logout session clearing. Avoid creating duplicate event-list pages; pass a list filter to a reusable view.

## Security boundary
Hard-coded credentials are coursework-only and must be removed before any public deployment. Production auth, authorization, privacy, and server-side validation require dedicated design.
