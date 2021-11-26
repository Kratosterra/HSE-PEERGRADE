using System;
using System.Linq;

namespace Peergrade003
{
    /// <summary>
    /// Основной класс, в котором содержится скелет программы.
    /// </summary>
    class FileManager
    {

        /// <summary>
        /// Вход в программу.
        /// </summary>
        static void Main()
        {
            // Вызов метода файлового менеджера.
            FileManagerApp();
        }

        /// <summary>
        /// Метод создания скелета программы.
        /// </summary>
        private static void FileManagerApp()
        {
            // Создаем переменую для хранения клавиши.
            ConsoleKeyInfo exitKey;
            FileManagerStart();
            string nowDirectory = ManageTools.SetStartDrive();
            string nowDrive = ManageTools.SetStartDrive();
            // Реализуем повтор программы.
            do
            {
                try
                {
                    // Входим в исполнитель файлового менеджера.
                    FileManagerExecutor(ref nowDirectory, ref nowDrive);
                    // Выводим инструкции по выходу из программы.
                    Environment.EndProgram();
                    // Получаем нажатую кнопку.
                    exitKey = Console.ReadKey();
                    Console.Clear();
                }
                catch
                {
                    // Если ты сюда попал(-а) - ты умница, профи, хакер! Моё уважение и респект! 
                    Environment.GreatError();
                    Environment.EndProgram();
                    exitKey = Console.ReadKey();
                }
            } while (exitKey.Key != ConsoleKey.Escape);
            Environment.ShowLastMessageFirst();
        }

        /// <summary>
        /// Метод, исполняющий действия файлового менеджера.
        /// </summary>
        /// <param name="nowDirectory"> Ссылка на строку, означающую директорию.</param>
        /// <param name="nowDrive">Ссылка на строку, означающую диск.</param>
        /// <returns>
        /// </returns>
        private static void FileManagerExecutor(ref string nowDirectory, ref string nowDrive)
        {
            // Если директория пустая - устанавливаем входную точку.
            if (nowDirectory == null || nowDrive == null)
            {
                nowDirectory = ManageTools.SetStartDrive();
                nowDrive = ManageTools.SetStartDrive();
            }
            // Если директория всё еще пустая - невозможно получить доступ к дискам.
            if (nowDirectory == null || nowDrive == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("На вашем копьютере не обнаружено доступных дисков," +
                    " пожалуйста проверьте права доступа в системе!");
                Console.ResetColor();
            }
            else
            {
                // Начинаем выполнение действий.
                FileManagerActions(nowDirectory, nowDrive, out nowDirectory, out nowDrive);
            }
        }

        /// <summary>
        /// Метод, выводящий начальные данные на экран.
        /// </summary>
        private static void FileManagerStart()
        {
            // Вызываем заставку и паузу с запросом нажатия кнопки.
            Environment.SplashScreen();
            Environment.Service("stop_button");
            // Выводим инструкции и паузу с запросом нажатия кнопки.
            Environment.Instructions();
            Environment.Service("stop_button");
        }

        /// <summary>
        /// Метод, выводящий главное меню и проверяющий запуск действия менджера.
        /// </summary>
        /// <param name="nowDirectoryOld">Строка означающая путь до текущей директории.</param>
        /// <param name="nowDriveOld">Строка означающая путь до текущего диска.</param>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <param name="nowDrive">Путь до нового текущего диска.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        private static bool FileManagerActions(string nowDirectoryOld, string nowDriveOld,
            out string nowDirectory, out string nowDrive)
        {
            // Выводим информацию о данном диске и выводим меню.
            Environment.NowDirectoryInfo(nowDirectoryOld, nowDriveOld);
            Environment.MainMenu();
            FileManagerChoiceActions(out ushort actionChoice);
            // Начинаем выбор действий.
            if (Actions.EngageActionSelection(actionChoice, nowDirectoryOld, nowDriveOld,
                out nowDirectory, out nowDrive))
            {
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nОбнаружена ошибка или аварийный выход из действия!");
                Console.ResetColor();
                return false;
            }
        }

        /// <summary>
        /// Метод, выводящий запрос на выбор действия.
        /// </summary>
        /// <param name="actionChoice">Число, означающее выбор текущего действия из меню.</param>
        private static void FileManagerChoiceActions(out ushort actionChoice)
        {
            string actionChoiceData;
            // До того момента пока пользователь не введет подходящее действие, просим ввести его снова. 
            do
            {
                Console.Write("Выберите действие из меню: ");
                actionChoiceData = Console.ReadLine();

            } while (!Input.GetAction(actionChoiceData, out actionChoice));
        }

    }
}
