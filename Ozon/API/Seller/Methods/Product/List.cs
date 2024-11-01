using System.Text;

namespace Ozon.API.Seller.Methods.Product
{
    public class List
    {
        public Result result { get; set; } = new Result();

        public class Result
        {
            public List<Item> items { get; set; } = new List<Item>();
            public string? last_id { get; set; }
            public Int32? total { get; set; }
        }

        public class Item
        {
            public string? offer_id { get; set; }
            public Int64 product_id { get; set; }

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(offer_id).Append(";");
                builder.Append(product_id);
                return builder.ToString();
            }
        }
    }

    public class ListRequest : GenericRequest
    {
        public override string path {get; } = "/v2/product/list";

        public Filter filter { get; set; } = new Filter();
        public string? last_id { get; set; }
        public Int64 limit { get; set; } = 1000;

        public class Filter
        {
            public List<string> offer_id { get; set; } = new List<string>();
            public List<string> product_id { get; set; } = new List<string>();
            public string visibility { get; set; } = "ALL";
        }
    }
}