namespace NexaWorks.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }
        public DateOnly CreationDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductVersion {  get; set; } = string.Empty;
        public string SystemOsName { get; set; } = string.Empty;
        public string StatusTitle { get; set; } = string.Empty;
        public DateOnly? ResolutionDate { get; set; }
        public string? Resolution {  get; set; }
    }
}
