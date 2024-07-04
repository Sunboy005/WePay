namespace wepay.Models.DTOs
{
    public class WalletCreationDto
    {
        public string UserId { get; set; }
        public string Pin { get; set; }
        public string CurrencyShortCode { get; set; }
    }
}
