namespace wepay.Utils
{
    public class OtpCodeGenerator
    {
        private static readonly Random _rdm = new Random();
        public static string GenerateOtp(int digits)
        {
            if (digits <= 1) return "";

            var _min = (int)Math.Pow(10, digits - 1);
            var _max = (int)Math.Pow(10, digits) - 1;
            return _rdm.Next(_min, _max).ToString();
        }
    }
}
