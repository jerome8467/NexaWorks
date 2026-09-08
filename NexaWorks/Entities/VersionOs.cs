namespace NexaWorks.Entities
{
    public class VersionOs
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; } = false;
        public required int VersionProductId { get; set; }
        public required int SystemOsId { get; set; }

        public VersionProduct VersionProduct { get; set; } = null!;
        public  SystemOs SystemOs { get; set; } = null!;
    }
}
