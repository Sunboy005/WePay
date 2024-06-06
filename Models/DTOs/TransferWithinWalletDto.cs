namespace wepay.Models.DTOs
{
    public class TransferWithinWalletDto
    {
        public string WalletAddress { get; set; }
        public string CurrencyFromShortCode { get; set; }
        public string CurrencyToShortCode { get; set;}

        public int Amount { get; set; }
    }
}
