# LINQD — Registration / Add User Screen

## 1. Purpose

Allow new users to enter their account information
and create a LINQD profile.

For Week 2, the form will validate user input,
but account information will not be saved.

## 2. Screen Layout

              ← Back

          Create Account

     Join LINQD and start connecting.

     First Name
     [ Enter first name       ]

     Last Name
     [ Enter last name        ]

     Email
     [ Enter email address    ]

     Username
     [ Choose username       ]

     Password
     [ Enter password        ]

     Confirm Password
     [ Confirm password      ]

     [ Cancel ] [ Create Account ]

## 3. UI Components

- Entry: First Name
- Entry: Last Name
- Entry: Email
- Entry: Username
- Entry: Password (masked)
- Entry: Confirm Password (masked)
- Button: Create Account
- Button: Cancel
- Back navigation
- Label: Validation messages

## 4. Navigation

WelcomePage → AddUserPage

LoginPage → AddUserPage

Cancel → Return to previous page

Successful Validation → Confirmation message,
then return to LoginPage

Invalid Submission → Remain on AddUserPage

## 5. Input Validation

All six fields are required.

Validation Rules:

1. No field may be empty or contain only spaces.
2. Email must have a valid basic format.
3. Password and Confirm Password must match.
4. Invalid submissions must display an error message.
5. User must remain on the page until validation
   succeeds or Cancel is selected.

Example Messages:

Missing Fields:
"Please complete all required fields."

Invalid Email:
"Please enter a valid email address."

Password Mismatch:
"Passwords do not match."

Successful Validation:
"Registration information validated successfully."

## 6. Button Behavior

### Create Account

1. Read all six Entry values.
2. Validate required fields.
3. Validate email format.
4. Compare passwords.
5. Display an error if validation fails.
6. If valid, display confirmation.
7. Navigate to LoginPage.

No user account is saved during Week 2.

### Cancel

1. Discard entered information.
2. Return to the previous page.
3. Do not save any data.

## 7. Data Model

Planned User properties:

- UserId
- FirstName
- LastName
- Email
- Username

Password credentials will be handled separately
using secure authentication practices in the
production version.

## 8. Future Database Integration

Week 3:

- Create SQLite Users table.
- Implement user registration persistence.
- Validate duplicate usernames and emails.
- Retrieve user information for authenticated sessions.

Production:

- Implement secure backend authentication.
- Hash passwords using an appropriate password
  hashing algorithm.
- Add email verification.
- Add password recovery.
- Add account management.

## 9. Acceptance Criteria

- All six input fields are visible.
- Password fields mask user input.
- Empty submissions are rejected.
- Invalid email addresses are rejected.
- Mismatched passwords are rejected.
- Valid submissions display confirmation.
- Valid submissions return to Login.
- Cancel returns to the previous screen.
- No registration data is persisted.
