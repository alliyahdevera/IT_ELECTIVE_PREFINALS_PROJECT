using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    [Table("Employees")]
    public class Employee
    {
        [Key] public int Id { get; set; }
        [Required] public int DepartmentId { get; set; }
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required] public string Email { get; set; } = string.Empty;
        [Required] public string JobTitle { get; set; } = string.Empty;
        [Required] public DateTime HireDate { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; } = null!;

        public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
        public ICollection<TicketAssignment> TicketAssignments { get; set; } = new List<TicketAssignment>();
        public ICollection<TicketComment> TicketComments { get; set; } = new List<TicketComment>();
    }
}
