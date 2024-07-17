namespace wepay.Models.DTOs
{
    public class TransferDto
    {
        public string WalletAddressFrom { get; set; }
        public string WalletAddressTo { get; set; }
        public string CurrencyFromShortCode { get; set; }
        public string CurrencyToShortCode { get; set; }
        public double Amount { get; set; }
        public string WalletPin { get; set; }
    }
}
