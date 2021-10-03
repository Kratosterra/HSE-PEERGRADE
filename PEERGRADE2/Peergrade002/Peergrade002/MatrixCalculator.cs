using System;

namespace Peergrade002
{
    /// <summary>
    /// Основной класс, тут происходит вызов метода калькулятора.
    /// </summary>
    class MatrixCalculator
    {
        /// <summary>
        /// Вход в программу.
        /// </summary>
        static void Main()
        {
            // Вызываем метод калькулятора.
            MatrixCalc();
        }

        /// <summary>
        /// Метод создания скелета программы.
        /// </summary>
        private static void MatrixCalc()
        {
            // Создаем переменую для хранения клавиши.
            ConsoleKeyInfo exitKey;
            // Вызываем заставку и паузу с запросом нажатия кнопки.
            Environment.SplashScreen();
            Environment.Service("stop_button");
            // Выводим инструкции и паузу с запросом нажатия кнопки.
            Environment.Instructions();
            Environment.Service("stop_button");
            // Реализуем повтор прогона программы.
            do
            {
                // Выводим главное меню.
                Environment.MainMenu();
                // Получем выбор действия, которое должен совершить калькулятор.
                SelectAction.Choice(out ushort actionChoice);
                // На основе полученого выбора, начинаем выполнение действия.
                SelectAction.Start(actionChoice);
                Environment.Service("stop_button");
                // Выводим инструкции по выходу из программы.
                Environment.EndProgram();
                // Получаем нажатую кнопку.
                exitKey = Console.ReadKey();
            } while (exitKey.Key != ConsoleKey.Escape);
            // Скажем пользователю "До свидания"!
            Environment.GoodBye1();
            Environment.GoodBye2();
        }
    }

}
