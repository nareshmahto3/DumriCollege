# DumriCollege

DumriCollege is a .NET 8 solution for a college management backend. The repository is organized as multiple APIs that cover admissions, users and masters, student fee handling, and an API gateway layer intended to route selected requests.

## Solution Overview

The solution file is [`DumriCollege.sln`](/c:/Users/KIIT/Documents/GitHub/DumriCollege/DumriCollege.sln) and currently contains these projects:

| Project | Purpose | Default local URL |
| --- | --- | --- |
| `Admission.Api` | Admission CRUD with MediatR, EF Core, Swagger | `http://localhost:5000` |
| `User.Api` | Authentication, teachers, classes, subjects, exams, notices, student documents | `https://localhost:7196` / `http://localhost:5258` |
| `Student.Api` | Student fee lookup and payment endpoints | `https://localhost:7004` / `http://localhost:5173` |
| `ApiGateway` | Ocelot-based gateway project for upstream routing | `https://localhost:7134` / `http://localhost:5136` |
| `FeeCollection` | Placeholder ASP.NET Core API scaffold | `https://localhost:7152` / `http://localhost:5062` |
| `LibraryService.Utility.Data.Core` | Shared repository, unit-of-work, and pagination utilities | n/a |

## Tech Stack

- .NET 8 / ASP.NET Core Web API
- Entity Framework Core with SQL Server
- MediatR for CQRS-style command and query handling
- JWT bearer authentication in `User.Api`
- Swagger/OpenAPI in `Admission.Api` and `User.Api`
- Ocelot in `ApiGateway`

## Project Structure

```text
.
|-- Admission.Api
|-- ApiGateway
|-- FeeCollection
|-- LibraryService.Utility.Data.Core
|-- Student.Api
|-- User.Api
|-- DumriCollege.sln
```

## Prerequisites

Before running the solution, make sure you have:

- .NET SDK 8.x
- SQL Server access
- A database named `DumriCommerceCollege`
- Development HTTPS certificate trusted for local ASP.NET Core use

## Database and Configuration

The APIs are currently wired to SQL Server using the `DumriCommerceCollege` database.

Configured connection strings:

- `Admission.Api/appsettings.json`
  - `ConnectionStrings:AdmissionDbConn`
- `User.Api/appsettings.json`
  - `ConnectionStrings:UserDbConn`

At the moment, both EF Core contexts also contain hard-coded fallback SQL Server connection strings in code that point to:

```text
Server=NARESH;Database=DumriCommerceCollege;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;
```

Update the JSON config and, ideally, remove or replace the hard-coded `OnConfiguring` values before using this outside the current development environment.

### JWT Configuration

`User.Api` expects JWT settings under:

```json
"JWT": {
  "JwtOptions": {
    "Secret": "your-secret-key",
    "Audience": "your-audience",
    "Issuer": "your-issuer",
    "ExpireMinutes": 60
  }
}
```

Those values are referenced in [`User.Api/Program.cs`](/c:/Users/KIIT/Documents/GitHub/DumriCollege/User.Api/Program.cs) and [`User.Api/JwtOptions.cs`](/c:/Users/KIIT/Documents/GitHub/DumriCollege/User.Api/JwtOptions.cs), but they are not present in the checked-in `appsettings.json`.

## Restore and Run

From the repository root:

```powershell
dotnet restore DumriCollege.sln
dotnet build DumriCollege.sln
```

Run a specific service:

```powershell
dotnet run --project Admission.Api
dotnet run --project User.Api
dotnet run --project Student.Api
dotnet run --project ApiGateway
```

If you prefer running everything from Visual Studio, open [`DumriCollege.sln`](/c:/Users/KIIT/Documents/GitHub/DumriCollege/DumriCollege.sln) and set multiple startup projects.

## API Summary

### Admission.Api

Base route: `api/v1/Admission`

- `GET /api/v1/Admission/GetById/{id}`
- `GET /api/v1/Admission/GetAll?pageNumber=1&pageSize=10`
- `POST /api/v1/Admission/AddNew`
- `PUT /api/v1/Admission/Update`

Swagger is enabled in development.

### User.Api

Main route groups:

- Authentication
  - `POST /api/Auth/login`
  - `POST /api/Auth/RefreshToken`
- Roles
  - `POST /api/Master/AddRole`
- Teachers
  - `POST /api/Teacher`
  - `POST /api/Teacher/AddWithPhoto`
  - `GET /api/Teacher/{id}`
  - `GET /api/Teacher`
  - `PUT /api/Teacher/{id}`
  - `DELETE /api/Teacher/{id}`
- Classes
  - `POST /api/Class`
  - `GET /api/Class/{id}`
  - `GET /api/Class`
- Subjects
  - `POST /api/Subject`
  - `GET /api/Subject/{id}`
  - `GET /api/Subject`
- Exams
  - `POST /api/Exam`
  - `GET /api/Exam/{id}`
  - `GET /api/Exam`
  - `PUT /api/Exam/{id}`
  - `DELETE /api/Exam/{id}`
- Notices
  - `POST /api/Notice`
  - `PUT /api/Notice/{id}`
  - `DELETE /api/Notice/{id}`
  - `GET /api/Notice/{id}`
  - `GET /api/Notice`
- Student documents
  - `POST /student/documents`
  - `GET /admin/certificates`
  - `PUT /admin/certificates/{id}/status`
- Users
  - `GET /api/User`
  - `GET /api/User/{id}`
  - `POST /api/User`
  - `PUT /api/User/{id}`
  - `DELETE /api/User/{id}`

Notes:

- CORS is configured for `http://localhost:3000`.
- Static files are enabled.
- Several controllers use `[Authorize]`, and some admin operations require the `Admin` role.

### Student.Api

Base route: `student/fees`

- `GET /student/fees/{studentId}`
- `POST /student/fees/pay`

This API currently uses a service-backed in-memory style flow and does not register Swagger in `Program.cs`.

### ApiGateway

The gateway includes Ocelot configuration in [`ApiGateway/ocelot.json`](/c:/Users/KIIT/Documents/GitHub/DumriCollege/ApiGateway/ocelot.json) for admission routes such as:

- `GET /admission/{version}/getbyid/{id}`
- `GET /admission/{version}/getall`
- `POST /admission/{version}/add`

Configured downstream target in `ocelot.json`:

- `https://localhost:7001`

Important: the current gateway startup in [`ApiGateway/Program.cs`](/c:/Users/KIIT/Documents/GitHub/DumriCollege/ApiGateway/Program.cs) registers Ocelot services, but it does not call the Ocelot middleware pipeline. If you want the gateway to actually proxy requests, that startup flow still needs to be completed.

## Development Notes

The repository mixes production-style features with work-in-progress pieces. Based on the current code:

- `User.Api` references `IAuthService` and `IUserService`, but their concrete implementations are not registered in `Program.cs`.
- `User.Api` expects JWT settings that are not present in the checked-in configuration.
- `ClassController` and `SubjectController` return placeholder responses for `GET` endpoints.
- `ApiGateway` has route config, but the request pipeline is not fully wired to use Ocelot.
- `FeeCollection` is still the default template scaffold.

## Migrations and EF Tooling

The repository includes a local tool manifest with `dotnet-ef`.

Restore tools if needed:

```powershell
dotnet tool restore
```

Existing EF Core migrations are currently present in `User.Api/Migrations`.

## Current Verification Status

I updated this README from the code currently in the repository. A `dotnet build DumriCollege.sln` attempt in this environment did not complete successfully, but it also did not emit compiler diagnostics, so this README reflects source inspection rather than a confirmed end-to-end local run.
