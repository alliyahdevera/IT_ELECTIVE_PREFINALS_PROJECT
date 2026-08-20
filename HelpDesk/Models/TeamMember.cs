using HelpDesk.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    [Table("TeamMembers")]
    public class TeamMember
    {
        public int TeamId { get; set; }
        public int EmployeeId { get; set; }
        [Required] public DateTime JoinedAt { get; set; }

        [ForeignKey(nameof(TeamId))]
        public Team Team { get; set; } = null!;

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; } = null!;
    }
}