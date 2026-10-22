# Sprint 3 — Create Event and Home Integration Testing

## Features Tested

- Event creation and form validation
- Authenticated user authorization
- Guest access restrictions
- In-memory event storage
- Public and private event visibility
- Home page event display

## Test Results

| Test Case                 | Expected Result            | Status |
| ------------------------- | -------------------------- | ------ |
| Submit empty form         | Required-field error       | Pass   |
| Missing visibility        | Visibility error           | Pass   |
| Past event date           | Date validation error      | Pass   |
| Valid event               | Event created successfully | Pass   |
| Guest creates event       | Access denied              | Pass   |
| Public event on Home      | Appears in public section  | Pass   |
| Private event on Home     | Appears for creator        | Pass   |
| Guest views private event | Event hidden               | Pass   |
| Profile logout and guest navigation | User logs out, returns to Welcome, and enters as Guest | Pass |
## Notes

- EventService uses in-memory storage.
- Events are not yet persisted to SQLite.
- Private event access is temporarily limited to the creator.
- Event lists refresh when Home appears.

## Outcome

Sprint 3 Create Event and Home integration tests passed.
