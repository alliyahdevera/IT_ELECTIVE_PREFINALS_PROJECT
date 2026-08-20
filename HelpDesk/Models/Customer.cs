using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    [Table("Customers")]
    public class Customer
    {
        [Key] public int Id { get; set; }
        [Required] public string CompanyName { get; set; } = string.Empty;
        [Required] public string ContactName { get; set; } = string.Empty;
        [Required] public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        [Required] public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
