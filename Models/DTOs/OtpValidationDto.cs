namespace wepay.Models.DTOs
{
    public class OtpValidationDto
    {
        public string Code { get; set; }
        public string Reason { get; set; }
        public string UserEmail { get; set; }
    }
}
