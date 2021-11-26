using System;
using System.IO;
using System.Text;

namespace Peergrade003
{
    /// <summary>
    /// Класс, содержащий крупные элементы интерфейса.
    /// </summary>
    class Environment
    {
        /// <summary>
        /// Метод вывода заставки на экран.
        /// </summary>
        public static void SplashScreen()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("────────────────────────────────────────────────────────────────────────" +
                              "───────────────────────────────────────────────");
            Console.WriteLine("                    ░▐█▀▀░▐██▒██░░░░▐█▀▀     ▒▐██▄▒▄██▌─░▄█▀▄─▒██▄░▒█▌─░▄" +
                              "█▀▄─░▐█▀▀▀─░▐█▀▀▒▐█▀▀▄\n" +
                              "                    ░▐█▀▀─░█▌▒██░░░░▐█▀▀     ░▒█░▒█░▒█░░▐█▄▄▐█▒▐█▒█▒█░░▐█▄▄" +
                              "▐█░▐█░▀█▌░▐█▀▀▒▐█▒▐█\n" +
                              "                    ░▐█──░▐██▒██▄▄█░▐█▄▄     ▒▐█░░░░▒█▌░▐█─░▐█▒██░▒██▌░▐█─░" +
                              "▐█░▐██▄█▌░▐█▄▄▒▐█▀▄▄\n" +
                              "──────────────────────────────── Лучший файловый менеджер за последние 0,00" +
                              "001 наносекунды ────────────────────────────\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод вывода инструкций на экран.
        /// </summary>
        public static void Instructions()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Добро пожаловать в маленький файловый менеджер!\nТут вы сможете оперировать файловой " +
                              "системой своего компьютера," +
                              " создавать, искать," +
                              " просматривать файлы, директории.\nВам также будет доступно удобное дополнение пути по" +
                              " клавише TAB.\n" +
                              "В любой момент вы можете выйти" +
                              " из действия прописав вместо пути к директории или файлу команду ?END?.\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Предупреждение для проверяющих вне .exe или стандартной консоли VisualStudio,\n" +
                              "будте готовы к визульными багам при вводе пути!\n" +
                              "В основном предупреждение касается проверяющих в среде Rider, в последних версиях " +
                              "в нем, к сожалению, сломана консоль.\n" +
                              "Проблема заключается в том, что консроль Rider'а" +
                              " неправильно исполняет смещение курсора и перезапись строки.\n" +
                              "И с этим ничего не поделаешь" +
                              ", ждём новых обновлений этой прекрасной среды разработки!\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод вывода главного меню на экран.
        /// </summary>
        public static void MainMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("| [1] Сменить диск\n" +
                              "| [2] Перейти в другую директорию\n" +
                              "| [3] Просмотр директории\n" +
                              "| [4] Вывод содержимого файла (4 кодировки)\n" +
                              "| [5] Копирование файла\n" +
                              "| [6] Перемещение файла\n" +
                              "| [7] Удаление файла\n" +
                              "| [8] Создание текстового файла (4 кодировки)\n" +
                              "| [9] Конкатенации двух и более файлов\n" +
                              "| [10] Поиск по маске\n" +
                              "| [11] Расширеный поиск по маске\n" +
                              "| [12] Копирование файлов по маске\n" +
                              "| [13] Сравнение двух текстовых файлов");
            Console.WriteLine("| [14] (EXTRA) Перейти к тестовой папке\n");
            Console.WriteLine("──────────────────────────────────────────────────────────────────────────" +
                              "─────────────────────────────────────────────\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод вывода меню методов кодировки на экран.
        /// </summary>
        public static void CodeDecodeMethodMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Выберете кодировку для считывания файла.\n");
            Console.WriteLine("| [1] UTF-8\n" +
                              "| [2] UTF-16\n" +
                              "| [3] ASCII\n" +
                              "| [4] UTF-32\n");
            Console.ResetColor();

        }

        /// <summary>
        /// Метод вывода меню опций копирования на экран.
        /// </summary>
        public static void CopyMethodMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Выберите что делать с файлами.\n");
            Console.WriteLine("| [1] Перезаписать файл\n" +
                              "| [2] Оставить без изменений\n");
            Console.ResetColor();

        }

        /// <summary>
        /// Метод, выводящий общую ошибку, которая оказалась непойманной.
        /// </summary>
        public static void GreatError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Произошла ошибка при выполнении операции" +
                              ", вероятно проблема возникла в вашей файловой системе!");
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
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
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
        /// Метод вывода инструкций по выходу на экран.
        /// </summary>
        public static void EndProgram()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("                                                                                      \n");
            Console.Write("Для выхода нажмите Escape, чтобы продолжить работу, нажмите любую другую кнопку!");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод, считывающий из приложенного файла конечную заставку.
        /// </summary>
        public static void ShowLastMessageFirst()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                using StreamReader sr = new(@"../../../Envitonment/Environment_end.txt");
                string line = sr.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line); line = sr.ReadLine();
                }
                sr.Close();
                Console.ResetColor();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        /// <summary>
        /// Метод, выводящий на экран информацию о текущей директории работы.
        /// </summary>
        /// <param name="nowDirectory">Путь до текущей директории работы.</param>
        public static void NowDirectoryInfo(string nowDirectory, string nowDrive)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("─────────────────────────────────────────────────────────────────────" +
                              "──────────────────────────────────────────────────");
            Console.WriteLine($"Текущий диск: {nowDrive}\nТекущая директория: {nowDirectory}");
            Console.WriteLine("──────────────────────────────────────────────────────────────────────" +
                              "─────────────────────────────────────────────────\n");
            Console.ResetColor();
        }
    }
}