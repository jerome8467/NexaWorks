namespace NexaWorks.Entities
{
    public class SystemOs
    {
        public int Id { get; set; }
        public required string NameSystem { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
