using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface ICurrencyService
    {
        Task<Currency> AddCurrency(CurrencyToAddDto currencyToAddDto);
        Task<Currency> GetCurrencyById(string currencyId);
        Task<bool> DeleteCurrency(CurrencyDeletionDto currencyDeletionDto);

    }
}
