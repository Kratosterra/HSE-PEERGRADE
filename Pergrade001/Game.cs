using System;

namespace Peergrade001
{
    class Game //Класс, содержащий основной исходный код игры:
               // 1)Установщик игровой сессии
               // 2)Монитор игровой сессии, возвращающий статистику
               // 3)Игровая петля
    {
        public static uint SetGame(out bool developMode)
        //Данный метод занимается получением от пользователя основных настроек для игры - количества загадываемых цифр в числе и игрового режима.
        //Метод возвращает одно значение типа uint для передачи в генератор случайных чисел и одно значение режима игры.
        {
            string dataDigits; //Задаём переменную для строки ввода настройки пользователя.
            string dataMode; //Задаём переменую для хранения ответа пользователя в отношении режима разработчика. 
            uint numDigits; //Задаём переменную для строки вывода настройки в числовом формате.

            do //Просим выбрать режим игры до того момента, пока ввод не будет корректен!
            {
                Environment.ConsoleText("set_game_mode");
                dataMode = Console.ReadLine();
            } while (!Input.ModeInputChecker(dataMode, out developMode)); //Проверяем корректность ввода режима игры.
            do //Просим выбрать размерность загадываемого числа до того момента, пока ввод не будет корректен!
            {
                Environment.ConsoleText("set_game_digit");
                dataDigits = Console.ReadLine();
            } while (!Input.DigitsInputChecker(dataDigits, out numDigits)); //Проверяем корректность ввода режима игры.

            return numDigits;
        }

        public static void GameMaster(ulong numPlayer, ulong numGame, out bool winGame)
        //Данный метод занимается подсчетом "быков" и "коров", получая на вход догадку пользователя и сгенерированное число.
        //Метод выводит на экран сообщение о том, сколько цифр "коров" угадано, но не расположено на своих местах,
        //и сколько цифр "быков" угадано и находится на своих местах. При победе подтверждает это выходным значением true.
        {
            winGame = false; //Задаем переменную победы по умолчанию на false. 
            char[] guessNum = numPlayer.ToString().ToCharArray(); //Создаем массив для догадки пользователя. 
            char[] trueNum = numGame.ToString().ToCharArray(); //Создаем массив для загаданного компьютером числа. 
            int bulls = 0; //тут будут "быки").
            int cows = 0; //тут будут "коровы").
            for (int i = 0; i < guessNum.Length; i++)
            {
                if (guessNum[i] == trueNum[i]) bulls += 1; //Если совпадает и цифра, и её место, прибавляем 1 к счетчику "быков".
                else
                {
                    if (Array.IndexOf(trueNum, guessNum[i]) != -1) cows += 1; //Если эта цифра из догадки содержится в загаданном числе,
                                                                              //прибавляем 1 к счетчику "коров".
                }
            }
            if (bulls == trueNum.Length) //Если количество "быков" совпадает с размерностью числа, пользователь победил!
            {
                Environment.ConsoleText("all_bulls");
                winGame = true;
            }
            else //Иначе, выводим статистику пользователя.
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"***************************************** Угадано Быков: {bulls} || Угадано коров: {cows} ****************************************\n");
                Console.ResetColor();
                Environment.Interface("try_again");
            }

        }

        public static void PlayLoop(ulong numGame)
        //Метод для создания игровой петли, он позволяет пользователю сколько угодно раз производить догадки относительно загаданного числа.
        //Метод получает на вход только загаданное значение.
        {
            string dataPlayer; //Переменная для строки догадки пользователя.
            ulong numPlayer; //Переменная для догадки в виде числа.
            bool winGame; //Переменная, фиксирующая победу или проигрыш.
            do
            {
                do //До того момента, пока пользователь не выиграет, продолжаем запрашивать всё новые и новые догадки
                {
                    Environment.GenerateGuessHelp(numGame); //Генерируем указатели, чтобы пользователь не ошибся с количеством символов в своей догадке.
                    Environment.ConsoleText("guess"); //Предлагаем ввести догадку.
                    dataPlayer = Console.ReadLine();
                } while (!Input.GuessInputChecker(dataPlayer, numGame, out numPlayer));//Проверяем, подходит ли нам догадка.

                GameMaster(numPlayer, numGame, out winGame); //Проверям и выводим количество "быков" и "коров".

            } while (!winGame);

        }

    }

}
