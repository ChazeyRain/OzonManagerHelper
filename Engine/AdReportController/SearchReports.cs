using System.Globalization;
using System.Text;

namespace Engine.AdReportController
{
    public class SearchReports
    {
        public static string[] Header { get; } = new string[] {
            "ID",
            "Дата",
            "ID Заказа",
            "Номер Заказа",
            "SKU",
            "SKU рекламы",
            "ID предложения",
            "Название",
            "Количество",
            "Цена",
            "Цена продажи",
            "Стоимость",
            "Ставка",
            "Стоимость ставки",
            "Потрачено денег"
        };

        public List<Campaign> campaigns { get; set; } = new List<Campaign>();

        public List<string>? GetCsv()
        {
            var builder = new List<string>();

            if (campaigns == null)
            {
                return null;
            }

            //builder.Add(GetHeader());

            foreach (var r in campaigns)
            {
                if (r.report != null)
                {
                    builder.AddRange(r.ToCsv());
                }
            }

            return builder;
        }

        public static string GetHeader()
        {
            System.Text.StringBuilder builder = new StringBuilder();

            foreach (var title in Header)
            {
                builder.Append(title).Append(";");
            }

            return builder.ToString();
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

        public class Campaign
        {
            public string? campaignId { get; set; }
            public string? title { get; set; }
            public Report? report { get; set; }

            public List<string> ToCsv()
            {
                if (report == null || report.rows == null) return new List<string>();

                List<string> builder = new List<string>();

                foreach (var row in report.rows)
                {
                    builder.Add(campaignId + ";" + row.ToString());
                }

                return builder;
            }

            public class Report
            {
                public Row[]? rows { get; set; }
                public Totals? totals { get; set; }

                public class Row
                {
                    public string? date { get; set; }
                    public string? orderId { get; set; }
                    public string? orderNumber { get; set; }
                    public string? sku { get; set; }
                    public string? advSku { get; set; }
                    public string? offerId { get; set; }
                    public string? title { get; set; }
                    public string? quantity { get; set; }
                    public string? price { get; set; }
                    public string? salePrice { get; set; }
                    public string? cost { get; set; }
                    public string? bid { get; set; }
                    public string? bidValue { get; set; }
                    public string? moneySpent { get; set; }
                    public string? card_id { get; set; }


                    public override string ToString()
                    {
                        StringBuilder builder = new StringBuilder();

                        builder.Append(date != null && date != "" ? DateTime.Parse(date, new CultureInfo("ru-RU")).ToString("yyyy-MM-dd") : "").Append(";");
                        
                        builder.Append(orderId).Append(";");
                        builder.Append(orderNumber).Append(";");
                        builder.Append(sku).Append(";");
                        builder.Append(advSku).Append(";");
                        builder.Append(offerId).Append(";");
                        builder.Append(card_id).Append(";");
                        builder.Append(quantity).Append(";");
                        builder.Append(price).Append(";");
                        builder.Append(salePrice).Append(";");
                        builder.Append(cost).Append(";");
                        builder.Append(bid).Append(";");
                        builder.Append(bidValue).Append(";");
                        builder.Append(moneySpent).Append(";");

                        return builder.ToString();
                    }
                }

                public class Totals
                {
                    public string? views { get; set; }
                    public string? clicks { get; set; }
                    public string? ctr { get; set; }
                    public string? moneySpent { get; set; }
                    public string? avgBid { get; set; }
                    public string? orders { get; set; }
                    public string? ordersMoney { get; set; }
                    public string? models { get; set; }
                    public string? modelsMoney { get; set; }
                }
            }
        }
    }
}