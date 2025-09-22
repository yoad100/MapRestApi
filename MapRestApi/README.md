```markdown
# Map Task Backend

This is the backend API for the Map Task application, built with **ASP.NET Core 7** and **MongoDB**. It provides CRUD operations for polygons and map objects.

## Features

- Manage polygons and map objects in MongoDB
- Bulk insert of objects and polygons
- RESTful API with controllers
- Proper error handling and status codes

## Tech Stack

- ASP.NET Core 7
- MongoDB
- C# 11
- Dependency Injection for repositories
- Swagger/OpenAPI for testing

## Getting Started

### Prerequisites

- .NET 7 SDK
- MongoDB (local or remote)

### Installation

```bash
# Clone the repo
git clone https://github.com/<your-username>/backend.git
cd backend

# Restore dependencies
dotnet restore
Running the API
bash
Copy code
dotnet run
The API will run on https://localhost:7115 by default

Swagger UI available at https://localhost:7115/swagger for testing endpoints

Project Structure
bash
Copy code
Controllers/          # API controllers for objects and polygons
Models/               # Data models and DTOs
Repositories/         # Repository interfaces and implementations
Services/             # Business logic (optional, can be skipped if using only repositories)
Program.cs            # App startup and DI configuration
API Endpoints
Polygons

GET /api/polygons - Get all polygons

DELETE /api/polygons/{id} - Delete polygon by ID

POST /api/polygons/save - Save multiple polygons

Objects

GET /api/objects - Get all objects

DELETE /api/objects/{id} - Delete object by ID

POST /api/objects/save - Save multiple objects

Notes
The repositories handle database operations using MongoDB.Driver.

Dependency Injection is configured in Program.cs for the repositories.