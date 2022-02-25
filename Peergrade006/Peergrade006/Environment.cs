using System;
using System.IO;

namespace Peergrade006
{
    /// <summary>
    /// Класс, отвечающий за визуальное сопровождение работы программы.
    /// </summary>
    public static class Environment
    {
        /// <summary>
        /// Метод, выводящий общую ошибку, которая оказалась непойманной.
        /// </summary>
        public static void GreatError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Произошла ошибка при выполнении тестирования" +
                              ", вероятно проблема возникла в вашей файловой системе!");
            Console.ResetColor();
        }
        
        /// <summary>
        /// Метод вывода инструкций по выходу на экран.
        /// </summary>
        public static void EndProgram()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("                                                                                      \n");
            Console.Write("Для выхода нажмите Escape, чтобы продолжить работу, нажмите любую другую кнопку!");
            Console.ResetColor();
        }
        
        /// <summary>
        /// Метод вывода заставки на экран.
        /// </summary>
        public static void SplashScreen()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("────────────────────────────────────────────────────────────────────────" +
                              "───────────────────────────────────────────────");
            Console.WriteLine("                                         ▀█▀ █▀▀▄ ▄▀▄ █▄░█ ▄▀▀ █▀▄ ▄▀▄ █▀▀▄ ▀█▀ \n" +
                              "                                         ░█░ █▐█▀ █▀█ █░▀█ ░▀▄ █░█ █░█ █▐█▀ ░█░ \n" +
                              "                                         ░▀░ ▀░▀▀ ▀░▀ ▀░░▀ ▀▀░ █▀░ ░▀░ ▀░▀▀ ░▀░ \n" +
                              "────────────────────────────────  Лучший тест библиотеки за последние" +
                              " 0,00001 наносекунды  ────────────────────────────\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод вывода инструкций на экран.
        /// </summary>
        public static void Instructions()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Добро пожаловать в тестировочное консольное приложение Транспорт!\n" +
                              "Тут вы сможете проверить работу " +
                              "библиотеки EKRLib, генерация мощности транспорта происходит в заданном ТЗ диапазоне,\n" +
                              "а тип генерации моделей вы можете задать сами, можно генерировать всегда валидные модели или случайные." +
                              "\n" +
                              "Вы сможете задать режим генерации моделей, выбрав генерировать ли модель" +
                              " согласно ТЗ или нет.\n");
            Console.ResetColor();
        }
        
        /// <summary>
        /// Метод создания интерактивного интерфейса для пользователя.
        /// </summary>
        /// <param name="workType">Тип требуемых данных.</param>
        public static void Service(string workType)
        {
            switch (workType)
            {
                case "stop_button":
                    //Выводим запрос на нажатие клавиши, создаём искусственную паузу.
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Нажмите любую кнопку, чтобы продолжить!");
                    Console.ResetColor();
                    Console.ReadKey();
                    //Если пользователь решил нажать не Enter,
                    //мы все равно перезаписываем верхнюю строку на пустую.
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine("");
                    break;
            }
        }

        /// <summary>
        /// Метод, выводящий информацию о том, что генерации обьектов завершена.
        /// </summary>
        public static void GenerationFinished()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"============================================================\n" +
                              $"Генерация завершена, приступаем к записи в файлы.\n");
            Console.ResetColor();
        }
        
        /// <summary>
        /// Метод, выводящий начальные данные на экран.
        /// </summary>
        public static void StartTransportInterface()
        {
            // Вызываем заставку и паузу с запросом нажатия кнопки.
            SplashScreen();
            Service("stop_button");
            // Выводим инструкции и паузу с запросом нажатия кнопки.
            Instructions();
            Service("stop_button");
        }
        
        /// <summary>
        /// Метод, считывающий из приложенного файла конечную заставку.
        /// </summary>
        public static void ShowLastMessageFirst()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                string path =
                    $"..{Path.DirectorySeparatorChar}" +
                    $"..{Path.DirectorySeparatorChar}" +
                    $"..{Path.DirectorySeparatorChar}Environment" +
                    $"{Path.DirectorySeparatorChar}Environment_end.txt";
                using StreamReader sr = new(path);
                string line = sr.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    line = sr.ReadLine();
                }
                sr.Close();
                Console.ResetColor();
            }
            catch (Exception)
            {
                // Игнорируется
            }
            Console.ReadKey();
        }
    }
}