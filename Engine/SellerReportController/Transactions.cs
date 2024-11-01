using System.Text;
using System.Text.Json;
using Engine.UserController;

namespace Engine.SellerReportController
{
    public class Transactions
    {
        public static string[] Header =
        {
            "Дата начисления",
            "Тип начисления",
            "Номер отправления или идентификатор услуги",
            "Дата принятия заказа в обработку или оказания услуги",
            "Склад отгрузки",
            "SKU",
            "Артикул",
            "Название товара или услуги",
            "Количество",
            "За продажу или возврат до вычета комиссий и услуг",
            "Ставка комиссии",
            "Комиссия за продажу",
            "Сборка заказа",
            "Обработка отправления (Drop-off/Pick-up) (разбивается по товарам пропорционально количеству в отправлении)",
            "Магистраль",
            "Последняя миля (разбивается по товарам пропорционально доле цены товара в сумме отправления)",
            "Обратная магистраль",
            "Обработка возврата",
            "Обработка отмененного или невостребованного товара (разбивается по товарам в отправлении в одинаковой пропорции)",
            "Обработка невыкупленного товара",
            "Логистика",
            "Индекс локализации",
            "Обратная логистика",
            "Итого",
            "Себестоимость",
            "Объединение"
        };

        public List<Transaction> list { get; set; } = new List<Transaction>();


        public void LoadTransactionList(Ozon.API.Seller.Methods.Finance.Transaction.List transactions)
        {
            foreach (var operation in transactions.result.operations)
            {
                string op = JsonSerializer.Serialize<Ozon.API.Seller.Methods.Finance.Transaction.List.Result.Operation>(operation);
                var transaction = JsonSerializer.Deserialize<Engine.SellerReportController.Transactions.Transaction>(op);

                list.Add(transaction);
            }
        }

        public void LoadItems(ItemController.Items items)
        {
            foreach (var item in items.list)
            {
                foreach (var transaction in list)
                {
                    foreach (var trItem in transaction.items)
                    {
                        if (item.sku == trItem.sku)
                        {
                            trItem.item_byuing_price = item.original_price;
                            trItem.unition = item.card_id;
                        }
                    }
                }
            }
        }

        public string GetHeader()
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
            var r = new List<string>();

            foreach (var transaction in list)
            {
                r.AddRange(transaction.ToStringList());
            }

            return r;
        }

        public class Transaction : Ozon.API.Seller.Methods.Finance.Transaction.List.Result.Operation
        {
            public new List<Item> items { get; set; } = new List<Item>();

