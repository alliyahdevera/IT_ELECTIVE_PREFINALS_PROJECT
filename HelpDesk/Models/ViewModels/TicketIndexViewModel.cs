namespace HelpDesk.Models.ViewModels
{
    public class TicketIndexViewModel
    {
        public int Id { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string CustomerCompany { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string PriorityName { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? DueAt { get; set; }
    }
}
