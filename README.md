# HelpDesk

An ASP.NET Core MVC application built with Entity Framework Core (Database-First) against an existing SQLite database (`lycevm.db`).

## .NET Version
.NET 10.0

## EF Core Version
Entity Framework Core 10.0.11

## NuGet Packages
- Microsoft.EntityFrameworkCore.Sqlite (10.0.11)

## Database Location
`lycevm.db` — located in the project root directory. It is copied to the build output automatically via the `<CopyToOutputDirectory>` setting in the `.csproj` file. The database is provided as-is and is never modified, migrated, or seeded by the application.

## How to Run
1. Ensure `lycevm.db` is in the project root.
2. Restore packages: dotnet restore
3. Run the application: dotnet run or F5 
4. Open the URL shown in the console (e.g. `https://localhost:7059`).

## Notes
- The database is not modified in any way: no migrations, no scaffolding, no seed data.
- All entity classes and the DbContext were written manually based on inspection of the existing schema.
- See `DATABASE.md` for the full schema investigation (tables, keys, relationships).

## Features
- Browse Departments, Employees, Teams, Customers, and Tickets
- Detailed Ticket view (assignments, comments, tags, attachments)
- Reports: Employee Workload, Department Workload, Unassigned Tickets, Multiple-Assignee Tickets, Primary Assignee, Category Hierarchy
