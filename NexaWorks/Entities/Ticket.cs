namespace NexaWorks.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateOnly CreationDate { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public required int StatusTicketId { get; set; }  
        public StatusTicket StatusTicket { get; set; } = null!;
        public required int VersionOsId { get; set; }
        public VersionOs VersionOs { get; set; } = null!;
        public Resolution? Resolution { get; set; }
    }
}
