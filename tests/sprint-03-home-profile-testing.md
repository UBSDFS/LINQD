# Sprint 3A — Home and Profile Testing

## Objective

Verify Home page navigation, the public and private event section layouts, Profile page functionality, and integration with UserSessionService.

## Test Environment

- Framework: .NET MAUI / .NET 10
- Language: C#
- IDE: Visual Studio Code
- Platform: Android
- Emulator: Pixel 10 Pro XL
- Authentication: In-memory UserSessionService

## Test Results

| Test Case                    | Expected Result                             | Actual Result                               | Status  |
| ---------------------------- | ------------------------------------------- | ------------------------------------------- | ------- |
| Home Page Build              | Application builds successfully             | Build succeeded                             | PASS    |
| Guest Navigation             | Continue as Guest opens Home                | Home opened successfully                    | PASS    |
| Authenticated Navigation     | Successful login opens Home                 | Login confirmation appeared and Home opened | PASS    |
| Home Layout                  | Public and private event sections display   | Layout displayed successfully               | PASS    |
| Guest Profile Access         | Guest can open Profile                      | Guest Profile opened                        | PASS    |
| Authenticated Profile Access | Logged-in user can open Profile             | Authenticated Profile opened                | PASS    |
| Session Information          | Profile displays current user's information | Not independently confirmed                 | PENDING |
| Logout                       | Clears session and returns to Welcome       | Not independently confirmed                 | PENDING |

## Issues and Resolutions

**Issue:** Deployment initially failed with `XA0010: No available device`.

**Resolution:** Started the Pixel 10 Pro XL emulator and successfully deployed LINQD.

## Known Limitations

- Home currently contains placeholder content rather than populated event records.
- Profile uses the current in-memory session.
- Permanent navigation and event management are still under development.
- Logout behavior requires a separate verification.

## Summary

Home and Profile navigation were successfully tested for guest and authenticated users. Session information and logout verification remain pending.

**Status:** PARTIAL PASS — verified navigation tests passed.
