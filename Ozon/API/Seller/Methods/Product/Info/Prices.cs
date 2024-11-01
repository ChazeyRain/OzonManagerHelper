namespace Ozon.API.Seller.Methods.Product.Info
{
    public class Prices
    {
        public Result result {get; set;} = new Result();

        public class Result
        {
            public List<Item> items {get; set;}= new List<Item>();
            public string? last_id {get; set;}
            public int total {get; set;}
            
            public class Item
            {
                public int acquiring { get; set; }
                public Comissions comissions { get; set; } = new Comissions();
                public Marketing_Actions marketing_actions { get; set; } = new Marketing_Actions();
                public string? offer_id { get; set; }
                public Price price { get; set; } = new Price();
                public string? price_index { get; set; }
                public Price_Indexes price_Indexes { get; set; } = new Price_Indexes();
                public Int64 product_id { get; set; }
                public double volume_weight { get; set; }


                public class Comissions
                {
                    double fbo_deliv_to_customer_amount { get; set; }
                    double fbo_direct_flow_trans_max_amount { get; set; }
                    double fbo_direct_flow_trans_min_amount { get; set; }
                    double fbo_fulfillment_amount { get; set; }
                    double fbo_return_flow_amount { get; set; }
                    double fbo_return_flow_trans_min_amount { get; set; }
                    double fbo_return_flow_trans_max_amount { get; set; }
                    double fbs_deliv_to_customer_amount { get; set; }
                    double fbs_direct_flow_trans_max_amount { get; set; }
                    double fbs_direct_flow_trans_min_amount { get; set; }
                    double fbs_first_mile_min_amount { get; set; }
                    double fbs_first_mile_max_amount { get; set; }
                    double fbs_return_flow_amount { get; set; }
                    double fbs_return_flow_trans_max_amount { get; set; }
                    double fbs_return_flow_trans_min_amount { get; set; }
                    double sales_percent_fbo { get; set; }
                    double sales_percent_fbs { get; set; }
                    double sales_percent { get; set; }
                }

                public class Marketing_Actions
                {
                    public List<Action> actions { get; set; } = new List<Action>();
                    public string? current_period_from { get; set; }
                    public string? current_period_to { get; set; }
                    public bool ozon_actions_exist { get; set; }

                    public class Action
                    {
                        string? date_from { get; set; }
                        string? date_to { get; set; }
                        string? discount_value { get; set; }
                        string? title { get; set; }
                    }
                }

                public class Price
                {
                    public bool auto_action_enabled { get; set; }
                    public string? currency_code { get; set; }
                    public string? marketing_price { get; set; }
                    public string? marketing_seller_price { get; set; }
                    public string? min_ozon_price { get; set; }
                    public string? min_price { get; set; }
                    public string? old_price { get; set; }
                    public string? price { get; set; }
                    public string? retail_price { get; set; }
                    public string? vat { get; set; }
                }

                public class Price_Indexes
                {
                    public Index_Data external_index_data { get; set; } = new Index_Data();
                    public Index_Data ozon_index_data { get; set; } = new Index_Data();
                    public string? price_index { get; set; }
                    public Index_Data self_marketplaces_index_data { get; set; } = new Index_Data();

                    public class Price_Index
                    {
                        public string WITHOUT_INDEX { get; } = "WITHOUT_INDEX";
                        public string PROFIT { get; } = "PROFIT";
                        public string AVG_PROFIT { get; } = "AVG_PROFIT";
                        public string NON_PROFIT { get; } = "NON_PROFIT";
                    }
                    public class Index_Data
                    {
                        public string? minimal_price { get; set; }
                        public string? minimal_price_currency { get; set; }
                        public double price_index_value { get; set; }
                    }
                }
            }

        }
    }

    public class PricesRequest : GenericRequest
    {
        public override string path {get;} = "/v4/product/info/prices";
        
        public Filter filter { get; set; } = new Filter();
        public string last_id { get; set; } = "";
        public int limit { get; set; } = 1000;

        public class Filter
        {
            public List<string>? offer_id;
            public List<string>? product_id;
            public string? visibility;

            public class Visibility
            {
                public string ALL { get; } = "ALL";
                public string VISIBLE { get; } = "VISIBLE";
                public string INVISIBLE { get; } = "INVISIBLE";
                public string EMPTY_STOCK { get; } = "EMPTY_STOCK";
                public string ANOT_MODERATEDLL { get; } = "NOT_MODERATED";
                public string MODERATED { get; } = "MODERATED";
                public string DISABLED { get; } = "DISABLED";
                public string STATE_FAILED { get; } = "STATE_FAILED";
                public string READY_TO_SUPPLY { get; } = "READY_TO_SUPPLY";
                public string VALIDATION_STATE_PENDING { get; } = "VALIDATION_STATE_PENDING";
                public string VALIDATION_STATE_FAIL { get; } = "VALIDATION_STATE_FAIL";
                public string VALIDATION_STATE_SUCCESS { get; } = "VALIDATION_STATE_SUCCESS";
                public string TO_SUPPLY { get; } = "TO_SUPPLY";
                public string IN_SALE { get; } = "IN_SALE";
                public string REMOVED_FROM_SALE { get; } = "REMOVED_FROM_SALE";
                public string BANNED { get; } = "BANNED";
                public string OVERPRICED { get; } = "OVERPRICED";
                public string CRITICALLY_OVERPRICED { get; } = "CRITICALLY_OVERPRICED";
                public string EMPTY_BARCODE { get; } = "EMPTY_BARCODE";
                public string BARCODE_EXISTS { get; } = "BARCODE_EXISTS";
                public string QUARANTINE { get; } = "QUARANTINE";
                public string ARCHIVED { get; } = "ARCHIVED";
                public string OVERPRICED_WITH_STOCK { get; } = "OVERPRICED_WITH_STOCK";
                public string PARTIAL_APPROVED { get; } = "PARTIAL_APPROVED";
                public string IMAGE_ABSENT { get; } = "IMAGE_ABSENT";
                public string MODERATION_BLOCK { get; } = "MODERATION_BLOCK";
            }
        }
    }
}