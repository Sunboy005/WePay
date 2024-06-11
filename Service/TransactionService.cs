using AutoMapper;
using Microsoft.AspNetCore.Identity;
using wepay.Models;
using wepay.Models.DTOs;
using wepay.Repository.Interface;
using wepay.Service.Interface;

namespace wepay.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public TransactionService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<List<Transaction>> GetTransactionsByWalletAddress(string address)
        {
            return await _repositoryManager.TransactionRepository.GetTransactionsByWalletAddress(address);
        }
    }
}
