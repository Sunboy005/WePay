using wepay.Repository.Interface;

namespace wepay.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoriesContext _repositoriesContext;
        private readonly Lazy<IWalletRepository> _walletRepository;
        private readonly Lazy<IOtpRepository> _otpRepository;   
        public RepositoryManager(RepositoriesContext repositoriesContext)
        {
            _repositoriesContext = repositoriesContext;
            _walletRepository = new Lazy<IWalletRepository> (() => new WalletRepository(repositoriesContext));
            _otpRepository   = new Lazy<IOtpRepository> (() => new OtpRepository(repositoriesContext));
        }

        public IWalletRepository WalletRepository { get { return _walletRepository.Value; } }

        public IOtpRepository OtpRepository { get { return _otpRepository.Value; } }
    }
}
