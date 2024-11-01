using Engine.UserController;

namespace UI
{
    public class ReportMenu
    {
        public static void Display(User user)
        {
            Tools.ConsoleClear();
            Console.WriteLine("1. Полный отчёт");
            Console.WriteLine("0. Назад");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    FullReportDate.Display(user);
                    break;
                case "0":
                    return;
            }
            Display(user);
        }
    }
}