# 💰 Expense Tracker REST API

A RESTful Web API for tracking personal expenses by category — built with **C# and ASP.NET Core 8**, using **Entity Framework Core** for data access and **Swagger/OpenAPI** for interactive, auto-generated API documentation.

This project demonstrates a full client-server architecture: proper REST conventions, relational data modeling, DTO-based contracts, and documented, testable endpoints.

![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET%208-512BD4?style=flat&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-5C2D91?style=flat&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat&logo=microsoft-sql-server&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/EF%20Core-512BD4?style=flat)
![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=flat&logo=swagger&logoColor=black)

---

## 📖 Overview

Expense Tracker REST API lets a client (Swagger UI, Postman, or any future frontend) create, read, update, and delete expenses, group them by category, and pull category-wise spending summaries — all through a clean, documented HTTP API rather than a server-rendered page.

It's a from-scratch rebuild of an earlier PHP/MySQL expense tracker, this time as a proper client-server REST API using the C#/.NET stack.

---

## ✨ Features

- Full CRUD for expenses (`GET`, `POST`, `PUT`, `DELETE`)
- Filter expenses by category
- Category-wise spending summary endpoint (totals + counts)
- Relational data model with EF Core Code-First migrations
- DTOs separating internal data models from the public API contract
- Interactive API documentation via Swagger/OpenAPI — every endpoint is testable straight from the browser
- Proper REST status codes (`200`, `201`, `204`, `400`, `404`) instead of always returning `200`

---

## 🛠️ Tech Stack

| Layer            | Technology                          |
|-------------------|--------------------------------------|
| Language          | C#                                   |
| Framework         | ASP.NET Core 8 (Web API)             |
| ORM               | Entity Framework Core (Code-First)   |
| Database          | SQL Server (LocalDB for development) |
| API Documentation | Swagger / OpenAPI (Swashbuckle)      |
| Architecture      | RESTful client-server                |

---

## 📡 API Endpoints

| Method | Endpoint                          | Description                                  |
|--------|------------------------------------|-----------------------------------------------|
| GET    | `/api/expenses`                    | Get all expenses (optional `?categoryId=`)   |
| GET    | `/api/expenses/{id}`               | Get a single expense by ID                   |
| POST   | `/api/expenses`                    | Create a new expense                         |
| PUT    | `/api/expenses/{id}`               | Update an existing expense                   |
| DELETE | `/api/expenses/{id}`               | Delete an expense                            |
| GET    | `/api/expenses/summary`            | Get total spending grouped by category       |
| GET    | `/api/categories`                  | Get all expense categories                   |

Full interactive documentation is available via Swagger UI once the project is running (see below).

---

## 🚀 Getting Started

### Prerequisites
- Visual Studio 2022 (Community edition is fine) with the **ASP.NET and web development** workload, **or** the [.NET 8 SDK](https://dotnet.microsoft.com/download) for CLI use
- SQL Server LocalDB (ships automatically with Visual Studio)

### Run with Visual Studio
1. Clone the repo and open `ExpenseTrackerAPI.csproj`
2. Open **Package Manager Console** → run:
   ```
   Add-Migration InitialCreate
   ```
3. Press **F5** — this applies the migration, starts the app, and opens Swagger UI automatically

### Run with the CLI
```bash
git clone https://github.com/areebaathar-dev/expense-tracker-api.git
cd expense-tracker-api
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet run
```
Then open the URL shown in the terminal (e.g. `https://localhost:7099`).

---

## 🧪 Testing

Try it directly from the Swagger UI, or with a sample request:

```json
POST /api/expenses
{
  "title": "Grocery shopping",
  "amount": 3500,
  "categoryId": 1,
  "notes": "Weekly groceries"
}
```

The API ships with four seeded categories: **Food, Transport, Utilities, Entertainment**.

---

## 📁 Project Structure

```
ExpenseTrackerAPI/
├── Controllers/     # API endpoints (ExpensesController, CategoriesController)
├── Models/          # EF Core entities (Expense, Category)
├── DTOs/            # Request/response contracts
├── Data/            # EF Core DbContext + seed data
├── Program.cs       # App startup, middleware, Swagger config
└── appsettings.json # Configuration
```

---

## 🔭 Possible Future Improvements

- Pagination on `GET /api/expenses`
- API key or JWT-based authentication
- Deployment to Azure App Service / Render for a live demo link

---

## 👩‍💻 Author

**Areeba Athar**
[LinkedIn](https://linkedin.com/in/areeba-athar) · [GitHub](https://github.com/areebaathar-dev)
