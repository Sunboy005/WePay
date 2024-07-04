namespace wepay.Models.DTOs
{
    public class ChangeWalletPinDto
    {
        public string UserEmail {  get; set; }
        public string Address { get; set; }   
        public string Code { get; set; }
        public string Pin { get; set; }
    }
}
