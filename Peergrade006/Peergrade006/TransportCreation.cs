using System;
using EKRLib;

namespace Peergrade006
{
    /// <summary>
    /// Класс, отвечающий за создание транспорта, его генерацию.
    /// </summary>
    static class TransportCreation
    {
        // Задаём переменную для генерации случайного числа.
        private static Random _random = new Random();
        
        /// <summary>
        /// Метод, создающий случайную машину.
        /// </summary>
        /// <returns>Обьект типа Car со случайными параметрами.</returns>
        internal static Car CreateRandomCar()
        {
            // Cоздаём флагЮ для фиксации события создания обьекта.
            bool isTransportCreated = false;
            Car newCar = null;
            // Пока обьект не будет создан, производим попытки его генерации.
            while (!isTransportCreated)
            {
                try
                {
                    newCar = new Car(Tools.GenerateModelName(), (uint)_random.Next(10, 100));
                    isTransportCreated = true;
                }
                catch (TransportException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                    isTransportCreated = false;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                    isTransportCreated = false;
                }
            }
            return newCar;
        }

        /// <summary>
        /// Метод, создающий случайную лодку.
        /// </summary>
        /// <returns>Обьект типа MotorBoat со случайными параметрами.</returns>
        internal static MotorBoat CreateRandomMotorBoat()
        {
            // Cоздаём флаг для фиксации события создания обьекта.
            bool isTransportCreated = false;
            MotorBoat newMotorBoat = null;
            // Пока обьект не будет создан, производим попытки его генерации.
            while (!isTransportCreated)
            {
                try
                {
                    newMotorBoat = new MotorBoat(Tools.GenerateModelName(), (uint)_random.Next(10, 100));
                    isTransportCreated = true;
                }
                catch (TransportException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                    isTransportCreated = false;
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                    isTransportCreated = false;
                }
            }
            return newMotorBoat;
        }
    }
}