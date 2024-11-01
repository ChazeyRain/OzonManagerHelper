using System.Text.Json;

namespace Engine.UserController
{
    public class User
    {
        public string name { get; set; } = "default";
        public Ozon.API.Performance.Credentials performanceCredentials { get; set; } = new Ozon.API.Performance.Credentials();

        public Ozon.API.Seller.Credentials sellerCredentials { get; set; } = new Ozon.API.Seller.Credentials();

        public ItemController.Items items { get; set; } = new ItemController.Items();

        public User() { }

        public User(string name, string[] sellerCredentials, string[] performanceCredentials)
        {
            this.name = name;
            this.sellerCredentials = new Ozon.API.Seller.Credentials()
            {
                ClientId = sellerCredentials[0],
                APIkey = sellerCredentials[1]
            };

            this.performanceCredentials = new Ozon.API.Performance.Credentials()
            {
                client_id = performanceCredentials[0],
                client_secret = performanceCredentials[1]
            };
        }

        public void SynchroniseProducts(ItemController.Items items)
        {
            foreach(var newItem in items.list)
            {
                bool isFound = false;
                foreach(var thisItem in this.items.list)
                {
                    if(thisItem.seller_id == newItem.seller_id)
                    {
                        thisItem.price = newItem.price;
                        thisItem.buyer_price = newItem.buyer_price;
                        thisItem.card_id = newItem.card_id;
                        isFound = true;
                    }
                }

                if(!isFound)
                {
                    this.items.list.Add(newItem);
                }
            }
        }
    }
}