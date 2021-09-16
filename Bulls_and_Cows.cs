using System;

namespace Peergrade001
{
    class Program //Основной класс, в котором содержится основной скелет программы.
    {
        static void Main()
        {
            uint numDigits; //Переменная для настройки колличества загадываемых цифр в числе.
            ulong numGame; //Переменная для загадного числа. 
            ConsoleKeyInfo exitKey;

            do
            {
                Environment.Interface("welcome");//Выводим на экран название игры.
                System.Threading.Thread.Sleep(900); //Сделаем задержку на названии.
                Environment.Interface("start"); //Выводим на экран начальную заставку.
                Environment.ConsoleText("rules"); //Выводим на экран правила игры.
                Environment.ConsoleText("settings"); //Выводим текст о настройках

                numDigits = Game.SetGame(out bool developMode); //Производим настройку, получая число цифр в загадываемом числе и режим игры.
                numGame = Tools.GenerateNum(numDigits, developMode); //Генерируем требуемое число (если пользователь в режиме разработчика, он увидит это число).

                Environment.ConsoleText("num_gen"); //Выводим текст о том, что число сгенерировано.

                Game.PlayLoop(numGame); //Входим в игровую петлю.

                System.Threading.Thread.Sleep(1500); //Создаём эффект сюрпириза!
                Environment.Interface("win"); //Выводим на экран победную заставку.
                Environment.ConsoleText("end_game"); //Текст о том, как выйти из игры.
                exitKey = Console.ReadKey();
            } while (exitKey.Key != ConsoleKey.Escape);
        }
    }

