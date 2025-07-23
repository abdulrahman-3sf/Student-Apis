# Student Management API

A .NET 8 Web API for managing student records with SQL Server integration. This project demonstrates a multi-layered architecture including a backend API and a console client that communicates via HTTP.

## Features

- Create, read, update, and delete student records
- SQL Server backend using stored procedures
- ASP.NET Core Web API project (`StudentAPI`)
- Console client application (`StudentAPIClient`) using `HttpClient`
- Separation of concerns using:
  - Controllers
  - Business Layer
  - Data Access Layer (ADO.NET)

---

## Project Structure
```
/StudentAPI → ASP.NET Core Web API
└── Controllers → Handles HTTP requests
└── BusinessLayer → Business logic
└── DataAccessLayer → SQL communication via stored procedures

/StudentAPIClient → Console app to interact with the API
```

---

## Technologies Used

- .NET 8
- ASP.NET Core Web API
- C# Console App
- SQL Server
- ADO.NET (SqlClient)
- Stored Procedures
- RESTful API
- JSON serialization

---

## Database

Database: `StudentsDB`

**Stored Procedures:**
- `SP_GetAllStudents`
- `SP_GetStudentById`
- `SP_AddStudent`
- `SP_UpdateStudent`
- `SP_DeleteStudent`

Each procedure handles one of the basic CRUD operations on the `Students` table.
