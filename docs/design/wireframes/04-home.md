# LINQD — Home Dashboard

## 1. Purpose

The Home Dashboard is the main landing page after
login or guest access.

It provides equal visibility for:

- Public event discovery
- Private circle plans

## 2. Screen Layout

Header:

- LINQD application name
- Product tagline
- Profile icon

Search:

- Search events (future functionality)

Discover Events:

- Section heading
- See All navigation
- Event preview cards

Your Circle's Plans:

- Section heading
- See All navigation
- Private event preview cards

Bottom Navigation:

- Home
- Discover
- Create
- My Circle
- Profile

## 3. Event Card Components

Each event preview displays:

- Event name
- Event date
- Event time
- Event image or placeholder

Selecting an event opens EventDetailsPage.

## 4. Navigation

Discover → EventListPage (All Events)

Discover See All → EventListPage (All Events)

Public Event Card → EventDetailsPage

Circle Event Card → EventDetailsPage

Create → AddEventPage

My Circle → Circle area (future)

Profile → Profile area (future)

Logout → WelcomePage

## 5. Authenticated User Behavior

Authenticated users can:

- Browse public events
- View accessible private circle events
- Open event details
- Access Attending and Hosting event lists
- Open Add Event
- Open RSVP with prepopulated user information
- Log out

## 6. Guest Behavior

Guest users can:

- Browse public events
- Open public event details
- Open RSVP with blank fields

Guest users cannot:

- View private circle events
- Access Attending or Hosting lists
- Access private circle membership features

For guests, the Your Circle's Plans section displays
a Create Account / Log In prompt.

## 7. Week 2 Implementation

- Use hard-coded event data.
- Display sample public events.
- Display sample circle events for the test user.
- Implement navigation to event lists and details.
- Implement navigation to Add Event.
- Provide access to Logout.
- Use session state to distinguish guests from users.

Search and private circle membership management
are not functional in Week 2.

## 8. Week 3 Integration

- Load locally created events from SQLite.
- Load public discovery events from an external API.
- Retrieve Attending and Hosting records from SQLite.
- Replace mock event lists with service-based data.

## 9. Future Enhancements

- Location-based event discovery
- Search and filtering
- Personalized event recommendations
- Private circle creation and membership
- Event notifications
- Dynamic event imagery
- Cloud synchronization

## 10. Acceptance Criteria

- Home opens after successful login.
- Home opens for guest access.
- Both public and circle sections are visible.
- Guest users see a circle sign-in prompt.
- Public event cards open event details.
- Discover opens the All Events list.
- Attending and Hosting lists are accessible
  to authenticated users.
- Create opens the Add Event screen.
- Logout returns the user to Welcome.
