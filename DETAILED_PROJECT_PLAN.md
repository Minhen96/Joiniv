# Joiniv Event Platform - Detailed Technical Specification

This document serves as the complete blueprint for the Joiniv Event Platform. As the developer, you will follow this specification to implement each layer.

## 1. System Architecture: Clean Architecture
We will organize the project into several logical layers (folders in your project):

- **Domain**: Core entities, interfaces, and shared logic (No dependencies).
- **Application**: Business logic, Service interfaces, DTOs, and AutoMapper profiles.
- **Infrastructure**: Database context (EF Core), Redis implementation, Data Persistence, and External Services.
- **API**: Controllers, Middleware, JWT Configuration, and Dependency Injection.

---

## 2. Database Schema (Detailed)

### Entities & Columns

#### 1. User
| Property     | Type     | Constraints              |
| :----------- | :------- | :----------------------- |
| Id           | Guid     | Primary Key              |
| Username     | string   | Required, Unique, Max 50 |
| Email        | string   | Required, Unique         |
| PasswordHash | string   | Required                 |
| Role         | string   | "Admin" or "User"        |
| CreatedAt    | DateTime | Default UTC Now          |

#### 2. Event
| Property        | Type     | Constraints        |
| :-------------- | :------- | :----------------- |
| Id              | Guid     | Primary Key        |
| Title           | string   | Required, Max 100  |
| Description     | string   | Required, Max 1000 |
| StartTime       | DateTime | Required           |
| EndTime         | DateTime | Required           |
| MaxParticipants | int      | Required, > 0      |
| Location        | string   | Optional           |

#### 3. EventImage
| Property | Type   | Constraints |
| :------- | :----- | :---------- |
| Id       | Guid   | Primary Key |
| ImageUrl | string | Required    |
| EventId  | Guid   | Foreign Key |

#### 4. Game
| Property    | Type   | Constraints      |
| :---------- | :----- | :--------------- |
| Id          | Guid   | Primary Key      |
| Name        | string | Required, Max 50 |
| Description | string | Max 500          |

#### 5. Booking
| Property | Type     | Constraints     |
| :------- | :------- | :-------------- |
| Id       | Guid     | Primary Key     |
| UserId   | Guid     | Foreign Key     |
| EventId  | Guid     | Foreign Key     |
| BookedAt | DateTime | Default UTC Now |

---

## 3. API Endpoint Definitions

### Authentication
- `POST /api/auth/register`: Signup for new users.
- `POST /api/auth/login`: Returns a JWT token and user role.

### Event Management (Admin)
- `GET /api/events`: List all (with filters).
- `POST /api/events`: Create new event with image uploads.
- `PUT /api/events/{id}`: Update details.
- `DELETE /api/events/{id}`: Soft or hard delete.

### User Features (Public/Logged in)
- `GET /api/events/browse`: Cached list via Redis.
- `POST /api/bookings`: Book a seat (requires validation of capacity).

---

## 4. Technical Implementation Logic

### 1. Redis Caching Strategy
- **Popular Events**: Store serialized JSON of top 5 events in Redis with a 15-minute expiration.
- **Participant Counts**: Store and increment counts in Redis for real-time updates without hitting the DB constantly.

### 2. SignalR Chat
- **Hub**: `EventChatHub`.
- **Logic**: Users join a "Room" based on `EventId` when they open the chat.

### 3. Concurrency Control
- Use **EF Core Row Versioning** or **Database Transactions** to ensure two users don't book the final seat at the exact same millisecond.

---

## 5. Development Roadmap (The Order You Will Follow)

1. **Step 1: Domain Entities**: Create classes in `/Domain`.
2. **Step 2: DbContext**: Setup EF Core configurations in `/Infrastructure`.
3. **Step 3: Repositories (Optional)**: If you prefer the Repository pattern.
4. **Step 4: JWT & Auth Service**: Implement logic to issue tokens.
5. **Step 5: Event CRUD**: Wire up the first controller.
6. **Step 6: Redis & SignalR**: Add the "extra" power features.

---
**Review this document carefully. Once you confirm it matches your vision, you can begin Step 1.**
