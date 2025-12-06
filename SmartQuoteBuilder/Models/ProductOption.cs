using System.ComponentModel.DataAnnotations;

namespace SmartQuoteBuilder.Models
{
    public class ProductOption
    {
        [Key]
        public int OptionId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public decimal AdditionalPrice { get; set; }
    }
}
