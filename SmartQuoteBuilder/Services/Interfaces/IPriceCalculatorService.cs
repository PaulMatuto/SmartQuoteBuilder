namespace SmartQuoteBuilder.Services.Interfaces
{
    public interface IPriceCalculatorService
    {
        Task<decimal> CalculateTotalPriceAsync(int productId, List<int> selectedOptionIds);
    }
}
