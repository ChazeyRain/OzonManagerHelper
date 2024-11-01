using System.Text.Json;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Ozon.API.Performance.Methods.Client;

namespace Engine
{
    public class PerformanceApiReport
    {
        public static Ozon.API.Performance.Token GetToken(Ozon.API.Performance.Credentials credentials)
        {
            var token = Ozon.API.Performance.RequsetMethods.GetToken(credentials);
            
            if (token == null)
            {
                throw new Exception("Couldn't retrieve the token");
            }

            return token;
        }

        public static Campaigns GetCampaignList(Ozon.API.Performance.Token token, string campaignType)
        {
            var result = Ozon.API.Performance.RequsetMethods.GetCampaignList(token, campaignType);
            
            if (result == null)
            {
                throw new Exception("Couldn't retrieve the campaign list");
            }

            return result;
        }

        public static List<string> GetReports(Ozon.API.Performance.Token token, Campaigns campaignList, DateTime startDate, DateTime endDate)
        {
            var reports = new List<string>();
            int max_report_count = 10;
            
            //string[] campaignIDs = new string[max_report_count];
            List<string> campaignIDs = new List<string>();
           
            for (int i = 0; i <= campaignList.list.Count; i++)
            {
                if ((i != 0 && i % max_report_count == 0) || i == campaignList.list.Count)
                {
                    var reportNumber = GetReportNumber(token, campaignIDs, startDate, endDate);
                    WaitForReport(token, reportNumber);

                    reports.Add(Ozon.API.Performance.RequsetMethods.GetReportJson(token, reportNumber));

                    campaignIDs = new List<string>();

                    if(i == campaignList.list.Count) {continue;}
                }

                campaignIDs.Add(campaignList.list[i].id);
            }

            return reports;
        }


        private static ReportNumber GetReportNumber(Ozon.API.Performance.Token token, IEnumerable<string> campaignIDs, DateTime startDate, DateTime endDate)
        {
            var c_stat_request = new Staticstics(campaignIDs, startDate, endDate, Staticstics.GroupBy.DATE);
            var reportNumber = Ozon.API.Performance.RequsetMethods.GetCampaignStaticstics(token, c_stat_request);

            if (reportNumber.error != null)
            {
                throw new HttpRequestException(reportNumber.error);
            }

            return reportNumber;
        }


        private static void WaitForReport(Ozon.API.Performance.Token token, ReportNumber reportNumber) {
            var state = Ozon.API.Performance.RequsetMethods.GetReportState(token, reportNumber);

            while (state.state != Ozon.API.Performance.Methods.ReportStatus.State.OK && state.state != Ozon.API.Performance.Methods.ReportStatus.State.ERROR)
            {
                System.Threading.Thread.Sleep(1000);
                state = Ozon.API.Performance.RequsetMethods.GetReportState(token, reportNumber);
            }
        }


        public static AdReportController.SKUReports ParseSKUReport(IEnumerable<string> reports)
        {
            var parsedReports = new AdReportController.SKUReports();
            foreach (string report in reports)
            {
                var result = RemoveCampaignNames(report);
                if (result == "") continue;

                var temp = JsonSerializer.Deserialize<AdReportController.SKUReports>(result);

                parsedReports.campaigns.AddRange(temp.campaigns);
            }
            
            Config.logger.LogInformation(JsonSerializer.Serialize<AdReportController.SKUReports>(parsedReports));

            return parsedReports;
        }

        public static AdReportController.SearchReports ParseSearchReports(IEnumerable<string> reports)
        {
            var parsedReports = new AdReportController.SearchReports();

            foreach (string report in reports)
            {
                var result = RemoveCampaignNames(report);
                if (result == "") continue;
                
                var temp = JsonSerializer.Deserialize<AdReportController.SearchReports>(result);

                parsedReports.campaigns.AddRange(temp.campaigns);
            }

            Config.logger.LogInformation(JsonSerializer.Serialize<AdReportController.SearchReports>(parsedReports));
            
            return parsedReports;
        }

        private static string RemoveCampaignNames(string report)
        {
            if (report.Length < 1)
            {
                return "";
            }

            Regex regex = new Regex("\"\\d+\":{");

            while (regex.IsMatch(report))
            {
                var match = regex.Match(report);
                var value = "{\"campaignId\":" + match.Value.Substring(0,match.Value.Length - 2) + ",";
                report = regex.Replace(report, value, 1);
            }

            report = "{\"campaigns\":[" + report.Substring(1, report.Length - 3) + "]}";

            Config.logger.LogInformation(report);

            return report;
        }
    }
}
