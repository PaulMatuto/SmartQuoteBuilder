using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SmartQuoteBuilder.Models
{
    public class QuoteRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "At least one option must be selected.")]
        public List<int> OptionIds { get; set; }
    }
}
