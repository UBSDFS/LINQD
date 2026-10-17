# LINQD — RSVP Screen

## 1. Purpose

Allow authenticated users and guests to submit
an RSVP response for a selected event.

For Week 2, the form validates input but
does not save RSVP records.

## 2. Screen Layout

              ← RSVP to Event

          Saturday Jazz Night
       Saturday, October 24 - 7 PM

    First Name *
    [ Enter first name       ]

    Last Name *
    [ Enter last name        ]

    Email *
    [ Enter email address    ]

    RSVP Response *
    ( ) Going
    ( ) Interested
    ( ) Not Going

    [ Cancel ] [ Submit RSVP ]

## 3. UI Components

- Event title label
- Event date/time label
- Entry: First Name
- Entry: Last Name
- Entry: Email
- RadioButtons: RSVP response
- Button: Submit RSVP
- Button: Cancel
- Validation message label
- Back navigation

## 4. Navigation

EventDetailsPage → RSVPPage

Cancel → EventDetailsPage

Valid Submission → Confirmation,
then EventDetailsPage

Invalid Submission → Remain on RSVPPage

## 5. Authenticated User Behavior

When an authenticated user opens RSVPPage:

- First Name is prepopulated.
- Last Name is prepopulated.
- Email is prepopulated.
- RSVP response is initially unselected.

Prepopulated values come from CurrentUser.

For Week 2, CurrentUser contains hard-coded data.

## 6. Guest Behavior

When a guest opens RSVPPage:

- First Name is blank.
- Last Name is blank.
- Email is blank.
- RSVP response is unselected.

Guests must manually enter their information.

## 7. Input Validation

Required:

- First Name
- Last Name
- Email
- RSVP Response

Validation Rules:

1. First Name cannot be empty.
2. Last Name cannot be empty.
3. Email cannot be empty.
4. Email must have a valid basic format.
5. RSVP response must be selected.
6. Invalid submissions remain on RSVPPage.

Error:
"Please complete all required fields."

## 8. Button Behavior

### Submit RSVP

1. Read all form values.
2. Validate required fields.
3. Validate email format.
4. Validate RSVP selection.
5. Display errors if validation fails.
6. If valid, display confirmation.
7. Return to EventDetailsPage.
8. Do not save RSVP data during Week 2.

### Cancel

1. Discard form changes.
2. Return to EventDetailsPage.
3. Do not save data.

## 9. Planned RSVP Model

- RSVPId
- EventId
- UserId (nullable for guests)
- GuestFirstName
- GuestLastName
- GuestEmail
- Response
- CreatedAt

Response values:

- Going
- Interested
- NotGoing

## 10. Week 3 Database Integration

- Create RSVP table in SQLite.
- Associate RSVP with EventId.
- Associate authenticated RSVP with UserId.
- Support guest RSVP records.
- Retrieve Attending events from Going responses.
- Update an existing RSVP response.

## 11. Future Enhancements

- RSVP confirmation notifications
- Attendance counts
- Event capacity limits
- RSVP deadlines
- Guest invitations
- Waitlists
- Calendar integration

## 12. Acceptance Criteria

- RSVPPage opens for selected event.
- Correct event information is displayed.
- Authenticated user fields are prepopulated.
- Guest fields are blank.
- RSVP response must be selected.
- Missing fields trigger validation.
- Valid submissions display confirmation.
- Cancel returns to EventDetailsPage.
- No RSVP data is persisted during Week 2.
