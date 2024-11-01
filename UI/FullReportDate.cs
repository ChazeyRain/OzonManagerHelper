using Engine;
using Engine.UserController;
using Microsoft.Extensions.Logging;

namespace UI
{
    public class FullReportDate()
    {
        public static void Display(User user)
        {
            try
            {
                FullReport report = new FullReport(user);

                var startDate = EnterDate("Введите дату начала:");
                var endDate = EnterDate("Введите дату конца:");

                List<string> output = new List<string>();

                Thread thread = new Thread(() => {
                        output = report.Get(startDate, endDate);
                    });

                Tools.Loading(thread);
                Tools.ConsoleClear();
                var path = SaveToFile.SaveCsv(output);

                Console.WriteLine(Directory.GetCurrentDirectory().ToString() + path);
                Console.ReadKey();
            } 
            catch (Exception e)
            {
                Config.logger.LogCritical("Ошибка при создании отчёта");
                Config.logger.LogCritical(e.StackTrace);
                Console.ReadKey();
            }
        }

        private static DateTime EnterDate(string message)
        {
            Tools.ConsoleClear();

            Console.WriteLine(message);
            var input = Console.ReadLine();

            try
            {
                return DateTime.Parse(input);
            }
            catch
            {
                Console.WriteLine("Ошибка в формате даты. Пожалуйста вводите дату в формате yyyy-MM-dd.");
                Console.ReadKey();
                return EnterDate(message);
            }
        }
    }
}