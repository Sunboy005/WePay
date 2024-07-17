using Entities.Exceptions;
using RestSharp;
using Newtonsoft.Json.Linq;



namespace wepay.Utils
{
    public class CurrencyConverter
    {
        private static readonly string baseUrl = "https://v6.exchangerate-api.com/v6/";
        private static readonly string apiKey = "0f821c9a6567f71b3b18930f";

        public static async Task<(double, double)> GetRate(string codeFrom, string codeTo, double amount)
        {
            var options = new RestClientOptions(baseUrl);
            var client = new RestClient(options);
            var request = new RestRequest($"{apiKey}/pair/{codeFrom}/{codeTo}/{amount}");
        
            var response = await client.GetAsync(request);
            if(response.IsSuccessful == false) {
                throw new InternalServerErrorException("An error occurred");
            }            
            JObject joResponse = JObject.Parse(response.Content);
            var conversionRate = (double)joResponse["conversion_rate"];
            var newAmount = (double) joResponse["conversion_result"];
            return (newAmount, conversionRate);

        }
    }
}