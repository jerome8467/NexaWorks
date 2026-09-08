namespace NexaWorks.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public required string NameProduct { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
