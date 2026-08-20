using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    [Table("Departments")]
    public class Department
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
        public ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
