using System;
using static System.Console;

namespace Peergrade002
{
    /// <summary>
    /// Класс для создания пользовательского интерфейса.
    /// </summary>
    class Environment
    {
        /// <summary>
        /// Метод вывода заставки на экран.
        /// </summary>
        public static void SplashScreen()
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine("───────────────────────────────────────────────────────────────────────────────────────────────────────────────────────\n");
            WriteLine("|                        █▄░▄█ ▄▀▄ ▀█▀ █▀▀▄ ▀ █░█     ▄▀ ▄▀▄ █░░ ▄▀ █░█ █░░ ▄▀▄ ▀█▀ ▄▀▄ █▀▀▄                          |\n" +
                      "|                        █░█░█ █▀█ ░█░ █▐█▀ █ ▄▀▄     █░ █▀█ █░▄ █░ █░█ █░▄ █▀█ ░█░ █░█ █▐█▀                          |\n" +
                      "|                        ▀░░░▀ ▀░▀ ░▀░ ▀░▀▀ ▀ ▀░▀     ░▀ ▀░▀ ▀▀▀ ░▀ ░▀░ ▀▀▀ ▀░▀ ░▀░ ░▀░ ▀░▀▀                          |\n" +
                      "───────────────────────────────── Лучший калькулятор за последние 0,00001 наносекунды ─────────────────────────────────\n");
            ResetColor();
        }

