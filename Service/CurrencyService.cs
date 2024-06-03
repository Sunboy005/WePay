using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

        public async Task<Currency> AddCurrency(CurrencyToAddDto currencyToAddDto)
        {

            var currency = _mapper.Map<Currency>(currencyToAddDto);
            await _repositoryManager.CurrencyRepository.AddCurrency(currency);
            return currency;
        }

        public async Task<Currency> GetCurrencyById(string currencyId)
        {
            return await _repositoryManager.CurrencyRepository.getCurrencyById(currencyId);

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

        public async Task<bool> DeleteCurrency(CurrencyDeletionDto currencyDeletionDto)
        {
            var currency = await _repositoryManager.CurrencyRepository.getCurrencyById(currencyDeletionDto.CurencyId);
            if (currency == null)
            {
                return false;
            }

            await _repositoryManager.CurrencyRepository.deleteCurrency(currency);
            return true;
        }

        public async Task<List<Currency>>? GetCurrencyListByWalletId(string walletId)
        {
            return await _repositoryManager.CurrencyRepository.getCurrencyListByWalletId(walletId);
        }
    }
}
