using System;
using System.IO;
using System.Text;

namespace Peergrade003
{
    /// <summary>
    /// Класс, cовершающий запрошенные пользователем действия.
    /// </summary>
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
            Console.WriteLine($"\nПроизошла смена диска на {allDrives[driveChoice - 1].Name}.\n");
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
            try
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
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Провал считывания директории.");
                Console.ResetColor();
            }
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
                                      $" на экран\nЕсли вы оказались в ситуации, когда в вашей директории нет файлов," +
                                      $" пропишите ?END?.\n");
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
            using StreamReader sr = new(filePath, Encoding.GetEncoding(encoding));
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
            CopyFileContentEnvironment("start");
            try
            {
                CopyFileContentEnvironment("file");
                // Запрашиваем путь до файла.
                bool conditionFirst = Input.AskForFile(nowDirectory, out string filePath);
                if (!conditionFirst) return false;
                CopyFileContentEnvironment("dir");
                // Запрашиваем путь до директории.
                bool conditionSecond = Input.AskForDirectory(nowDrive, out string directoryPath);
                if (!conditionSecond) return false;
                // Если надо, изменяем название файла для копирования в ту же самую директорию.
                string destPointPath = CopyInSameDirectoryName(filePath, directoryPath);
                File.Copy(filePath, destPointPath, true);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nФайл {Path.GetFileName(filePath)} копирован в {directoryPath}.\n");
                Console.ResetColor();
                return true;
            }
            catch (Exception)
            {
                CopyFileContentEnvironment("big_error");
                return false;
            }
        }

        /// <summary>
        /// Метод, выводящий информацию для пользователя.
        /// </summary>
        /// <param name="workType">Тип вывода для пользователя.</param>
        private static void CopyFileContentEnvironment(string workType)
        {
            switch (workType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n──────────────────────────────── Копирование файла\nЕсли вы оказались" +
                                      $" в ситуации," +
                                      $" когда на вашем диске нет директорий или в директории нет файлов, пропишите" +
                                      $" ?END?\n");
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
            FileTransferEnvironment("start");
            try
            {
                FileTransferEnvironment("file");
                // Запрашиваем путь до файла.
                bool conditionFirst = Input.AskForFile(nowDirectory, out string filePath);
                if (!conditionFirst) return false;
                FileTransferEnvironment("dir");
                // Запрашиваем директорию.
                bool conditionSecond = Input.AskForDirectory(nowDrive, out string directoryPath);
                if (!conditionSecond) return false;
                File.Move(filePath, $"{directoryPath}{Path.DirectorySeparatorChar}" +
                                    $"{Path.GetFileName(filePath)}", true);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nФайл {Path.GetFileName(filePath)} перенесён в {directoryPath}.\n");
                Console.ResetColor();
                return true;
            }
            catch
            {
                FileTransferEnvironment("big_error");
                return false;
            }
        }

        /// <summary>
        /// Метод, выводящий информацию для пользователя.
        /// </summary>
        /// <param name="workType">Тип вывода для пользователя.</param>
        private static void FileTransferEnvironment(string workType)
        {
            switch (workType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n──────────────────────────────── Перенос файла\n" +
                                      $"Если вы оказались в ситуации, когда на вашем диске " +
                                      $"нет директорий или в директории нет файлов," +
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
                              $"\nЕсли вы оказались в ситуации, когда в вашей директории нет файлов, пропишите" +
                              $" ?END?.\n");
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
                Console.WriteLine("Ошибка при удалении файла. Вероятно он используется другим приложением.");
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
                GetDataForFileCreation(nowDirectory, out string filePath, out string data);
                try
                {
                    // Записываем полученные данные.
                    StreamWriter sw = new StreamWriter(filePath + ".txt", false,
                        Encoding.GetEncoding(encoding));
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
            MergingMultipleFilesEnvironment("start");
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
                MergingMultipleFilesEnvironment("big_error");
                return false;
            }
        }

        /// <summary>
        /// Метод, выводящий информацию для пользователя по поводу обьеденения файлов.
        /// </summary>
        /// <param name="workType">Тип вывода для пользователя.</param>
        private static void MergingMultipleFilesEnvironment(string workType)
        {
            switch (workType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n──────────────────────────────── Обьединение текста нескольких файлов\n" +
                                      $"\nЕсли вы оказались в ситуации, когда в вашей директории нет файлов," +
                                      $" пропишите ?END? при выборе " +
                                      $"пути.\nВы также можете складывать один файл несколько раз," +
                                      $" если вам так нужно.\n" +
                                      $"Cохранение результата будет в первом введённом файле.\n");
                    Console.ResetColor();
                    break;
                case "big_error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка при складывании файлов." +
                                      "\nВозможно один из файлов недоступен для чтения или" +
                                      " используется другим процессом!");
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
                    // Если длина строки слишком большая, мы обрежем её.
                    if (line.Length > 2048)
                    {
                        Console.WriteLine(line[..2048]);
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
                              $"──────────────────────────────────────\n───── " +
                              $"Результат записан в {Path.GetFileName(data[0])}\n");
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
                // Получаем инофрмацию, требуемую для копирования.
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
            // Если файл копируется в свою же директорию, мы изменим его название в автоматическом порядке.
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
        /// <param name="isRewriteNeeded">Выходной параметр, означающий согласие или несогласие на пере -
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
                                      $"Если хотите отменить действие, во время ввода пути " +
                                      $"до директории копирования напишите ?END?\n");
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
                // Запрашиваем старый файл.
                DifferencesBetweenFilesEnvironment("file_old", nowDirectory);
                bool condition = Input.AskForFile(nowDirectory, true, out string filePathFirst);
                if (!condition) return false;
                condition = Input.CheckSizeOfFile(filePathFirst);
                if (!condition) return false;
                // Запрашиваем новый файл.
                DifferencesBetweenFilesEnvironment("file_new", nowDirectory);
                condition = Input.AskForFile(nowDirectory, true, out string filePathSecond);
                if (!condition) return false;
                condition = Input.CheckSizeOfFile(filePathSecond);
                if (!condition) return false;
                SetDiffOutput(filePathFirst, filePathSecond);
                return true;
            }
            catch
            {
                DifferencesBetweenFilesEnvironment("big_error", nowDirectory);
                return false;
            }

        }

        /// <summary>
        /// Метод, считывающий данные из заданных файлов и инициализирующий поиск разницы между двумя файлами.
        /// </summary>
        /// <param name="filePathFirst">Строка, содержащая путь до первого файла (старого).</param>
        /// <param name="filePathSecond">Строка, содержащая путь до второго файла (нового).</param>
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
            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────" +
                              "───────────────────────────────────\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод, устанавливающий цвет текста в зависимости от его статуса после обработки diff.
        /// </summary>
        /// <param name="item">Строка текста.</param>
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
                else Console.WriteLine(item);
            }
            else Console.WriteLine(item);
        }

        /// <summary>
        /// Метод, выводящий информацию для пользователя по поводу нахождения различий между файлами.
        /// </summary>
        /// <param name="nowDirectory">Путь до новой текущей директории.</param>
        /// <param name="workType">Тип вывода для пользователя.</param>
        private static void DifferencesBetweenFilesEnvironment(string workType, string nowDirectory)
        {
            switch (workType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\n──────────────────────────────── Вывод разницы между двумя файлами" +
                                      $" на экран\nЕсли вы оказались в ситуации, когда в вашей директории нет" +
                                      $" файлов, пропишите ?END?.\n");
                    Console.ResetColor();
                    break;
                case "big_error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка при получении данных, возможно файл занят другим процессом.");
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

        /// <summary>
        /// Метод, получающий таблицу сопоставления строк (стандартный метод реализации Diff).
        /// </summary>
        /// <param name="fileOld"> Массив строк первого файла (старого).</param>
        /// <param name="fileNew">Массив строк второго файла (нового).</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает двумерный массив целых чисел.</item>
        /// </list>
        /// </returns>
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

        /// <summary>
        /// Метод, создающий классический diff вывод в виде одной строки для подданых строк на основе таблицы
        /// соответсвий.
        /// </summary>
        /// <param name="fileOld"> Массив строк первого файла (старого).</param>
        /// <param name="fileNew">Массив строк второго файла (нового).</param>
        /// <param name="subMatrix">Матрица/таблица соответствий.</param>
        /// <param name="i">Массив строк второго файла (нового).</param>
        /// <param name="j"> Массив строк первого файла (старого).</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает cтроку с установленными добавленными, удалёнными строками.</item>
        /// </list>
        /// </returns>
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
}