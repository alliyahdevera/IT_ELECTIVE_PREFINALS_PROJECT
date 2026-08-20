using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    [Table("Teams")]
    public class Team
    {
        [Key] public int Id { get; set; }
        [Required] public int DepartmentId { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department Department { get; set; } = null!;

        public ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
    }
}
