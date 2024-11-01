using System.Text.RegularExpressions;
using Engine.UserController;

namespace UI
{
    public class UserLogin
    {
        public static void Display()
        {
            var users = Engine.UserController.Manager.GetSavedUsers();

            Tools.ConsoleClear();

            Console.WriteLine("Выберите пользователя:");

            int id = 1;
            foreach (var user in users)
            {
                Console.WriteLine(id++ + ". " + user.Replace(".json", "").Replace(Config.UserDirectory + "\\", ""));
            }

            Console.WriteLine("0. Создать нового");

            var input = Console.ReadLine();

            if (input == "0")
            {
                UserMenu.Display(CreateNewUser());
                return;
            }

            try
            {
                int userID = int.Parse(input);
                UserMenu.Display(Manager.LoadUser(users.ElementAt(userID - 1)));
                return;
            }
            catch
            {
                Display();
                return;
            }
        }

        private static User CreateNewUser()
        {
            Tools.ConsoleClear();
            var name = EnterName();
            var sellerApi = EnterSellerApi();
            var performanceAPI = EnterPerformanceApi();

            var user = ConfirmUser(new User(name, sellerApi, performanceAPI));
            Engine.UserController.Manager.Save(user);
            
            return user;
        }

        private static User ConfirmUser(User user)
        {
            Tools.ConsoleClear();
            Console.WriteLine("Имя пользователя: " + user.name);
            Console.WriteLine();
            Console.WriteLine("Seller API");
            Console.WriteLine("Client ID: " + user.sellerCredentials.ClientId);
            Console.WriteLine("API Key: " + user.sellerCredentials.APIkey);
            Console.WriteLine();
            Console.WriteLine("Performance API");
            Console.WriteLine("Client ID: " + user.sellerCredentials.ClientId);
            Console.WriteLine("API Key: " + user.sellerCredentials.APIkey);
            Console.WriteLine();
            Console.WriteLine("1. Далее");
            Console.WriteLine("0. Изменить");

            switch (Console.ReadLine())
            {
                case "0":
                    return CreateNewUser();
                case "1":
                    return user;
                default:
                    return ConfirmUser(user);
            }
        }

        private static string EnterName()
        {
            Tools.ConsoleClear();
            Console.WriteLine("Введите имя пользователя (только латинские буквы):");
            string name = Console.ReadLine();
            Regex nameRegex = new Regex("[\\w\\d ]+");

            if (!nameRegex.IsMatch(name))
            {
                return EnterName();
            }

            return ConfirmName(name);
        }

        private static string ConfirmName(string name)
        {
            Tools.ConsoleClear();

            Console.WriteLine("Имя пользователя: " + name);
            Console.WriteLine("1. Далее");
            Console.WriteLine("0. Изменить");

            switch (Console.ReadLine())
            {
                case "0":
                    return EnterName();
                case "1":
                    return name;
                default:
                    return ConfirmName(name);
            }
        }

        private static string[] EnterSellerApi()
        {
            Tools.ConsoleClear();
            Console.WriteLine("Введите Client ID для Seller API:");
            string client_id = Console.ReadLine();

            Tools.ConsoleClear();
            Console.WriteLine("Введите API Key для Seller API:");
            string APIkey = Console.ReadLine();


            return ConfirmSellerApi([client_id, APIkey]);
        }

        private static string[] ConfirmSellerApi(string[] strings)
        {
            Tools.ConsoleClear();

            Console.WriteLine("Данные для Seller API");
            Console.WriteLine("Client ID: " + strings[0]);
            Console.WriteLine("API Key: " + strings[1]);

            Console.WriteLine("1. Далее");
            Console.WriteLine("0. Изменить");

            switch (Console.ReadLine())
            {
                case "0":
                    return EnterSellerApi();
                case "1":
                    return strings;
                default:
                    return ConfirmSellerApi(strings);
            }
        }

        private static string[] EnterPerformanceApi()
        {
            Tools.ConsoleClear();
            Console.WriteLine("Введите Client ID для Performance API:");
            string client_id = Console.ReadLine();

            Tools.ConsoleClear();
            Console.WriteLine("Введите Client Secret для Performance API:");
            string APIkey = Console.ReadLine();


            return ConfirmPerformanceApi([client_id, APIkey]);
        }

        private static string[] ConfirmPerformanceApi(string[] strings)
        {
            Tools.ConsoleClear();

            Console.WriteLine("Данные для Performance API");
            Console.WriteLine("Client ID: " + strings[0]);
            Console.WriteLine("Client Secret: " + strings[1]);

            Console.WriteLine("1. Далее");
            Console.WriteLine("0. Изменить");

            switch (Console.ReadLine())
            {
                case "0":
                    return EnterSellerApi();
                case "1":
                    return strings;
                default:
                    return ConfirmPerformanceApi(strings);
            }
        }
    }
}