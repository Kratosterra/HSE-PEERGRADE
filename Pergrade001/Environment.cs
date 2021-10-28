﻿using System;

namespace Peergrade001
{
    class Environment //Класс, отвечающий за создание интерфейса и надписей. Методы в этом классе превышают рекомендованный методам размер в 40 строк,
                      //из-за большого колличества текстовой информации.
    {
        public static void Interface(string interfaceType)
        //Данный метод занимается выводом на экран крупных элементов интерфейса по кейсам:
        // 0)Запрос на нажатие клавиши, чтобы продолжить
        // 1)Начальная заставка
        // 2)Название игры
        // 3)"Это победа!" после абсолютного угадывания числа
        // 4)"Попробуйте снова!" после неудачной партии
        {
            switch (interfaceType)
            {
                case "stop_button":
                    Environment.ConsoleText("stop"); //Выводим запрос на нажатие клавиши, создаём искусственную паузу.
                    Console.ReadKey();
                    Console.SetCursorPosition(0, Console.CursorTop - 1); //Если пользователь додумался нажать не Enter, мы все равно перезаписываем верхнюю строку на пустую.
                    Console.WriteLine("");
                    break;
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
        //Метод, содержащий простую текстовую информацию, выводимую на экран пользователя.
        {
            switch (interfaceType)
            {
                case "stop":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Нажимите Enter, чтобы продолжить!");
                    Console.ResetColor();
                    break;
                case "rules":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("Добро пожаловать в игру \"Быки и коровы!\"\nВ этой игре вам предстоит отгадать число, которое случайно сгененрировано непостижимыми компьютерными технологиями!\n" +
                                      "Вам предстоит вводить свои догадки, чтобы понять, какие из цифр в вашей догадке являются:\n \"Быками\" - цифрами, точное положение и значение которых отгадано\n" +
                                      " \"Коровами\" - цифрами, значение которых отгадано, однако положение - нет\nТак как цифры в игровом числе не повторяются, в ваших догадках они тоже не должны повторятся, иначе было бы легко!\n" +
                                      "Ведь вы бы могли заполнять одними и тем же числом все доступное поле, таким образом быстрым перебором узнавая число!\nНо в нашей игре такое не пройдет - придется думать!\n");
                    Console.ResetColor();
                    break;
                case "digits_error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Неверный формат представления количества цифр в будущем числе!" +
                        " Пожалуйста, повторите ввод!");
                    Console.ResetColor();
                    break;
                case "mode_error":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Чтобы выбрать режим игры, нужно отправить либо [y], либо [n]!\n" +
                        "Пример отправки: n");
                    Console.ResetColor();
                    break;
                case "guess_error_1":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Догадка представлена в неправильном формате!\n" +
                        "Не забывайте делать так, чтобы догадка была такой же по размеру, что и загаданное число)\n" +
                        "Ориентируетесь на стрелочки над полем ввода догадки, они наглядно показывают количество требуемых цифр!\n" +
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
                    Console.WriteLine("*********************** Перед игрой давайте настроим размер загадываемого числа и режим игры! ************************");
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

        public static void GenerateGuessHelp(ulong numGame)
        //Метод создающий стрелочки над полем ввода, чтобы пользователь видел, сколько ещё символов необходимо ввести.
        {
            string helpUser = "";

            for (int i = 0; i < numGame.ToString().Length; i++)
            {
                helpUser += "▼";
            }                                         
            Console.WriteLine($"                      {helpUser}");
        }
    }

}