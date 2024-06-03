using wepay.Models;
using wepay.Models.DTOs;

namespace wepay.Service.Interface
{
    public interface ICurrencyService
    {
        Task<Currency> ChangeBaseCurrency(string currencyIdFrom,string currencyIdTo);
        Task<bool> DeleteCurrency(CurrencyDeletionDto currencyDeletionDto);
    }
}
