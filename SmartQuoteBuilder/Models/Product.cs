namespace SmartQuoteBuilder.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }

        public List<ProductOption> Options { get; set; }
    }
}
