# User Flows
## Authentication
Welcome -> Login -> Home (member) OR Continue as Guest -> Home (guest)
Login -> Add User -> (valid Add/OK OR Cancel) -> Login
Home -> Logout -> Login

## Discovery
Home -> All Events -> Event Details -> RSVP -> (valid Save OR Cancel) -> Event Details
Home -> Attending / Hosting (member only) -> Event Details

## Creation
Home -> Add Event -> (valid Save OR Cancel) -> Home

## Rules
- Invalid submissions remain on the current page and show field-specific feedback.
- In this week's prototype, valid submissions acknowledge success but do not persist data.
- Guest RSVP fields start blank; member RSVP fields use hard-coded profile values.
- Guest access must not expose member-only Attending / Hosting views.
