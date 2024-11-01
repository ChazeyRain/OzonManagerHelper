using System.Text;

namespace Ozon.API.Performance.Methods.Client
{
    public class Campaigns
    {
        public static string[] Header { get; } = new string[]
        {
            "ID",
            "Тип оплаты",
            "Название кампании",
            "Состояние кампании",
            "Тип кампании",
            "Дата старта кампании",
            "Дата завершения кампании",
            "Бюджет",
            "Дневной бюджет",
            "Недельный бюджет",
            "Место размещения рекламы",
            "Место размещения рекламы",
            "Место размещения рекламы",
            "Место размещения рекламы"
        };

        public List<Campaign> list { get; set; } = new List<Campaign>();

        public string GetHeader()
        {
            var h = new StringBuilder();

            foreach (var word in Header)
            {
                h.Append(word).Append(";");
            }

            return h.ToString();
        }

        public List<string> ToCsv()
        {
            var result = new List<string>();

            foreach (var campaign in list)
            {
                result.Add(campaign.ToString());
            }

            return result;
        }

        public class Campaign
        {
            public string? id { get; set; }
            public string? paymentType { get; set; }
            public string? title { get; set; }
            public string? state { get; set; }
            public string? advObjectType { get; set; }
            public string? fromDate { get; set; }
            public string? toDate { get; set; }
            public string? budget { get; set; }
            public string? dailyBudget { get; set; }
            public string? weeklyBudget { get; set; }
            public string[]? placement { get; set; }
            public string? productAutopilotStrategy { get; set; }
            public Autopilot? autopilot { get; set; }
            public string? createdAt { get; set; }
            public string? updatedAt { get; set; }
            public string? productCampaignMode { get; set; }

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();

                builder.Append(id).Append(";");
                builder.Append(paymentType).Append(";");
                builder.Append(title).Append(";");
                builder.Append(state).Append(";");
                builder.Append(advObjectType).Append(";");
                builder.Append(fromDate).Append(";");
                builder.Append(toDate).Append(";");
                builder.Append(budget).Append(";");
                builder.Append(dailyBudget).Append(";");
                builder.Append(weeklyBudget).Append(";");
                for (int i = 0; i < 4; i++)
                {
                    if (placement != null && i < placement.Length)
                    {
                        builder.Append(placement[i]).Append(";");
                    }
                    else
                    {
                        builder.Append(";");
                    }
                }
                return builder.ToString();
            }

            public class Autopilot
            {
                public string? categoryId { get; set; }
                public string? skuAddMode { get; set; }
            }
            public class AdvObjectType
            {
                public static string SKU { get; } = "SKU";
                public static string BANNER { get; } = "BANNER";
                public static string SEARCH_PROMO { get; } = "SEARCH_PROMO";
            }

            public class State
            {
                public static string CAMPAIGN_STATE_UNKNOWN { get; } = "";
                public static string CAMPAIGN_STATE_RUNNING { get; } = "CAMPAIGN_STATE_RUNNING";
                public static string CAMPAIGN_STATE_PLANNED { get; } = "CAMPAIGN_STATE_PLANNED";
                public static string CAMPAIGN_STATE_STOPPED { get; } = "CAMPAIGN_STATE_STOPPED";
                public static string CAMPAIGN_STATE_INACTIVE { get; } = "CAMPAIGN_STATE_INACTIVE";
                public static string CAMPAIGN_STATE_ARCHIVED { get; } = "CAMPAIGN_STATE_ARCHIVED";
                public static string CAMPAIGN_STATE_MODERATION_DRAFT { get; } = "CAMPAIGN_STATE_MODERATION_DRAFT";
                public static string CAMPAIGN_STATE_MODERATION_IN_PROGRESS { get; } = "CAMPAIGN_STATE_MODERATION_IN_PROGRESS";
                public static string CAMPAIGN_STATE_MODERATION_FAILED { get; } = "CAMPAIGN_STATE_MODERATION_FAILED";
                public static string CAMPAIGN_STATE_FINISHED { get; } = "CAMPAIGN_STATE_FINISHED";
            }

            public class PaymentType
            {
                public static string CPC { get; } = "CPC";
                public static string CPM { get; } = "CPM";
                public static string CPO { get; } = "CPO";
            }

            public class Placement
            {
                public static string PLACEMENT_INVALID { get; } = "PLACEMENT_INVALID";
                public static string PLACEMENT_PDP { get; } = "PLACEMENT_PDP";
                public static string PLACEMENT_SEARCH_AND_CATEGORY { get; } = "PLACEMENT_SEARCH_AND_CATEGORY";
                public static string PLACEMENT_TOP_PROMOTION { get; } = "PLACEMENT_TOP_PROMOTION";
            }

            public class ProductAutopilotStrategy
            {
                public static string MAX_VIEWS { get; } = "MAX_VIEWS";
                public static string MAX_CLICKS { get; } = "MAX_CLICKS";
                public static string TOP_MAX_CLICKS { get; } = "TOP_MAX_CLICKS";
                public static string NO_AUTO_STRATEGY { get; } = "NO_AUTO_STRATEGY";
            }

            public class SkuAddMode
            {
                public static string PRODUCT_CAMPAIGN_SKU_ADD_MODE_UNKNOWN { get; } = "PRODUCT_CAMPAIGN_SKU_ADD_MODE_UNKNOWN";
                public static string PRODUCT_CAMPAIGN_SKU_ADD_MODE_MANUAL { get; } = "PRODUCT_CAMPAIGN_SKU_ADD_MODE_MANUAL";
                public static string PRODUCT_CAMPAIGN_SKU_ADD_MODE_AUTO { get; } = "PRODUCT_CAMPAIGN_SKU_ADD_MODE_AUTO";
            }

            public class ProductCampaignMode
            {
                public static string PRODUCT_CAMPAIGN_MODE_AUTO { get; } = "PRODUCT_CAMPAIGN_MODE_AUTO";
                public static string PRODUCT_CAMPAIGN_MODE_MANUAL { get; } = "PRODUCT_CAMPAIGN_MODE_MANUAL";
            }
        }
    }
}