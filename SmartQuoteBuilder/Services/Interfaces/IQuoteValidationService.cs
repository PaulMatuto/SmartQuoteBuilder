namespace SmartQuoteBuilder.Services.Interfaces
{
    public interface IQuoteValidationService
    {
        Task ValidateQuoteRequestAsync(int productId, List<int> optionIds);
    }
}
