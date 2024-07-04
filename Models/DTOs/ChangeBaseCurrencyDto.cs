namespace wepay.Models.DTOs
{
    public class ChangeBaseCurrencyDto
    {
        public string WalletAddress { get; set; }
        public string CurrencyCodeFrom { get; set; }

        public string CurrencyCodeTo { get; set; }
    }
}
