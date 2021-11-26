using System;
using System.IO;

namespace Peergrade003
{
    /// <summary>
    /// Класс, содержащий методы помогающие в работе программы.
    /// </summary>
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
                    Console.WriteLine("Невозможно получить доступ к первому диску, он не готов," +
                                      " выполняю запрос на получение диска от пользователя.");    
                    Console.ResetColor();
                    if (Actions.DriveSelection(out string nowDirectory, out string nowDrive))
                    {
                        return nowDrive;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Невозможно получить доступ к дискам, проверьте систему!");
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
            TrimPathToValidOne(rawPath, out string rawPathTrim, out _, out _);
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
            TrimPathToValidOne(rawPath, out rawPathTrim, out control, out mask);
            mask = ReverseString(mask);
        }

        /// <summary>
        /// Метод, создающий новый путь по вводу пользователя используя полученную маску.
        /// </summary>
        /// <param name="newPath">Доработанный путь.</param>
        /// <param name="rawPathTrim">Путь, который ввёл пользователь, обрезанный до существующего.</param>
        /// <param name="dataFile">Файлы, найденные по маске от пользователя.</param>
        /// <param name="dataDirect">Директории, найденные по маске от пользователя.</param>
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
        private static void TrimPathToValidOne(string rawPath, out string rawPathTrim, out int control,
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
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}