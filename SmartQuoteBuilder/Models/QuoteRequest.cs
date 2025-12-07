using System.Collections.Generic;

namespace SmartQuoteBuilder.Models
{
    public class QuoteRequest
    {
        public int ProductId { get; set; }
        public List<int> OptionIds { get; set; }
    }
}
