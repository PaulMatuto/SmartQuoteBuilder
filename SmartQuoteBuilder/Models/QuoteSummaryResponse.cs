namespace SmartQuoteBuilder.Models
{
    public class QuoteSummaryResponse
    {
        public int QuoteId { get; set; }
        public string ProductName { get; set; }
        public List<string> SelectedOptionNames { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
