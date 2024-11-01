using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Ozon.API.Performance
{
    public class RequsetMethods
    {
        private static string host = "https://api-performance.ozon.ru";

        public static Token GetToken(Credentials credentials)
        {   
            HttpClient httpClient = new HttpClient();
            string body = JsonSerializer.Serialize(credentials);

            var content = new StringContent(body);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            
            var response = httpClient.PostAsync(host + "/api/client/token",content);
            var stringResponse = response.Result.Content.ReadAsStringAsync().Result;

            if(!response.Result.IsSuccessStatusCode)
            {
                Config.logger.LogError(stringResponse);
            } 
            else
            {
                Config.logger.LogInformation("Credentials recieved");
                Config.logger.LogInformation(stringResponse);
            }

            var token = JsonSerializer.Deserialize<Token>(stringResponse, Config.JsonOptions);

            token.credentials = credentials;

            return token;
        }

        public static Methods.Client.Campaigns? GetCampaignList(Token token)
        {
            var httpClient = GetAuthoirizedClient(token);

            var response = httpClient.GetAsync(host + "/api/client/campaign");
            var stringResponse = response.Result.Content.ReadAsStringAsync().Result;

            if(!response.Result.IsSuccessStatusCode)
            {
                Config.logger.LogError(stringResponse);
            } 
            else
            {
                Config.logger.LogInformation("Campaign list recieved");    
                Config.logger.LogInformation(stringResponse);
            }

            return JsonSerializer.Deserialize<Methods.Client.Campaigns>(stringResponse, Config.JsonOptions);
        }

        public static Methods.Client.Campaigns? GetCampaignList(Token token, string advObjectType)
        {
            var httpClient = GetAuthoirizedClient(token);

            var response = httpClient.GetAsync(host + "/api/client/campaign?advObjectType=" + advObjectType);
            
            var stringResponse = response.Result.Content.ReadAsStringAsync().Result;

            if(!response.Result.IsSuccessStatusCode)
            {
                Config.logger.LogError(stringResponse);
            } 
            else
            {
                Config.logger.LogInformation("Campaign list recieved");
                Config.logger.LogInformation(stringResponse);
            }

            return JsonSerializer.Deserialize<Methods.Client.Campaigns>(stringResponse);
        }

        public static Methods.Client.ReportNumber? GetCampaignStaticstics(Token token, Methods.Client.Staticstics campaignStaticstics)
        {
            var httpClient = GetAuthoirizedClient(token);

            string body = JsonSerializer.Serialize(campaignStaticstics);

            var content = new StringContent(body);
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var response = httpClient.PostAsync(host + "/api/client/statistics/json", content);
            var stringResponse = response.Result.Content.ReadAsStringAsync().Result;

            if(!response.Result.IsSuccessStatusCode)
            {
                Config.logger.LogError(stringResponse);
            } 
            else
            {
                Config.logger.LogInformation(stringResponse);
            }
            

            return JsonSerializer.Deserialize<Methods.Client.ReportNumber>(stringResponse, Config.JsonOptions);
        }

        public static Methods.ReportStatus? GetReportState(Token token, Methods.Client.ReportNumber reportNumber)
        {
            var httpClient = GetAuthoirizedClient(token);

            var response = httpClient.GetAsync(host + "/api/client/statistics/" + reportNumber.UUID);

            var stringResponse = response.Result.Content.ReadAsStringAsync().Result;

            if(!response.Result.IsSuccessStatusCode)
            {
                Config.logger.LogError(stringResponse);
            } 
            else
            {
                Config.logger.LogInformation(stringResponse);
            }

            Methods.ReportStatus? status = JsonSerializer.Deserialize<Methods.ReportStatus>(stringResponse, Config.JsonOptions);

            return status;
        }

        public static string GetReportJson(Token token, Methods.Client.ReportNumber reportNumber)
        {
            var httpClient = GetAuthoirizedClient(token);

            var response = httpClient.GetAsync(host + "/api/client/statistics/report?UUID=" + reportNumber.UUID);

            var stringResponse = response.Result.Content.ReadAsStringAsync().Result;

            if(!response.Result.IsSuccessStatusCode)
            {
                Config.logger.LogError("Something went wrong when retrieving the report");
                Config.logger.LogError(stringResponse);
            } 
            else
            {
                Config.logger.LogInformation("Report recieved");    
                Config.logger.LogInformation(stringResponse);
            }

            return stringResponse;
        }

        private static HttpClient GetAuthoirizedClient(Token token)
        {
            HttpClient httpClient = new HttpClient();
        
            if (DateTime.Now > token.getExpirationTime())
            {
                token.update(GetToken(token.credentials));
            }
            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.token_type, token.access_token);
            return httpClient;
        }
    }
}