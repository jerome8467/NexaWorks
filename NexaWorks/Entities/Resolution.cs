namespace NexaWorks.Entities
{
    public class Resolution
    {
        public int Id { get; set; }
        public DateOnly ResolutionDate { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; } = false;
        public required int TicketId { get; set; }
    }
}
