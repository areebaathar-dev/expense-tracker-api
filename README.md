# Expense Tracker API

A RESTful Web API built with **C# and ASP.NET Core 8**, using **Entity Framework Core** for
data access and **Swagger** for interactive API documentation. Tracks expenses grouped by
category — CRUD endpoints plus a summary/reporting endpoint.

This mirrors the PHP/MySQL Expense Management System project, rebuilt as a proper
client-server REST API to demonstrate C#/.NET backend and API development skills.

---

## What this project demonstrates

- RESTful API design (proper HTTP verbs, status codes, resource-based routes)
- Client-server architecture (this API is the server; Swagger UI, Postman, or any future
  frontend acts as the client)
- Entity Framework Core (Code-First, migrations, relationships, LINQ queries)
- DTOs (separating what the database stores from what the API exposes)
- Auto-generated API documentation via Swagger/OpenAPI

---

## Prerequisites

You already have what you need:
- **Visual Studio 2022** (Community edition is fine) with the **ASP.NET and web development**
  workload installed
- **SQL Server LocalDB** — this ships automatically with Visual Studio, no separate install needed

If you'd rather use the command line instead of Visual Studio, install the
[.NET 8 SDK](https://dotnet.microsoft.com/download) first.

---

## Setup — Option A: Visual Studio (recommended for you)

1. Open Visual Studio → **Open a project or solution** → select `ExpenseTrackerAPI.csproj`
   from this folder.
2. Visual Studio will automatically restore the NuGet packages listed in the `.csproj`
   (EF Core, Swashbuckle). If it doesn't, right-click the project in Solution Explorer →
   **Restore NuGet Packages**.
3. Open the **Package Manager Console** (Tools → NuGet Package Manager → Package Manager
   Console) and run:
   ```
   Add-Migration InitialCreate
   ```
   This generates the database schema from the `Expense` and `Category` models.
4. Press **F5** (or click the green "Run" button). This will:
   - Apply the migration automatically (creating `ExpenseTrackerDb` in LocalDB)
   - Launch your browser straight into the Swagger UI

That's it — you should see an interactive page listing every endpoint, and you can click
**"Try it out"** on any of them to send real requests.

## Setup — Option B: Command line

```bash
cd ExpenseTrackerAPI
dotnet restore
dotnet tool install --global dotnet-ef   # only needed once, ever
dotnet ef migrations add InitialCreate
dotnet run
```

Then open the URL shown in the terminal (e.g. `https://localhost:7099`) in your browser.

---

## Testing it

Once running, the Swagger UI at the root URL lets you try every endpoint. Suggested test flow:

1. `GET /api/categories` — confirm the 4 seeded categories (Food, Transport, Utilities,
   Entertainment) come back.
2. `POST /api/expenses` — create an expense:
   ```json
   {
     "title": "Grocery shopping",
     "amount": 3500,
     "categoryId": 1,
     "notes": "Weekly groceries"
   }
   ```
3. `GET /api/expenses` — see it in the list.
4. `GET /api/expenses?categoryId=1` — filter by category.
5. `PUT /api/expenses/1` — update it.
6. `GET /api/expenses/summary` — see totals grouped by category.
7. `DELETE /api/expenses/1` — remove it.

You can also import the API into **Postman** by pointing it at
`https://localhost:7099/swagger/v1/swagger.json` (Postman can auto-generate a full
collection from that).

---

## Project structure

```
ExpenseTrackerAPI/
├── Controllers/
│   ├── ExpensesController.cs    # CRUD + filtering + summary endpoint
│   └── CategoriesController.cs  # lookup endpoint
├── Models/
│   ├── Expense.cs                # EF Core entity
│   └── Category.cs               # EF Core entity
├── DTOs/
│   └── ExpenseDtos.cs            # request/response shapes (never expose entities directly)
├── Data/
│   └── AppDbContext.cs           # EF Core DbContext + seed data
├── Program.cs                    # app startup: services, middleware, Swagger
└── appsettings.json              # connection string
```

---

## Extending it further (optional, if you have time before applying)

- Add a `PATCH` endpoint for partial updates
- Add pagination to `GET /api/expenses` (`?page=1&pageSize=10`)
- Add simple API key authentication middleware
- Deploy it for free on **Render** or **Azure App Service (free tier)** so you can link a
  live Swagger URL on your resume/GitHub, not just source code

---

## Resume bullet suggestions (once you've run it yourself and understand it)

- Built a RESTful Expense Tracker API in C# and ASP.NET Core with full CRUD endpoints,
  Entity Framework Core data access, and Swagger-generated API documentation.
- Designed a client-server architecture with DTO-based request/response contracts,
  relational data modeling, and category-based filtering and reporting endpoints.
