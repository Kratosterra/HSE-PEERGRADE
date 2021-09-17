using System;

namespace Peergrade001
{
    class Tools //Класс с генератором чисел и инструментами для работы с массивами
    {
        public static bool NumInArray(ulong caseRan, ulong[] caseOfRandom)
        //Данный метод занимается анализом массивов.
        //Если в массиве присутсвует указанный обьект - возвращает значение true, по-другому - false.
        {
            int indexFilling;
            indexFilling = Array.IndexOf(caseOfRandom, caseRan);
            if (indexFilling == -1) return false;
            else return true;
        }

        public static ulong[] FillArray(ulong caseRan, ulong[] CaseOfRandom, int indexFilling)
        //Данный метод заполняет массив указанным числом по определеному индексу.
        {
            ulong[] ans = CaseOfRandom;

            if (ans.Length > 1 && caseRan == 0 && indexFilling == 0) ans[indexFilling] = caseRan + 1; //Учтем, что если размерность числа больше 1,
                                                                                                      //оно не может начинаться с 0, поэтому сохраним 1
            else ans[indexFilling] = caseRan;

            return ans;
        }

        public static ulong GenerateNum(uint numDigits, bool developMode)
        //Данный метод занимается генерацией подходящего игрового числа, получая на ввод колличество генерируемых цифр в числе.
        //Условие генарции: никакая цифра не может присутствовать в числе дважды, число не может начинаться с нуля (если оно, конечно не одноразраядное).
        //Метод возвращает одно значения типа ulong - сгененрированое число, удволетворяющее условиям игры и начальным настройкам пользователя.
        {
            ulong numGame; //Переменная для генерируемого числа.
            var ran = new Random(); //Cоздаем обьект рандома.
            ulong caseRan; //Сюда мы будем генерировать цифру для числа.
            ulong[] caseOfRandom = new ulong[numDigits]; //Пременная для сохранения уже сгенерированных цифр.
            for (int i = 0; i < numDigits; i++) caseOfRandom[i] = 10; //заменяем каждый эллемент одномерного массива недостижимой 10,
                                                                      //чтобы ориентироваться в массиве.
            numGame = 0; //Присваиваем игровому числу значение 0.
            for (int i = 0; i < numDigits; i++) //Начинаем генерацию цифр для числа.
            {
                if (numDigits == 10 && caseOfRandom[9] == 10 && caseOfRandom[8] != 10) //Сразу учтём, что генерация последней уникальной цифры при размере числа 10 занимет довольно много времени,
                                                                                       //поэтому, воспользуемся простейшей арифметикой и будем задавать
                                                                                       //последенюю цифру как разность суммы чисел (1-9) и суммы чисел, которые сохранены в массиве.
                {
                    ulong sumOfAll = 45; //сумма числе от 1 до 9 включительно.
                    for (int g = 0; g < numDigits - 1; g++) sumOfAll -= caseOfRandom[g];
                    numGame = (numGame * 10) + sumOfAll;
                }
                else
                {
                    do { caseRan = (uint)(ran.Next(0, 10)); } while (Tools.NumInArray(caseRan, caseOfRandom)); //Пока цифра неуникальна - генерируем новую!
                    caseOfRandom = Tools.FillArray(caseRan, caseOfRandom, Array.IndexOf(caseOfRandom, (ulong)10)); //Если цифра уникальна, она записывается
                                                                                                                   //на пустое место в массиве, где стоит первая 10
                    numGame = numGame * 10 + caseRan; //Арифметически сдвигаем разряд прошлого значения влево, добавляя правее новое число.
                    if (numDigits > 1 && numGame == 0) numGame++; //Учтем, что если размерность числа больше 1, оно не может начинаться с 0.
                }
            }
            if (developMode) //При активном режиме разработчика выводим на экран сгенерированное число.
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"[Режим разработчика активен]\nСгенерированное число: {numGame}");
                Console.ResetColor();
            }
            return numGame; //выводим сгенерированное число из метода.
        }
    }

}
