using System;
using System.IO;
using System.Text;
using EKRLib;

namespace Peergrade006
{
    /// <summary>
    /// Класс, отвечающий за тестирование функционала библиотеки EKRLib.
    /// </summary>
    static class Testing
    { 
        // Задаём переменную для генерации случайного числа.
        private static Random _random = new Random();
        
        /// <summary>
        /// Метод генерирующий масив транспорта для тестирования библиотеки.
        /// </summary>
        /// <param name="listOfTransports">Массив сгенерированного транспорта.</param>
        internal static void CreateTransportList(out Transport[] listOfTransports)
        {
            // Задаём колличество генерируемого транспорта.
            int numOfTransports = _random.Next(6, 10);
            // Выводим информацию
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"============================================================\n"+
                              $"Будет сгенерировано {numOfTransports} транспортных средств.\n" +
                              $"============================================================");
            Console.ResetColor();
            // Создаём массив транспорта.
            listOfTransports = new Transport[numOfTransports];
            for (int i = 0; i < listOfTransports.Length; i++)
            {
                // Равновероятно выбираем тип трансопрта.
                int randomChoice = _random.Next(0, 2);
                if (randomChoice == 1)
                {
                    Car newCar = TransportCreation.CreateRandomCar();
                    listOfTransports[i] = newCar;
                    Console.WriteLine(newCar.StartEngine());
                }
                else
                {
                    MotorBoat newMotorBoat = TransportCreation.CreateRandomMotorBoat();
                    listOfTransports[i] = newMotorBoat;
                    Console.WriteLine(newMotorBoat.StartEngine());
                }
            }
        }

        /// <summary>
        /// Метод, записывающий массив транспорта по двум файлам.
        /// </summary>
        /// <param name="listOfTransports">Массив сгенерированного транспорта.</param>
        internal static void CreateTwoFiles(Transport[] listOfTransports)
        {
            // Флаг проверки корректности операций.
            bool isWorkCorrect = true;
            try
            {
                // Производим попытку записи в файлы.
                TryToCreateFiles(listOfTransports);
            }
            catch (UnauthorizedAccessException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("У вас нет доступа к данным файлам!");
                Console.ResetColor();
                isWorkCorrect = false;
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка при доступе к файлу!\nВероятно перехвачен поток одного из файлов!");
                Console.ResetColor();
                isWorkCorrect = false;
            }
            finally
            {
                // Выводим коненчную информацию.
                string info = isWorkCorrect ? "корректно, успех" : "некорректно, провал, исправьте ошибки";
                string filePath = Path.GetFullPath(
                    $"..{Path.DirectorySeparatorChar}" +
                    $"..{Path.DirectorySeparatorChar}" +
                    $"..{Path.DirectorySeparatorChar}" +
                    $"..{Path.DirectorySeparatorChar}");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"\nРабота завершена {info}!\n" +
                                  $"Путь до файлов: {filePath}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Метод, производящий попытку записи всей информации о массиве в два файла.
        /// </summary>
        /// <param name="listOfTransports">Массив сгенерированного транспорта.</param>
        private static void TryToCreateFiles(Transport[] listOfTransports)
        {
            // Подготавливаем файлы для записи.
            Tools.PrepareFilesToWriting(out var carFileName, out var boatFileName);
            foreach (var transport in listOfTransports)
            {
                // В зависимости от типа обьекта, записываем его в тот или иной файл.
                switch (transport)
                {
                    case Car:
                        File.AppendAllLines(carFileName, new[] {transport.ToString()}, Encoding.Unicode);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"Файл {Path.GetFileName(carFileName)}:\n {transport} - Успешно записано!");
                        Console.ResetColor();
                        break;
                    case MotorBoat:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"Файл {Path.GetFileName(boatFileName)}:\n {transport} - Успешно записано!");
                        Console.ResetColor();
                        File.AppendAllLines(boatFileName, new[] {transport.ToString()}, Encoding.Unicode);
                        break;
                }
            }
        }
    }
}