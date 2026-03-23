namespace WordInverterService.Models
{
    public class WordInversionRecord
    {
        public int Id { get; set; }
        public string OriginalRequest { get; set; } = string.Empty;
        public string InvertedResult { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
