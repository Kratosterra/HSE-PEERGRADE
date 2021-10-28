using System;
using System.IO;
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
        static void Main(string[] args)
        {
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
                    if (nowDirectory == null)
                    {
                        nowDirectory = ManageTools.SetStartDrive();
                        nowDrive = ManageTools.SetStartDrive();
                    }
                    if (nowDirectory == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("На вашем копьютере не обнаружено доступных дисков, пожалуйста проверьте права доступа в системе!");
                        Console.ResetColor();
                    }
                    else 
                    {
                        FileManagerActions(nowDirectory, nowDrive, out nowDirectory, out nowDrive);
                    }
                    // Выводим инструкции по выходу из программы.
                    Environment.EndProgram();
                    // Получаем нажатую кнопку.
                    exitKey = Console.ReadKey();
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

        private static void FileManagerStart()
        {
            // Вызываем заставку и паузу с запросом нажатия кнопки.
            Environment.SplashScreen();
            Environment.Service("stop_button");
            // Выводим инструкции и паузу с запросом нажатия кнопки.
            Environment.Instructions();
            Environment.Service("stop_button");
        }

        private static bool FileManagerActions(string nowDirectoryOld, string nowDriveOld, out string nowDirectory, out string nowDrive)
        {
            Environment.NowDirectoryInfo(nowDirectoryOld);
            Environment.MainMenu();
            FileManagerChoiceActions(out ushort actionChoice);
            if (Actions.EngageActionSelection(actionChoice, nowDirectoryOld, nowDriveOld, out nowDirectory, out nowDrive))
            {
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Обнаружена ошибка или аварийный выход из действия!");
                Console.ResetColor();
                return false;
            }
        }

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
        public static bool EngageActionSelection(ushort actionChoice, string nowDirectoryOld, string nowDriveOld, out string nowDirectory, out string nowDrive)
        {
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
                _ => false,
            };
        }

        public static bool DriveSelection(out string nowDirectory, out string nowDrive)
        {
            int driveChoice;
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n──────────────────────────────── Приступаем к смене диска.\n");
                Console.ResetColor();
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                if (allDrives.Length < 1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невозможно получить доступ к дискам, проверьте систему!");
                    Console.ResetColor();
                    nowDirectory = null;
                    nowDrive = null;
                    return false;
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                int numDrives = 0;
                foreach (DriveInfo drive in allDrives)
                {
                    numDrives += 1;
                    Console.WriteLine($"{numDrives}. Диск {drive.Name}");
                }
                Console.ResetColor();
                string driveData;
                do
                {
                    Console.Write($"Введите номер диска от 1 до {numDrives} включительно: ");
                    driveData = Console.ReadLine();
                } while (!Input.GetDrive(driveData, numDrives, out driveChoice));
                if (allDrives[driveChoice - 1].IsReady == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Выбранный диск не готов к использованию!");
                    Console.ResetColor();
                    nowDirectory = null;
                    nowDrive = null;
                    return false;
                }
                nowDirectory = (allDrives[driveChoice - 1].Name).ToString();
                nowDrive = (allDrives[driveChoice - 1].Name).ToString();
                return true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неизвестная ошибка при выборе диска!");
                Console.ResetColor();
                nowDirectory = null;
                nowDrive = null;
                return false;
            }
        }

        public static bool ChangeDirectory(string nowDirectoryOld, string nowDrive, out string nowDirectory)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n──────────────────────────────── Приступаем к смене директории\n" +
                    "Если вы оказались в ситуации, когда на вашем диске нет директорий, пропишите ?END? или просто нажмите Enter.\n");
                Console.ResetColor();
                bool condition = Input.AskForDirectory(nowDrive, out string directoryPath);
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (condition) Console.WriteLine($"\nПроизошла смена директории с {nowDirectoryOld} на {directoryPath}.\n");
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

        public static bool ListOfFiles(string nowDirectory, string nowDrive)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Папки и файлы в директории {nowDirectory}\n");
            Console.ResetColor();
            try
            {
                string[] dataFile = Directory.GetFiles(nowDirectory);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                foreach (var item in dataFile)
                { 
                    Console.WriteLine($"Файл: {Path.GetFileName(item)}\t Путь: {item}");
                }
                string[] dataDir = Directory.GetDirectories(nowDirectory);
                foreach (var item in dataDir)
                {
                    Console.WriteLine($"Папка: {item}");
                }
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nВсе файлы и директории выведены.\n");
                Console.ResetColor();
                return true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Не получается получить доступ к файлам в данной директории, вероятно, система запрещает это делать!\n" +
                    "Смените диск или директорию в следующей итерации."); 
                Console.ResetColor();
                return false;
            }  
        }

        public static bool OutputtingFileContent(string nowDirectory, string nowDrive)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Вывод файла из {nowDirectory}\n" +
                $"\nЕсли вы оказались в ситуации, когда в вашей директории нет файлов, пропишите ?END?.\n");
            Console.ResetColor();
            try
            {
                bool condition = Input.AskForFile(nowDirectory, true, out string filePath);
                if (!condition) return false;
                string encoding = Input.AskForEncoding();
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n──────────────────────────────── {Path.GetFileName(filePath)}\n");
                    Console.ResetColor();
                    Console.WriteLine($"{File.ReadAllText(filePath, Encoding.GetEncoding(encoding))}");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("───────────────────────────────────────────────────────────────────────────────────────" +
                    "────────────────────────────────\n");
                    Console.WriteLine($"\nФайл {Path.GetFileName(filePath)} прочитан!\n");
                    Console.ResetColor();
                    return true;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невозможно прочитать файл, убедитесь что он доступен для чтения в предложенных кодировках.");
                    Console.ResetColor();
                    return false;
                }
            }
            catch 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при выводе содержания файла.");
                Console.ResetColor();
                return false;
            }
        }

        public static bool CopyFile(string nowDirectory, string nowDrive)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Копирование файла\n" +
                $"Если вы оказались в ситуации, когда на вашем диске нет директорий или в директории нет файлов, пропишите ?END?\n");
            Console.ResetColor();
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nВведите файл, который хотите скопировать.\n");
                Console.ResetColor();
                bool conditionFirst = Input.AskForFile(nowDirectory, out string filePath);
                if (!conditionFirst) return false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nВвёдите директорию, в которую будет производится копирование.\n");
                Console.ResetColor();
                bool conditionSecond = Input.AskForDirectory(nowDrive, out string directoryPath);
                if (!conditionSecond) return false;
                File.Copy(filePath, $"{directoryPath}/{Path.GetFileName(filePath)}", true);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nФайл {Path.GetFileName(filePath)} копирован в {directoryPath}.\n");
                Console.ResetColor();
                return true;
            }
            catch (System.IO.IOException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Невозможно копировать файл в директорию самого же файла.");
                Console.ResetColor();
                return false;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ошибка при копировании файла в директорию.");
                Console.ResetColor();
                return false;
            }
        }

        public static bool FileTransfer(string nowDirectory, string nowDrive)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Перенос файла\n" +
                $"Если вы оказались в ситуации, когда на вашем диске нет директорий или в директории нет файлов, пропишите ?END?\n");
            Console.ResetColor();
            try
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nВведите файл, который хотите перенести.\n");
                Console.ResetColor();
                bool conditionFirst = Input.AskForFile(nowDirectory, out string filePath);
                if (!conditionFirst) return false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nВвёдите директорию, в которую будет производится перенос.\n");
                Console.ResetColor();
                bool conditionSecond = Input.AskForDirectory(nowDrive, out string directoryPath);
                if (!conditionSecond) return false;
                File.Move(filePath, $"{directoryPath}/{Path.GetFileName(filePath)}", true);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nФайл {Path.GetFileName(filePath)} перенесён в {directoryPath}.\n");
                Console.ResetColor();
                return true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при переносе файла в директорию.");
                Console.ResetColor();
                return false;
            }
        }

        public static bool DeletingFile(string nowDirectory, string nowDrive)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Удаление файла из {nowDirectory}\n" +
                $"\nЕсли вы оказались в ситуации, когда в вашей директории нет файлов, пропишите ?END?.\n");
            Console.ResetColor();
            try
            {
                bool condition = Input.AskForFile(nowDirectory, out string filePath);
                if (!condition) return false;
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

        public static bool CreatingTextFile(string nowDirectory, string nowDrive)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Создание/перезапись текстового файла в {nowDirectory}\n" +
                $"\nЕсли вы хотите отменить действие, пропишите ?END?\n");
            Console.ResetColor();
            try
            {
                string encoding = Input.AskForEncoding();
                try
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nВыберем имя файла.\n");
                    Console.ResetColor();
                    Input.AskForCreationFileName(nowDirectory, out string filePath);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nВведём текст файла.\n");
                    Console.ResetColor();
                    Input.AskForTextCreationFIle(out string data);
                    try
                    {
                        StreamWriter sw = new StreamWriter(filePath+".txt", false, Encoding.GetEncoding(encoding));
                        sw.Write(data);
                        sw.Close();
                    }
                    catch 
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Возникла ошибка при записи тескста в файл.");
                        Console.ResetColor();
                        return false;
                    }
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nФайл {Path.GetFileName(filePath)} успешно создан.\n");
                    Console.ResetColor();
                    return true;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невозможно создать файл, убедитесь, что вы можете создавать файлы в данной системе.");
                    Console.ResetColor();
                    return false;
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при создании файла.");
                Console.ResetColor();
                return false;
            }
        }

        public static bool MergingMultipleFiles(string nowDirectory, string nowDrive)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n──────────────────────────────── Обьеденение текста нескольких файлов\n" +
                $"\nЕсли вы оказались в ситуации, когда в вашей директории нет файлов, пропишите ?END? при выборе пути.\n" +
                $"Вы также можете складывать один и тотже файл, если вам так нужно.\n");
            Console.ResetColor();
            try
            {
                Input.AskForNumFiles(out int filesNum);
                string[] data = new string[filesNum];
                for (int i = 0; i < filesNum; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nВводим файл [{i+1}]\n");
                    Console.ResetColor();
                    bool condition = Input.AskForFile(nowDirectory, true, out string filePath);
                    if (!condition) return false;
                    data[i] = filePath;
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\n──────────────────────────────── Сложение {filesNum} файлов\n");
                Console.ResetColor();
                for (int i = 0; i < data.Length; i++)
                {
                    Console.WriteLine($"{File.ReadAllText(data[i])}");
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("───────────────────────────────────────────────────────────────────────────────────────" +
                "────────────────────────────────\n");
                return true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка при складывании файлов.");
                Console.ResetColor();
                return false;
            }
            return false;
        }

        public static bool SearchByMask(string nowDirectory, string nowDrive)
        {
            return false;
        }

        public static bool AdvancedSearchByMask(string nowDirectory, string nowDrive)
        {
            return false;
        }

        public static bool CopyByMask(string nowDirectory, string nowDrive)
        {
            return false;
        }

        public static bool DifferencesBetweenFiles(string nowDirectory, string nowDrive)
        {
            return false;
        }
    }

    class ManageTools
    {
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
        /// удаляет все символы которые не разрешены в именах файлов
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string RemoveInvalidChars(string fileName)
        {
            foreach (char invalid_char in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(oldValue: invalid_char.ToString(), newValue: "");
            }
            return fileName;
        }

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

    }

    class Environment
    {
        /// <summary>
        /// Метод вывода заставки на экран.
        /// </summary>
        public static void SplashScreen()
        {
            Console.WriteLine("+");
        }

        /// <summary>
        /// Метод вывода инструкций на экран.
        /// </summary>
        public static void Instructions()
        {
            Console.WriteLine("+");
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
                "| [13] Сравнение двух текстовых файлов\n");
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
                    //Если пользователь додумался нажать не Enter,
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

        public static void NowDirectoryInfo(string nowDirectory)
        {
            Console.WriteLine($"Директория сейчас: {nowDirectory}");
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
            // Если введёное число не подходит под выбор действия - выводим ошибку.
            if (!ushort.TryParse(actionChoiceData, out actionChoice) || actionChoice == 0 || actionChoice > 13)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Такого действия не предусмотрено, пожалуйста повторите ввод!");
                Console.ResetColor();
                return false;
            }
            return true;
        }

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

        public static bool GetDirectoryPath(string directoryData, string nowDrive, out string directoryPath)
        {
            if (directoryData == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Старайтесь не вводить пустые строки!");
                Console.ResetColor();
                directoryPath = null;
                return false;
            }
            if (Directory.Exists($"{nowDrive}{directoryData}"))
            {
                directoryPath = $"{nowDrive}{directoryData}";
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Такой директории не существует, пожалуйста, повторите ввод.");
                Console.ResetColor();
                directoryPath = null;
                return false;
            }
        }

        public static bool AskForDirectory(string nowDrive, out string directoryPath)
        {
            string directoryData;
            do
            {
                Console.WriteLine($"Введите путь до директории исключая название своего диска {nowDrive}.\n" +
                    $"Например для доступа к дериктории {nowDrive}Test вы должны написать просто Test.");
                Console.Write("Путь: ");
                directoryData = Console.ReadLine();
                if (directoryData == "?END?")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Задействован аварийный выход!");
                    Console.ResetColor();
                    directoryPath = null;
                    return false;
                }
            } while (!Input.GetDirectoryPath(directoryData, nowDrive, out directoryPath));
            return true;
        }

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
            if (File.Exists($"{nowDirectory}/{fileData}"))
            {
                filePath = $"{nowDirectory}{Path.DirectorySeparatorChar}{fileData}";
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
            if (File.Exists($"{nowDirectory}/{fileData}"))
            {
                filePath = $"{nowDirectory}{Path.DirectorySeparatorChar}{fileData}";
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

        public static bool AskForFile(string nowDirectory, out string filePath)
        {
            string fileData;
            do
            {
                Console.WriteLine($"Введите название файла с расширением, но без директории {nowDirectory}.\n" +
                    $"Например для доступа к файлу {nowDirectory}{Path.DirectorySeparatorChar}Test.txt вы должны написать просто Test.txt.");
                Console.Write("Файл: ");
                fileData = Console.ReadLine();
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

        public static bool AskForFile(string nowDirectory, bool isTxtNeeded, out string filePath)
        {
            string fileData;
            do
            {
                Console.WriteLine($"Введите название файла с расширением, но без директории {nowDirectory}.\n" +
                    $"Например для доступа к файлу {nowDirectory}{Path.DirectorySeparatorChar}Test.txt вы должны написать просто Test.txt.");
                Console.Write("Файл: ");
                fileData = Console.ReadLine();
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
        public static string AskForEncoding()
        {
            Input.AskForCoddingMethod(out ushort choice);
            string encoding = "UTF-8";
            switch (choice)
            {
                case 1:
                    encoding = "UTF-8";
                    break;
                case 2:
                    encoding = "Unicode";
                    break;
                case 3:
                    encoding = "ASCII";
                    break;
                case 4:
                    encoding = "UTF-32";
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
                Console.WriteLine("Введите номер кодировки из меню: ");
                methodChoiceData = Console.ReadLine();
            } while (!GetMethod(methodChoiceData, out method));
        }

        /// <summary>
        /// Метод, возвращающий номер метода кодирования/декодирования информации.
        /// </summary>
        /// <param name="methodChoiceData">Строка, означающая ввод пользователя.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении метода!</item>
        /// <item>Число типа ushort, означающее выбор кодирования/декодирования информации.</item>
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


        public static void AskForCreationFileName(string path ,out string filePath)
        {
            string nameData;
            // Пока пользователь не введёт подходящий, запрашивать его снова.
            do
            {
                Console.WriteLine("Введите имя текстового файла без расширения (все недопустимые символы будут автоматически удалены из названия)");
                Console.Write("Название: ");
                nameData = Console.ReadLine();
            } while (!GetCreationFileName(path, nameData, out filePath));
            filePath = $"{path}/{filePath}";
        }

        public static bool GetCreationFileName(string path, string nameData, out string fileName)
        {
            if (nameData == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Старайтесь не вводить пустые строки!");
                Console.ResetColor();
                fileName = null;
                return false;
            }
            nameData = ManageTools.RemoveInvalidChars(nameData);
            if (nameData.Length+path.Length > 235)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Вы задали слишком длинное имя файла." +
                    "Пожалуйста повторите ввод!");
                Console.ResetColor();
                fileName = null;
                return false;
            }
            fileName = nameData;
            return true;

        }

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
            if (rawData.Length > 1000000)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Вы задали слишком большое колличество символов." +
                    "Пожалуйста повторите ввод!");
                Console.ResetColor();
                data = null;
                return false;
            }
            data = rawData;
            return true;

        }

        /// <summary>
        /// Метод, возвращающий и запрашивающий от пользователя границы генерации чисел.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа int, означающее значение границ генерации.</item>
        /// </list>
        /// </returns>
        public static void AskForNumFiles(out int filesNum)
        {
            string numDataRaw;
            // Пока пользователь не введёт подходящую границу генерации, запрашивать ёё снова.
            do
            {
                Console.Write("Введите число файлов для сложения от 2 до 10: ");
                numDataRaw = Console.ReadLine();
            } while (!Input.GetNumFiles(numDataRaw, out filesNum));

        }

        /// <summary>
        /// Метод, возвращающий int число в диапазоне [0, 10], полученное от пользователя.
        /// </summary>
        /// <returns>
        /// <param name="numDataRaw">Строка, означающая ввод пользователя.</param>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении числа!</item>
        /// <item>Число типа int.</item>
        /// </list>
        /// </returns>
        public static bool GetNumFiles(string numDataRaw, out int num)
        {
            if (!int.TryParse(numDataRaw, out num) || num < 2 || num > 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Это не число, или число, которое не принадлижит [2, 10]. Пожалуйста, повторите ввод.");
                Console.ResetColor();
                return false;
            }
            return true;
        }
    }
}
