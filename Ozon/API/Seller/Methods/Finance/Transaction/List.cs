using System.Text;

namespace Ozon.API.Seller.Methods.Finance.Transaction
{
    public class List
    {
        public Result result { get; set; } = new Result();
        
        public class Result
        {
            public List<Operation> operations { get; set; } = new List<Operation>();
            public Int64 page_count { get; set; }
            public Int64 row_count { get; set; }

            public class Operation
            {
                public double? accruals_for_sale { get; set; }
                public double? amount { get; set; }
                public List<Item> items { get; set; } = new List<Item>();
                public string operation_date { get; set; } = "";
                public Int64 operation_id { get; set; }
                public string? operation_type { get; set; }
                public string? operation_type_name { get; set; }
                public Posting posting { get; set; } = new Posting();
                public double return_delivery_charge { get; set; }
                public double sale_commission { get; set; }
                public List<Service> services { get; set; } = new List<Service>();

                public class Item
                {
                    public string? name { get; set; }
                    public string? sellerName { get; set; }
                    public Int64 sku { get; set; }
                }

                public class Posting
                {
                    public string? delivery_schema { get; set; }
                    public string? order_date { get; set; }
                    public string? posting_number { get; set; }
                    public Int64 warehouse_id { get; set; }
                }

                public class Service
                {
                    public string? name { get; set; }
                    public double price { get; set; }
                }

                public Int64 page_count { get; set; }
                public Int64 row_count { get; set; }
            }
        }
    }

    public class ListRequest
    {
        public Filter filter { get; set; } = new Filter();
        public Int64 page { get; set; } = 1;
        public Int64 page_size { get; set; } = 1000;

        public class Filter
        {
            public Date date { get; set; } = new Date();
            public string? operation_type { get; set; }
            public string? posting_number { get; set; } = "";
            public string? transacton_type { get; set; } = "";
            public class Date
            {
                public string? from { get; set; } = "";
                public string? to { get; set; } = "";
            }
        }
    }
}