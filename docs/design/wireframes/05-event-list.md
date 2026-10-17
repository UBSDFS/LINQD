# LINQD — Event List Screen

## 1. Purpose

Display available events and allow authenticated
users to filter events by participation.

The page supports three views:

- All Events
- Attending
- Hosting

## 2. Screen Layout

             ← Events

[ All Events | Attending | Hosting ]

---

[Image] Jazz Night
Saturday - 7:00 PM
Downtown

---

[Image] Food Truck Festival
Sunday - 12:00 PM
City Park

---

[Image] Photography Walk
Saturday - 9:00 AM
Waterfront

---

[Home] [Discover] [Create]
[My Circle] [Profile]

## 3. UI Components

- Page title
- Back navigation
- Event filter controls
- CollectionView for event listings
- Event cards containing:
  - Event name
  - Date
  - Time
  - Location
  - Image or placeholder

## 4. Event Filters

### All Events

Displays public events available to browse.

### Attending

Displays events the authenticated user
has marked as Going.

### Hosting

Displays events created or hosted by
the authenticated user.

## 5. Navigation

Home → EventListPage

Discover → EventListPage (All Events)

Attending → EventListPage (Attending)

Hosting → EventListPage (Hosting)

Select Event → EventDetailsPage

Back → Previous Page

## 6. Authenticated User Behavior

Authenticated users can:

- View all public events
- View events they are attending
- View events they are hosting
- Select events to view details

## 7. Guest Behavior

Guest users can:

- View All Events
- Select public events
- View event details

Attending and Hosting are unavailable
to guest users.

## 8. Week 2 Implementation

Use a hard-coded collection of Event objects.

Each Event contains:

- EventId
- Title
- Description
- Date
- Time
- Location
- HostUserId
- Visibility

Use mock RSVP relationships to determine
which events the test user is attending.

No database persistence is required.

## 9. Week 3 Integration

- Retrieve local events from SQLite.
- Retrieve public events from the external API.
- Combine event data through EventService.
- Retrieve Attending records from RSVP data.
- Retrieve Hosting records by HostUserId.

## 10. Future Enhancements

- Search events
- Filter by date and category
- Filter by location
- Sort by distance
- Event images
- Saved events
- Pagination and API caching

## 11. Acceptance Criteria

- All Events displays hard-coded events.
- Attending displays appropriate user events.
- Hosting displays appropriate user events.
- Guests cannot access personal event lists.
- Selecting an event opens its details.
- Back navigation functions correctly.
- Empty lists display a helpful message.
