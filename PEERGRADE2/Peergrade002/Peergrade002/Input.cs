using System;
using static System.Console;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace Peergrade002
{
    /// <summary>
    /// Класс, заведующий проверкой ввода пользователя и его выводом.
    /// </summary>
    class Input
    {
        /// <summary>
        /// Метод, возвращающий номер требуемого действия, полученного от пользователя.
        /// </summary>
        /// <param name="actionChoiceData">Строка, означающая ввод пользователя.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении числа!</item>
        /// <item>Число типа ushort, oзначающее выбор действия.</item>
        /// </list>
        /// </returns>
        public static bool GetAction(string actionChoiceData, out ushort actionChoice)
        {
            // Если введёное число не подходит под выбор действия - выводим ошибку.
            if (!ushort.TryParse(actionChoiceData, out actionChoice) || actionChoice == 0 || actionChoice > 8)
            {
                Environment.InputError("get_action");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Метод, возвращающий дополненую матрицу, получая строку матрицы от пользователя,
        /// может подогнать введёную строку под размер матрицы,
        /// если она содержит больше обьектов для матрицы, чем надо.
        /// </summary>
        /// <param name="matrixData">Строка матрицы в строчном представлении.</param>
        /// <param name="height">Колличество строк матрице.</param>
        /// <param name="width">Колличество стобцов в матрице.</param>
        /// <param name="matrix">Матрица с модификатором ref, для записи на ходу.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении матрицы!</item>
        /// <item>Модифичированный двумерный массив типа double[,].</item>
        /// </list>
        /// </returns>
        public static bool GetMatrix(string matrixData, int height, int width, ref double[,] matrix)
        {
            // Создаём сторочный массив для хранения обьёктов, разделённых пробелом.
            string[] sizeData;
            // Проверяем строку на содержания null или EOF.
            if (matrixData == null)
            {
                Environment.Error("EOF");
                return false;
            }
            sizeData = matrixData.Split(' ', '\t');
            // Если в строке недостаочно обьёктов, выводим ошибку!
            if (sizeData.Length < width)
            {
                Environment.InputError("oversize_get_matrix");
                return false;
            }
            string approvString = "";
            int conWidth = width;
            for (int i = 0; i < width; i++)
            {
                // Если считанный обьект нельзя преобразовать в число - выводим ошибку!
                // Иначе добавляем его в матрицу и строку для подтверждения))).
                if (double.TryParse(sizeData[i], out double obj))
                {
                    matrix[height, width - conWidth] = obj;
                    conWidth -= 1;
                    approvString += $"{obj} ";
                }
                else
                {
                    Environment.InputError("get_matrix");
                    return false;
                }

            }
            // Выводим строку матрицы, которую мы приняли!
            Environment.StringAproved(approvString);
            return true;
        }

        /// <summary>
        /// Метод, возвращающий матрицу, построчно полученную от пользователя.
        /// </summary>
        /// <param name="size1">Колличество строк матрице.</param>
        /// <param name="size2">Колличество стобцов в матрице.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Двумерный массив типа double[,], полученную от пользователя матрицу</item>
        /// </list>
        /// </returns>
        public static void AskForMatrix(int size1, int size2, out double[,] matrix)
        {
            matrix = new double[size1, size2];
            string matrixData;
            // Просим пользователя ввести каждую строку матрицы.
            for (int i = 0; i < size1; i++)
            {
                // Пока пользователь не ввёдет подходящую строку, запрашиваем ёё снова и снова.
                do
                {
                    Environment.Information("ask_matrix");
                    matrixData = ReadLine();
                } while (!GetMatrix(matrixData, i, size2, ref matrix));
            }
        }

        /// <summary>
        /// Метод, возвращающий double число, полученное от пользователя.
        /// </summary>
        /// <returns>
        /// <param name="numDataRaw">Строка, означающая ввод пользователя.</param>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении числа!</item>
        /// <item>Число типа double.</item>
        /// </list>
        /// </returns>
        public static bool GetNum(string numDataRaw, out double num)
        {
            // Если число невозможно преобразовать к типу double - выводим ошибку.
            if (!double.TryParse(numDataRaw, out num))
            {
                Environment.InputError("get_num");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Метод, возвращающий int число, полученное от пользователя.
        /// </summary>
        /// <returns>
        /// <param name="numDataRaw">Строка, означающая ввод пользователя.</param>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении числа!</item>
        /// <item>Число типа int.</item>
        /// </list>
        /// </returns>
        public static bool GetNumInt(string numDataRaw, out int num)
        {
            // Если число невозможно преобразовать к типу int - выводим ошибку.
            if (!int.TryParse(numDataRaw, out num) && num <= 0)
            {
                Environment.InputError("get_num_int_generate");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Метод, возвращающий число, размер матрицы, полученный от пользователя.
        /// </summary>
        /// <param name="sizeData">Строка, означающая ввод пользователя.</param>
        /// <param name="square">Значение утверждающее, что матрица квадратная и размерность 0 не подойдет.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении размера!</item>
        /// <item>Число типа int, Означающее размер квадратной матрицы.</item>
        /// </list>
        /// </returns>
        public static bool GetSize(string sizeData, bool square, out int size)
        {
            // Если строку невозможно преобразовать к типу int или размер заданной матрицы больше 10 или меньше 1 - выводим ошибку.
            if (!int.TryParse(sizeData, out size) || size <= 0 || size > 10)
            {
                Environment.InputError("get_size");
                return false;
            }
            if (square && size == 0)
            {
                Environment.InputError("get_size_square");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Метод, возвращающий число, размер матрицы, полученный от пользователя.
        /// </summary>
        /// <param name="sizeDataRaw">Строка, означающая ввод пользователя.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении размеров!</item>
        /// <item>Число типа int, колличесвто строк в матрице.</item>
        /// <item>Число типа int, колличество столбцов в матрице.</item>
        /// </list>
        /// </returns>
        public static bool GetSizes(string sizeDataRaw, out int size1, out int size2)
        {
            string[] sizeData;
            // Проверка строки на EOF и null.
            if (sizeDataRaw == null)
            {
                size1 = 0;
                size2 = 0;
                Environment.Error("EOF");
                return false;
            }
            sizeData = sizeDataRaw.Split(' ', '\t');
            // Проверка, если строке было меньше двух обьектов - ошибка.
            if (sizeData.Length < 2)
            {
                Environment.InputError("get_sizes");
                size1 = 0;
                size2 = 0;
                return false;
            }
            // Если обьекты невозможно преробразовать в целые числа и/или их значение не в [1, 10] - ошибка.
            if (!int.TryParse(sizeData[0], out size1) || size1 <= 0 || size1 > 10 ||
                !int.TryParse(sizeData[1], out size2) || size2 <= 0 || size2 > 10)
            {
                Environment.InputError("get_sizes");
                size1 = 0;
                size2 = 0;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Метод, запрашивающий и возвращающий номер метода для ввода информации.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает номер метода в виде числа типа ushort.</item>
        /// </list>
        /// </returns>
        public static void AskForInputMethod(out ushort method)
        {
            string methodChoiceData;
            Environment.MethodMenu();
            // Пока пользователь не введёт подходящий номер метода, запрашивать его снова.
            do
            {
                Environment.Information("ask_input");
                methodChoiceData = ReadLine();
            } while (!GetMethod(methodChoiceData, out method));
        }

        /// <summary>
        /// Метод, возвращающий номер метода для ввода информации.
        /// </summary>
        /// <param name="methodChoiceData">Строка, означающая ввод пользователя.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении метода!</item>
        /// <item>Число типа ushort, означающее выбор метода ввода информации.</item>
        /// </list>
        /// </returns>
        public static bool GetMethod(string methodChoiceData, out ushort methodChoice)
        {
            // Если ввод пользователя нельзя представить в виде числа или число не принадлежит [1, 3] - ошибка.
            if (!ushort.TryParse(methodChoiceData, out methodChoice) || methodChoice == 0 || methodChoice > 3)
            {
                Environment.InputError("get_method");
                return false;
            }
            return true;

        }

        /// <summary>
        /// Метод, запрашивающий и возвращающий размер квадратной матрицы.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает размер квадратной матрицы.</item>
        /// </list>
        /// </returns>
        public static void AskForSize(bool square, out int size)
        {
            string sizeData;
            // Пока пользователь не введёт подходящий размер квадратной матрицы, запрашивать его снова.
            do
            {
                Environment.Information("ask_size");
                sizeData = ReadLine();
            } while (!Input.GetSize(sizeData, square, out size));


        }

        /// <summary>
        /// Метод, запрашивающий и возвращающий размер матрицы.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Число типа int, колличесвто строк в матрице.</item>
        /// <item>Число типа int, колличество столбцов в матрице.</item>
        /// </list>
        /// </returns>
        public static void AskForSizes(out int size1, out int size2)
        {
            string sizeDataRaw;
            // Пока пользователь не введёт подходящий размер матрицы, запрашивать его снова.
            do
            {
                Environment.Information("ask_sizes");
                sizeDataRaw = ReadLine();
            } while (!Input.GetSizes(sizeDataRaw, out size1, out size2));

        }

        /// <summary>
        /// Метод, запрашивающий и возвращающий число типа double!
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает число типа double.</item>
        /// </list>
        /// </returns>
        public static void AskForNum(out double num)
        {
            string numDataRaw;
            // Пока пользователь не введёт число, запрашиваем его снова!
            do
            {
                Environment.Information("ask_num");
                numDataRaw = ReadLine();
            } while (!Input.GetNum(numDataRaw, out num));

        }

        /// <summary>
        /// Метод, возвращающий тип генерации числа или с дробными числами, или без дробных.
        /// </summary>
        /// <param name="doubleModeData">Строка, означающая ввод пользователя.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, которое означает успех или проблему в получении настройки генератора!</item>
        /// <item>Возвращает значение типа bool, означающее значение генерации (использовать ли дробные числа).</item>
        /// </list>
        /// </returns>
        public static bool GetModeDouble(string doubleModeData, out bool doubleMode)
        {
            //Если ввод не подходит, выводим ошибку.
            if (doubleModeData != "n" && doubleModeData != "y")
            {
                Environment.InputError("mode_error");
                doubleMode = false;
                return false;
            }
            //Если ввод корректен выбираем значение developMode исходя из ввода пользователя. 
            else
            {
                //Устанавливает значение режима игры.
                doubleMode = (doubleModeData == "y");
                if (doubleMode) Environment.Information("need_double_true");
                else Environment.Information("need_double_false");
                return true;
            }

        }

        /// <summary>
        /// Метод, возвращающий и запрашивающий от пользователя тип генерации числа или с дробными числами, или без дробных.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, означающее значение генерации (использовать ли дробные числа).</item>
        /// </list>
        /// </returns>
        public static void AskForDouble(out bool needDouble)
        {
            string doubleModeData;
            // Пока пользователь не введёт подходящую для выбора строку, запрашшиваем ёё снова.
            do
            {
                Environment.Information("ask_double");
                doubleModeData = ReadLine();
            } while (!Input.GetModeDouble(doubleModeData, out needDouble));
        }

        /// <summary>
        /// Метод, возвращающий и запрашивающий от пользователя границы генерации чисел.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа int, означающее значение границ генерации.</item>
        /// </list>
        /// </returns>
        public static void AskForControlNum(out int controlNum)
        {
            string numDataRaw;
            // Пока пользователь не введёт подходящую границу генерации, запрашивать ёё снова.
            do
            {
                Environment.Information("ask_num_generate");
                numDataRaw = ReadLine();
            } while (!Input.GetNumInt(numDataRaw, out controlNum));

        }

        /// <summary>
        /// Метод, считывающий и возвращающий матрицу из файла input.txt.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, означающее удалось ли считать матрицу.</item>
        /// <item>Возвращает значение типа double[,], то есть саму матрицу.</item>
        /// </list>
        /// </returns>
        public static bool TryToReadFileOne(out double[,] matrix)
        {
            try
            {
                // Пытаемся считать значения из файла, что-то пошло не так? Ошибка!
                if (!ReadFileRaw(out string num, out string[] matrix1S, out string[] matrix2S))
                {
                    Environment.FileError("file_one_error");
                    matrix = null;
                    return false;
                }
                // Преобразуем данные из файла в матрицу
                return MatrixStringToDoubleOne(matrix1S, out matrix);
            }
            catch
            {
                Environment.FileError("file_one_error");
                matrix = null;
                return false;
            }
        }

        /// <summary>
        /// Метод, считывающий и возвращающий две матрицы из файла input.txt.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, означающее удалось ли считать матрицы.</item>
        /// <item>Возвращает два значения типа double[,], то есть сами матрицы.</item>
        /// </list>
        /// </returns>
        public static bool TryToReadFileTwo(out double[,] matrix1, out double[,] matrix2)
        {
            try
            {
                // Пытаемся считать значения из файла, что-то пошло не так? Ошибка!
                if (!ReadFileRaw(out string num, out string[] matrix1S, out string[] matrix2S))
                {
                    Environment.FileError("file_one_error");
                    matrix1 = null;
                    matrix2 = null;
                    return false;
                }
                // Преобразуем значения из файла в матрицы + проверяем удачность действий!
                bool ans1 = MatrixStringToDoubleOne(matrix1S, out matrix1);
                bool ans2 = MatrixStringToDoubleOne(matrix2S, out matrix2);
                return ans1 && ans2;
            }
            catch
            {
                Environment.FileError("file_one_error");
                matrix1 = null;
                matrix2 = null;
                return false;
            }
        }

        /// <summary>
        /// Метод, считывающий и возвращающий матрицу и число из файла input.txt.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, означающее удалось ли считать матрицы.</item>
        /// <item>Возвращает значение типа double[,], то есть саму матрицу.</item>
        /// <item>Возвращает значение типа double, число для домножения.</item>
        /// </list>
        /// </returns>
        public static bool TryToReadFileMatrixNum(out double[,] matrix, out double num)
        {
            try
            {
                // Пытаемся считать значения из файла, что-то пошло не так? Ошибка!
                if (!ReadFileRaw(out string numS, out string[] matrix1S, out string[] matrix2S))
                {
                    Environment.FileError("file_one_error");
                    matrix = null;
                    num = 0;
                    return false;
                }
                // Преобразуем значения из файла в матрицы + проверяем удачность действий!
                bool ans1 = MatrixStringToDoubleOne(matrix1S, out matrix);
                bool ans2 = NumStringToDoubleOne(numS, out num);
                return ans1 && ans2;
            }
            catch
            {
                Environment.FileError("file_one_error");
                matrix = null;
                num = 0;
                return false;
            }
        }

        /// <summary>
        /// Метод, считывающий и возвращающий данные из файла input.txt.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, означающее удалось ли считать данные.</item>
        /// <item>Возвращает значение типа string, число, для перемножения в строчном виде.</item>
        /// <item>Возвращает значение типа string[], представление первой матрицы в строчном виде.</item>
        /// <item>Возвращает значение типа string[], представление второй матрицы в строчном виде.</item>
        /// </list>
        /// </returns>
        public static bool ReadFileRaw(out string num, out string[] matrix1, out string[] matrix2)
        {
            try
            {
                // Построчно считываем данные из файла.
                string[] lines = File.ReadAllLines("..\\input.txt");
                // Если файл пустой - выводим ошибку
                if (lines == null || lines.Length <= 1)
                {
                    Environment.FileError("null_data_file");
                }
                // Если при считывании файл произошла ошибка или хоть один обьект оказался пуст - ошибка.
                if (!ArrayStringData(out num, out matrix1, out matrix2, lines) || num == null || matrix1.Length == 0 || matrix2.Length == 0)
                {
                    Environment.FileError("null_data_file");
                    return false;
                }
                return true;
            }
            catch
            {
                Environment.FileError("read_data_file");
                matrix1 = null;
                matrix2 = null;
                num = "";
                return false;
            }

        }

        /// <summary>
        /// Дополнительный метод, считывающий и возвращающий данные из файла input.txt.
        /// </summary>
        /// <param name="lines">Массив со строками файла input.txt.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, означающее удалось ли считать данные.</item>
        /// <item>Возвращает значение типа string, число, для перемножения в строчном виде.</item>
        /// <item>Возвращает значение типа string[], представление первой матрицы в строчном виде.</item>
        /// <item>Возвращает значение типа string[], представление второй матрицы в строчном виде.</item>
        /// </list>
        /// </returns>
        private static bool ArrayStringData(out string num, out string[] matrix1, out string[] matrix2, string[] lines)
        {
            string[] matrix1R = new string[lines.Length];
            string[] matrix2R = new string[lines.Length];
            num = lines[0];
            // Если файл не устроен так, как описано в README.txt - ошибка.
            if (lines[1] != ";" || !Input.GetNum(lines[0], out _))
            {
                Environment.FileError("read_num_file_error");
                matrix1 = null;
                matrix2 = null;
                return false;
            }
            // Прекращаем жизнь первой ";" и числа в первой строке.
            lines[0] = null;
            lines[1] = null;
            // Считываем построчно первую матрицу в массив, до второй ";".
            int start1 = Array.IndexOf(lines, ";");
            for (int i = 2; i < start1; i++)
            {
                if (lines[i] != null)
                {
                    matrix1R[i - 2] = lines[i];
                }
            }
            // Прекращаем жизнь второй ";".
            lines[start1] = null;
            // Считываем построчно вторую матрицу в массив, до третьей ";".
            int start2 = Array.IndexOf(lines, ";");
            for (int i = start1 + 1; i < start2; i++)
            {
                if (lines[i] != null)
                {
                    matrix2R[i - start1 + 1] = lines[i];
                }
            }
            // Удаляем пустые элементы из двух массивов.
            matrix1 = matrix1R.Where(s => s != null).ToArray();
            matrix2 = matrix2R.Where(s => s != null).ToArray();
            return true;
        }

        /// <summary>
        /// Метод занимается перводом строки числа из input.txt в число.
        /// </summary>
        /// <param name="numString">Строка из файла input.txt.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, означающее удалось ли преобразовать число.</item>
        /// <item>Возвращает значение типа double, число, для перемножения в строчном виде.</item>
        /// </list>
        /// </returns>
        public static bool NumStringToDoubleOne(string numString, out double num)
        {
            // Если не удалосб перобразовать строку в число типа double - выводим ошибку.
            if (!Input.GetNum(numString, out num))
            {
                Environment.FileError("read_num_file_error");
                return false;
            }
            else return true;
        }

        /// <summary>
        /// Метод занимается перводом строк, состовляющих матрицу, из input.txt в числовую матрицу. 
        /// </summary>
        /// <param name="matrixRaw">Массив строк из файла input.txt.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, означающее удалось ли преобразовать строчную матрицу в числовую.</item>
        /// <item>Возвращает значение типа double[,], то есть требуемую матрицу.</item>
        /// </list>
        /// </returns>
        public static bool MatrixStringToDoubleOne(string[] matrixRaw, out double[,] matrix)
        {
            try
            {
                // Фиксируем размер матрицы!
                string[] len = matrixRaw[0].Split(" ");
                // Создаём числовую матрицу!
                matrix = new double[matrixRaw.Length, len.Length];
                for (int i = 0; i < matrixRaw.Length; i++)
                {
                    // Разбиваем каждую строку, состовляющую строку матрицы на подстроки.
                    string[] workString = matrixRaw[i].Split(" ");
                    for (int j = 0; j < len.Length; j++)
                    {
                        // Если  подстроку невозможно представить в виде числа - выбрасываем ошибку.
                        if (!double.TryParse(workString[j], out double obj))
                        {
                            Environment.FileError("get_matrix_txt");
                            return false;
                        }
                        // Забиваем преобразоваными подстроками нашу матрицу
                        matrix[i, j] = obj;
                    }
                }
                // Дополнительно выводим матрицу из файла.
                Environment.PrintMatrix(matrix);
                return true;
            }
            catch
            {
                matrix = null;
                Environment.FileError("get_matrix_double");
                return false;
            }


        }

    }
}
