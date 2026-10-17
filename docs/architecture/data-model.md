# Data Model — Initial Draft
## User
UserId, DisplayName, Email, Profile fields

## Event
EventId, HostUserId, Title, Description, Location, StartDateTime, Visibility (public/private)

## RSVP
RsvpId, EventId, UserId (nullable for guest workflows only if supported), Status (Interested/Going/NotGoing), SubmittedAt

## Circle (future MVP)
CircleId, Name, OwnerUserId

## CircleMembership (future MVP)
CircleId, UserId, Role

## Relationships
User 1-to-many hosted Events; User many-to-many Events through RSVPs; Circle many-to-many Users through memberships.

Questions: guest RSVP identity/verification, uniqueness of one RSVP per user/event, private-event authorization, and event time zone handling.
