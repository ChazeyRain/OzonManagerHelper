using System.Text.Json;
using Microsoft.Extensions.Logging;
using Ozon.API.Seller.Methods;

namespace Ozon.API.Seller
{
    public class RequestMethods
    {
        private static string path = "https://api-seller.ozon.ru";


        public static Methods.Finance.Transaction.List? GetTransactionList(Credentials credentials, Methods.Finance.Transaction.ListRequest request)
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();

            requestMessage.RequestUri = new Uri(path + "/v3/finance/transaction/list");
            requestMessage.Method = HttpMethod.Post;
            requestMessage.Headers.Add("Client-id", credentials.ClientId);
            requestMessage.Headers.Add("Api-Key", credentials.APIkey);

            requestMessage.Content = new StringContent(JsonSerializer.Serialize<Methods.Finance.Transaction.ListRequest>(request));

            Config.logger.LogInformation(JsonSerializer.Serialize<Methods.Finance.Transaction.ListRequest>(request));

            var response = httpClient.SendAsync(requestMessage);

            if (!response.Result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.Result.Content.ReadAsStringAsync().Result);
            }

            Config.logger.LogInformation(response.Result.Content.ReadAsStringAsync().Result);

            return JsonSerializer.Deserialize<Methods.Finance.Transaction.List>(response.Result.Content.ReadAsStringAsync().Result);
        }

        //public static Methods.Product.Info.Prices GetPrices(Credentials credentials, Methods.Product.Info.PricesRequest request)


        public static TResponse Post<TRequest, TResponse>(Credentials credentials, TRequest request) where TRequest : GenericRequest
        {
            HttpClient httpClient = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage();

            requestMessage.RequestUri = new Uri(RequestMethods.path + request.path);
            requestMessage.Method = HttpMethod.Post;
            requestMessage.Headers.Add("Client-id", credentials.ClientId);
            requestMessage.Headers.Add("Api-Key", credentials.APIkey);

            requestMessage.Content = new StringContent(JsonSerializer.Serialize<TRequest>(request));

            var response = httpClient.SendAsync(requestMessage);

            if (!response.Result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(response.Result.Content.ReadAsStringAsync().Result);
            }

            Config.logger.LogInformation(response.Result.Content.ReadAsStringAsync().Result);
            
            //File.AppendAllText("poop.json", response.Result.Content.ReadAsStringAsync().Result);

            return JsonSerializer.Deserialize<TResponse>(response.Result.Content.ReadAsStringAsync().Result, Config.JsonOptions);
        }
    }
}