using AutoMapper;
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
    }
}