        /// <summary>
        /// Метод вывода инструкций на экран.
        /// </summary>
        public static void Instructions()
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine("Добро пожаловать в в Калькулятор матриц! Тут можно производить действия над матрицами! Неожиданный поворот, правда?\n" +
                      "Вам будет доступно восемь действий, и для каждого три метода ввода! Целых три! Но последний метод для куртых гиков!\n" +
                      "Ведь калькулятор умеет считывать информацию из файла, но его еще надо правильно написать! Иначе у вас будет ошибочка(\n" +
                       "Но вы не отчаивайтесь, просто почитате файл README.txt! Там вам наглядно расскажут как стать богом создания файлов .txt!\n");
            ResetColor();
        }

        /// <summary>
        /// Метод вывода главного меню на экран.
        /// </summary>
        public static void MainMenu()
        {
            Environment.Interface("info_main_menu");
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("1. След матрицы!\n" +
                "2. Транспонирование матрицы!\n" +
                "3. Сумма двух матриц!\n" +
                "4. Разность двух матриц!\n" +
                "5. Произведение двух матриц!\n" +
                "6. Умножение матрицы на число!\n" +
                "7. Нахождение определителя матрицы!\n" +
                "8. Найти решение СЛАУ методом Крамера!\n");
            ResetColor();
        }

        /// <summary>
        /// Метод вывода меню методов ввода на экран.
        /// </summary>
        public static void MethodMenu()
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("\n" +
                       "1. Ввести вручную\n" +
                       "2. Сгенерировать\n" +
                       "3. Прочитать из файла input.txt\n");
            ResetColor();
        }

        /// <summary>
        /// Метод вывода ошибок общего плана на экран.
        /// </summary>
        /// <param name="workType">Тип требуемых данных.</param>
        public static void Error(string workType)
        {
            switch (workType)
            {
                case "EOF":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Кто пытается использовать EOF? Пожалуйста, повторите ввод!");
                    ResetColor();
                    break;
                case "-":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("-");
                    ResetColor();
                    break;
            }
        }

        /// <summary>
        /// Метод вывода ошибок, связанных с вводом данных.
        /// </summary>
        /// <param name="workType">Тип требуемых данных.</param>
        public static void InputError(string workType)
        {
            switch (workType)
            {
                case "get_action":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Неверно выбран номер действия, введите число от 1 до 8 включительно)\n" +
                        "Пожалуйста, повторите ввод!");
                    ResetColor();
                    break;
                case "get_matrix":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("В данной строке матрицы обнаружились проблемы! " +
                        "Скорее всего число - не число(\nПожалуйста, повторите ввод!");
                    ResetColor();
                    break;

                case "oversize_get_matrix":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Размер введенной строки меньше строки матрицы!\n" +
                        "Не забывайте про размерность матрицы! Пожалуйста, повторите ввод!");
                    ResetColor();
                    break;
                case "get_num":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Введите число! Пожалуйста, повторите ввод!");
                    ResetColor();
                    break;
                case "get_num_int_generate":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Введите целое положительное число, чтобы обозначить границы генерации!\n" +
                        "От -{введённое число} до +{введённое число}");
                    ResetColor();
                    break;
                case "get_size":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Размер матрицы - это целое положительное число! Не превышает 10! Пожалуйста, повторите ввод!");
                    ResetColor();
                    break;
                case "get_sizes":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Размеры матрицы это два целых положительных числа, введённых через пробел! Не превышают 10!" +
                        " Пожалуйста, повторите ввод!");
                    ResetColor();
                    break;
                case "mode_error":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Чтобы выбрать нужно ли гнерировать дробные числа, нужно отправить либо [y], либо [n]!\n" +
                        "Пример отправки: n");
                    ResetColor();
                    break;
                case "get_method":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Неверный выбор метода ввода данных, введите целое число от 1 до 3 включительно\n" +
                        "Пожалуйста, повторите ввод!");
                    ResetColor();
                    break;
            }

        }

        /// <summary>
        /// Метод вывода ошибок, связанных с матрицами.
        /// </summary>
        ///  <param name="workType">Тип требуемых данных.</param>
        public static void MatrixError(string workType)
        {
            switch (workType)
            {
                case "matrix_multiplication":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Перемножение подобных матриц невозможно, колличество стобцов в первой матрице" +
                        " равно колличеству строк во второй!\nПожалуйста, повторите ввод!");
                    ResetColor();
                    break;
                case "matrix_sum":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Складывать и вычитать можно только матрицы одинаковой размерности!" +
                        " Пожалуйста, повторите ввод!");
                    ResetColor();
                    break;
                case "square_matrix":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Матрица обязана быть квадратной! Пожалуйста, повторите ввод!");
                    ResetColor();
                    break;
                case "sove_matrix":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Невозможно найти решения подобной матрицы, если у вас A переменных, то размерность матрицы: A x A+1!\n" +
                        "В последнем столбце вы должны записывать то, что находится" +
                        " в вашей системе урованений после знака равно.\nПожалуйста, повторите ввод!");
                    ResetColor();
                    break;
            }
        }

        /// <summary>
        /// Метод вывода ошибок, связанных с чтением файлов.
        /// </summary>
        /// <param name="workType">Тип требуемых данных.</param>
        public static void FileError(string workType)
        {
            switch (workType)
            {
                case "file_error":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Ошибка при считывании файла, проверьте README.txt!\n" +
                        "Там вы сможете узнать, как правильно создать файл input.txt.");
                    ResetColor();
                    break;
                case "get_matrix_txt":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("В одной из строк матрицы в файле Input.txt обнаружились проблемы! " +
                        "Скорее всего число - не число!\nВероятно не соблюдена размерность матрицы!\nПожалуйста, проверьте!");
                    ResetColor();
                    break;
                case "get_matrix_double":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Обнаружены проблемы при переводе данных в числа, следите за размерностью матрицы в файле input.txt!");
                    ResetColor();
                    break;
                case "file_one_error":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Не удалось правильно обработать: input.txt!\n" +
                        "Предполагаемая директория \\Peergrade002\\bin\\Debug\\input.txt\n" +
                        "");
                    ResetColor();
                    break;
                case "null_data_file":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("В файле обнаружились проблемы! Следите за присутствием структурных элементов.");
                    ResetColor();
                    break;
                case "read_data_file":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Обнаружена ошибка при чтении файла, вероятно он пуст или отсутствует!\n");
                    ResetColor();
                    break;
                case "read_num_file_error":
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine("Ошибка при получении из файла числа для вычислений с матрицами!\n" +
                        "Возможно в первой строке файла находится не число!\n" +
                        "Проверьте файл input.txt!");
                    ResetColor();
                    break;
            }
        }

        /// <summary>
        /// Метод вывода информации, подсказок для пользователя.
        /// </summary>
        /// <param name="workType">Тип требуемых данных.</param>
        public static void Information(string workType)
        {
            switch (workType)
            {
                case "ask_matrix":
                    Write("\nПожалуйста введите строку матрицы, каждое число через пробел: ");
                    break;
                case "ask_size":
                    Write("\nВведите размер(порядок) квадратной матрицы: ");
                    break;
                case "ask_sizes":
                    Write("\nВведите размеры матрицы через пробел!\n" +
                        "{кол-во строк} {кол-во столбцов}: ");
                    break;
                case "ask_num":
                    Write("\nВведите число для умножения: ");
                    break;
                case "need_double_true":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\nВ генерации будут использованы дробные числа!\n");
                    ResetColor();
                    break;
                case "need_double_false":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\nВ генерации будут отсутствовать дробные числа!\n");
                    ResetColor();
                    break;
                case "ask_double":
                    Write("\nВключить дробные числа в генерацию (y/n): ");
                    break;
                case "ask_num_generate":
                    Write("Введите целое положительное число, чтобы задать края генерации\n" +
                        "От -{введённое число} до +{введённое число}: ");
                    break;
                case "info_matrix":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    Write("\nВывожу матрицу на экран!\n");
                    break;
                case "ask_input":
                    Write("\nВыберете метод ввода: ");
                    break;
                case "choice_start":
                    Write("\nВыберете действие: ");
                    break;
            }
        }

        /// <summary>
        /// Метод создания интерфейса для пользователя.
        /// </summary>
        /// <param name="workType">Тип требуемых данных.</param>
        public static void Interface(string workType)
        {
            switch (workType)
            {
                case "info_main_menu":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("───────────────────────────────────────────── Приступим к выбору действия ────────────────────────────────────────────── \n");
                    ResetColor();
                    break;
                case "trace":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n────────────────────────────────────────── Приступаем к поиску следа матрицы! ─────────────────────────────────────────\n");
                    ResetColor();
                    break;
                case "trans":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n──────────────────────────────────────── Приступаем к транспонированию матрицы! ───────────────────────────────────────\n");
                    ResetColor();
                    break;
                case "sum":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n────────────────────────────────────────── Приступаем к суммированию матрицы! ─────────────────────────────────────────\n");
                    ResetColor();
                    break;
                case "sub":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n───────────────────────────────────────────── Приступаем к вычитанию матриц! ──────────────────────────────────────────\n");
                    ResetColor();
                    break;
                case "multi":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n────────────────────────────────────────── Приступаем к перемножению матриц! ──────────────────────────────────────────\n");
                    ResetColor();
                    break;
                case "multi_num":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n───────────────────────────────────── Приступаем к перемножению матрицы и числа! ──────────────────────────────────────\n");
                    ResetColor();
                    break;
                case "det":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n──────────────────────────────────── Приступаем к вычислению определителя матрицы! ────────────────────────────────────\n");
                    ResetColor();
                    break;
                case "solve":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n───────────────────────────────────── Приступаем к решению СЛАУ в виде матрицы! ───────────────────────────────────────\n");
                    ResetColor();
                    break;
            }
        }

        /// <summary>
        /// Метод создания интерактивного интерфейса для пользователя.
        /// </summary>
        /// <param name="workType">Тип требуемых данных.</param>
        public static void Service(string workType)
        {
            switch (workType)
            {
                case "stop_button":
                    //Выводим запрос на нажатие клавиши, создаём искусственную паузу.
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("Нажимите, чтобы продолжить!");
                    Console.ResetColor();
                    Console.ReadKey();
                    //Если пользователь додумался нажать не Enter,
                    //мы все равно перезаписываем верхнюю строку на пустую.
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.WriteLine("");
                    break;
            }
        }

        /// <summary>
        /// Метод вывода инструкций по выходу на экран.
        /// </summary>
        public static void EndProgram()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Для выхода нажмите Escape, чтобы повторить игру, нажмите любую другую кнопку!");
            Console.ResetColor();
        }

        /// <summary>
        /// Метод вывода принятой строки матрицы на экран.
        /// </summary>
        /// <param name="data">Строка с элементами строки матрицы</param>
        public static void StringAproved(string data)
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine($"\nПринята строка: | {data}|\n");
            ResetColor();
        }

        /// <summary>
        /// Метод красивого вывода матрицы на экран.
        /// </summary>
        /// <param name="matrix">Матрица типа double[,]</param>
        public static void PrintMatrix(double[,] matrix)
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine("\n");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Write("|\t");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Write($"{matrix[i, j]}\t");
                }
                Write("|");
                WriteLine();
            }
            WriteLine("\n");
            ResetColor();
        }

        /// <summary>
        /// Метод вывода принятого числа на экран.
        /// </summary>
        /// <param name="num">Преобразованное полученное число.</param>
        public static void NumReceived(double num)
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine($"\nПолучено число: {num}");
            ResetColor();
        }

        /// <summary>
        /// Метод вывода сгенерированного числа на экран.
        /// </summary>
        /// <param name="num">Сгенерированное число.</param>
        public static void NumGen(double num)
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine($"\nСгенерировано число: {num}");
            ResetColor();
        }

        /// <summary>
        /// Метод создания интерфейса пользователя для вывода на экран данных о вводе.
        /// </summary>
        /// <param name="workType">Тип требуемых данных.</param>
        public static void ConstructInfo(string workType)
        {
            switch (workType)
            {
                case "one_matrix_received":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n───────────────────────────────────────── Вывожу полученную матрицу на экран! ─────────────────────────────────────────\n");
                    ResetColor();
                    break;
                case "two_matrix_received":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n───────────────────────────────────────── Вывожу полученные матрицы на экран! ─────────────────────────────────────────\n");
                    ResetColor();
                    break;
                case "one_matrix_gen":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n─────────────────────────────────────── Вывожу сгенерированную матрицу на экран! ──────────────────────────────────────\n");
                    ResetColor();
                    break;
                case "two_matrix_gen":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n─────────────────────────────────────── Вывожу сгенерированные матрицы на экран! ──────────────────────────────────────\n");
                    ResetColor();
                    break;
                case "first_matrix":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n─I──────────────────Данные для первой матрицы!");
                    ResetColor();
                    break;
                case "second_matrix":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n─II─────────────────Данные для второй матрицы!");
                    ResetColor();
                    break;
                case "start_gen":
                    ForegroundColor = ConsoleColor.DarkCyan;
                    WriteLine("\n─GEN────────────────Настройки генерации!");
                    ResetColor();
                    break;
            }
        }

        /// <summary>
        /// Метод вывода ответа в виде следа матрицы.
        /// </summary>
        /// <param name="ans">След матрицы.</param>
        public static void Trace(double ans)
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine($"\nCлед матрицы: {ans}\n");
            ResetColor();
        }

        /// <summary>
        /// Метод вывода ответа в виде транспонированной матрицы.
        /// </summary>
        /// <param name="ans">Транспонированная матрица.</param>
        public static void Transposition(double[,] ans)
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("\nТранспонированная матрица: ");
            ResetColor();
            Environment.PrintMatrix(ans);
        }

        /// <summary>
        /// Метод вывода ответа в виде матрицы, суммы двух других.
        /// </summary>
        /// <param name="ans">Сумма матриц.</param>
        public static void Sum(double[,] ans)
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("\nСумма матриц равна: ");
            ResetColor();
            Environment.PrintMatrix(ans);
        }

        /// <summary>
        /// Метод вывода ответа в виде матрицы, разности двух других.
        /// </summary>
        /// <param name="ans">Разность матриц.</param>
        public static void SubSum(double[,] ans)
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("\nРазность матриц равна: ");
            ResetColor();
            Environment.PrintMatrix(ans);
        }

        /// <summary>
        /// Метод вывода ответа в виде матрицы, произведения двух других.
        /// </summary>
        /// <param name="ans">Произведение матриц.</param>
        public static void Multi(double[,] ans)
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("\nПроизведение матриц равно:");
            ResetColor();
            Environment.PrintMatrix(ans);
        }

        /// <summary>
        /// Метод вывода ответа в виде матрицы, произведения матрицы и числа.
        /// </summary>
        /// <param name="ans">Произведение матрицы и числа.</param>
        public static void MultiNum(double[,] ans)
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine("\nПроизведение числа на матрицу равно: ");
            ResetColor();
            Environment.PrintMatrix(ans);
        }

        /// <summary>
        /// Метод вывода ответа в виде числа, определителя матрицы.
        /// </summary>
        /// <param name="ans">Опредлелитель матрицы.</param>
        public static void Determinant(double ans)
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine($"\nОпределитель матрицы равен: {ans}\n");
            ResetColor();
        }

        /// <summary>
        /// Метод вывода ответа в виде нескольких строк, содержащих корни уравнения.
        /// </summary>
        /// <param name="ans">Строка с корнями уравнений.</param>
        public static void Solve(string ans)
        {
            ForegroundColor = ConsoleColor.Cyan;
            WriteLine($"\nКорни уравнения:\n{ans}\n");
            ResetColor();
        }

        /// <summary>
        /// Метод прощания с пользователем!
        /// </summary>
        public static void GoodBye1()
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine("───────────────────────────────── Лучший калькулятор за последние 0,00001 наносекунды ─────────────────────────────────\n" +
                "═══════════░▓\n" +
                "══════════░▓▓\n" +
                "════════░▓░░▓\n" +
                "═══════▓▓░░░▓\n" +
                "▓════░▓░░░░░▓══════════░▓▓░\n" +
                "▓═══▒▓░░░░░░▓░══════░▓▓░░▓░\n" +
                "▓══▓▒═░░░░░░░▓════░▓▓░░░░▓\n" +
                "░▓▓░═░░░░░░░░▓══░▒▓░░░░░▒▓\n" +
                "░░░░░░░░░░░░░▒▓▓▓░░░░░░═▓═════░▓▓▓▓▓\n" +
                "░░░░░░░░░░░░░░░░░░░░░░░░▓═░▓▓▓▓░░▒▓\n" +
                "░░░░░░░░░░░░░░░░░░░░░░░░▓▓▓░░░═░▒▓░\n" +
                "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░═░▓▓▒\n" +
                "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▓▓▒\n" +
                "░░░░░░░░░░░░░░░░░══░░░░░░░░░▒▓▓▓▓▓▓▓▓▓▓▓\n" +
                "░══════════░░════░▒▒░░░═░░░░▒░░░░░░░░░▓▓\n" +
                "▒░══░▒▒▓▓██▓══░▒█▓░════▒▓░░░═░░░═░░▒▓▓▒\n" +
                "▓█████████▓░▒▓███═══░▒██▒═░░░░░░░▒▓▓▒\n" +
                "▓█▓▓▓▓▓▓▓█▓█████══░▓████═══▒▒═░░░░░▓▓\n" +
                "▓▓▓▓▓▓▓▓▓██▓▓▓█▒░▓█████▒══▒▓▒═░░░░══▓▓▓▓\n" +
                "▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓████▓▓█░═▒▒█▓═░░░░░░▒▒▓\n" +
                "▓▓▓▓▓▓▓▓▓▓▓▓▓████▓▓▓▓▓▓═▒▓██▓═░░░═░▓▓▓\n" +
                "▓█▓▓▓▓▓▓▓▓▓▓▓██▓▓▓▓▓▓█▒▒████▓═░░░░▓\n" +
                "▒▒▓████████████████▓▓▓▓██▓▓█▓═░░═▓█\n" +
                "▒▒░░▒░░░░░░▒░▒▒▒▓▓██████▓▓▓█▓═░═▒▓█▓\n" +
                "▒██▒░░══════░▓░═░░░▒▒▓████▓▓█══░█══▓\n" +
                "▒▒▒▒░▒═░░░═░▓░░░░░░░░░░▒▓████══▓▒══▓\n" +
                "▒▒▒▒░▒═░░░═░▓░░░░░░░░░░▒▓████══▓▒══▓\n" +
                "▒▒▓▓░▓═░░░═▒▓░░░░░░░░░░░░░▓██═▓▓══░▓\n" +
                "▒▓▓▒░▓═░░═▒▓░░░░░░░░░░░░░══▒▒▒██▒══▓\n" +
                "▓▒░░▒▓═══░▓░░░░░░░░░░░░░░▓▓▓▒░▓██▓═▓\n" +
                "▓▓░░▒▓═░░▒▒░░░░░░░░░░░░░░▓═▒█░░▓██▓▓\n" +
                "▓█▒░▒▓═▓▓▒░░░░░░░░░░░░░░░▓══██▒▒▓██");
            ResetColor();
        }

        /// <summary>
        /// Метод прощания с пользователем!
        /// </summary>
        public static void GoodBye2()
        {
            ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine(
                "░░░░▒▓▒█▓░░░░░░░░░░░░░░░░▓═▒█▓▓▒██░\n" +
                "░░░░▒█▓▒░░░░▒░░░░░░░░░░░░░▓▓▓▒▒▒▒\n" +
                "▒░░░▒█▒░░░░▒▒▒▒▒▒▒▒▒▒▒░░░░▒▓▒░▓░\n" +
                "█▒░░▒▓░░░░░░░░░░░░▒▒▒▒▒░░░░░░░▓\n" +
                "▓▓░░▒▒░░░░░░░░░░░░░░░░░░░░░░░░▓\n" +
                "▒▓░░░░░░░▒▒▒▒▒▒▒▒▒▒▒░░░░░░░░░░░▓\n" +
                "▒▓░░░░░░░░░░░░░░░░░▒░░░░░░░░░░░▒▓\n" +
                "▒▓░░░░░░░░░░░░░░▒▒░░░░░░░░░░░░░░▓▒\n" +
                "▒▓░░░░░░░▒▒▒▒▒▒▒▒▒░░░░░░░░░░▒▓▓▒▒█░\n" +
                "▒▓▒░░░░░░▒▒░░░░░░░░░░░░░░░░░▒▓██▓▒\n" +
                "▒▓▓░░░░░░░░░░░░░░░░░░▒▒▒▒░░░░▓░\n" +
                "░▒▓▓░░░░░░░░░░░░░░░░▒▒▒▒▓▓▓░▓\n" +
                "░▒▓▓▓▒░░░░░░░░░░░░░░░░░░░▒▓█▓\n" +
                "░░█▒▒▓▓▒░░░░░░░░░░░░░░░░▒▒▓▒\n" +
                "░░▓▓▒▒▒▓▓▒░░░░░░░░░░░░░░░▒▓\n" +
                "░░▒█▒▒▒▒▒▓▓▒▒░░░░░░░░░░░░▓\n" +
                "░░░▓▓▒▒▒▒▒▒▓▓▓▒░░░░░░░░░░▓\n" +
                "░░░▒▓▒▒▒▒▒▒▒▒▒▓▓▓▒░░░░░░░▓\n" +
                "░░░░▓▒▒▒▒▒▒▒▒▒▒▓▓▓▒▒░░░░▓░\n" +
                "░░░░▓▓▒▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓▓▓░\n" +
                "░░░░░▓▒▒▒▒▒▓░\n" +
                "▓▓██▓▓▓██▓▓██▒\n" +
                "▓▓██▓▓▓██▓▓██▒\n" +
                "▓▓██▓▓▓██▓▓██▒\n" +
                "▓▓██▓▓▓██▓▓██▒\n" +
                "\n" +
                "▄▀▀ ▄▀▄ ▄▀▀ █░█ █░▄▀ █▀▀     █░░░█ █░░ █▀▀ █▀▀▄ █▀▀     ▄▀▄ █▀▀▄ █▀▀     █▄░▄█ ▀▄░▄▀     █▀▄ █░█ █░░ █░░ ▄▀▀\n" +
                "░▀▄ █▀█ ░▀▄ █░█ █▀▄░ █▀▀     █░█░█ █▀▄ █▀▀ █▐█▀ █▀▀     █▀█ █▐█▀ █▀▀     █░█░█ ░░█░░     █▀█ █░█ █░▄ █░▄ ░▀▄\n" +
                "▀▀░ ▀░▀ ▀▀░ ░▀░ ▀░▀▀ ▀▀▀     ░▀░▀░ ▀░▀ ▀▀▀ ▀░▀▀ ▀▀▀     ▀░▀ ▀░▀▀ ▀▀▀     ▀░░░▀ ░░▀░░     ▀▀░ ░▀░ ▀▀▀ ▀▀▀ ▀▀░\n");
            ResetColor();
        }

    }

}
