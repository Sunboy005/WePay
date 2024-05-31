using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface ICurrencyService
    {
        Task<Currency?> ChangeBaseCurrency(string currencyIdFrom,string currencyIdTo);
        Task<Currency> AddCurrency(CurrencyToAddDto currencyToAddDto);
        Task<Currency> GetCurrencyById(string currencyId);
    }
}
