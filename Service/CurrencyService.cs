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

        public async Task<bool> DeleteCurrency(CurrencyDeletionDto currencyDeletionDto)
        {
            var currency = await _repositoryManager.CurrencyRepository.getCurrencyById(currencyDeletionDto.CurrencyId);
            if (currency == null)
            {
                return false;
            }

            await _repositoryManager.CurrencyRepository.deleteCurrency(currency);
            return true;
        }

        public async Task<Currency?> getCurrencyByShortCode(string shortCode)
        {
            return await _repositoryManager.CurrencyRepository.getCurrencyByShortCode(shortCode);
        }
    }
}
