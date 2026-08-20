using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    [Table("TicketPriorities")]
    public class TicketPriority
    {
        [Key] public int Id { get; set; }
        [Required] public string Name { get; set; } = string.Empty;
        public int SortOrder { get; set; }
        public int ResponseHours { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}