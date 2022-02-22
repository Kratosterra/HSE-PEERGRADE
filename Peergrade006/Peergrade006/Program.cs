using System;
using EKRLib;

namespace Peergrade006
{
    /// <summary>
    /// Основной класс программы, содержащий основные методы для её работы.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Точка входа в программу.
        /// </summary>
        static void Main()
        {
            // Вызываем стартовый метод.
            StartTestingSession();
        }
        
        /// <summary>
        /// Метод, отвечающий за последовательный вызов необходимого для работы функционала.
        /// </summary>
        static void StartTestingSession()
        {
            // Создаем переменую для хранения клавиши.
            ConsoleKeyInfo exitKey;
            // Запускаем стартовый интерфейс.
            Environment.StartTransportInterface();
            // Реализуем повтор программы.
            do
            {
                try
                {
                    // Генерируем требуемый массив.
                    Testing.CreateTransportList(out Transport[] listOfTransports);
                    // Оповещаем об окончании генерации.
                    Environment.GenerationFinished();
                    // Создаём файлы.
                    Testing.CreateTwoFiles(listOfTransports);
                    // Выводим инструкции по выходу из программы.
                    Environment.EndProgram();
                    // Получаем нажатую кнопку.
                    exitKey = Console.ReadKey();
                    // Производим отчистку консоли.
                    Console.Clear();
                }
                catch
                {
                    // Вызываем метод, отображающий ошибку общего вида.
                    Environment.GreatError();
                    Environment.EndProgram();
                    exitKey = Console.ReadKey();
                }
            } while (exitKey.Key != ConsoleKey.Escape);
            
            Environment.ShowLastMessageFirst();
        }
        
    }
}