            public List<string> ToStringList()
            {
                StringBuilder builder = new StringBuilder();
                var r = new List<string>();

                if (items == null || items.Count <= 1)
                {
                    builder.Append(operation_date.Substring(0, 10)).Append(";");
                    builder.Append(operation_type_name).Append(";");
                    builder.Append(operation_id).Append(";");
                    builder.Append(posting.order_date).Append(";");
                    builder.Append(posting.delivery_schema).Append(";");
                    builder.Append(items != null && items.Count != 0 ? items[0].sku : "").Append(";");
                    builder.Append(items != null && items.Count != 0 ? items[0].sellerName : "").Append(";");
                    builder.Append(items != null && items.Count != 0 ? items[0].name : "").Append(";");
                    builder.Append(items != null ? items.Count() : 0).Append(";");
                    builder.Append(accruals_for_sale).Append(";");
                    builder.Append(accruals_for_sale != 0 ? (sale_commission / accruals_for_sale) * 100 : 0).Append("%;");
                    builder.Append(sale_commission).Append(";");

                    builder.Append(SumServicesThatContain(["MarketplaceServiceItemFulfillment"])).Append(";");
                    builder.Append(SumServicesThatContain(["MarketplaceServiceItemDropoffFF", "MarketplaceServiceItemDropoffPVZ", "MarketplaceServiceItemDropoffSC"])).Append(";");
                    builder.Append(SumServicesThatContain(["MarketplaceServiceItemDirectFlowTrans"])).Append(";");
                    builder.Append(SumServicesThatContain(["MarketplaceServiceItemDelivToCustomer"])).Append(";");
                    builder.Append(SumServicesThatContain(["MarketplaceServiceItemReturnFlowTrans"])).Append(";");
                    builder.Append(SumServicesThatContain(["MarketplaceServiceItemReturnAfterDelivToCustomer", "MarketplaceServiceItemRedistributionReturnsPVZ"])).Append(";");
                    builder.Append(SumServicesThatContain(["MarketplaceServiceItemReturnNotDelivToCustomer"])).Append(";");
                    builder.Append(SumServicesThatContain(["MarketplaceServiceItemReturnPartGoodsCustomer"])).Append(";");
                    builder.Append(SumServicesThatContain(["MarketplaceServiceItemDirectFlowLogistic", "MarketplaceServiceItemDirectFlowLogisticVDC"])).Append(";");
                    builder.Append("").Append(";");
                    builder.Append(SumServicesThatContain(["MarketplaceServiceItemReturnFlowLogistic"])).Append(";");
                    builder.Append(amount).Append(";");
                    builder.Append(items != null && items.Count != 0 ? items[0].item_byuing_price : 0).Append(";");
                    builder.Append(items != null && items.Count != 0 ? items[0].unition : "").Append(";");
                    r.Add(builder.ToString());
                }
                else
                {
                    foreach (var item in items)
                    {
                        builder = new StringBuilder();
                        builder.Append(operation_date.Substring(0, 10)).Append(";");
                        builder.Append(operation_type_name).Append(";");
                        builder.Append(operation_id).Append(";");
                        builder.Append(posting.order_date).Append(";");
                        builder.Append(posting.delivery_schema).Append(";");
                        builder.Append(items != null && items.Count != 0 ? items[0].sku : "").Append(";");
                        builder.Append(items != null && items.Count != 0 ? items[0].sellerName : "").Append(";");
                        builder.Append(items != null && items.Count != 0 ? items[0].name : "").Append(";");
                        builder.Append(items != null && items.Count != 0 ? 1 : 0).Append(";");
                        builder.Append(items != null ? accruals_for_sale / items.Count : 0).Append(";");
                        builder.Append(accruals_for_sale != 0 ? (sale_commission / accruals_for_sale) * 100 : 0).Append("%;");
                        builder.Append(items != null ? sale_commission / items.Count : 0).Append(";");

                        builder.Append(SumServicesThatContain(["MarketplaceServiceItemFulfillment"]) / items.Count).Append(";");
                        builder.Append(SumServicesThatContain(["MarketplaceServiceItemDropoffFF", "MarketplaceServiceItemDropoffPVZ", "MarketplaceServiceItemDropoffSC"]) / items.Count).Append(";");
                        builder.Append(SumServicesThatContain(["MarketplaceServiceItemDirectFlowTrans"]) / items.Count).Append(";");
                        builder.Append(SumServicesThatContain(["MarketplaceServiceItemDelivToCustomer"]) / items.Count).Append(";");
                        builder.Append(SumServicesThatContain(["MarketplaceServiceItemReturnFlowTrans"]) / items.Count).Append(";");
                        builder.Append(SumServicesThatContain(["MarketplaceServiceItemReturnAfterDelivToCustomer"]) / items.Count).Append(";");
                        builder.Append(SumServicesThatContain(["MarketplaceServiceItemReturnNotDelivToCustomer"]) / items.Count).Append(";");
                        builder.Append(SumServicesThatContain(["MarketplaceServiceItemReturnPartGoodsCustomer"]) / items.Count).Append(";");
                        builder.Append(SumServicesThatContain(["MarketplaceServiceItemDirectFlowLogistic", "MarketplaceServiceItemDirectFlowLogisticVDC"]) / items.Count).Append(";");
                        builder.Append("").Append(";");
                        builder.Append(SumServicesThatContain(["MarketplaceServiceItemReturnFlowLogistic"]) / items.Count).Append(";");
                        builder.Append(amount / items.Count).Append(";");
                        builder.Append(items != null && items.Count != 0 ? items[0].item_byuing_price : 0).Append(";");
                        builder.Append(items != null && items.Count != 0 ? items[0].unition : "").Append(";");
                        r.Add(builder.ToString());
                    }
                }
                return r;
            }

            private double SumServicesThatContain(string[] patterns)
            {
                double result = 0;

                if (services == null) return 0;

                foreach (var service in services)
                {
                    foreach (var pattern in patterns)
                    {
                        result += service.name == pattern ? service.price : 0;
                    }
                }

                return result;
            }

            public new class Item : Ozon.API.Seller.Methods.Finance.Transaction.List.Result.Operation.Item
            {
                public double item_byuing_price { get; set; }
                public string? unition { get; set; }
            }
        }
    }
}
