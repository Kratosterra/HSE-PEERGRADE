using System;
using System.IO;
using System.Text;

namespace Peergrade003
{
    /// <summary>
    /// Класс, содержащий методы для работы с вводом пользователя.
    /// </summary>
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
                Console.WriteLine($"Диска с таким порядковым номером не существует, введите число от 1" +
                                  $" до {numDrives}." +
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
                workPath = $"{workPath}{Path.DirectorySeparatorChar}";
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
                workPath = $"{workPath}{Path.DirectorySeparatorChar}";
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
            Console.WriteLine("─────────────────────────────────────────────────────────────────────────" +
                              "──────────────────────────────────────────────\n");
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
            Console.WriteLine("──────────────────────────────────────────────" +
                              "─────────────────────────────────────────────────────────────────────────\n");
            Console.Write("\nНажатием на кнопку TAB, вы сможете дополнить свой путь, даже убрать лишнее!\n" +
                          "Дополненный путь будет подсвечиваться ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("зелёным цветом\n\n");
            Console.ResetColor();
            do
            {
                Console.WriteLine($"Введите название файла с расширением, но без директории {nowDirectory}.\n" +
                                  $"Например для доступа к файлу {nowDirectory}Test.txt" +
                                  $" вы должны написать просто Test.txt.");
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
                                  $"Например для доступа к файлу {nowDirectory}Test.txt вы должны написать просто" +
                                  $" Test.txt.");
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