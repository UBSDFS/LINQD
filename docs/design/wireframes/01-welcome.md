# LINQD — Welcome Screen

## 1. Purpose

The Welcome screen is the entry point to LINQD.

It allows users to log in, create an account, or explore public events as a guest.

## 2. Screen Layout

                 LINQD

     Discover events. Connect with
       people. Make plans.

        [    Log In    ]

        [ Create Account ]

        Continue as Guest →

## 3. UI Components

- Label: Application name (LINQD)
- Label: Product tagline
- Button: Log In
- Button: Create Account
- Button: Continue as Guest

## 4. Navigation

Log In → LoginPage

Create Account → AddUserPage

Continue as Guest → HomePage

## 5. Expected Behavior

### Log In

Navigates to the Login screen.

### Create Account

Navigates to the registration screen.

### Continue as Guest

Navigates directly to Home without authentication.

Guest users can:

- Browse public events
- View public event details
- Access the RSVP form with blank fields

Guest users cannot:

- Access private circle events
- View personal Attending or Hosting lists

## 6. Future Enhancements

- Application branding and logo
- Personalized welcome experience
- Persistent authenticated sessions
- Onboarding for new users
