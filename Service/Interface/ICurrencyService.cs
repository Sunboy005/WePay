using wepay.Models;

namespace wepay.Service.Interface
{
    public interface ICurrencyService
    {
        Task<Currency> ChangeBaseCurrency(string currencyIdFrom,string currencyIdTo);
    }
}
