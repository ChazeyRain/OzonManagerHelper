using Microsoft.Extensions.Logging;

namespace UI
{

    public class Tools
    {
        public class ConsoleSpiner
        {
            private static int counter = 0;

            internal static void Turn()
            {
                try
                {
                    counter++;
                    counter = counter % 4;
                    switch (counter)
                    {
                        case 0: Console.Write(".   "); break;
                        case 1: Console.Write("..  "); break;
                        case 2: Console.Write("... "); break;
                        case 3: Console.Write("...."); break;
                    }
                    Thread.Sleep(100);
                    Console.SetCursorPosition(Console.CursorLeft - 4, Console.CursorTop);
                }
                catch
                {

                }

            }
        }

        public static void ConsoleClear()
        {
            if (!Config.logger.IsEnabled(LogLevel.Information))
            {
                Console.Clear();
                Console.WriteLine("\x1b[3J");
            }

        }

        public static void Loading(Thread thread)
        {
            Console.Write("Loading:");

            thread.Start();

            while (thread.IsAlive)
            {
                UI.Tools.ConsoleSpiner.Turn();
            }
        }
    }
}