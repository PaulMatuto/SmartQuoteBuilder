using Microsoft.EntityFrameworkCore.Metadata.Conventions;

using System;
using System.ComponentModel.DataAnnotations;

namespace SmartQuoteBuilder.Models
{
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }
        public int ProductId { get; set; }
        public string SelectedOptionIds { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
