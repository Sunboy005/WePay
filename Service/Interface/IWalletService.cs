namespace wepay.Service.Interface
{
    public interface IWalletService
    {
        Task LockWallet(String id);
    }
}
