using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    [Table("TicketAssignments")]
    public class TicketAssignment
    {
        public int TicketId { get; set; }
        public int EmployeeId { get; set; }
        [Required] public DateTime AssignedAt { get; set; }
        public DateTime? UnassignedAt { get; set; }
        public bool IsPrimary { get; set; }

        [ForeignKey(nameof(TicketId))] public Ticket Ticket { get; set; } = null!;
        [ForeignKey(nameof(EmployeeId))] public Employee Employee { get; set; } = null!;
    }
}