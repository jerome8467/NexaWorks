using System.ComponentModel.DataAnnotations;

namespace NexaWorks.Entities
{
    public class StatusTicket
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
