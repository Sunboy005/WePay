using wepay.Models;

namespace wepay.Repository.Interface
{
    public interface ICurrencyRepository
    {
        Task updateCurrency(Currency currency);
        Task<Currency> getCurrencyById(string id);
        Task deleteCurrency(Currency currency);
    }
}