    class Tools //Класс с генератором чисел и инструментами для работы с массивами
    {
        public static bool NumInArray(ulong caseRan, ulong[] caseOfRandom)
        //Данный метод занимается анализом массивов.
        //Если в массиве присутсвует указанный обьект - возвращает значение true, по-другому - false.
        {
            bool ok;
            int indexFilling;
            indexFilling = Array.IndexOf(caseOfRandom, caseRan);
            if (indexFilling == -1) ok = false;
            else ok = true;

            return ok;
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
                Console.WriteLine($"[Режим разарбочика активен]\nСгенерированное число: {numGame}");
                Console.ResetColor();
            }
            return numGame; //выводим сгенерированное число из метода.
        }
    }

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

    class Game //Класс, содержащий основной исходный код игры:
               // 1)Установщик игровой ссесии
               // 2)Монитор игровой ссесии, возвращающий статистику
               // 3)Игровая петля
    {
        public static uint SetGame(out bool developMode)
        //Данный метод занимается получением от пользователя основной настроек для игры - количества загадываемых цифр в числе и игрового режима.
        //Метод возвращает одно значение типа uint для передачи в генератор случайных чисел и одно значение режима игры.
        {
            string dataDigits; //Задаем пременную для строки ввода настройки пользователя.
            string dataMode; //Задаём пременую для хранения ответа пользователя в отношении режима разработчика. 
            uint numDigits; //Задаем пременную для строки вывода настройки в числовом формате.

            do //Просим выбрать режим игры до того момента, пока ввод не будет корректен!
            {
                Environment.ConsoleText("set_game_mode");
                dataMode = Console.ReadLine();
            } while (!Input.ModeInputChecker(dataMode, out developMode)); //Проверяем коректность ввода режима игры.
            do //Просим выбрать режим игры до того момента, пока ввод не будет корректен!
            {
                Environment.ConsoleText("set_game_digit");
                dataDigits = Console.ReadLine();
            } while (!Input.DigitsInputChecker(dataDigits, out numDigits)); //Проверяем коректность ввода режима игры.

            return numDigits;
        }

        public static void GameMaster(ulong numPlayer, ulong numGame, out bool winGame)
        //Данный метод занимается подсчетом "быков" и "коров", получая на вход догадку пользователя и сгенерированое число.
        //Метод выводит на экран сообщение о том, сколько цифр "коров" угадано, но не расположено на своих местах,
        //и сколько цифр "быков" угадано и находится на своих местах. При победе подтверждает это выходным значением true.
        {
            winGame = false; //Задаем переменую победы по-умолчанию на false. 
            char[] guessNum = numPlayer.ToString().ToCharArray(); //Создаем массив для догадки пользователя. 
            char[] trueNum = numGame.ToString().ToCharArray(); //Создаем массив для загаданного компьютером числа. 
            int bulls = 0; //тут будут "быки").
            int cows = 0; //тут будут "коровы").
            for (int i = 0; i < guessNum.Length; i++)
            {
                if (guessNum[i] == trueNum[i]) bulls += 1; //Если совпадает и цифра и его место, прибавляем 1 к сетчику "быков".
                else
                {
                    if (Array.IndexOf(trueNum, guessNum[i]) != -1) cows += 1; //Если эта цифра из догадки содежится в загаданном числе,
                                                                              //прибавляем 1 к сетчику "коров".
                }
            }
            if (bulls == trueNum.Length) //Если колличество "быков" совпадает с размерностью числа, пользователь победил!
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
            string dataPlayer; //Переменая для строки догадки пользователя.
            ulong numPlayer; //Переменная для догадки в виде числа.
            bool winGame; //пременная, фикмсирующая победу или проигрышь.
            do
            {
                do //До того моента, пока пользователь не выйграет, продолжаем запрашивать все новые и новые догадки
                {
                    Environment.ConsoleText("guess");
                    dataPlayer = Console.ReadLine();
                } while (!Input.GuessInputChecker(dataPlayer, numGame, out numPlayer));//Проверяем, подходит ли нам догадка.

                GameMaster(numPlayer, numGame, out winGame); //Проверям и выводим колличество "быков" и "коров".

            } while (!winGame);

        }

    }

    class Environment //Класс, отвечающий за создание интерфеса и надписей. Методы в этом классе превышают рекомендованный методам размер в 40 строк,
                      //из-за большого колличества текстовой информации.
    {
        public static void Interface(string interfaceType)
        //Данный метод занимается выводом на экран крупных элементов интерфейса по кейсам:
        // 1)Начальная заставка
        // 2)Название игры
        // 3)"Это победа!" после абсолютного угадывания числа
        // 4)"Попробуйте снова!" после не слишком удачной партии
        {
            switch (interfaceType)
            {
                case "start":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine(
                        "                                                ░░░░░░░░░░░░░░░░░░░░░░\n" +
                        "                                                ░░░░▄░░▄░░░░░░░░░░░░░░\n" +
                        "                                                ░░░░░█████▓████╗░░░░░░\n" +
                        "                                                ░░░░░└┘████████╚░░░░░░\n" +
                        "───────────────────────────────────────────────────────▌▐──└┘▌▐────────────────────────────────────────────────────────\n" +
                        "──────────────────────────────────── Лучшая игра за последние 0,00001 наносекунды ─────────────────────────────────────\n");
                    Console.ResetColor();
                    break;
                case "welcome":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("\n" +
                        "                              █▀▄ █░█ █░░ █░░ ▄▀▀     ▄▀▄ █▄░█ █▀▄     ▄▀ ▄▀▄ █░░░█ ▄▀▀\n" +
                        "                              █▀█ █░█ █░▄ █░▄ ░▀▄     █▀█ █░▀█ █░█     █░ █░█ █░█░█ ░▀▄ \n" +
                        "                              ▀▀░ ░▀░ ▀▀▀ ▀▀▀ ▀▀░     ▀░▀ ▀░░▀ ▀▀░     ░▀ ░▀░ ░▀░▀░ ▀▀░ \n");
                    Console.ResetColor();
                    break;
                case "win":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("────────────░█▒──────────▒██\n " +
                                      "─────────▓███▓─▒▒▓▓███████▒\n " +
                                      "──────▓██████████████████▒▒███▓\n " +
                                      "───▒████████████████████████▓░\n " +
                                      "─▓███████████████████████████████████\n" +
                                      "████████████████████████████████████\n" +
                                      "██████████████████████████████████\n" +
                                      "███████████████████████████████████████▓\n" +
                                      "████████████████████████████████████████\n" +
                                      "█████████████████████████████████████▒\n" +
                                      "█████████████████████████████████████▓\n" +
                                      "███████████████████████████████████████▓\n" +
                                      "████████████████████████████████████████\n" +
                                      "█████████████████████████████████████▒\n" +
                                      "████████████████████████████████████▒▓\n" +
                                      "██████────▒░░░░░░░▒█████████████████▒\n" +
                                      "█▒████─────▒▒░──▒░░▓████████████████▒\n" +
                                      "█░▓██▓───▓██▓▓▓██▒▒▓███▓█████████████\n" +
                                      "█─▒██▓──▒█▒▒▓▓▓▒░─▓▓██▓▒█████████████\n" +
                                      "█─░██▓──██─█▒░█▒─░▓▓██░▒█████████████\n" +
                                      "▓──██▒─████▒▒▓█▒─▒█▓██─░██▓██████████\n" +
                                      "▒──░█▓─▓▒▒▓▓▓▓░──▓▓▓█▓──██▓██████████░\n" +
                                      "█▓▓▒██─▒▒░░░░░░░▒▓▓██░░░█████████████▒\n" +
                                      "█████████████████████████████████████▒\n" +
                                      "▒▒░░░▒███▓▒░░░░▒████▓▒▒▒▓▒▒██████████▓\n" +
                                      "██████▒▒▓▓──────▒▓▒░▓█████░███████████\n" +
                                      "▒████▒▒░─────────░─▒─█████─▓██████████\n" +
                                      "─▒██▒─────────────────▓▓▓──███████████\n" +
                                      "───────────────────────────███████████\n" +
                                      "───────────────────────────███████████\n" +
                                      "───────────────────────────███████████\n" +
                                      "───────────────────────────████████─██\n" +
                                      "───────────────────────────████████─██\n" +
                                      "───────────██▓▓────────────███████▒─█▓\n" +
                                      "───────────▒█▓▒────────────███████──▓\n" +
                                      "───────────────────────────██████▓\n" +
                                      "────────░████████─────────▒██████\n" +
                                      "█────────────────────────▓██████▓──░▓\n" +
                                      "██▒────────────────────▒█████▓▒█─▒███\n" +
                                      "██▓▓░────────────────░▓██░███▒███████\n" +
                                      "▒█▓─▒▓░─────────────▓▒▒▒█▓██████████\n" +
                                      "████▓▓▓▓▒────────▒▓█████████████████\n" +
                                      "███████████████████████████████████░\n" +
                                      "███████████████████████████████████▒\n" +
                                      "████████████████████████████████████\n" +
                                      "████████████████████████████████████\n" +
                                      "█████████████████████████████████████▓\n" +
                                      "███████████████████████████████████████▒\n " +
                                      "████████████████████████████████████████\n ");
                    Console.WriteLine("─────────────────────────────────────   Украв всех быков, вы стали Саске Учихой  ──────────────────────────────────────\n");
                    Console.ResetColor();
                    break;
                case "try_again":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("******************************************* Не отчаивайся, попробуй еще раз! ******************************************");
                    Console.ResetColor();
                    break;
            }
        }

        public static void ConsoleText(string interfaceType)
        //Метод, содержащий простую текстовую информацию, выводимую на жкран пользователя
        {
            switch (interfaceType)
            {
                case "rules":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Добро пожаловать в игру \"Быки и коровы!\"\nВ этой игре вам предстоит отгадать число, которое случайно сгененрированно непостижимыми компьютерными технологиями!\n" +
                                      "Вам предстоит вводить свои догадки, чтобы понять, какие из цифр в вашей догадке являются:\n \"Быками\" - цифрами, точное положение и значение которых отгадано\n" +
                                      " \"Коровами\" - цифрами, значение которых отгадано, однако положение - нет\nТак как цифры в игровом числе не повторяются, в ваших догодках они тоже не должны повторятся, иначе было бы легко!\n" +
                                      "Ведь вы бы могли заполнять одними и тем же числом все доступное поле, таким образом быстрым перебором узнавая число!\nНо в нашей игре такое не пройдет - придется думать!\n");
                    Console.ResetColor();
                    break;
                case "digits_error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Неверный формат представления колличества цифр в будущем числе!" +
                        " Пожалуйста, повторите ввод!");
                    Console.ResetColor();
                    break;
                case "mode_error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Чтобы выбрать режим игры нужно отправить либо [y], либо [n]!\n" +
                        "Пример отправки: n");
                    Console.ResetColor();
                    break;
                case "guess_error_1":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Догадка представлена в неправильном формате!\n" +
                        "Не забывайте делать так, чтобы догадка была такой же по размеру, что и загаданное число)\n" +
                        "Пожалуйста, повторите ввод!");
                    Console.ResetColor();
                    break;
                case "guess_error_2":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Старайтесь не повторять цифры в своей догадке! Пожалуйста, повторите ввод!");
                    Console.ResetColor();
                    break;
                case "guess_error_3":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Старайтесь не начинать догадку с нуля, если она связана с числом, имеющим больше одного разряда!\n" +
                        "Большое число ведь не может начинаться с нуля)\nПожалуйста, повторите ввод!");
                    Console.ResetColor();
                    break;
                case "set_game_digit":
                    Console.Write("Cколько цифр загадать в числе (от 1 до 10 включительно): ");
                    break;
                case "set_game_mode":
                    Console.Write("Режим разработчика позволяет видеть сгенерированное число!\nВключить его (y/n): ");
                    break;
                case "guess":
                    Console.Write("Введите свою догадку: ");
                    break;
                case "end_game":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Для выхода нажмите Escape, чтобы повторить игру, нажмите любую другую кнопку!");
                    Console.ResetColor();
                    break;
                case "settings":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("*********************** Перед игрой, давайте настроим размер загадываемого числа и режим игры! ************************");
                    Console.ResetColor();
                    break;
                case "all_bulls":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("*****************************************          Вы нашли всех быков!       *****************************************");
                    Console.ResetColor();
                    break;
                case "mode_warning":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!     Режим разработчика активирован     !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    Console.ResetColor();
                    break;
                case "num_gen":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("*****************************************       Ваше число сгенерировано!     *****************************************\n" +
                                      "********************** Давайте постараемcя отгадать число и забрать себе как можно больше быков! **********************");
                    Console.ResetColor();
                    break;
            }
        }

    }

}
