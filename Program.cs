using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Engine;
using Engine.AdReportController;
using Engine.SellerReportController;
using Engine.UserController;
using Microsoft.Extensions.Logging;


public class Program
{
    public static void Main(string[] args)
    {
        Config.CreateLogger(LogLevel.Information);
        Config.encoderSettings.AllowRange(UnicodeRanges.All);

        Config.JsonOptions = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(Config.encoderSettings),
            WriteIndented = true,
        };

        
        UI.UserLogin.Display();
        
        //TestProductInfoAttributes();
        //TestGenericPost();

        //TestSkuReport();
        //TestSearchReport();
        //TestSynchro();
        //TesCampaignList();  
        //TestTransactions();
        //TestProductList();
    }

    private static void TestProductInfoAttributes()
    {
        User user = Engine.UserController.Manager.LoadUser("Users\\Marhchenko.json");

        var request = new Ozon.API.Seller.Methods.Product.Info.AttributesRequest();

        var result = Ozon.API.Seller.RequestMethods.Post<Ozon.API.Seller.Methods.Product.Info.AttributesRequest, Ozon.API.Seller.Methods.Product.Info.Attributes>(user.sellerCredentials, request);
    
        Console.WriteLine(JsonSerializer.Serialize<Ozon.API.Seller.Methods.Product.Info.Attributes>(result, Config.JsonOptions));
    }

    private static void TestGenericPost()
    {
        User user = Engine.UserController.Manager.LoadUser("Users\\Marhchenko.json");

        var productInfoPricesRequest = new Ozon.API.Seller.Methods.Product.Info.PricesRequest();

        var result = Ozon.API.Seller.RequestMethods.Post<Ozon.API.Seller.Methods.Product.Info.PricesRequest, Ozon.API.Seller.Methods.Product.Info.Prices>(user.sellerCredentials, productInfoPricesRequest);
    
        var o = JsonSerializer.Serialize<Ozon.API.Seller.Methods.Product.Info.Prices>(result, Config.JsonOptions);

        Console.WriteLine(o);
    }

    private static void TestSynchro()
    {
        User user = Engine.UserController.Manager.LoadUser("Users\\Marhchenko.json");
        Engine.ItemController.Synchroniser.Synchronise(user);
        Manager.Save(user);
    }

    private static void TestSkuReport()
    {
        User user = Engine.UserController.Manager.LoadUser("Users\\Marhchenko.json");


        var token = PerformanceApiReport.GetToken(user.performanceCredentials);
        var skuCampaignList = PerformanceApiReport.GetCampaignList(token, Ozon.API.Performance.Methods.Client.Campaigns.Campaign.AdvObjectType.SKU);
        var skuReports = PerformanceApiReport.ParseSKUReport(PerformanceApiReport.GetReports(token, skuCampaignList, new DateTime(2024, 10, 28), new DateTime(2024, 10, 29)));

        var result = skuReports.GetCsv();

        Console.WriteLine(SKUReports.GetHeader());
        foreach (var line in result)
        {
            Console.WriteLine(line);
        }
    }

    private static void TestSearchReport()
    {
        User user = Engine.UserController.Manager.LoadUser("Users\\Marhchenko.json");


        var token = PerformanceApiReport.GetToken(user.performanceCredentials);
        var search = PerformanceApiReport.GetCampaignList(token, Ozon.API.Performance.Methods.Client.Campaigns.Campaign.AdvObjectType.SEARCH_PROMO);
        var searchReports = PerformanceApiReport.ParseSearchReports(PerformanceApiReport.GetReports(token, search, new DateTime(2024, 10, 28), new DateTime(2024, 10, 29)));

        var result = searchReports.GetCsv();

        Console.WriteLine(SearchReports.GetHeader());
        foreach (var line in result)
        {
            Console.WriteLine(line);
        }
    }

    
    private static void TestTransactions()
    {
        User user = Engine.UserController.Manager.LoadUser("Users\\Marhchenko.json");

        var transacrions = SellerApiReport.GetTransactions(user.sellerCredentials, new DateTime(2024, 10, 01), new DateTime(2024, 10, 29));
        var tr = new Engine.SellerReportController.Transactions();
        tr.LoadTransactionList(transacrions);
        tr.LoadItems(user.items);
        
        var csv = tr.ToCsv();

        foreach (var line in csv)
        {
            Console.WriteLine(line);
        }
        SaveToFile.SaveCsv(csv);
    }
}
