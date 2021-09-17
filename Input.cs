using System;

namespace Peergrade001
{
    class Input //Класс, содержаший методы для отлова некорректных вводов пользователя и возвращения корректных.
    {
        public static bool DigitsInputChecker(string dataDigits, out uint numDigits)
        //Данный метод занимается проверкой корректности ввода колличества цифр для начала игры (от 1 до 10 генерируемых цифр в числе).
        //Метод возвращает одно значение типа bool; true, если ввод корректен и false, если нет.
        //А также значение обработанной пременной для количества цифр.
        {
            bool ok;
            if (uint.TryParse(dataDigits, out numDigits) && numDigits < 11 && numDigits > 0) ok = true; //Если ввод удволетворяет требованиям - выводим true.
            else
            {
                Environment.ConsoleText("digits_error"); //Иначе поднимаем ошибку.
                ok = false;
            }
            return ok;
        }

        public static bool ModeInputChecker(string dataMode, out bool developMode)
        //Данный метод занимается проверкой корректности ввода режима игры.
        //Метод возвращает одно значение типа bool; true, если ввод корректен и false, если нет.
        //А также значение обработанной пременной для режима игры, также в виде bool.
        {
            if (dataMode != "n" && dataMode != "y") //Если ввод не подходит, выводим ошибку.
            {
                Environment.ConsoleText("mode_error");
                developMode = false;
                return false;
            }
            else //Если ввод корректен выбираем значение developMode исходя из ввода пользователя. 
            {
                developMode = (dataMode == "y"); //Устанавливает значение режима игры.
                if (developMode) Environment.ConsoleText("mode_warning"); //Выводим предупреждение о включенном режиме разрабочика.
                return true;
            }
            
        }
        public static bool GuessInputChecker(string dataPlayer, ulong numGame, out ulong numPlayer)
        //Данный метод занимается проверкой корректности ввода догадки пользователя.
        //Метод возвращает одно значение типа bool; true, если ввод корректен и false, если нет.
        //А также значение обработанной пременной догадки пользователя.
        {
            char[] caseOfNums = new char[numGame.ToString().Length]; //Создаем переменную для хранения уникальных введеных чисел.
            char[] caseDataPlayer = dataPlayer.ToCharArray(); //Создаём переменную в которой будет хранится массив с догадкой пользователя.
            for (int i = 0; i < numGame.ToString().Length; i++) caseOfNums[i] = '-'; //Заполняем массив недостижимым '-'.
            if (ulong.TryParse(dataPlayer, out numPlayer) && dataPlayer.Length == numGame.ToString().Length)
            //Если число можно представить в ulong и длинна догадки совпадает с загаданным числом, мы проходим дальше.
            {
                if (dataPlayer.Length != numPlayer.ToString().Length) //если догадка человека начиналась с 0 при разрядности числа больше 1, то сработает данное исключение!
                {
                    Environment.ConsoleText("guess_error_3");
                    return false;
                }
                for (int i = 0; i < numGame.ToString().Length; i++)
                //Задача: узнать уникально ли каждая цифра в числе догадки.
                {
                    if (Array.IndexOf(caseOfNums, caseDataPlayer[i]) == -1) //Если цифра уникальна, мы сохраняем ее в массиве уникальных цифр,
                                                                            //для дальнейшей проверки.
                    {
                        caseOfNums[Array.IndexOf(caseOfNums, '-')] = caseDataPlayer[i];
                    }
                    else // Иначе выводим ошибку, что число содержит повторяющейся цифры.
                    {
                        Environment.ConsoleText("guess_error_2");
                        return false;
                    };

                }
                return true;
            }
            else
            {
                Environment.ConsoleText("guess_error_1"); //Выводим, если формат догадки не подходит.
                return false;
            }
        }
    }

}
