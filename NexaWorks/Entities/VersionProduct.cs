namespace NexaWorks.Entities
{
    public class VersionProduct
    {
        public int Id { get; set; }
        public required string RefVersion { get; set; }
        public bool IsDeleted { get; set; } = false;
        public required int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
