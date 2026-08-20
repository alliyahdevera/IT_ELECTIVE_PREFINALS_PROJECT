# Database Documentation — lycevm.db

## Tables and Primary Keys
- Customers (Id)
- Departments (Id)
- Employees (Id)
- Teams (Id)
- TeamMembers (TeamId, EmployeeId) — composite
- Tags (Id)
- TicketCategories (Id)
- TicketPriorities (Id)
- TicketStatuses (Id)
- Tickets (Id)
- TicketAssignments (TicketId, EmployeeId) — composite
- TicketComments (Id)
- TicketAttachments (Id)
- TicketTags (TicketId, TagId) — composite

## Foreign Keys
- Employees.DepartmentId → Departments.Id
- Teams.DepartmentId → Departments.Id
- TeamMembers.TeamId → Teams.Id
- TeamMembers.EmployeeId → Employees.Id
- Tickets.CustomerId → Customers.Id
- Tickets.CategoryId → TicketCategories.Id
- Tickets.PriorityId → TicketPriorities.Id
- Tickets.StatusId → TicketStatuses.Id
- TicketAssignments.TicketId → Tickets.Id
- TicketAssignments.EmployeeId → Employees.Id
- TicketComments.TicketId → Tickets.Id
- TicketComments.EmployeeId → Employees.Id (nullable)
- TicketAttachments.TicketId → Tickets.Id
- TicketTags.TicketId → Tickets.Id
- TicketTags.TagId → Tags.Id
- TicketCategories.ParentCategoryId → TicketCategories.Id (self-referencing, nullable)

## One-to-Many Relationships
- Department → Employees
- Department → Teams
- Customer → Tickets
- TicketCategory → Tickets
- TicketPriority → Tickets
- TicketStatus → Tickets
- Ticket → TicketComments
- Ticket → TicketAttachments

## Many-to-Many Relationships
- Teams ↔ Employees, via TeamMembers (extra column: JoinedAt)
- Tickets ↔ Employees, via TicketAssignments (extra columns: AssignedAt, UnassignedAt, IsPrimary)
- Tickets ↔ Tags, via TicketTags (no extra columns)

## Self-Referencing Relationship
- TicketCategories.ParentCategoryId references TicketCategories.Id, forming a category tree. Root categories have ParentCategoryId = NULL.

## Optional (Nullable) Relationships
- TicketCategories.ParentCategoryId (nullable — root categories)
- TicketComments.EmployeeId (nullable — system-generated comments)
- TicketAssignments.UnassignedAt (nullable — currently active assignment)
- Tickets.DueAt, Tickets.ResolvedAt, Tickets.ClosedAt (nullable)
- Customers.Phone (nullable)
- Departments.Description, Teams.Description, TicketCategories.Description (nullable)

## Composite Primary Keys
- TeamMembers (TeamId, EmployeeId)
- TicketAssignments (TicketId, EmployeeId)
- TicketTags (TicketId, TagId)

## Unique Constraints (discovered via indexes)
- Departments.Name
- Employees.Email
- Tags.Name
- TicketPriorities.Name
- TicketStatuses.Name
- Teams (DepartmentId, Name)

## Row Counts (at time of inspection)
Customers 20, Departments 7, Employees 17, Tags 20, TeamMembers 17, Teams 8, TicketAssignments 42, TicketAttachments 8, TicketCategories 17, TicketComments 23, TicketPriorities 4, TicketStatuses 5, TicketTags 46, Tickets 30