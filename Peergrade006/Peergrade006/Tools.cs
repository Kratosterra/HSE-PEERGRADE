using System;
using System.IO;
using System.Text;

namespace Peergrade006
{
    /// <summary>
    /// Класс, содержащий методы помогающие в работе программы.
    /// </summary>
    static class Tools
    {
        // Задаём переменную для генерации случайного числа.
        private static readonly Random _random = new Random();

        /// <summary>
        /// Метод, подготавливающий файлы для записи информации.
        /// </summary>
        /// <param name="carFileName">Путь до файла для сохранения автомобилей.</param>
        /// <param name="boatFileName">Путь до файла для сохранения лодок.</param>
        internal static void PrepareFilesToWriting(out string carFileName, out string boatFileName)
        {
            // Получаем пути до файлов.
            carFileName = Path.GetFullPath(
                $"..{Path.DirectorySeparatorChar}" +
                $"..{Path.DirectorySeparatorChar}" +
                $"..{Path.DirectorySeparatorChar}" +
                $"..{Path.DirectorySeparatorChar}Cars.txt");
            boatFileName = Path.GetFullPath(
                $"..{Path.DirectorySeparatorChar}" +
                $"..{Path.DirectorySeparatorChar}" +
                $"..{Path.DirectorySeparatorChar}" +
                $"..{Path.DirectorySeparatorChar}MotorBoats.txt");

            // Производим создание пустого файла или его перезапись.
            using (var fs = File.Create(carFileName))
            {
                var info = new UnicodeEncoding().GetBytes(String.Empty);
                fs.Write(info, 0, info.Length);
            }
            using (var fs = File.Create(boatFileName))
            {
                var info = new UnicodeEncoding().GetBytes(String.Empty);
                fs.Write(info, 0, info.Length);
            }
        }

        /// <summary>
        /// Метод, генерирующий модели транспорта.
        /// </summary>
        /// <param name="generateBySpecification">Генерировать ли модель согласно спецификации?</param>
        /// <returns>Строка с названием модели.</returns>
        internal static string GenerateModelName(bool generateBySpecification)
        {
            var stringBuilder = new StringBuilder();
            var choice = _random.Next(0, 101);
            if (generateBySpecification)
            {
                for (var i = 0; i < 5; i++)
                {
                    choice = _random.Next(0, 101);
                    switch (choice)
                    {
                        case <= 50:
                            stringBuilder.Append((char)_random.Next('A', 'Z'));
                            break;
                        default:
                            stringBuilder.Append(_random.Next(0, 10));
                            break;
                    }
                }
                return stringBuilder.ToString();
            }
            // В зависимости от шанса, мы выбираем длину названия модели.
            for (var i = 0; i < ((choice > 80) ? 4 : 5); i++)
            {
                choice = _random.Next(0, 101);
                switch (choice)
                {
                    case <= 40:
                        stringBuilder.Append((char)_random.Next('A', 'Z'));
                        break;
                    case > 75:
                        stringBuilder.Append((char)_random.Next(33, 110));
                        break;
                    default:
                        stringBuilder.Append(_random.Next(0, 10));
                        break;
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Метод, устанавливающий тип генерации модели.
        /// </summary>
        /// <returns>Boolean-значение, репрезентующее тип генерации модели.</returns>
        public static bool SetGeneratorMode()
        {
            bool generatorMode;
            string dataMode;
            // Просим выбрать режим игры до того момента, пока ввод не будет корректен.
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Включить генерацию моделей согласно спецификации? (y/n): ");
                Console.ResetColor();
                dataMode = Console.ReadLine();
            } while (!ModeInputChecker(dataMode, out generatorMode));

            return generatorMode;
        }

        /// <summary>
        /// Метод, преобразующий ввод пользователя в Boolean значение, означающее тип генерации модели.
        /// </summary>
        /// <param name="dataMode">Строка с вводом пользователя</param>
        /// <param name="generatorMode">Следует ли генерировать модель согласно спецификации.</param>
        /// <returns>Значение подтверждающее или опровергающее успешное получение данных.</returns>
        public static bool ModeInputChecker(string dataMode, out bool generatorMode)
        {
            // Если ввод не подходит, выводим ошибку.
            if (dataMode != "n" && dataMode != "y") 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Чтобы выбрать тип генерации, нужно отправить либо [y], либо [n]!\n" +
                                  "Пример отправки: n");
                Console.ResetColor();
                generatorMode = false;
                return false;
            }
            generatorMode = (dataMode == "y");
            return true;
        }
    }


}