# LINQD — Login Screen

## 1. Purpose

The Login screen allows registered users to access LINQD
using their username and password.

For the Week 2 prototype, authentication will use
hard-coded credentials.

## 2. Screen Layout

             ← Back

             LINQD

          Welcome Back

    Username or Email
    [ Enter username         ]

    Password
    [ Enter password         ]

         [ Log In ]

    Don't have an account?
           Register

## 3. UI Components

- Label: LINQD
- Label: Welcome Back
- Entry: Username or Email
- Entry: Password (masked)
- Button: Log In
- Link/Button: Register
- Back navigation

## 4. Navigation

Back → WelcomePage

Register → AddUserPage

Successful Login → HomePage

Failed Login → Remain on LoginPage

## 5. Expected Behavior

### Successful Login

1. User enters username and password.
2. Application validates credentials.
3. Application sets authenticated session state.
4. Application navigates to HomePage.

### Failed Login

1. User enters invalid credentials.
2. Application displays an error message.
3. User remains on LoginPage.

### Empty Fields

If either field is empty, display:

"Please enter your username and password."

## 6. Hard-Coded Test Credentials

Username: testuser

Password: Password1

These credentials are for development only.

## 7. Session Management

After successful login:

- IsLoggedIn = true
- CurrentUser = authenticated test user

The session will determine access to:

- Attending Events
- Hosting Events
- Private Circle Plans
- Prepopulated RSVP fields

## 8. Future Enhancements

- Database-backed user accounts
- Secure password hashing
- Backend authentication
- Password recovery
- Persistent sessions
