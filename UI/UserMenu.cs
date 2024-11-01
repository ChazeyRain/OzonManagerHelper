using Engine.UserController;

namespace UI
{
    public class UserMenu
    {
        public static void Display(User user)
        {
            try
            {
                Tools.ConsoleClear();
                Console.WriteLine("1. Редактировать пользователя");
                Console.WriteLine("2. Выгрузить отчёт");
                Console.WriteLine("0. Выход");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        UserSettings.Display(user);
                        break;
                    case "2":
                        ReportMenu.Display(user);
                        break;
                    case "0":
                        return;
                }
                Display(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                System.Environment.Exit(-1);
            }
        }
    }
}