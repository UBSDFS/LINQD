# LINQD — Add Event Screen

## 1. Purpose

Allow authenticated users to create public or
private events.

For Week 2, the form will validate input,
but events will not be saved.

## 2. Screen Layout

              ← Create Event

    Event Name *
    [ Enter event name        ]

    Description *
    [ What's happening?       ]

    Date *            Time *
    [ Select Date ]   [ Select Time ]

    Location *
    [ Enter location          ]

    Event Visibility *
    ( ) Public    ( ) Private

    [ Cancel ] [ Create Event ]

## 3. UI Components

- Entry: Event Name
- Editor: Event Description
- DatePicker: Event Date
- TimePicker: Event Time
- Entry: Location
- RadioButton: Public
- RadioButton: Private
- Button: Cancel
- Button: Create Event
- Label: Validation messages

## 4. Navigation

HomePage → AddEventPage

Create Event (valid) → Confirmation,
then return to HomePage

Create Event (invalid) → Remain on AddEventPage

Cancel → Previous Page

Back → Previous Page

## 5. Input Validation

Required fields:

- Event Name
- Description
- Date
- Time
- Location
- Visibility

Validation Rules:

1. Event Name cannot be empty.
2. Description cannot be empty.
3. Location cannot be empty.
4. Date must be explicitly selected.
5. Time must be explicitly selected.
6. Public or Private must be selected.

Note: MAUI DatePicker and TimePicker can display
default values. Track whether the user has
confirmed or selected a value before submission.

Example Error:
"Please complete all required fields."

## 6. Button Behavior

### Create Event

1. Read all form inputs.
2. Validate required fields.
3. Display error if validation fails.
4. If valid, display confirmation.
5. Return to HomePage.
6. Do not save event data during Week 2.

### Cancel

1. Discard form input.
2. Return to the previous page.
3. Do not save data.

## 7. Authentication Rules

Authenticated users:

- Can open AddEventPage.
- Can validate event creation forms.

Guest users:

- Cannot create events.
- Are prompted to log in or register.

## 8. Planned Event Model

- EventId
- Title
- Description
- StartDateTime
- Location
- HostUserId
- Visibility
- Source

Source identifies whether an event was
created locally or retrieved from an API.

## 9. Week 3 Integration

- Save created events to SQLite.
- Associate events with HostUserId.
- Retrieve events through EventService.
- Display created events in Hosting.
- Support editing and deleting local events.

## 10. Future Enhancements

- Event image upload
- Invitations
- Event categories
- Maximum attendance
- Recurring events
- Maps and location suggestions
- Cloud synchronization
- Circle-specific event visibility

## 11. Acceptance Criteria

- Add Event screen opens correctly.
- All required inputs are displayed.
- Missing fields trigger validation.
- Invalid submissions remain on the page.
- Valid submissions display confirmation.
- Cancel returns to the previous page.
- No event is saved during Week 2.
- Guest access is restricted.
