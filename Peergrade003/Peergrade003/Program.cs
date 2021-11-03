using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Peergrade003
{
    /// <summary>
    /// Основной класс.
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
            // Если директория всё еще пустая - значит невозможно получить доступ к дискам.
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

    class Actions
    {
        /// <summary>
        /// Метод, запускающий действия в зависимости от полученого от пользователя числа.
        /// </summary>
        /// <param name="actionChoice">Число, означающее выбор действия, требуемого от менеджера.</param>
        /// <param name="nowDirectoryOld">Строка, означающая путь до текущей директории.</param>
        /// <param name="nowDriveOld">Строка означающая путь до текущего диска.</param>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <param name="nowDrive">Путь до нового текущего диска.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool EngageActionSelection(ushort actionChoice, string nowDirectoryOld, string nowDriveOld,
            out string nowDirectory, out string nowDrive)
        {
            // В зависимости от выбора пользователя начинаем исполнение соответствующего метода.
            nowDirectory = nowDirectoryOld;
            nowDrive = nowDriveOld;
            return actionChoice switch
            {
                1 => DriveSelection(out nowDirectory, out nowDrive),
                2 => ChangeDirectory(nowDirectoryOld, nowDrive, out nowDirectory),
                3 => ListOfFiles(nowDirectory, nowDrive),
                4 => OutputtingFileContent(nowDirectory, nowDrive),
                5 => CopyFile(nowDirectory, nowDrive),
                6 => FileTransfer(nowDirectory, nowDrive),
                7 => DeletingFile(nowDirectory, nowDrive),
                8 => CreatingTextFile(nowDirectory, nowDrive),
                9 => MergingMultipleFiles(nowDirectory, nowDrive),
                10 => SearchByMask(nowDirectory, nowDrive),
                11 => AdvancedSearchByMask(nowDirectory, nowDrive),
                12 => CopyByMask(nowDirectory, nowDrive),
                13 => DifferencesBetweenFiles(nowDirectory, nowDrive),
                14 => SetTestDirectory(out nowDrive, out nowDirectory),
                _ => false,
            };
        }

        /// <summary>
        /// Метод, сменяющий текущий диск.
        /// </summary>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <param name="nowDrive">Путь до нового текущего диска.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool DriveSelection(out string nowDirectory, out string nowDrive)
        {
            try
            {
                DriveSelectionEnvironment("start");
                int driveChoice;
                // Получаем все диски.
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                if (allDrives.Length < 1)
                {
                    DriveSelectionEnvironment("no_drives");
                    return ReturnErrorDirectory(out nowDirectory, out nowDrive);
                }
                // Запрашиваем выбор диска.
                int numDrives = DriveDisplay(allDrives);
                driveChoice = AskForDrive(numDrives);
                if (allDrives[driveChoice - 1].IsReady == false)
                {
                    DriveSelectionEnvironment("not_ready");
                    return ReturnErrorDirectory(out nowDirectory, out nowDrive);
                }
                DisplayMethodInformationDriveSelection(driveChoice, allDrives);
                // Устанавливаем диск.
                return ReturnNewDirectory(out nowDirectory, out nowDrive, driveChoice, allDrives);
            }
            catch
            {
                DriveSelectionEnvironment("error");
                return ReturnErrorDirectory(out nowDirectory, out nowDrive);
            }
        }

        /// <summary>
        /// Метод, устанавливающий диск и текущую директорию.
        /// </summary>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <param name="nowDrive">Путь до нового текущего диска.</param>
        /// <param name="driveChoice">Выбор пользователя в числовом формате.</param>
        /// <param name="allDrives">Индексатор, содержащий текущие диски в системе.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        private static bool ReturnNewDirectory(out string nowDirectory, out string nowDrive,
            int driveChoice, DriveInfo[] allDrives)
        {
            nowDirectory = (allDrives[driveChoice - 1].Name).ToString();
            nowDrive = (allDrives[driveChoice - 1].Name).ToString();
            return true;
        }

        /// <summary>
        /// Метод, показывающий установленный текущий диск пользователю.
        /// </summary>
        /// <param name="driveChoice">Выбор пользователя в числовом формате.</param>
        /// <param name="allDrives">Индексатор, содержащий текущие диски в системе.</param>
        private static void DisplayMethodInformationDriveSelection(int driveChoice, DriveInfo[] allDrives)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nПроизошла смена диска на {(allDrives[driveChoice - 1].Name).ToString()}.\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод, сменяющий текущий диск на null при ошибке.
        /// </summary>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <param name="nowDrive">Путь до нового текущего диска.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        private static bool ReturnErrorDirectory(out string nowDirectory, out string nowDrive)
        {
            nowDirectory = null;
            nowDrive = null;
            return false;
        }

        /// <summary>
        /// Метод, выводящий на эран требуемый тип интрефейса.
        /// </summary>
        /// <param name="workType">Строка, означающая требуемый тип интерфейса.</param>
        /// <returns>
        private static void DriveSelectionEnvironment(string workType)
        {
            switch (workType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n──────────────────────────────── Приступаем к смене диска.\n");
                    Console.ResetColor();
                    break;
                case "no_drives":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невозможно получить доступ к дискам, проверьте систему!");
                    Console.ResetColor();
                    break;
                case "not_ready":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Выбранный диск не готов к использованию!");
                    Console.ResetColor();
                    break;
                case "error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Неизвестная ошибка при выборе диска!");
                    Console.ResetColor();
                    break;
            }

        }

        /// <summary>
        /// Метод, запрашивающий выбор диска.
        /// </summary>
        /// <param name="numDrives">Число дисков в системе.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает число типа int, означающее номер нового диска.</item>
        /// </list>
        /// </returns>
        private static int AskForDrive(int numDrives)
        {
            int driveChoice;
            string driveData;
            // Пока пользователь не введёт номер существующего диска, запрашиваем ввод снова.
            do
            {
                Console.Write($"Введите номер диска от 1 до {numDrives} включительно: ");
                driveData = Console.ReadLine();
            } while (!Input.GetDrive(driveData, numDrives, out driveChoice));
            return driveChoice;
        }

        /// <summary>
        /// Метод, выводящий на экран все диски в системе с их порядковыми номерами.
        /// </summary>
        /// <param name="allDrives">Индексатор типа DriveInfo.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает число типа int, означающее колличество дисков в системе.</item>
        /// </list>
        /// </returns>
        private static int DriveDisplay(DriveInfo[] allDrives)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            int numDrives = 0;
            foreach (DriveInfo drive in allDrives)
            {
                numDrives += 1;
                Console.WriteLine($"{numDrives}. Диск {drive.Name}");
            }
            Console.ResetColor();
            return numDrives;
        }

        /// <summary>
        /// Метод, производящий смену текущей директории.
        /// </summary>
        /// <param name="nowDirectoryOld">Строка, содержащая путь до текущей директории.</param>
        /// <param name="nowDrive">Строка, содержащая информацию о текущем диске.</param>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool ChangeDirectory(string nowDirectoryOld, string nowDrive, out string nowDirectory)
        {
            try
            {
                // Выводим данные о директории.
                DisplayNowDirectory(nowDrive);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n──────────────────────────────── Приступаем к смене директории\n" +
                    "Если вы оказались в ситуации, когда на вашем диске нет директорий, пропишите ?END?" +
                    " или просто нажмите Enter.\n");
                Console.ResetColor();
                // Запрашиваем диреторию для смены.
                bool condition = Input.AskForDirectory(nowDrive, out string directoryPath);
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (condition) Console.WriteLine($"\nПроизошла смена директории с {nowDirectoryOld} " +
                    $"на {directoryPath}.\n");
                Console.ResetColor();
                nowDirectory = directoryPath;
                return condition;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при смене директории, проверьте права доступа!");
                Console.ResetColor();
                nowDirectory = null;
                return false;
            }
        }

        /// <summary>
        /// Метод, выводящий список файлов и директорий внутри рабочей директории.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="nowDrive">Строка, содержащая информацию о текущем диске работы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool ListOfFiles(string nowDirectory, string nowDrive)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Папки и файлы в директории {nowDirectory}\n");
            Console.ResetColor();
            try
            {
                DisplayNowDirectory(nowDirectory);
                return true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Не получается получить доступ к файлам в данной директории, вероятно," +
                    " система запрещает это делать!\n" +
                    "Смените диск или директорию в следующей итерации.");
                Console.ResetColor();
                return false;
            }
        }

        /// <summary>
        /// Метод, выводящий данные о директории на экран.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        private static void DisplayNowDirectory(string nowDirectory)
        {
            var options = new EnumerationOptions();
            options.IgnoreInaccessible = true;
            string[] dataFile = Directory.GetFiles(nowDirectory, "*", options);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n─────────────── Файлы\n");
            foreach (var item in dataFile)
            {
                Console.WriteLine($"Файл: {Path.GetFileName(item)}\t Путь: {item}");
            }
            string[] dataDir = Directory.GetDirectories(nowDirectory, "*", options);
            Console.WriteLine("\n─────────────── Папки\n");
            foreach (var item in dataDir)
            {
                Console.WriteLine($"Папка: {item}");
            }
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nВсе файлы и директории, кроме недоступных вам, выведены.\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод, выводящий выбранный пользователем текстовый файл на экран (поддерживаются файлы формата .txt
        /// и только размером до 10 МБ).
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="nowDrive">Строка, содержащая информацию о текущем диске работы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool OutputtingFileContent(string nowDirectory, string nowDrive)
        {
            DisplayNowDirectory(nowDirectory);
            OutputtingFileContentEnvironment("start", nowDirectory);
            try
            {
                // Запрашиваем файл.
                bool condition = Input.AskForFile(nowDirectory, true, out string filePath);
                if (!condition) return false;
                // Проверяем его размер.
                condition = Input.CheckSizeOfFile(filePath);
                if (!condition) return false;
                string encoding = Input.AskForEncoding();
                try
                {
                    // Выводим содержание файла.
                    DisplayFile(filePath, encoding);
                    return true;
                }
                catch
                {
                    OutputtingFileContentEnvironment("error", nowDirectory);
                    return false;
                }
            }
            catch
            {
                OutputtingFileContentEnvironment("big_error", nowDirectory);
                return false;
            }
        }

        /// <summary>
        /// Метод, выводящий информацию для пользователя.
        /// </summary>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <param name="workType">Тип вывода для пользователя.</param>
        private static void OutputtingFileContentEnvironment(string workType, string nowDirectory)
        {
            switch (workType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n──────────────────────────────── Вывод текстового файла из {nowDirectory}" +
                        $" на экран\nЕсли вы оказались в ситуации, когда в вашей директории нет файлов, пропишите ?END?.\n");
                    Console.ResetColor();
                    break;
                case "big_error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка при выводе содержания файла.");
                    Console.ResetColor();
                    break;
                case "error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невозможно прочитать файл, убедитесь что он доступен для чтения в" +
                        " предложенных кодировках. Возможно он занят другим процессом.");
                    Console.ResetColor();
                    break;
            }

        }

        /// <summary>
        /// Метод, выводящий файл на экран, получая путь и кодировку.
        /// </summary>
        /// <param name="filePath">Путь до файла, который следует вывести.</param>
        /// <param name="encoding">Метод кодировки в строковом представлении.</param>
        private static void DisplayFile(string filePath, string encoding)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── {Path.GetFileName(filePath)}\n");
            Console.ResetColor();
            using StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(encoding));
            string line = sr.ReadLine();
            while (line != null)
            {
                // Если в файле попалась очень длинная строка, ограничем её размер.
                if (line.Length > 60024)
                {
                    Console.WriteLine(line[..60024] + "... (задействованы ограничения размера одной строки)");
                }
                else
                {
                    Console.WriteLine(line);
                }
                line = sr.ReadLine();
            }
            sr.Close();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("───────────────────────────────────────────────────────────────────────────────────────" +
            "────────────────────────────────\n");
            Console.WriteLine($"\nФайл {Path.GetFileName(filePath)} прочитан!\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод, копирующий файл в указанную пользователем директорию.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="nowDrive">Строка, содержащая информацию о текущем диске работы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool CopyFile(string nowDirectory, string nowDrive)
        {
            DisplayNowDirectory(nowDirectory);
            CopyFileContentEnvironment("start", nowDirectory);
            try
            {
                CopyFileContentEnvironment("file", nowDirectory);
                // Запрашиваем путь до файла.
                bool conditionFirst = Input.AskForFile(nowDirectory, out string filePath);
                if (!conditionFirst) return false;
                CopyFileContentEnvironment("dir", nowDirectory);
                // Запрашиваем путь до директории.
                bool conditionSecond = Input.AskForDirectory(nowDrive, out string directoryPath);
                if (!conditionSecond) return false;
                // Если надо, изменяем название файла для копирования в ту же самую директорию.
                string destPointPath = CopyInSameDirectoryName(filePath, directoryPath);
                // Копируем.
                File.Copy(filePath, destPointPath, true);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nФайл {Path.GetFileName(filePath)} копирован в {directoryPath}.\n");
                Console.ResetColor();
                return true;
            }
            catch (Exception)
            {
                CopyFileContentEnvironment("big_error", nowDirectory);
                return false;
            }
        }

        /// <summary>
        /// Метод, выводящий информацию для пользователя.
        /// </summary>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <param name="workType">Тип вывода для пользователя.</param>
        private static void CopyFileContentEnvironment(string workType, string nowDirectory)
        {
            switch (workType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n──────────────────────────────── Копирование файла\nЕсли вы оказались в ситуации," +
                        $" когда на вашем диске нет директорий или в директории нет файлов, пропишите ?END?\n");
                    Console.ResetColor();
                    break;
                case "big_error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка при копировании файла в директорию.");
                    Console.ResetColor();
                    break;
                case "file":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nВведите файл, который хотите скопировать.\n");
                    Console.ResetColor();
                    break;
                case "dir":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nВведите директорию, в которую будет производится копирование.\n");
                    Console.ResetColor();
                    break;
            }

        }

        /// <summary>
        /// Метод, создающий новые названия для файлов, копированных в свою же директорию.
        /// </summary>
        /// <param name="filePath">Путь до файла, который следует копировать.</param>
        /// <param name="directoryPath">Путь до директории, в которую следует копировать.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает новое имя файла, включающее путь в который надо скопировать файл.</item>
        /// </list>
        /// </returns>
        private static string CopyInSameDirectoryName(string filePath, string directoryPath)
        {
            string destPointPath = $"{directoryPath}{Path.GetFileNameWithoutExtension(filePath)}" +
                $"{Path.GetExtension(filePath)}";
            // Если пути совпадают, изменяем название файла.
            if (Path.GetFullPath(filePath).ToLower() == Path.GetFullPath(destPointPath).ToLower())
            {
                int n = 1;
                destPointPath = $"{directoryPath}({n}) {Path.GetFileNameWithoutExtension(filePath)}" +
                    $"{Path.GetExtension(filePath)}";
                // Пока файл с указанным именем существует, устанавливаем новое название.
                while (File.Exists(destPointPath))
                {
                    n += 1;
                    destPointPath = $"{directoryPath}({n}) {Path.GetFileNameWithoutExtension(filePath)}" +
                        $"{Path.GetExtension(filePath)}";
                }
            }
            // Возвращаем готовый путь.
            return destPointPath;
        }

        /// <summary>
        /// Метод, перемещающий файл в указанную директорию.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="nowDrive">Строка, содержащая информацию о текущем диске работы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool FileTransfer(string nowDirectory, string nowDrive)
        {
            DisplayNowDirectory(nowDirectory);
            FileTransferEnvironment("start", nowDirectory);
            try
            {
                FileTransferEnvironment("file", nowDirectory);
                // Запрашиваем путь до файла.
                bool conditionFirst = Input.AskForFile(nowDirectory, out string filePath);
                if (!conditionFirst) return false;
                FileTransferEnvironment("dir", nowDirectory);
                // Запрашиваем директорию.
                bool conditionSecond = Input.AskForDirectory(nowDrive, out string directoryPath);
                if (!conditionSecond) return false;
                // Перемещаем.
                File.Move(filePath, $"{directoryPath}{Path.DirectorySeparatorChar}{Path.GetFileName(filePath)}", true);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nФайл {Path.GetFileName(filePath)} перенесён в {directoryPath}.\n");
                Console.ResetColor();
                return true;
            }
            catch
            {
                FileTransferEnvironment("big_error", nowDirectory);
                return false;
            }
        }

        /// <summary>
        /// Метод, выводящий информацию для пользователя.
        /// </summary>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <param name="workType">Тип вывода для пользователя.</param>
        private static void FileTransferEnvironment(string workType, string nowDirectory)
        {
            switch (workType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n──────────────────────────────── Перенос файла\n" +
                        $"Если вы оказались в ситуации, когда на вашем диске нет директорий или в директории нет файлов," +
                        $" пропишите ?END?\n");
                    Console.ResetColor();
                    break;
                case "big_error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка при переносе файла в директорию.");
                    Console.ResetColor();
                    break;
                case "file":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nВведите файл, который хотите перенести.\n");
                    Console.ResetColor();
                    break;
                case "dir":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nВвёдите директорию, в которую будет производится перенос.\n");
                    Console.ResetColor();
                    break;
            }

        }

        /// <summary>
        /// Метод, удаляющий файл по указанному пути.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="nowDrive">Строка, содержащая информацию о текущем диске работы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool DeletingFile(string nowDirectory, string nowDrive)
        {
            DisplayNowDirectory(nowDirectory);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Удаление файла из {nowDirectory}\n" +
                $"\nЕсли вы оказались в ситуации, когда в вашей директории нет файлов, пропишите ?END?.\n");
            Console.ResetColor();
            try
            {
                // Запрашиваем путь до файла. 
                bool condition = Input.AskForFile(nowDirectory, out string filePath);
                if (!condition) return false;
                // Производим удаление.
                File.Delete(filePath);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nФайл по {filePath} удалён\n");
                Console.ResetColor();
                return true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при удалении файла.");
                Console.ResetColor();
                return false;
            }
        }

        /// <summary>
        /// Метод, создающий новый текстовый файл или перезаписывающий существующий.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="nowDrive">Строка, содержащая информацию о текущем диске работы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool CreatingTextFile(string nowDirectory, string nowDrive)
        {
            CreatingTextFileEnvironment("start", nowDirectory);
            try
            {
                string encoding = Input.AskForEncoding();
                string filePath, data;
                // Получаем информацию для создания файлов.
                GetDataForFileCreation(nowDirectory, out filePath, out data);
                try
                {
                    // Записываем полученные данные.
                    StreamWriter sw = new StreamWriter(filePath + ".txt", false, Encoding.GetEncoding(encoding));
                    sw.Write(data);
                    sw.Close();
                }
                catch
                {
                    CreatingTextFileEnvironment("error", nowDirectory);
                    return false;
                }
                DisplayCreatingTextFileInfo(filePath);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CreatingTextFileEnvironment("big_error", nowDirectory);
                return false;
            }
        }


        /// <summary>
        /// Метод, отображающий информацию о том, что файл успешно создан.
        /// </summary>
        /// <param name="filePath"> Строка, содержащая путь до созданного файла.</param>
        private static void DisplayCreatingTextFileInfo(string filePath)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nФайл {Path.GetFileName(filePath)} успешно создан.\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод, выводящий информацию для пользователя по поводу создания текстового файла.
        /// </summary>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <param name="workType">Тип вывода для пользователя.</param>
        private static void CreatingTextFileEnvironment(string workType, string nowDirectory)
        {
            switch (workType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n──────────────────────────────── Создание/перезапись текстового файла в" +
                        $" {nowDirectory}\n");
                    Console.ResetColor();
                    break;
                case "big_error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка при создании файла.");
                    Console.ResetColor();
                    break;
                case "error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Возникла ошибка при записи тескста в файл.");
                    Console.ResetColor();
                    break;
            }
        }

        /// <summary>
        /// Метод, получающий данные для создания или перезаписи файла.
        /// </summary>
        /// <param name="nowDirectory">Строка, содержащая информацию о текущей директории.</param>
        /// <param name="">.</param>
        private static void GetDataForFileCreation(string nowDirectory, out string filePath, out string data)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nВыберем имя файла.\n");
            Console.ResetColor();
            // Получим имя файла.
            Input.AskForCreationFileName(nowDirectory, out filePath);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nВведём текст файла.\n");
            Console.ResetColor();
            // Получим текст для создания файла.
            Input.AskForTextCreationFIle(out data);
        }

        /// <summary>
        /// Метод, выводящий несколько соединённых текстовых файлов в консоль.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="nowDrive">Строка, содержащая информацию о текущем диске работы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool MergingMultipleFiles(string nowDirectory, string nowDrive)
        {
            DisplayNowDirectory(nowDirectory);
            MergingMultipleFilesEnvironment("start", nowDirectory);
            try
            {
                Input.AskForNumFiles(out int filesNum);
                string[] data = new string[filesNum];
                // Запрашиваем каждый файл.
                for (int i = 0; i < filesNum; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nВводим файл [{i + 1}]\n");
                    Console.ResetColor();
                    bool condition = Input.AskForFile(nowDirectory, true, out string filePath);
                    if (!condition) return false;
                    condition = Input.CheckSizeOfFile(filePath);
                    if (!condition) return false;
                    data[i] = filePath;
                }
                // Выводим полученные файлы.
                DisplayAndWriteFiles(filesNum, data);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MergingMultipleFilesEnvironment("big_error", nowDirectory);
                return false;
            }
        }

        /// <summary>
        /// Метод, выводящий информацию для пользователя по поводу обьеденения файлов.
        /// </summary>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <param name="workType">Тип вывода для пользователя.</param>
        private static void MergingMultipleFilesEnvironment(string workType, string nowDirectory)
        {
            switch (workType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n──────────────────────────────── Обьединение текста нескольких файлов\n" +
                        $"\nЕсли вы оказались в ситуации, когда в вашей директории нет файлов, пропишите ?END? при выборе " +
                        $"пути.\nВы также можете складывать один файл несколько раз, если вам так нужно.\n" +
                        $"Cохранение результата будет в первом введённом файле.\n");
                    Console.ResetColor();
                    break;
                case "big_error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка при складывании файлов." +
                        "\nВозможно один из файлов недоступен для чтения или используется другим процессом!");
                    Console.ResetColor();
                    break;
            }
        }

        /// <summary>
        /// Метод, выводящий заданные текстовые файлы в консоль.
        /// </summary>
        /// <param name="filesNum">Количество файлов.</param>
        /// <param name="data">Пути до всех файлов, которые следует вывести.</param>
        private static void DisplayAndWriteFiles(int filesNum, string[] data)
        {
            DisplayInfoNumOfFiles(filesNum);
            string pathWrite = data[0];
            for (int i = 0; i < data.Length; i++)
            {
                using StreamReader sr = new StreamReader(data[i]);
                string line = sr.ReadLine();
                while (line != null)
                {
                    if (line.Length > 1024)
                    {
                        Console.WriteLine(line[..1024]);
                    }
                    else
                    {
                        Console.WriteLine(line);
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
                if (i != 0)
                {
                    File.AppendAllText(pathWrite, File.ReadAllText(data[i]));
                }
            }
            DisplayResaultOfWriteFiles(data);
        }

        /// <summary>
        /// Метод, выводящий результат записывания файла при обьединении.
        /// </summary>
        /// <param name="data">Список файлов для обьединении.</param>
        private static void DisplayResaultOfWriteFiles(string[] data)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"─────────────────────────────────────────────────────────────────────────────────" +
                $"──────────────────────────────────────\n───── Результат записан в {Path.GetFileName(data[0])}\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод, выводящий результат записывания файла при обьединении.
        /// </summary>
        /// <param name="filesNum">Количество обьеденяемых файлов.</param>
        private static void DisplayInfoNumOfFiles(int filesNum)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Сложение {filesNum} файлов\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод, производящий поиск по маске в текущей директории.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="nowDrive">Строка, содержащая информацию о текущем диске работы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool SearchByMask(string nowDirectory, string nowDrive)
        {
            DisplayNowDirectory(nowDirectory);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Поиск по маске в {nowDirectory}\n\n");
            Console.ResetColor();
            try
            {
                return TryToSearchByMask(nowDirectory, false);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\nФайлов не найдено\n");
                Console.ResetColor();
                return true;
            }
        }

        /// <summary>
        /// Метод, производящий поиск по маске в текущей директории и всех её поддиректориях.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="nowDrive">Строка, содержащая информацию о текущем диске работы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool AdvancedSearchByMask(string nowDirectory, string nowDrive)
        {
            DisplayNowDirectory(nowDirectory);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Расширенный поиск по маске в {nowDirectory}\n" +
                $"\n");
            Console.ResetColor();
            try
            {
                return TryToSearchByMask(nowDirectory, true);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("\nФайлов не найдено\n");
                Console.ResetColor();
                return true;
            }
        }

        /// <summary>
        /// Метод, проводящий поиск по маске с заданными значениями.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="NeedRecurse">Нужно ли искать в поддиректориях текущей директорий.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        private static bool TryToSearchByMask(string nowDirectory, bool NeedRecurse)
        {
            // Запрашиваем маску для поиска.
            Input.AskForMask(out string mask);
            var options = new EnumerationOptions();
            options.RecurseSubdirectories = NeedRecurse;
            // Получаем подходящие файлы.
            string[] dataFile = Directory.GetFiles(nowDirectory, mask, options);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            if (dataFile.Length == 0)
            {
                Console.WriteLine("\nФайлов не найдено\n");
            }
            else
            {
                // Выводим данные о найденных файлах.
                foreach (var item in dataFile)
                {
                    Console.WriteLine($"Файл: {Path.GetFileName(item)}\t Путь: {item}");
                }
            }
            Console.ResetColor();
            return true;
        }

        /// <summary>
        /// Метод, копирующий всей файлы из текущей директории и всех её поддиректорий по заданной маске.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="nowDrive">Строка, содержащая информацию о текущем диске работы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool CopyByMask(string nowDirectory, string nowDrive)
        {
            DisplayNowDirectory(nowDirectory);
            try
            {
                GetDataForCopyByMask(out bool isRewriteNeeded, out string mask);
                bool condition = Input.AskForDirectory(nowDrive, out string directoryPath, true);
                if (!condition) return false;
                string[] dataFile = FindAllFilesForCopyMask(nowDirectory, mask);
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (dataFile.Length == 0) Console.WriteLine("\nФайлов не найдено\n");
                else
                {
                    // Копируем все файлы.
                    foreach (var item in dataFile)
                    {

                        CopyItemByMask(isRewriteNeeded, directoryPath, item);

                    }
                }
                Console.ResetColor();
                return true;
            }
            catch
            {
                CopyByMaskEnvironment("main_error");
                return true;
            }
        }

        /// <summary>
        /// Метод, проводящий специализированный поиск по маске.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="mask">Содержит маску, полученную от пользователя.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает массив строк, содержащих пути к найденным файлам.</item>
        /// </list>
        /// </returns>
        private static string[] FindAllFilesForCopyMask(string nowDirectory, string mask)
        {
            var options = new EnumerationOptions();
            options.RecurseSubdirectories = true;
            string[] dataFile = Directory.GetFiles(nowDirectory, mask, options);
            return dataFile;
        }

        /// <summary>
        /// Метод, производящий копирование в указанную диреторию.
        /// </summary>
        /// <param name="isRewriteNeeded">Bool значение, означающее нужно ли перезаписывать файлы с
        /// таким же именем или нет.</param>
        /// <param name="directoryPath">Путь до директории, в которую следует провести копирование.</param>
        /// <param name="">Путь до обьекта, который следует копировать.</param>
        private static void CopyItemByMask(bool isRewriteNeeded, string directoryPath, string item)
        {
            string destPointPath = CopyInSameDirectoryName(item, directoryPath);
            try
            {
                File.Copy(item, destPointPath, isRewriteNeeded);
            }
            catch (System.IO.IOException)
            {
                Console.WriteLine($"Файл: {Path.GetFileName(item)}\t отмена копирования! Файл уже существует." +
                    $"Согласно заданым параметрам файл не будет тронут.");
                return;
            }
            catch
            {
                Console.WriteLine($"Файл: {Path.GetFileName(item)}\t Возникла неизвестная ошибка!");
                return;
            }
            Console.WriteLine($"Файл: {Path.GetFileName(item)}\t копирован в {directoryPath}");
        }

        /// <summary>
        /// Метод, получающий начальную информацию для копирования по маске.
        /// </summary>
        /// <param name="isRewriteNeeded">Выходной параметр, означающий согласие или несогласие на пере
        /// запись файла.</param>
        /// <param name="mask">Маска поиска, полученная от пользователя.</param>
        private static void GetDataForCopyByMask(out bool isRewriteNeeded, out string mask)
        {
            CopyByMaskEnvironment("start");
            Input.AskForCopyMethod(out isRewriteNeeded);
            Input.AskForMask(out mask);
            CopyByMaskEnvironment("ask_directory");
        }

        /// <summary>
        /// Метод, содержащий элементы интерфейса для копирования по маске.
        /// </summary>
        /// <param name="workType">Строка означающая, что следует вывести на экран пользователю.</param>
        private static void CopyByMaskEnvironment(string workType)
        {
            switch (workType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n──────────────────────────────── Копирование по маске\n" +
                        $"Если хотите отменить действие, во время ввода пути до директории копирования напишите ?END?\n");
                    Console.ResetColor();
                    break;
                case "ask_directory":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nВвёдите директорию, в которую будет производится копирование.\n");
                    Console.ResetColor();
                    break;
                case "main_error":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Нужных файлов не найдено");
                    Console.ResetColor();
                    break;
                case "error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Неизвестная ошибка при копировании!");
                    Console.ResetColor();
                    break;
            }
        }

        /// <summary>
        /// Метод, выводящий на экран разницу между двумя текстовыми файлами.
        /// </summary>
        /// <param name="nowDirectory"> Строка, содержащая путь до текущей директории работы программы.</param>
        /// <param name="nowDrive">Строка, содержащая информацию о текущем диске работы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool DifferencesBetweenFiles(string nowDirectory, string nowDrive)
        {
            DisplayNowDirectory(nowDirectory);
            DifferencesBetweenFilesEnvironment("start", nowDirectory);
            try
            {
                // Запрашиваем файл.
                DifferencesBetweenFilesEnvironment("file_old", nowDirectory);
                bool condition = Input.AskForFile(nowDirectory, true, out string filePathFirst);
                if (!condition) return false;
                condition = Input.CheckSizeOfFile(filePathFirst);
                if (!condition) return false;
                // Запрашиваем файл.
                DifferencesBetweenFilesEnvironment("file_new", nowDirectory);
                condition = Input.AskForFile(nowDirectory, true, out string filePathSecond);
                if (!condition) return false;
                condition = Input.CheckSizeOfFile(filePathSecond);
                if (!condition) return false;
                try
                {
                    SetDiffOutput(filePathFirst, filePathSecond);
                    return true;
                }
                catch
                {
                    DifferencesBetweenFilesEnvironment("error", nowDirectory);
                    return false;
                }
            }
            catch
            {
                DifferencesBetweenFilesEnvironment("big_error", nowDirectory);
                return false;
            }

        }

        private static void SetDiffOutput(string filePathFirst, string filePathSecond)
        {
            string[] firstFile = File.ReadAllLines(filePathFirst);
            string[] secondFile = File.ReadAllLines(filePathSecond);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Результат\n");
            Console.ResetColor();
            foreach (var item in CreateDiffDataString(
                FindLongestCommonSubsequenceSubMatrix(firstFile, secondFile),
                firstFile, secondFile, firstFile.Length, secondFile.Length).Split("\n"))
            {
                SetColorMaskForDiffText(item);
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("───────────────────────────────────────────────────────────────────────────────────────" +
            "────────────────────────────────\n");
            Console.ResetColor();
        }

        private static void SetColorMaskForDiffText(string item)
        {
            if (item.Length != 0)
            {
                if (item[0] != ' ')
                {
                    if (item[0] == '=')
                    {
                        Console.WriteLine(item[1..]);
                    }
                    else if (item[0] == '+')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(item);
                        Console.ResetColor();
                    }
                    else if (item[0] == '-')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(item);
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                Console.WriteLine(item);
            }
        }

        private static void DifferencesBetweenFilesEnvironment(string workType, string nowDirectory)
        {
            switch (workType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n──────────────────────────────── Вывод разницы между двумя файлами" +
                        $" на экран\nЕсли вы оказались в ситуации, когда в вашей директории нет файлов, пропишите ?END?.\n");
                    Console.ResetColor();
                    break;
                case "big_error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка при получении данных, возможно файл занят другим процессом.");
                    Console.ResetColor();
                    break;
                case "error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невозможно прочитать файлы, возможно они заняты другим процессом.");
                    Console.ResetColor();
                    break;
                case "file_old":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nВведите путь до старой версии файла.\n");
                    Console.ResetColor();
                    break;
                case "file_new":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nВведите путь до новой версии файла.\n");
                    Console.ResetColor();
                    break;
            }

        }

        private static int[,] FindLongestCommonSubsequenceSubMatrix(string[] fileOld, string[] fileNew)
        {
            int[,] subMatrix = new int[fileOld.Length + 1, fileNew.Length + 1];
            for (int i = 1; i <= fileOld.Length; i++)
            {
                for (int j = 1; j <= fileNew.Length; j++)
                {
                    if (fileOld[i - 1] == fileNew[j - 1])
                    {
                        subMatrix[i, j] = subMatrix[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        if (subMatrix[i - 1, j] > subMatrix[i, j - 1])
                        {
                            subMatrix[i, j] = subMatrix[i - 1, j];
                        }
                        else
                        {
                            subMatrix[i, j] = subMatrix[i, j - 1];
                        }
                    }
                }
            }
            return subMatrix;
        }

        private static string CreateDiffDataString(int[,] subMatrix, string[] fileOld, string[] fileNew, int i, int j)
        {
            string dataAns = "";

            if (i > 0 && j > 0 && fileOld[i - 1] == fileNew[j - 1])
            {
                return $"{CreateDiffDataString(subMatrix, fileOld, fileNew, i - 1, j - 1)}={fileOld[i - 1]}\n";
            }
            else if (j > 0 && (i == 0 || (subMatrix[i, j - 1] > subMatrix[i - 1, j])))
            {
                return $"{CreateDiffDataString(subMatrix, fileOld, fileNew, i, j - 1)}+| {fileNew[j - 1]}\n";
            }
            else if (i > 0 && (j == 0 || (subMatrix[i, j - 1] <= subMatrix[i - 1, j])))
            {
                return $"{CreateDiffDataString(subMatrix, fileOld, fileNew, i - 1, j)}-| {fileOld[i - 1]}\n";
            }
            return $"{dataAns}\n";
        }

        /// <summary>
        /// Метод, сменяющий текущий диск и директорию на тестовую, если она есть.
        /// </summary>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <param name="nowDrive">Путь до нового текущего диска.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее статус выполнения.</item>
        /// </list>
        /// </returns>
        public static bool SetTestDirectory(out string nowDrive, out string nowDirectory)
        {
            try
            {
                if (!Directory.Exists(@"../../../../Test"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невозможно получить доступ к тестовой директории!");
                    Console.ResetColor();
                    return ReturnErrorDirectory(out nowDirectory, out nowDrive);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("Тестовая директория установлена!");
                    Console.ResetColor();
                    nowDirectory = Path.GetFullPath(@"../../../../Test") + Path.DirectorySeparatorChar;
                    nowDrive = Path.GetPathRoot(nowDirectory);
                    DisplayNowTestDirectory(nowDrive, nowDirectory);
                    return true;
                }

            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невозможно получить доступ к тестовой директории!");
                Console.ResetColor();
                return ReturnErrorDirectory(out nowDirectory, out nowDrive);
            }
        }

        /// <summary>
        /// Метод, показывающий текущую тестовую директорию и диск.
        /// </summary>
        /// <param name="nowDirectory">Путь до тестовой директории.</param>
        /// <param name="nowDrive">Путь до тестового диска.</param>
        private static void DisplayNowTestDirectory(string nowDrive, string nowDirectory)
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Текущий диск: {nowDrive}\nТекущая директория: {nowDirectory}");
            Console.ResetColor();
        }
    }

    class ManageTools
    {
        /// <summary>
        /// Метод, устанавливающий стартовый диск.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает строковое представление полученного диска.</item>
        /// </list>
        /// </returns>
        public static string SetStartDrive()
        {
            try
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                if (allDrives.Length < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невозможно получить доступ к дискам, проверьте систему!");
                    Console.ResetColor();
                    return null;
                }
                if (allDrives[0].IsReady == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невозможно получить доступ к первому диску, он не готов, проверьте систему!");
                    Console.ResetColor();
                    return null;
                }
                // Устанавливаем первый в системе диск, как точку входа.
                return allDrives[0].Name;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невозможно получить доступ к дискам, проверьте систему!");
                Console.ResetColor();
                return null;
            }
        }

        /// <summary>
        /// Удаляет все символы, которые не разрешены в именах файлов.
        /// </summary>
        /// <param name="fileName"> Имя файла, введенное пользователем.</param>
        /// <returns></returns>
        public static string RemoveInvalidChars(string fileName)
        {
            foreach (char invalid_char in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(oldValue: invalid_char.ToString(), newValue: "");
            }
            return fileName;
        }

        /// <summary>
        /// Метод, проверяющий файл на его принадлежность к текстовым файлам.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее является ли файл текстовым.</item>
        /// </list>
        /// </returns>
        public static bool IsFIleTxt(string filePath)
        {
            if (Path.GetExtension(filePath) == ".txt")
            {
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Файл не является текстовым (не в формате .txt)!");
                Console.ResetColor();
                return false;
            }
        }

        /// <summary>
        /// Метод, разворачивающий строку.
        /// </summary>
        /// <param name="stringData">Строка.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает развёрнутую строку.</item>
        /// </list>
        /// </returns>
        public static string ReverseString(string stringData)
        {
            char[] arrString = stringData.ToCharArray();
            Array.Reverse(arrString);
            return new string(arrString);
        }

        /// <summary>
        /// Метод, обновляющий ввод пользователя, находя совпадения в файловой системе.
        /// </summary>
        /// <param name="rawPath">Ввод пользователя.</param>
        /// <param name="newPath">Обновлённый ввод.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее успех или провал в дополнении пути.</item>
        /// </list>
        /// </returns>
        public static bool AddSomePath(string rawPath, out string newPath)
        {
            try
            {
                FindAllDataForPath(rawPath, out EnumerationOptions options, out string rawPathTrim,
                    out int control, out string mask);
                if (control == 0)
                {
                    if (Directory.Exists(rawPathTrim) && rawPathTrim[^1] != Path.DirectorySeparatorChar)
                    {
                        newPath = rawPathTrim + Path.DirectorySeparatorChar;
                        return false;
                    }
                    return ReturnValidUserPath(out newPath, rawPathTrim);
                }
                FindPathVariation(options, rawPathTrim, mask, out string[] dataFile, out string[] dataDirect);
                if (dataFile.Length == 0 && dataDirect.Length == 0)
                {
                    return ReturnValidUserPath(out newPath, rawPathTrim);
                }
                else
                {
                    return CreateTruePath(out newPath, rawPathTrim, dataFile, dataDirect);
                }
            }
            catch (Exception)
            {
                return ReturnExceptionDataForAddPath(rawPath, out newPath);
            }
        }

        /// <summary>
        /// Метод, возвращающий валидное значение пути.
        /// </summary>
        /// <param name="rawPathTrim">Обрезанный до валидного путь.</param>
        /// <param name="newPath">Обновлённый ввод.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение равное false.</item>
        /// </list>
        /// </returns>
        private static bool ReturnValidUserPath(out string newPath, string rawPathTrim)
        {
            newPath = rawPathTrim;
            return false;
        }

        /// <summary>
        /// Метод, возвращающий при ошибке валидное значение пути.
        /// </summary>
        /// <param name="rawPathTrim">Обрезанный до валидного путь.</param>
        /// <param name="newPath">Обновлённый ввод.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение равное false.</item>
        /// </list>
        /// </returns>
        private static bool ReturnExceptionDataForAddPath(string rawPath, out string newPath)
        {
            TrimPathToVailidOne(rawPath, out string rawPathTrim, out _, out _);
            newPath = rawPathTrim;
            return false;
        }

        /// <summary>
        /// Метод, находящий все папки и файлы подходящие под ввод пользователя.
        /// </summary>
        /// <param name="options">Настройки поиска.</param>
        /// <param name="rawPathTrim">Обрезанный до валидного путь.</param>
        /// <param name="mask">Маска, основанная на вводе пользователя.</param>
        /// <param name="dataFile">Массив с файлами, найденными по вводу пользователя.</param>
        /// <param name="dataDirect">Массив с директориями, найденными по вводу пользователя.</param>
        private static void FindPathVariation(EnumerationOptions options, string rawPathTrim, string mask,
            out string[] dataFile, out string[] dataDirect)
        {
            dataFile = Directory.GetFiles(rawPathTrim, mask + "*", options);
            dataDirect = Directory.GetDirectories(rawPathTrim, mask + "*", options);
        }

        /// <summary>
        /// Метод, получающий и создающий всю информацию для достоения пути.
        /// </summary>
        /// <param name="options">Настройки поиска.</param>
        /// <param name="rawPathTrim">Обрезанный до валидного путь.</param>
        /// <param name="mask">Маска, основанная на вводе пользователя.</param>
        /// <param name="rawPath">Строка, содержащая ввод пользователя.</param>
        /// <param name="control">Количество обрезанных символов до получения валидного пути.</param>
        private static void FindAllDataForPath(string rawPath, out EnumerationOptions options, out string rawPathTrim,
            out int control, out string mask)
        {
            options = new EnumerationOptions();
            options.RecurseSubdirectories = false;
            TrimPathToVailidOne(rawPath, out rawPathTrim, out control, out mask);
            mask = ReverseString(mask);
        }

        /// <summary>
        /// Метод, создающий новый путь по вводу пользователя используя полученную маску.
        /// </summary>
        /// <param name="newPath">Доработанный путь.</param>
        /// <param name="rawPathTrim">Путь, который ввёл пользователь, обрезанный до существующего.</param>
        /// <param name="dataFile">Файлы, найденные по маске от пользователя.</param>
        /// <param name="">Директории, найденные по маске от пользователя.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее успех или провал в дополнении пути.</item>
        /// </list>
        /// </returns>
        private static bool CreateTruePath(out string newPath, string rawPathTrim, string[] dataFile,
            string[] dataDirect)
        {
            if (dataFile.Length == 0 && dataDirect.Length != 0)
            {
                if (dataDirect.Length == 1)
                {
                    newPath = dataDirect[0] + Path.DirectorySeparatorChar;
                    return true;
                }
                return CommonPath(out newPath, dataDirect);
            }
            if (dataFile.Length != 0)
            {
                if (dataFile.Length >= 2)
                {
                    if (Path.GetFileNameWithoutExtension(dataFile[0]) == Path.GetFileNameWithoutExtension(dataFile[1]))
                    {
                        newPath = $"{rawPathTrim}{Path.GetFileNameWithoutExtension(dataFile[0])}.";
                        return true;
                    }
                    return CommonPath(out newPath, dataFile);
                }
                else
                {
                    return ReturnDefaultDataForPathCreation(out newPath, dataFile);
                }
            }
            return ReturnDefaultDataForPathCreation(out newPath, dataFile);
        }

        /// <summary>
        /// Метод, устанавливающий первый файл в списке подходящих по маске.
        /// </summary>
        /// <param name="newPath">Доработанный путь.</param>
        /// <param name="dataFile">Файлы, найденные по маске от пользователя.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение равное false.</item>
        /// </list>
        /// </returns>
        private static bool ReturnDefaultDataForPathCreation(out string newPath, string[] dataFile)
        {
            newPath = dataFile[0];
            return false;
        }

        /// <summary>
        /// Метод, обрезающий ввод пользователя до существующего, а то что было обрезано возвращает в 
        /// качестве маски поиска.
        /// </summary>
        /// <param name="rawPath">Путь, введённый пользователем.</param>
        /// <param name="rawPathTrim">Доработанный путь, обрезан до существующего.</param>
        /// <param name="control">Число обрезанных символов.</param>
        /// <param name="mask">Обрезанные символы в качестве маски (порядок искажён).</param>
        private static void TrimPathToVailidOne(string rawPath, out string rawPathTrim, out int control,
            out string mask)
        {
            rawPathTrim = $"{rawPath}";
            control = 0;
            mask = "";
            // Пока директория не существует, мы будем обрезать путь, а обрезанное заносить в маску.
            while (!Directory.Exists(rawPathTrim))
            {
                if (rawPathTrim.Length == 0)
                {
                    break;
                }
                mask += rawPathTrim[^1];
                rawPathTrim = rawPathTrim[..^1];
                control += 1;
            }
        }

        /// <summary>
        /// Метод, находящий общий путь у нескольких путей.
        /// </summary>
        /// <param name="newPath">Общий путь у всех принятых путей.</param>
        /// <param name="dataFile">Массив путей.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает bool значение, означающее успех или провал в нахождении общего пути.</item>
        /// </list>
        /// </returns>
        private static bool CommonPath(out string newPath, string[] dataFile)
        {
            string commonPath = "";
            bool isPathValid = true;
            int indexChar = 0;
            // Сравниваем каждый символ из первого совпадения с другими, выделяя общий для всех путь.
            foreach (var charControl in dataFile[0])
            {
                foreach (var path in dataFile)
                {
                    if ($"{(path)[indexChar]}".ToLower() != $"{charControl}".ToLower())
                    {
                        isPathValid = false;
                        break;
                    }
                }
                if (!isPathValid) break;
                if (isPathValid)
                {
                    commonPath += charControl;
                    indexChar += 1;
                }
            }
            newPath = $"{commonPath}";
            return true;
        }

        /// <summary>
        /// Метод, заменяющий стандартный метод из пространства имён Система.
        /// </summary>
        /// <param name="nowDirectory">Текущая рабочая директория.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает ввод пользователя.</item>
        /// </list>
        /// </returns>
        public static string ReadLine(string nowDirectory)
        {
            ConsoleKeyInfo key;
            string data = "";
            while (true)
            {
                // Cчитываем клавишу.
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    return data;
                }
                else if (key.Key == ConsoleKey.Tab)
                {
                    data = TabMethod(nowDirectory, data);
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    data = BackspaceMethod(data);
                }
                else if (key.KeyChar == 10)
                {
                    return data;
                }
                else
                {
                    data = ReturnKeyCharInfo(key, data);
                }
            }
        }

        /// <summary>
        /// Метод, дополняющий строку нажатой пользователем кнопкой.
        /// </summary>
        /// <param name="key">Нажатая кнопка.</param>
        /// <param name="data">Дополненная информация о вводе пользователя.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает обработанный ввод пользователя.</item>
        /// </list>
        /// </returns>
        private static string ReturnKeyCharInfo(ConsoleKeyInfo key, string data)
        {
            // Если символ является для нас подходящим - записываем его.
            bool isCharValid = true;
            if ((int)key.KeyChar == 0 || (int)key.KeyChar == 10 || (int)key.KeyChar == 9)
                isCharValid = false;
            if (isCharValid)
            {
                data += key.KeyChar;
                Console.Write((key.KeyChar).ToString()[..^1]);
            }
            return data;
        }

        /// <summary>
        /// Метод, преобразовывающий текущий ввод пользователя согласно
        /// его файловой системе и его вводу, чтобы дополнить введённую строку.
        /// </summary>
        /// <param name="nowDirectory">Текущая директория работы.</param>
        /// <param name="">Текущий ввод пользователя.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает преобразованный путь до файла или папки.</item>
        /// </list>
        /// </returns>
        private static string TabMethod(string nowDirectory, string data)
        {
            // Вызываем метод дополнения пути.
            AddSomePath(nowDirectory + Path.DirectorySeparatorChar + data, out string newPath);
            newPath = newPath.Replace(nowDirectory + Path.DirectorySeparatorChar, "");
            data = newPath;
            ClearConsoleLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(data);
            Console.ResetColor();
            return data;
        }

        /// <summary>
        /// Метод, убирающий символ из строки.
        /// </summary>
        /// <param name="data">Текущая строка.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает новую обрезанную строку.</item>
        /// </list>
        /// </returns>
        private static string BackspaceMethod(string data)
        {
            ClearConsoleLine();
            if (data.Length != 0)
            {
                data = data[..^1];
            }
            Console.Write(data);
            return data;
        }

        /// <summary>
        /// Метод, очищающий строку консоли.
        /// </summary>
        private static void ClearConsoleLine()
        {
            Console.Write("\r" + new string(' ', Console.WindowWidth) + "\r");
        }
    }

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
            Console.WriteLine("Добро пожаловать в маленький файловый менеджер!\nТут вы сможете оперировать файловой системой своего компьютера," +
                " создавать, искать," +
                " просматривать файлы, директории.\nВам также будет доступно удобное дополнение пути по клавише TAB.\nВ любой момент вы можете выйти" +
                " из действия прописав вместо пути к директории или файлу команду ?END?.\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Предупреждение для проверяющих вне .exe или стандартной консоли VisualStudio,\nбудте готовы к визульными багам при вводе пути!\n" +
                "В основном предупреждение касается проверяющих в среде Rider, в последних версиях в нем, к сожалению, сломана консоль.\n" +
                "Проблема заключается в том, что консроль Rider'а неправильно исполняет смещение курсора и перезапись строки.\n" +
                "И с этим ничего не поделаешь, ждём новых обновлений этой прекрасной среды разработки!\n");
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
                "| [9] Конкотенация двух и более файлов\n" +
                "| [10] Поиск по маске\n" +
                "| [11] Расширеный поиск по маске\n" +
                "| [12] Копирование файлов по маске\n" +
                "| [13] Сравнение двух текстовых файлов");
            Console.WriteLine("| [14] (EXTRA) Перейти к тестовой папке\n");
            Console.WriteLine("───────────────────────────────────────────────────────────────────────────────────────────────────────────────────────\n");
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
            Console.WriteLine("ErrorGraet");
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

    class Input
    {
        /// <summary>
        /// Метод, возвращающий номер требуемого действия, полученного от пользователя.
        /// </summary>
        /// <param name="actionChoiceData">Строка, означающая ввод пользователя.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении числа!</item>
        /// <item>Число типа ushort, oзначающее выбор действия.</item>
        /// </list>
        /// </returns>
        public static bool GetAction(string actionChoiceData, out ushort actionChoice)
        {
            // Если введённое число не подходит под выбор действия - выводим ошибку.
            if (!ushort.TryParse(actionChoiceData, out actionChoice) || actionChoice == 0 || actionChoice > 14)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Такого действия не предусмотрено, пожалуйста повторите ввод!");
                Console.ResetColor();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Метод, возвращающий номер диска, выбранного пользователем.
        /// </summary>
        /// <param name="driveChoiceData">Строка, означающая ввод пользователя.</param>
        /// <param name="numDrives">Число доступных дисков.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении данных!</item>
        /// </list>
        /// </returns>
        public static bool GetDrive(string driveChoiceData, int numDrives, out int driveChoice)
        {
            if (!int.TryParse(driveChoiceData, out driveChoice) || driveChoice <= 0 || driveChoice > numDrives)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Диска с таким порядковым номером не существует, введите число от 1 до {numDrives}." +
                    "Пожалуйста повторите ввод!");
                Console.ResetColor();
                return false;
            }
            return true;

        }

        /// <summary>
        /// Метод, возвращающий путь до директории.
        /// </summary>
        /// <param name="directoryData">Строка, означающая ввод пользователя.</param>
        /// <param name="nowDrive">Текущий рабочий диск.</param>
        /// <param name="isDirectoryCreationNeeded">Нужно ли создавать новую директорию.</param>
        /// <param name="directoryPath">Путь до новой директории.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении данных!</item>
        /// </list>
        /// </returns>
        public static bool GetDirectoryPath(string directoryData, string nowDrive, out string directoryPath,
            bool isDirectoryCreationNeeded)
        {
            if (directoryData == null)
            {
                GetDirectoryPathEnvironment("error_null");
                directoryPath = null;
                return false;
            }
            if (Directory.Exists($"{nowDrive}{directoryData}"))
            {
                directoryPath = ClearDirectoryPath(directoryData, nowDrive);
                return true;
            }
            else if (!Directory.Exists($"{nowDrive}{directoryData}") && isDirectoryCreationNeeded)
            {
                return TryToCreateDirectory(directoryData, nowDrive, out directoryPath);
            }
            else
            {
                GetDirectoryPathEnvironment("not_exists");
                directoryPath = null;
                return false;
            }
        }

        /// <summary>
        /// Метод, производящий попытку создания директории по заданным данным.
        /// </summary>
        /// <param name="directoryData">Строка, означающая ввод пользователя.</param>
        /// <param name="nowDrive">Текущий рабочий диск.</param>
        /// <param name="directoryPath">Путь до новой директории.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении данных!</item>
        /// </list>
        /// </returns>
        private static bool TryToCreateDirectory(string directoryData, string nowDrive, out string directoryPath)
        {
            try
            {
                directoryPath = CreateNewDirectory(directoryData, nowDrive);
            }
            catch
            {
                GetDirectoryPathEnvironment("error_directory");
                directoryPath = null;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Метод, выводящий информацию для пользователя по поводу поиска и создания директории.
        /// </summary>
        /// <param name="workType">Тип вывода для пользователя.</param>
        private static void GetDirectoryPathEnvironment(string workType)
        {
            switch (workType)
            {
                case "error_null":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Старайтесь не вводить пустые строки!");
                    Console.ResetColor();
                    break;
                case "error_directory":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невозможно создать директорию по такому пути, пожалуйста, повторите ввод.");
                    Console.ResetColor();
                    break;
                case "not_exists":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Такой директории не существует, пожалуйста, повторите ввод.");
                    Console.ResetColor();
                    break;
            }
        }

        /// <summary>
        /// Метод, производящий доработку пути до системно-корректного.
        /// </summary>
        /// <param name="directoryData">Путь до директории.</param>
        /// <param name="nowDrive">Текущий диск системы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает строку с обработанным путём до директории.</item>
        /// </list>
        /// </returns>
        private static string ClearDirectoryPath(string directoryData, string nowDrive)
        {
            string directoryPath;
            string workPath = Path.GetFullPath($"{nowDrive}{directoryData}");
            if (workPath[^1] != Path.DirectorySeparatorChar)
                workPath = workPath + Path.DirectorySeparatorChar;
            directoryPath = workPath;
            return directoryPath;
        }

        /// <summary>
        /// Метод, создающий новую директорию.
        /// </summary>
        /// <param name="directoryData">Путь до директории.</param>
        /// <param name="nowDrive">Текущий диск системы.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает путь до новой директории.</item>
        /// </list>
        /// </returns>
        private static string CreateNewDirectory(string directoryData, string nowDrive)
        {
            string directoryPath;
            Console.WriteLine($"{directoryData}");
            string workPath = Path.GetFullPath($"{nowDrive}{directoryData}");
            if (workPath[^1] != Path.DirectorySeparatorChar)
                workPath = workPath + Path.DirectorySeparatorChar;
            directoryPath = workPath;
            Directory.CreateDirectory(directoryPath);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nСоздана директория {directoryPath}\n");
            Console.ResetColor();
            return directoryPath;
        }

        /// <summary>
        /// Метод, запрашивающий путь до новой директории.
        /// </summary>
        /// <param name="nowDrive">Текущий рабочий диск.</param>
        /// <param name="directoryPath">Новый путь до новой директории.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении данных!</item>
        /// </list>
        /// </returns>
        public static bool AskForDirectory(string nowDrive, out string directoryPath)
        {
            string directoryData;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("───────────────────────────────────────────────────────────────────────────" +
                "────────────────────────────────────────────\n");
            Console.Write("\nНажатием на кнопку TAB, вы сможете дополнить свой путь, даже убрать лишнее!\n" +
                "\nДополненный путь будет подсвечиваться ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("зелёным цветом\n\n");
            Console.ResetColor();
            do
            {
                Console.WriteLine($"Введите путь до директории исключая название своего диска {nowDrive}.\n" +
                    $"Например для доступа к директории {nowDrive}Test вы должны написать просто Test.");
                directoryData = ManageTools.ReadLine(nowDrive);
                if (directoryData == "?END?")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Задействован аварийный выход!");
                    Console.ResetColor();
                    directoryPath = null;
                    return false;
                }
            } while (!Input.GetDirectoryPath(directoryData, nowDrive, out directoryPath, false));
            return true;
        }

        /// <summary>
        /// Метод, запрашивающий путь до новой директории.
        /// </summary>
        /// <param name="nowDrive">Текущий рабочий диск.</param>
        /// <param name="directoryPath">Новый путь до новой директории.</param>
        /// <param name="isDirectoryNeeded">Нужно ли создавать новую директорию.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении данных!</item>
        /// </list>
        /// </returns>
        public static bool AskForDirectory(string nowDrive, out string directoryPath, bool isDirectoryNeeded)
        {
            string directoryData;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("───────────────────────────────────────────────────────────────────────────────────────────────────────────────────────\n");
            Console.Write("\nНажатием на кнопку TAB, вы сможете дополнить свой путь, даже убрать лишнее!" +
                "\nДополненный путь будет подсвечиваться ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("зелёным цветом\n\n");
            Console.ResetColor();
            do
            {
                Console.WriteLine($"Введите путь до директории исключая название своего диска {nowDrive}.\n" +
                    $"Например для доступа к дериктории {nowDrive}Test вы должны написать просто Test.");
                directoryData = ManageTools.ReadLine(nowDrive);
                if (directoryData == "?END?")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Задействован аварийный выход!");
                    Console.ResetColor();
                    directoryPath = null;
                    return false;
                }
            } while (!Input.GetDirectoryPath(directoryData, nowDrive, out directoryPath, true));
            return true;
        }

        /// <summary>
        /// Метод, возвращающий путь до существующего файла.
        /// </summary>
        /// <param name="fileData">Строка, означающая ввод пользователя.</param>
        /// <param name="nowDirectory">Текущая рабочая директория.</param>
        /// <param name="filePath">Путь до файла.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении данных!</item>
        /// </list>
        /// </returns>
        public static bool GetFilePath(string fileData, string nowDirectory, out string filePath)
        {
            if (fileData == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Старайтесь не вводить пустые строки!");
                Console.ResetColor();
                filePath = null;
                return false;
            }
            if (File.Exists($"{nowDirectory}{fileData}"))
            {
                filePath = Path.GetFullPath($"{nowDirectory}{fileData}");
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Такого файла не существует, пожалуйста, повторите ввод.");
                Console.ResetColor();
                filePath = null;
                return false;
            }
        }

        /// <summary>
        /// Метод, возвращающий путь до существующего файла.
        /// </summary>
        /// <param name="fileData">Строка, означающая ввод пользователя.</param>
        /// <param name="nowDirectory">Текущая рабочая директория.</param>
        /// <param name="isTxtNeded">Нужен ли файл формата .txt.</param>
        /// <param name="filePath">Путь до файла.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении данных!</item>
        /// </list>
        /// </returns>
        public static bool GetFilePath(string fileData, string nowDirectory, bool isTxtNeded, out string filePath)
        {
            if (fileData == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Старайтесь не вводить пустые строки!");
                Console.ResetColor();
                filePath = null;
                return false;
            }
            if (File.Exists($"{nowDirectory}{fileData}"))
            {
                filePath = Path.GetFullPath($"{nowDirectory}{fileData}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Такого файла не существует, пожалуйста, повторите ввод.");
                Console.ResetColor();
                filePath = null;
                return false;
            }
            if (isTxtNeded) return ManageTools.IsFIleTxt(filePath);
            else return true;
        }

        /// <summary>
        /// Метод, запрашивающий путь до файла.
        /// </summary>
        /// <param name="nowDirectory">Текущая рабочая директория.</param>
        /// <param name="filePath">Новый путь до файла.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении данных!</item>
        /// </list>
        /// </returns>
        public static bool AskForFile(string nowDirectory, out string filePath)
        {
            string fileData;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("───────────────────────────────────────────────────────────────────────────────────────────────────────────────────────\n");
            Console.Write("\nНажатием на кнопку TAB, вы сможете дополнить свой путь, даже убрать лишнее!\n" +
                "Дополненный путь будет подсвечиваться ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("зелёным цветом\n\n");
            Console.ResetColor();
            do
            {
                Console.WriteLine($"Введите название файла с расширением, но без директории {nowDirectory}.\n" +
                    $"Например для доступа к файлу {nowDirectory}Test.txt вы должны написать просто Test.txt.");
                fileData = ManageTools.ReadLine(nowDirectory);
                if (fileData == "?END?")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Задействован аварийный выход!");
                    Console.ResetColor();
                    filePath = null;
                    return false;
                }
            }
            while (!Input.GetFilePath(fileData, nowDirectory, out filePath));
            return true;
        }

        /// <summary>
        /// Метод, запрашивающий путь до файла.
        /// </summary>
        /// <param name="nowDirectory">Текущая рабочая директория.</param>
        /// <param name="isTxtNeeded">Нужен ли только текстовый файл.</param>
        /// <param name="filePath">Новый путь до файла.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении данных!</item>
        /// </list>
        /// </returns>
        public static bool AskForFile(string nowDirectory, bool isTxtNeeded, out string filePath)
        {
            string fileData;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("\nНажатием на кнопку TAB, вы сможете дополнить свой путь, даже убрать лишнее!" +
                "\nДополненный путь будет подсвечиваться ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("зелёным цветом\n\n");
            Console.ResetColor();
            do
            {
                Console.WriteLine($"Введите название файла с расширением, но без директории {nowDirectory}.\n" +
                    $"Например для доступа к файлу {nowDirectory}Test.txt вы должны написать просто Test.txt.");
                fileData = ManageTools.ReadLine(nowDirectory);
                if (fileData == "?END?")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Задействован аварийный выход!");
                    Console.ResetColor();
                    filePath = null;
                    return false;
                }
            }
            while (!Input.GetFilePath(fileData, nowDirectory, isTxtNeeded, out filePath));
            return true;
        }

        /// <summary>
        /// Метод, запрашивающий метод декодирования/кодирования файлов.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает строковое представление кодировки.</item>
        /// </list>
        /// </returns>
        public static string AskForEncoding()
        {
            Input.AskForCoddingMethod(out ushort choice);
            string encoding = "utf-8";
            switch (choice)
            {
                case 1:
                    encoding = Encoding.UTF8.BodyName;
                    break;
                case 2:
                    encoding = Encoding.Unicode.BodyName;
                    break;
                case 3:
                    encoding = Encoding.ASCII.BodyName;
                    break;
                case 4:
                    encoding = Encoding.UTF32.BodyName;
                    break;
            }

            return encoding;
        }

        /// <summary>
        /// Метод, запрашивающий и возвращающий номер метода кодирования/декодирования информации.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает номер метода в виде числа типа ushort.</item>
        /// </list>
        /// </returns>
        public static void AskForCoddingMethod(out ushort method)
        {
            string methodChoiceData;
            Environment.CodeDecodeMethodMenu();
            // Пока пользователь не введёт подходящий номер метода, запрашивать его снова.
            do
            {
                Console.Write("Введите номер кодировки из меню: ");
                methodChoiceData = Console.ReadLine();
            } while (!GetMethod(methodChoiceData, out method));
        }

        /// <summary>
        /// Метод, возвращающий номер метода кодирования/декодирования информации.
        /// </summary>
        /// <param name="methodChoiceData">Строка, означающая ввод пользователя.</param>
        /// <param name="methodChoice">Числовое представление выбора кодирования/декодирования информации.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении метода!</item>
        /// </list>
        /// </returns>
        public static bool GetMethod(string methodChoiceData, out ushort methodChoice)
        {
            if (!ushort.TryParse(methodChoiceData, out methodChoice) || methodChoice == 0 || methodChoice > 4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Такого номера коддировки не предусмотрено, введите число от 1 до 4." +
                    "Пожалуйста повторите ввод!");
                Console.ResetColor();
                return false;
            }
            return true;

        }

        /// <summary>
        /// Метод, запрашивающий метод копирования файлов.
        /// </summary>
        /// <param name="isRewriteNeeded">Нужно ли перезаписывать файлы с тем же именем.</param>
        public static void AskForCopyMethod(out bool isRewriteNeeded)
        {
            string methodChoiceData;
            ushort method;
            Environment.CopyMethodMenu();
            do
            {
                Console.Write("Введите номер варианта копирования из меню: ");
                methodChoiceData = Console.ReadLine();
            } while (!GetCopyMethod(methodChoiceData, out method));

            if (method == 1)
            {
                isRewriteNeeded = true;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nФайлы с тем же названием будут перезаписаны!\n");
                Console.ResetColor();
            }
            else
            {
                isRewriteNeeded = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\nФайлы с тем же названием будут не тронуты!\n");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Метод, возвращающий числовое представление действия копирования.
        /// </summary>
        /// <param name="methodChoiceData">Строка, означающая ввод пользователя.</param>
        /// <param name="methodChoice">Числовое представление выбора типа копирования информации.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему
        /// в получении метода копирования!</item>
        /// </list>
        /// </returns>
        public static bool GetCopyMethod(string methodChoiceData, out ushort methodChoice)
        {
            if (!ushort.TryParse(methodChoiceData, out methodChoice) || methodChoice == 0 || methodChoice > 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Такого номера не предусмотрено, введите число от 1 до 2.\n" +
                    "Пожалуйста повторите ввод!");
                Console.ResetColor();
                return false;
            }
            return true;

        }

        /// <summary>
        /// Метод, запрашивающий имя файла для его создания.
        /// </summary>
        /// <param name="path">Путь текущей директории.</param>
        /// <param name="filePath">Путь до нового файла.</param>
        public static void AskForCreationFileName(string path, out string filePath)
        {
            string nameData;
            // Пока пользователь не введёт подходящий, запрашивать его снова.
            do
            {
                Console.WriteLine("Введите имя текстового файла без расширения " +
                    "(все недопустимые символы будут автоматически удалены из названия)\n" +
                    "Если вы введете название уже существуещего файла, он будет перезаписан.");
                Console.Write("Название: ");
                nameData = Console.ReadLine();
            } while (!GetCreationFileName(path, nameData, out filePath));
            filePath = $"{path}/{filePath}";
        }

        /// <summary>
        /// Метод, возвращающий корректное имя файла и путь для его создания.
        /// </summary>
        /// <param name="path">Путь до текущей директории.</param>
        /// <param name="nameData">Ввод пользователя.</param>
        /// <param name="fileName">Скорректированное имя файла.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении данных.</item>
        /// </list>
        /// </returns>
        public static bool GetCreationFileName(string path, string nameData, out string fileName)
        {
            if (nameData == null || (nameData.Split(' ', StringSplitOptions.RemoveEmptyEntries)).Length == 0)
            {
                GetCreationFileNameEnvironment("error_null");
                fileName = null;
                return false;
            }
            else if (nameData.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0] == " ")
            {
                GetCreationFileNameEnvironment("error_name");
                fileName = null;
                return false;
            }
            nameData = ManageTools.RemoveInvalidChars(nameData);
            if (nameData.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length == 0)
            {
                GetCreationFileNameEnvironment("error_name_del");
                fileName = null;
                return false;
            }
            if (nameData.Length + path.Length > 235)
            {
                GetCreationFileNameEnvironment("to_long");
                fileName = null;
                return false;
            }
            fileName = nameData;
            return true;
        }

        /// <summary>
        /// Метод, выводящий информацию для пользователя по поводу создания файла.
        /// </summary>
        /// <param name="workType">Тип вывода для пользователя.</param>
        private static void GetCreationFileNameEnvironment(string workType)
        {
            switch (workType)
            {
                case "error_null":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Старайтесь не вводить пустые строки!");
                    Console.ResetColor();
                    break;
                case "error_name":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введено пустое имя файла!");
                    Console.ResetColor();
                    break;
                case "error_name_del":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введено пустое имя файла или " +
                        "содержащее только недопустимые символы!");
                    Console.ResetColor();
                    break;
                case "to_long":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Вы задали слишком длинное имя файла, оно превышает ограничение длины пути." +
                        "\nПожалуйста повторите ввод!");
                    Console.ResetColor();
                    break;
            }
        }

        /// <summary>
        /// Метод, запрашивающий текст для создания файла.
        /// </summary>
        /// <param name="data">Строка для записи файла.</param>
        public static void AskForTextCreationFIle(out string data)
        {
            string rawData;
            // Пока пользователь не введёт подходящий, запрашивать его снова.
            do
            {
                Console.WriteLine("Введите текст текстового файла.");
                Console.Write("Текст: ");
                rawData = Console.ReadLine();
            } while (!GetTextCreationFIle(rawData, out data));

        }

        /// <summary>
        /// Метод, возвращающий корректный текст для записи в новый файл.
        /// </summary>
        /// <param name="rawData">Ввод пользователя.</param>
        /// <param name="data">Проверенный текст для записи в файл.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении данных.</item>
        /// </list>
        /// </returns>
        public static bool GetTextCreationFIle(string rawData, out string data)
        {
            if (rawData == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Старайтесь не вводить пустые строки!");
                Console.ResetColor();
                data = null;
                return false;
            }
            if (rawData.Length > 10024)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Вы задали слишком большое количество символов.\n" +
                    "Пожалуйста повторите ввод!");
                Console.ResetColor();
                data = null;
                return false;
            }
            data = rawData;
            return true;

        }

        /// <summary>
        /// Метод, запрашивающий маску поиска от пользователя.
        /// </summary>
        /// <param name="mask">Корректная маска для поиска.</param>
        public static void AskForMask(out string mask)
        {
            string rawData;
            do
            {
                Console.WriteLine("Введите маску, по которой будет происходить поиск.");
                Console.Write("Маска: ");
                rawData = Console.ReadLine();
            } while (!GetMask(rawData, out mask));

        }

        /// <summary>
        /// Метод, проверяющий размер файла.
        /// </summary>
        /// <param name="path">Путь до проверяемого файла.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение bool, означающее превысил ли пользователь лимит на размер файла.</item>
        /// </list>
        /// </returns>
        public static bool CheckSizeOfFile(string path)
        {
            long length = new FileInfo(path).Length;
            if (length > 268435456)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Считывание отменено, размер файла слишком большой!\n" +
                    "Получен отказ на считывание файла больше 256 МБ.");
                Console.ResetColor();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Метод, возвращающий корректную маску поиска.
        /// </summary>
        /// <param name="rawData">Ввод пользователя.</param>
        /// <param name="mask">Проверенная маска поиска.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении данных.</item>            
        /// </list>
        /// </returns>
        public static bool GetMask(string rawData, out string mask)
        {
            if ((rawData.Split(' ', StringSplitOptions.RemoveEmptyEntries)).Length == 0 || rawData == null)
            {
                GetMaskEnvironment("error_null");
                mask = null;
                return false;
            }
            if (rawData.Length > 1024)
            {
                GetMaskEnvironment("error_long");
                mask = null;
                return false;
            }
            foreach (var item in Path.GetInvalidPathChars())
            {
                for (int i = 0; i < rawData.Length; i++)
                {
                    if (rawData[i] == item)
                    {
                        GetMaskEnvironment("error_mask");
                        mask = null;
                        return false;
                    }
                }
            }
            mask = rawData;
            return true;
        }

        /// <summary>
        /// Метод, выводящий информацию для пользователя по поводу получения маски.
        /// </summary>
        /// <param name="workType">Тип вывода для пользователя.</param>
        private static void GetMaskEnvironment(string workType)
        {
            switch (workType)
            {
                case "error_null":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Введена пустая маска!");
                    Console.ResetColor();
                    break;
                case "error_long":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Вы задали слишком большое количество символов." +
                        "Пожалуйста повторите ввод!");
                    Console.ResetColor();
                    break;
                case "error_mask":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Маска содержит недопустимые символы." +
                        "Пожалуйста повторите ввод!");
                    Console.ResetColor();
                    break;
            }
        }

        /// <summary>
        /// Метод, возвращающий и запрашивающий от пользователя количество складываемых файлов.
        /// </summary>
        /// <param name="filesNum">Количество складываемых файлов.</param>
        public static void AskForNumFiles(out int filesNum)
        {
            string numDataRaw;
            do
            {
                Console.Write("Введите число файлов для сложения от 2 до 10: ");
                numDataRaw = Console.ReadLine();
            } while (!Input.GetNumFiles(numDataRaw, out filesNum));

        }

        /// <summary>
        /// Метод, возвращающий int число в диапазоне [0, 10], полученное от пользователя.
        /// </summary>
        /// <param name="numDataRaw">Строка, означающая ввод пользователя.</param>
        /// <param name="num">Число, означающее количество складываемых файлов.</param>
        public static bool GetNumFiles(string numDataRaw, out int num)
        {
            if (!int.TryParse(numDataRaw, out num) || num < 2 || num > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Это не число, или число, которое не принадлижит [2, 10]." +
                    " Пожалуйста, повторите ввод.");
                Console.ResetColor();
                return false;
            }
            return true;
        }
    }
}
