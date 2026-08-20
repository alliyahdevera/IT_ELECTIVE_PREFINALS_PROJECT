using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDesk.Models
{
    [Table("TicketAttachments")]
    public class TicketAttachment
    {
        [Key] public int Id { get; set; }
        [Required] public int TicketId { get; set; }
        [Required] public string FileName { get; set; } = string.Empty;
        [Required] public string ContentType { get; set; } = string.Empty;
        [Required] public long FileSize { get; set; }
        [Required] public DateTime UploadedAt { get; set; }

        [ForeignKey(nameof(TicketId))] public Ticket Ticket { get; set; } = null!;
    }
}