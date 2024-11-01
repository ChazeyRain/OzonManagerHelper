namespace Ozon.API.Seller.Methods.Product.Info
{
    public class List
    {
        public Result result { get; set; } = new Result();

        public class Result
        {
            public List<Item> items { get; set; } = new List<Item>();

            public class Item
            {
                public bool is_archived { get; set; }
                public bool is_autoarchived { get; set; }
                public string? barcode { get; set; }
                public List<string> barcodes { get; set; } = new List<string>();
                public string? buybox_price { get; set; }
                public Int64 description_category_id { get; set; }
                public string? color_image { get; set; }
                public string? created_at { get; set; }
                public Int64 sku { get; set; }
                public Int64 fbo_sku { get; set; }
                public Int64 fbs_sku { get; set; }
                public Int64 id { get; set; }
                public List<string> images { get; set; } = new List<string>();
                public string? primary_image { get; set; }
                public List<string> images360 { get; set; } = new List<string>();
                public bool has_discounted_item { get; set; }
                public Discounted_Stocks discounted_stocks { get; set; } = new Discounted_Stocks();
                public bool is_kgt { get; set; }
                public string? currency_code { get; set; }
                public string? marketing_price { get; set; }
                public string? min_ozon_price { get; set; }
                public string? min_price { get; set; }
                public string? name { get; set; }
                public string? offer_id { get; set; }
                public string? old_price { get; set; }
                public string? price { get; set; }
                public Price_Indexes price_indexes {get; set;} = new Price_Indexes();
                public string? recommended_price {get; set;}
                //Допилить с этого момента, мне пока что лень
                
                public class Discounted_Stocks
                {
                    public int coming { get; set; }
                    public int present { get; set; }
                    public int reserved { get; set; }
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

    public class ListRequest : GenericRequest
    {
        public override string path {get;} = "/v2/product/info/list";
        public List<string> offer_id { get; set; } = new List<string>();
        public List<string> product_id { get; set; } = new List<string>();
        public List<Int64> sku { get; set; } = new List<long>();
    }
}