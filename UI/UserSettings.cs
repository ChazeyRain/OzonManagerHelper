using Engine.ItemController;
using Engine.UserController;

namespace UI
{
    public class UserSettings
    {
        public static void Display(User user)
        {
            Tools.ConsoleClear();

            Console.WriteLine("1. Синхронизировать товары");
            Console.WriteLine("2. Добавить цены на все товары");
            Console.WriteLine("3. Добавить объединения товарам");
            Console.WriteLine("0. Назад");

             var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Tools.Loading(new Thread(() => Engine.ItemController.Synchroniser.Synchronise(user)));
                    Manager.Save(user);
                    break;
                case "2":
                    AddPrices(user);
                    Manager.Save(user);
                    break;
                case "0":
                    return;
            }
            
            Display(user);
        }

        private static void AddPrices(User user)
        {
            foreach(var item in user.items.list)
            {
                AddItemPrice(item);
            }
        }

        private static void AddItemPrice(Items.Item item)
        {
            Tools.ConsoleClear();
            Console.WriteLine("Введите цену для этого товара:");
            Console.WriteLine("Артикул: " + item.seller_id);
            
            var input = Console.ReadLine();

            try {
                var price = double.Parse(input);
                item.original_price = price;
            }
            catch
            {
                AddItemPrice(item);
            }
        }
    }
}