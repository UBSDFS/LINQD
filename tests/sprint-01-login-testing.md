# LINQD — Sprint 1 Test Report

**Feature:** Login Authentication and Session Management  
**Platform:** .NET MAUI / Android Emulator  
**Test Date:** October 18, 2026  
**Status:** Passed

## 1. Test Objective

Verify that the LINQD Login page correctly validates user input, authenticates hard-coded test credentials, initializes the user session, and supports navigation back to the Welcome screen.

## 2. Test Environment

- **Framework:** .NET MAUI (.NET 10)
- **Language:** C#
- **IDE:** Visual Studio Code
- **Device:** Pixel 10 Pro XL Android Emulator
- **Authentication:** Hard-coded test credentials
- **Test Username:** `testuser`
- **Test Password:** `password1`

## 3. Test Cases and Results

| Test ID | Test Scenario                  | Expected Result                                 | Status |
| ------- | ------------------------------ | ----------------------------------------------- | ------ |
| TC-001  | Submit empty username/password | Display required-fields validation message      | PASS   |
| TC-002  | Submit incorrect credentials   | Display invalid credentials message             | PASS   |
| TC-003  | Submit valid credentials       | Display "Login Successful" and "Welcome, Test!" | PASS   |
| TC-004  | Select Cancel                  | Navigate back to Welcome screen                 | PASS   |

## 4. Implementation Verified

- Login page successfully connected to `AuthenticationService`.
- Username and password validation implemented.
- `UserSessionService` integrated to store the authenticated user.
- Public service method naming corrected to PascalCase.
- Shell navigation supports returning to the Welcome screen.
- Android build and deployment completed successfully.

## 5. Issues Resolved

**CS1061 — Method Not Found**

The Login page called `Login()`, but `UserSessionService` defined the method as `login()`.

**Resolution:** Renamed the public methods to `Login()` and `Logout()` to maintain consistent C# naming conventions.

## 6. Test Summary

**Total Test Cases:** 4  
**Passed:** 4  
**Failed:** 0

All planned Sprint 1 Login test cases passed on the Android emulator.

The authenticated user session is initialized in code. Persistence and access to that session across additional pages will be verified during subsequent sprints.

## 7. Version Control

Sprint 1 implementation was committed to the LINQD Git repository.

**Commit Message:** `feat: implement login authentication and session management`

## 8. Next Sprint

**Sprint 2 — Registration and Guest Access**

- Create Registration page.
- Validate registration inputs.
- Implement in-memory user registration.
- Implement Continue as Guest.
- Test navigation and session behavior.
