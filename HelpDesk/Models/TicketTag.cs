using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    [Table("TicketTags")]
    public class TicketTag
    {
        public int TicketId { get; set; }
        public int TagId { get; set; }

        [ForeignKey(nameof(TicketId))] public Ticket Ticket { get; set; } = null!;
        [ForeignKey(nameof(TagId))] public Tag Tag { get; set; } = null!;
    }
}