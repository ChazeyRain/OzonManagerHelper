using Engine.UserController;
using Ozon.API.Performance.Methods.Client;

namespace Engine
{
    public class FullReport
    {
        private User user;

        public FullReport(User user)
        {
            this.user = user;
        }

        public List<string> Get(DateTime startDate, DateTime endDate)
        {
            var token = PerformanceApiReport.GetToken(user.performanceCredentials);
            var skuCampaignList = PerformanceApiReport.GetCampaignList(token, Campaigns.Campaign.AdvObjectType.SKU);
            var skuReports = PerformanceApiReport.ParseSKUReport(PerformanceApiReport.GetReports(token, skuCampaignList, startDate, endDate));

            skuReports.LoadAdTypes(skuCampaignList);
            skuReports.LoadProductUnifier(user.items);
            

            var searchCampaignList = PerformanceApiReport.GetCampaignList(token, Campaigns.Campaign.AdvObjectType.SEARCH_PROMO);
            var searchReports = PerformanceApiReport.ParseSearchReports(PerformanceApiReport.GetReports(token, searchCampaignList, startDate, endDate));

            searchReports.LoadProductUnifier(user.items);
            

            var transactionReport = SellerReportController.SellerApiReport.GetTransactions(user.sellerCredentials, startDate, endDate);
            var transacrions = new Engine.SellerReportController.Transactions();
            transacrions.LoadTransactionList(transactionReport);
            transacrions.LoadItems(user.items);

            var result = ParseReport(transacrions, skuReports, searchReports, [skuCampaignList, searchCampaignList]);
            
            return result;
        }

        private List<string> ParseCampaignList(IEnumerable<Campaigns> campaignLists)
        {
            var result = new List<string>();
            
            foreach (var list in campaignLists)
            {
                result.AddRange(list.ToCsv());
            }

            return result;
        }

        private List<string> ParseReport(SellerReportController.Transactions transacrions, AdReportController.SKUReports skuReports, AdReportController.SearchReports searchReports, IEnumerable<Campaigns> campaignLists)
        {
            var list = new List<string>();

            var parsedSkuReports = skuReports.GetCsv();
            var parsedSearchReports = searchReports.GetCsv();
            var parsedCampaignList = ParseCampaignList(campaignLists);
            var parsedTransactions = transacrions.ToCsv();
            var parsedLookupTable = user.items.ToCsv();

            list.Add(transacrions.GetHeader() + ";" + campaignLists.First().GetHeader() + ";" + AdReportController.SKUReports.GetHeader() + ";" + AdReportController.SearchReports.GetHeader() + ";" + ItemController.Items.GetHeader() + ";");

            int length = parsedSkuReports.Count();
            length = length > parsedSearchReports.Count() ? length : parsedSearchReports.Count();
            length = length > parsedCampaignList.Count() ? length : parsedCampaignList.Count(); 
            length = length > parsedTransactions.Count() ? length : parsedTransactions.Count();
            length = length > parsedLookupTable.Count() ? length : parsedLookupTable.Count();

            for (int i = 0; i < length; i++)
            {
                string line = "";
                line += parsedTransactions.ElementAtOrDefault(i) == null ? new string(';', SellerReportController.Transactions.Header.Length) : parsedTransactions.ElementAtOrDefault(i);
                line += ";";
                line += parsedCampaignList.ElementAtOrDefault(i) == null ? new string(';', Campaigns.Header.Length) : parsedCampaignList.ElementAtOrDefault(i);
                line += ";";
                line += parsedSkuReports.ElementAtOrDefault(i) == null ? new string(';', AdReportController.SKUReports.Header.Length) : parsedSkuReports.ElementAtOrDefault(i);
                line += ";";
                line += parsedSearchReports.ElementAtOrDefault(i) == null ? new string(';', AdReportController.SearchReports.Header.Length) : parsedSearchReports.ElementAtOrDefault(i);
                line += ";";
                line += parsedLookupTable.ElementAtOrDefault(i) == null ? new string(';', ItemController.Items.Header.Count) : parsedLookupTable.ElementAtOrDefault(i);
                line += ";";

                list.Add(line);
            }
            return list;
        }
    }
}