# LINQD (Working Title)
A .NET MAUI event-discovery and RSVP application. **Status:** coursework prototype / pre-MVP.

## Product concept
Find public events, coordinate private plans with your circle, and manage RSVPs in one place.

## Documentation
- [Product vision](docs/product/product-vision.md)
- [Requirements](docs/product/product-requirements.md)
- [Feature roadmap](docs/product/feature-roadmap.md)
- [Target users](docs/product/target-users.md)
- [User flows](docs/design/user-flows.md)
- [Architecture](docs/architecture/architecture-overview.md)
- [Current class requirements](docs/coursework/week-02-requirements.md)

## Tech plan
.NET MAUI / C# / XAML; mock data first, SQLite for local persistence later, and a separately designed API when needed. These are plans, not completed integrations.

## Getting started
From repository root, after installing .NET MAUI workload:
```powershell
dotnet new maui -n Linqd.App -o src/Linqd.App
```
Run the app with the appropriate target framework and emulator/device. See the architecture notes before adding new services.

## Development status
- [ ] Initial MAUI project
- [ ] Week 2 navigation and validation prototype
- [ ] Persisted event and RSVP records
- [ ] API-backed discovery

## Portfolio focus
Product planning, UX wireframes, maintainable architecture, C# application development, validation, tests, and incremental delivery.
