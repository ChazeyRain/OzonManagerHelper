using System.Globalization;
using System.Text;
using Ozon.API.Performance.Methods.Client;

namespace Engine.AdReportController
{
    public class SKUReports
    {
        public static string[] Header { get; } = [
            "Тип",
            "ID",
            "Дата",
            "Показы",
            "Клики",
            "Потрачено",
            "Средняя ставка",
            "Заказы",
            "Сумма заказов",
            "Модель",
            "Модель деньги",
            "SKU",
            "Название",
            "Цена"
        ];

        public List<Campaign> campaigns { get; set; } = new List<Campaign>();

        public void LoadAdTypes(Campaigns campaigns)
        {
            foreach (var thisCampaign in this.campaigns)
            {
                foreach (var campaign in campaigns.list)
                {
                    if (campaign.id == thisCampaign.campaignId)
                    {
                        thisCampaign.type = campaign.placement.FirstOrDefault("");
                    }
                }
            }
        }
        public void LoadProductUnifier(ItemController.Items productList)
        {
            foreach (var campaign in campaigns)
            {
                foreach (var row in campaign.report.rows)
                {
                    foreach (var product in productList.list)
                    {
                        if (row.sku == product.sku + "")
                        {
                            row.card_id = product.card_id;
                        }
                    }
                }
            }
        }
        public List<string> GetCsv()
        {
            var builder = new List<string>();

            foreach (var r in campaigns)
            {
                builder.AddRange(r.ToCsv());
            }

            return builder;
        }
        public static string GetHeader()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var title in Header)
            {
                builder.Append(title).Append(";");
            }

            return builder.ToString();
        }
        
        public class Campaign
        {
            public string? title { get; set; }
            public string? campaignId { get; set; }
            public Report report { get; set; } = new Report();
            public string type { get; set; } = "PLACEMENT_SEARCH_AND_CATEGORY";

            public List<string> ToCsv()
            {
                List<string> builder = new List<string>();

                foreach (var row in report.rows)
                {
                    builder.Add(type + ";" + campaignId + ";" + row.ToString());
                }

                return builder;
            }

            public class Report
            {
                public List<Row> rows { get; set; } = new List<Row>();

                public class Row
                {
                    public string? date { get; set; }
                    public string? views { get; set; }
                    public string? clicks { get; set; }
                    public string? moneySpent { get; set; }
                    public string? avgBid { get; set; }
                    public string? orders { get; set; }
                    public string? ordersMoney { get; set; }
                    public string? models { get; set; }
                    public string? modelsMoney { get; set; }
                    public string? sku { get; set; }
                    public string? title { get; set; }
                    public string? price { get; set; }
                    public string? card_id { get; set; }

                    public override string ToString()
                    {
                        StringBuilder builder = new StringBuilder();

                        //builder.Append(date).Append(";");
                        builder.Append(date != null && date != "" ? DateTime.Parse(date, new CultureInfo("ru-RU")).ToString("yyyy-MM-dd") : "").Append(";");
                        builder.Append(views).Append(";");
                        builder.Append(clicks).Append(";");
                        builder.Append(moneySpent).Append(";");
                        builder.Append(avgBid).Append(";");
                        builder.Append(orders).Append(";");
                        builder.Append(ordersMoney).Append(";");
                        builder.Append(models).Append(";");
                        builder.Append(modelsMoney).Append(";");
                        builder.Append(sku).Append(";");
                        builder.Append(card_id).Append(";");
                        builder.Append(price).Append(";");

                        return builder.ToString();
                    }
                }
            }
        }
    }
}