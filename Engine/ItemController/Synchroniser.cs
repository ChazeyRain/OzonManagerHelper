using Engine.UserController;

namespace Engine.ItemController
{
    public class Synchroniser()
    {
        public static void Synchronise(User user)
        {
            var result = new Items();

            SynchroniseItems(user, result);
            SynchroniseSku(user, result);
            SynchronisePrices(user, result);
            SynchroniseCardID(user, result);

            user.SynchroniseProducts(result);
        }

        private static void SynchroniseItems(User user, Items result)
        {
            var request = new Ozon.API.Seller.Methods.Product.ListRequest();
            var response = Ozon.API.Seller.RequestMethods.Post<Ozon.API.Seller.Methods.Product.ListRequest, Ozon.API.Seller.Methods.Product.List>(user.sellerCredentials, request);

            for (long i = request.limit; i < response.result.total; i += request.limit)
            {
                request.last_id = response.result.last_id;
                var temp = Ozon.API.Seller.RequestMethods.Post<Ozon.API.Seller.Methods.Product.ListRequest, Ozon.API.Seller.Methods.Product.List>(user.sellerCredentials, request);
                response.result.last_id = temp.result.last_id;
                response.result.items.AddRange(temp.result.items);
            }


            foreach (var item in response.result.items)
            {
                var resultItem = new Items.Item();
                resultItem.seller_id = item.offer_id;
                resultItem.ozon_id = item.product_id;

                result.list.Add(resultItem);
            }
        }

        private static void SynchroniseSku(User user, Items result)
        {
            var response = new Ozon.API.Seller.Methods.Product.Info.List();
            List<string> ids = new List<string>();

            int counter = 0;

            foreach (var item in result.list)
            {
                counter++;
                
                ids.Add(item.ozon_id + "");
                Console.WriteLine(item.ozon_id);

                if (counter % 1000 == 0 || counter == result.list.Count)
                {
                    var productInfoListRequest = new Ozon.API.Seller.Methods.Product.Info.ListRequest();
                    productInfoListRequest.product_id.AddRange(ids);
                    var temp = Ozon.API.Seller.RequestMethods.Post<Ozon.API.Seller.Methods.Product.Info.ListRequest, Ozon.API.Seller.Methods.Product.Info.List>(user.sellerCredentials, productInfoListRequest);
                    response.result.items.AddRange(temp.result.items);

                    ids = new List<string>();
                }
            }

            foreach (var item in result.list)
            {
                foreach (var i in response.result.items)
                {
                    if (item.ozon_id == i.id)
                    {
                        item.sku = i.sku;
                        item.buyer_price = Double.Parse(i.marketing_price);
                    }
                }
            }
        }

        private static void SynchronisePrices(User user, Items result)
        {
            var request = new Ozon.API.Seller.Methods.Product.Info.PricesRequest();

            var response = Ozon.API.Seller.RequestMethods.Post<Ozon.API.Seller.Methods.Product.Info.PricesRequest, Ozon.API.Seller.Methods.Product.Info.Prices>(user.sellerCredentials, request);

            int total = response.result.total;

            for (int i = request.limit; i < total; i += request.limit)
            {
                request.last_id = response.result.last_id;
                var temp = Ozon.API.Seller.RequestMethods.Post<Ozon.API.Seller.Methods.Product.Info.PricesRequest, Ozon.API.Seller.Methods.Product.Info.Prices>(user.sellerCredentials, request);
                response.result.last_id = temp.result.last_id;
                response.result.items.AddRange(temp.result.items);
            }

            foreach (var resItems in response.result.items)
            {
                foreach (var item in result.list)
                {
                    if (resItems.offer_id == item.seller_id)
                    {
                        item.price = double.Parse(resItems.price.marketing_seller_price);
                    }
                }
            }
        }

        private static void SynchroniseCardID(User user, Items result)
        {
            var request = new Ozon.API.Seller.Methods.Product.Info.AttributesRequest();

            var response = Ozon.API.Seller.RequestMethods.Post<Ozon.API.Seller.Methods.Product.Info.AttributesRequest, Ozon.API.Seller.Methods.Product.Info.Attributes>(user.sellerCredentials, request);

            for (long i = request.limit; i < response.total; i += request.limit)
            {
                request.last_id = response.last_id;
                var temp = Ozon.API.Seller.RequestMethods.Post<Ozon.API.Seller.Methods.Product.Info.AttributesRequest, Ozon.API.Seller.Methods.Product.Info.Attributes>(user.sellerCredentials, request);
                response.last_id = temp.last_id;
                response.result.AddRange(temp.result);
            }

            foreach (var item in response.result)
            {
                string? card_id = "";

                foreach (var attribute in item.attributes)
                {
                    if (attribute.attribute_id == 8292L)
                    {
                        card_id = attribute.values.Count >= 1 ? attribute.values[0].value : "";
                        
                    }
                }

                foreach (var resItem in result.list)
                {
                    if (resItem.seller_id == item.offer_id)
                    {
                        resItem.card_id = card_id;
                        //Console.WriteLine(attribute.values[0].value);
                    }
                }
            }
        }
    }
}