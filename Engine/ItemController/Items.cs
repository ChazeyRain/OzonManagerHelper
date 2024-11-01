using System.Text;

namespace Engine.ItemController
{
    public class Items
    {
        public static List<string> Header = [
            "Артикул",
            "SKU",
            "Объединение",
            "Себестоимость",
            "Цена",
            "Цена для покупателя",
        ];

        public static string GetHeader()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var word in Header)
            {
                builder.Append(word).Append(";");
            }
            return builder.ToString();
        }
        public List<string> ToCsv()
        {
            var res = new List<string>();

            foreach (var item in list)
            {
                res.Add(item.ToString());
            }

            return res;
        }
        public List<Item> list {get; set;} = new List<Item>();

        public class Item
        {
            public Int64 sku { get; set; }
            public string? seller_id { get; set; }
            public string? card_id { get; set; }
            public Int64 ozon_id {get; set;}

            public double original_price { get; set; }
            public double price { get; set; }
            public double buyer_price { get; set; }

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder();
                
                builder.Append(seller_id).Append(";");                
                builder.Append(sku).Append(";");
                builder.Append(card_id).Append(";");

                builder.Append(original_price).Append(";");
                builder.Append(price).Append(";");
                builder.Append(buyer_price).Append(";");
                
                return builder.ToString();
            }
        }
    }
}