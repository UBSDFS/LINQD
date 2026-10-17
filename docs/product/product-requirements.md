# Product Requirements
## Core journeys
- Guest: open app -> browse all public events -> view event details -> complete RSVP form (initial prototype only).
- Member: log in -> browse all / attending / hosting -> view details -> RSVP.
- Host: log in -> open add event -> validate form -> later publish an event.
- New user: open registration -> validate all required fields -> return to login (prototype does not persist).

## Functional requirements
- Navigation between welcome, login, registration, home, event lists, event details, add event, and RSVP.
- Hard-coded test user login and guest mode in classroom prototype.
- Distinct event filters: all, attending, hosting (latter two require login).
- Required-field validation for registration, add event, and RSVP.
- Cancel returns without saving; logout clears session.

## Non-functional goals
- Accessible labels, clear error messages, readable UI, predictable back navigation.
- Separate UI from data access and session logic.
- No secrets or real passwords committed to repository.
- Validate data at trust boundaries when persistence/API is introduced.

## Open decisions
- Final brand/name; guest RSVP persistence rules; private circle invitation permissions; launch geography; event API/provider; authentication provider; privacy and moderation policies.
