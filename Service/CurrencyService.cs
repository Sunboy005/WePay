using AutoMapper;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Repository.Interface;
using wepay.Service.Interface;

namespace wepay.Service
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public CurrencyService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        

        public async Task<Currency?> ChangeBaseCurrency(string currencyIdFrom,string currencyIdTo)
        {
                var previousbase = await _repositoryManager.CurrencyRepository.getCurrencyById(currencyIdFrom);
                if (previousbase == null)
                {
                    return null;
                }        
                    
                var newbase = await _repositoryManager.CurrencyRepository.getCurrencyById(currencyIdTo);
                if (newbase == null)
                {
                return null;
                }

                previousbase.IsBase = false;
                await _repositoryManager.CurrencyRepository.updateCurrency(previousbase);
                newbase.IsBase = true;
                await  _repositoryManager.CurrencyRepository.updateCurrency(newbase);
                return newbase;
        }
    }
}
