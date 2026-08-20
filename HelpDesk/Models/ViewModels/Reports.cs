namespace HelpDesk.Models.ViewModels
{
    public class EmployeeWorkloadViewModel
    {
        public string EmployeeName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public int UnresolvedTicketCount { get; set; }
    }

    public class DepartmentWorkloadViewModel
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int EmployeeCount { get; set; }
        public int UnresolvedTicketCount { get; set; }
    }

    public class UnassignedTicketViewModel
    {
        public int TicketId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string CustomerCompany { get; set; } = string.Empty;
        public string PriorityName { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class MultiAssigneeTicketViewModel
    {
        public int TicketId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public int ActiveAssigneeCount { get; set; }
        public string Assignees { get; set; } = string.Empty;
    }

    public class PrimaryAssigneeViewModel
    {
        public int TicketId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string PrimaryAssigneeName { get; set; } = "Unassigned";
    }

    public class CategoryHierarchyViewModel
    {
        public string CategoryName { get; set; } = string.Empty;
        public string ParentCategoryName { get; set; } = "(Root)";
    }
}