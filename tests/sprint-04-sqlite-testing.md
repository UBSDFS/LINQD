# LINQD — Sprint 4 SQLite & DAL Testing Report

**Project:** LINQD — RSVP & Social Events Application  
**Sprint:** 04 — SQLite Database & Data Access Layer  
**Platform:** .NET MAUI / Android Emulator  
**Database:** SQLite (`linqd.db3`)  
**Test Date:** October 22, 2026  
**Overall Result:** PASS — Event Persistence Milestone

---

## 1. Test Objective

Verify that LINQD can initialize its local SQLite database, create and retrieve event records through the Data Access Layer (DAL), and preserve saved events after the application restarts.

## 2. Components Tested

| Component       | Responsibility                                 |
| --------------- | ---------------------------------------------- |
| LinqdDatabase   | SQLite connection, initialization, and queries |
| EventRepository | Event data access                              |
| EventService    | Event application operations                   |
| CreateEventPage | Event input, validation, and creation          |
| HomePage        | Event retrieval and display                    |
| MauiProgram     | Dependency injection registration              |

## 3. Database Schema Verification

The SQLite database file was confirmed on the Android emulator using ADB.

**Database file:** `linqd.db3`

Python's built-in SQLite library was used to inspect the copied database.

**Verified tables:**

- User
- Event
- Rsvp
- sqlite_sequence (SQLite internal table)

**Result:** PASS

## 4. Functional Test Cases

| Test ID | Test Description         | Expected Result                   | Actual Result              | Status |
| ------- | ------------------------ | --------------------------------- | -------------------------- | ------ |
| S04-T01 | Launch application       | Application launches successfully | Launched successfully      | PASS   |
| S04-T02 | User login               | User can authenticate             | Login successful           | PASS   |
| S04-T03 | Create public event      | Event saved without error         | Event created successfully | PASS   |
| S04-T04 | Retrieve created event   | Event appears on Home             | Event displayed            | PASS   |
| S04-T05 | Restart application      | Application reopens successfully  | Restart successful         | PASS   |
| S04-T06 | Verify event persistence | Event remains after restart       | Event remained visible     | PASS   |

## 5. Test Data

| Field       | Value                            |
| ----------- | -------------------------------- |
| Event Title | LINQD Database Test              |
| Description | Testing SQLite event persistence |
| Location    | Newport News, VA                 |
| Date/Time   | Future date and time             |
| Visibility  | Public                           |
| Host        | Authenticated test user          |

## 6. Issues Encountered and Resolutions

**Issue 1 — Namespace Resolution**

The database class initially could not resolve the model classes.

**Resolution:** Added the required `using Linqd.App.Models;` directive.

**Issue 2 — Application Not Responding**

The application became unresponsive when database initialization was synchronously awaited during startup.

**Resolution:** Removed the blocking `.GetAwaiter().GetResult()` call and moved initialization into an asynchronous application startup handler.

**Issue 3 — EventService Method References**

Compilation failed after replacing synchronous service methods with asynchronous methods.

**Resolution:** Updated CreateEventPage and HomePage to call `AddEventAsync()` and `GetEventsAsync()` using `await`.

## 7. Results and Observations

The application successfully created a public event, retrieved it on the Home page, and displayed the same event after restarting.

This confirms that the event creation and retrieval workflow uses persistent SQLite storage rather than relying solely on the previous in-memory event list.

Database initialization and event retrieval operated without the previously observed startup freeze.

## 8. Pending Tests

- Update existing event records
- Delete event records
- Persist registered user profiles
- Verify authentication and session behavior after restart
- Save and retrieve RSVP records
- Validate private-event ownership across sessions
- Test database operation failures and recovery

## 9. Conclusion

Sprint 4's initial SQLite and DAL integration milestone passed functional testing.

LINQD can now create and retrieve event records using SQLite and retain those records after an application restart.

Additional CRUD operations, user persistence, and RSVP integration remain for subsequent development and testing.

**Milestone Status: PASSED**
