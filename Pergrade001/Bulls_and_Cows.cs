using System;

namespace Peergrade001
{
    class Program //Основной класс, в котором содержится основной скелет программы.
    {
        static void Main() //Основной класс, тут происходит вызов оcновного метода игры
        {
            BullsAndCows(out _, out _, out _); //Вызываем метод игры
        }

        private static void BullsAndCows(out uint numDigits, out ulong numGame, out ConsoleKeyInfo exitKey)
        //Данный метод представляет собой сам скелет игры, в котором содержится вызов методов для реализации игры.
        //numDigits - переменная для настройки количества загадываемых цифр в числе.
        //numGame - переменная для настройки количества загадываемых цифр в числе.
        //exitKey - переменная для хранения информации о клавише выхода
        {
            do //В конце игры пользователь имеет право нажать Escape, чтобы выйти из игры, или любую другую клавишу, чтобы начать игру заново.
            {
                Environment.Interface("welcome");//Выводим на экран название игры.
                Environment.Interface("start"); //Выводим на экран начальную заставку.
                Environment.Interface("stop_button"); //Выводим запрос на нажатие клавиши, создаём искусственную паузу.
                Environment.ConsoleText("rules"); //Выводим на экран правила игры.
                Environment.Interface("stop_button"); //Выводим запрос на нажатие клавиши, создаём искусственную паузу.
                Environment.ConsoleText("settings"); //Выводим текст о настройках.

                numDigits = Game.SetGame(out bool developMode); //Производим настройку, получая число цифр в загадываемом числе и режим игры.
                numGame = Tools.GenerateNum(numDigits, developMode); //Генерируем требуемое число (если пользователь в режиме разработчика, он увидит это число).

                Environment.ConsoleText("num_gen"); //Выводим текст, что число сгенерировано.

                Game.PlayLoop(numGame); //Входим в игровую петлю.

                System.Threading.Thread.Sleep(1200); //Создаём эффект сюрприза, задержку в одну секунду!
                Environment.Interface("win"); //Выводим на экран победную заставку.
                Environment.ConsoleText("end_game"); //Текст о том, как выйти из игры.
                exitKey = Console.ReadKey();
            } while (exitKey.Key != ConsoleKey.Escape);
        }
    }

}
