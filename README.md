# Map REST API

A backend RESTful API for managing map objects and polygons using **ASP.NET Core** and **MongoDB**.

---

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [API Endpoints](#api-endpoints)
- [Project Structure](#project-structure)
- [License](#license)

---

## Features

- CRUD operations for **Map Objects** and **Polygons**
- Bulk insert for objects and polygons
- Error handling with proper HTTP response codes
- CORS enabled for React frontend
- Swagger/OpenAPI documentation

---

## Tech Stack

- **Backend:** ASP.NET Core 7
- **Database:** MongoDB
- **Language:** C#
- **ORM/Driver:** MongoDB.Driver
- **API Documentation:** Swagger

---

## Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download)
- [MongoDB](https://www.mongodb.com/try/download/community)
- (Optional) Docker for running MongoDB in a container

### Run Locally

1. Clone the repository:

```bash
git clone https://github.com/yourusername/MapRestApi.git
cd MapRestApi
Set environment variables (or update launchSettings.json):

bash
Copy code
MONGODB_URI=mongodb://localhost:27017
Restore dependencies:

bash
Copy code
dotnet restore
Run the project:

bash
Copy code
dotnet run
The API will be available at: https://localhost:7115

Configuration
MongoDB Connection: Set via environment variable MONGODB_URI.

Database Name: MapDb

Collections: Objects, Polygons

CORS: Configured for http://localhost:3000 (React frontend)

API Endpoints
Objects
Method	Endpoint	Description
GET	/api/objects	Get all objects
POST	/api/objects/save	Save multiple objects in bulk
DELETE	/api/objects/{id}	Delete object by ID

Polygons
Method	Endpoint	Description
GET	/api/polygons	Get all polygons
POST	/api/polygons/save	Save multiple polygons in bulk
DELETE	/api/polygons/{id}	Delete polygon by ID
DELETE	/api/polygons	Delete all polygons

Project Structure
graphql
Copy code
MapRestApi/
├─ Controllers/        # API controllers
│  ├─ ObjectController.cs
│  └─ PolygonController.cs
├─ Models/             # DTOs and database models
├─ Repositories/       # MongoDB repository classes
│  ├─ Interfaces/
│  └─ Implementations
├─ Services/           # Optional business logic / MongoService
├─ Program.cs          # App configuration and DI
├─ appsettings.json
└─ launchSettings.json
