# Sprint 2 — Registration and Guest Access Testing

## Objective

Verify user registration, input validation, account creation,
authentication integration, and guest navigation in LINQD.

## Environment

- Framework: .NET MAUI / .NET 10
- Language: C#
- IDE: Visual Studio Code
- Platform: Android
- Emulator: Pixel 10 Pro XL
- Storage: In-memory authentication service

## Test Results

| Test Case              | Expected Result                     | Actual Result                | Status |
| ---------------------- | ----------------------------------- | ---------------------------- | ------ |
| Open Registration      | Registration page opens             | Page opened successfully     | PASS   |
| Required Fields        | Reject empty or missing fields      | Validation message displayed | PASS   |
| Email Validation       | Reject invalid email formats        | Invalid emails rejected      | PASS   |
| Password Length        | Reject passwords under 8 characters | Short passwords rejected     | PASS   |
| Password Confirmation  | Reject mismatched passwords         | Mismatch rejected            | PASS   |
| Account Creation       | Register a valid user               | Success message displayed    | PASS   |
| Duplicate Registration | Reject existing username/email      | Duplicate message displayed  | PASS   |
| Test Account Login     | Authenticate original test user     | Welcome, Test! displayed     | PASS   |
| Registered User Login  | Authenticate John Smith             | Welcome, John! displayed     | PASS   |
| Guest Navigation       | Open Guest page                     | Guest page opened            | PASS   |
| Guest Return           | Return to Welcome                   | Welcome page displayed       | PASS   |

## Issues and Resolutions

1. Registration validation messages were initially hidden.
   Resolved by correcting MessageLabel visibility.

2. Android emulator package installation failed with an ADB
   broken-pipe error.
   Resolved by restarting ADB and cold-booting the emulator.

3. Login initially created a hard-coded Test User profile.
   Resolved by retrieving the registered user from
   AuthenticationService.

## Known Limitations

- Registered accounts are stored in memory and do not persist
  across application restarts.
- Guest access currently opens a temporary Guest page.
- Event browsing, RSVP permissions, and persistent storage
  will be implemented in later sprints.
- Password handling is prototype-only and must be secured
  before persistent storage or production use.

## Summary

Sprint 2 registration, authentication integration, and guest
navigation tests passed.

Status: PASS
