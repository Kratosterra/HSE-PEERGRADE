using System;

namespace Peergrade002
{
    /// <summary>
    /// Класс для решения матриц и других прикладных задач.
    /// </summary>
    class Tools
    {
        /// <summary>
        /// Метод, находящий след матрицы и выводит его на экран!
        /// </summary>
        /// <param name="matrix">Готовая матрица.</param>
        public static void Trace(double[,] matrix)
        {
            double ans = 0;
            int size = matrix.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                ans += matrix[i, i];
            }
            Environment.Trace(ans);
        }

        /// <summary>
        /// Метод, производящий транспонирование матрицы!
        /// </summary>
        /// <param name="matrix">Готовая матрица.</param>
        public static void Transposition(double[,] matrix)
        {
            double[,] ans = new double[matrix.GetLength(1), matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    ans[j, i] = matrix[i, j];
                }
            }
            Environment.Transposition(ans);
        }

        /// <summary>
        /// Метод, суммирующий матрицы!
        /// </summary>
        /// <param name="matrix1">Готовая первая матрица.</param>
        /// <param name="matrix2">Готовая вторая матрица.</param>
        public static void Sum(double[,] matrix1, double[,] matrix2)
        {
            double[,] ans = new double[matrix1.GetLength(0), matrix1.GetLength(1)];

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    ans[i, j] = matrix1[i, j] + matrix2[i, j];
                }
            }
            Environment.Sum(ans);
        }

        /// <summary>
        /// Метод, производящий вычитание матрицы из матрицы!
        /// </summary>
        /// <param name="matrix1">Готовая первая матрица.</param>
        /// <param name="matrix2">Готовая вторая матрица.</param>
        public static void SubSum(double[,] matrix1, double[,] matrix2)
        {
            double[,] ans = new double[matrix1.GetLength(0), matrix1.GetLength(1)];

            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    ans[i, j] = matrix1[i, j] - matrix2[i, j];
                }
            }
            Environment.SubSum(ans);
        }

        /// <summary>
        /// Метод, находящий произведение двух матриц!
        /// </summary>
        /// <param name="matrix1">Готовая первая матрица.</param>
        /// <param name="matrix2">Готовая вторая матрица.</param>
        public static void MultiplicateMatrix(double[,] matrix1, double[,] matrix2)
        {
            double[,] ans = new double[matrix1.GetLength(0), matrix2.GetLength(1)];
            for (int i = 0; i < matrix1.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    double sum = 0;

                    for (int t = 0; t < matrix1.GetLength(1); t++)
                    {
                        sum += matrix1[i, t] * matrix2[t, j];
                    }

                    ans[i, j] = sum;
                }
            }
            Environment.Multi(ans);

        }

        /// <summary>
        /// Метод, находящий произведение матрицы и числа!
        /// </summary>
        /// <param name="matrix">Готовая первая матрица.</param>
        /// <param name="num">Готовое число.</param>
        public static void MultiplicateNum(double[,] matrix, double num)
        {
            double[,] ans = new double[matrix.GetLength(0), matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    ans[i, j] = matrix[i, j] * num;
                }
            }

            Environment.MultiNum(ans);
        }

        /// <summary>
        /// Метод, находящий и выводящий определитель матрицы!
        /// </summary>
        /// <param name="matrix">Готовая матрица.</param>
        public static void DeterminantTool(double[,] matrix)
        {
            // Вызов метода вывода ответа на экран с аргументом из определителя матрицы.
            Environment.Determinant(Tools.Determinant(matrix));
        }

        /// <summary>
        /// Метод, находящий определитель матрицы!
        /// </summary>
        /// <param name="matrix">Готовая матрица.</param>
        private static double Determinant(double[,] matrix)
        {
            // Прированиваем определитель к нулю.
            double ans = 0;
            // Если размерность минора или матрицы равна одному, возвращаем единственное значение в матрице/миноре.
            if (matrix.GetLength(0) == 1) return matrix[0, 0];
            // Проходимся по каждой строке матрицы.
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                // Прибавляем к определителю алгебраическое дополнение от минора умноженное на i-ый элемент первой строки.
                ans += AlgebraicСomplementMinorEdition(matrix, 0, i) * matrix[0, i];
            }
            return ans;
        }

        /// <summary>
        /// Метод, находящий алгебраическое дополнение!
        /// </summary>
        /// <param name="matrix">Готовая матрица.</param>
        /// <param name="size1">Колличество строк в матрице.</param>
        /// <param name="size2">Колличесвто столбцов в матриц.</param>
        private static double AlgebraicСomplementMinorEdition(double[,] matrix, int size1, int size2)
        {
            // Вычисляем множитель алгебраического дополнения от данной матрицы или минора)
            double algebraicСomplement = Convert.ToDouble(Math.Pow(-1, size1 + size2));
            // Вычисляем минор от матрицы/минора.
            double[,] minor = DetermenantMinor(matrix, size1, size2);
            // Вычисляем определитель от минора.
            double minorDeterminant = Determinant(minor);
            // Возвращаем алгебраическое дополнение.
            return algebraicСomplement * minorDeterminant;
        }

        /// <summary>
        /// Метод, нахождения минора из матрицы!
        /// </summary>
        /// <param name="matrix">Готовая матрица.</param>
        /// <param name="size1">Колличество строк в матрице.</param>
        /// <param name="size2">Колличесвто столбцов в матриц.</param>
        private static double[,] DetermenantMinor(double[,] matrix, int size1, int size2)
        {
            // Создаём переменную для минора
            double[,] minorNew = new double[matrix.GetLength(0) - 1, matrix.GetLength(0) - 1];
            int control1 = 0;
            // Проходимся по каждому элементу матрице и с уменьшением размерности заполняем минор.
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (i != size1)
                {
                    int control2 = 0;
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        if (j != size2)
                        {
                            // Заполняем минор!
                            minorNew[control1, control2] = matrix[i, j];
                            control2++;
                        }
                    }
                    control1++;
                }
            }
            return minorNew;
        }

        /// <summary>
        /// Метод, находящий и выводящий решение СЛАУ!
        /// </summary>
        /// <param name="matrix">Готовая матрица.</param>
        public static void SolveTool(double[,] matrix)
        {
            double[,] mainMatrix;
            double[] freeNums;
            // Преобразуем матрицу, в квадратную матрицу и массив свободных коофицентов.
            WorkSolveData(matrix, out mainMatrix, out freeNums);
            // Вызываем метод решения СЛАУ методом Крамера!
            Solve(matrix, mainMatrix, freeNums);
        }

        /// <summary>
        /// Метод, производящий основную работу по выводу и решению методом Крамера!
        /// </summary>
        /// <param name="matrix">Основная матрица СЛАУ.</param>
        /// <param name="matrix">Квадратная матрица коофицентов.</param>
        /// <param name="matrix">Массив свободных коофицентов.</param>
        private static void Solve(double[,] matrix, double[,] mainMatrix, double[] freeNums)
        {
            // Cоздаём переменную для ответа.
            string ans = "";
            // Находим определитель матрицы коофицентов.
            double delta = Tools.Determinant(mainMatrix);
            // Если определитель равен нулю, СЛАУ не имеет решений.
            if (delta == 0)
            {
                ans = "Невозможно найти решения, которые могут быть представлены в числовом виде.\n" +
                    "Метод Крамера получил матрицу с нулевым отпределитем.";
            }
            else
            {
                // Задаём рабочую матрицу, в которой мы будем менять столбцы на масиввы свободных членов,
                // как того требует метод Крамера.
                double[,] workMatrix;
                for (int i = 0; i < mainMatrix.GetLength(1); i++)
                {
                    // Клонируем квадратную матрицу коофицентов в рабочую. 
                    workMatrix = CloneMatrix(mainMatrix);
                    for (int j = 0; j < matrix.GetLength(0); j++)
                    {
                        // Заменяем каждый элемент i-ого стобца на значения из массива свободных коофицентов.
                        workMatrix[j, i] = freeNums[j];
                    }
                    // По методу Крамера - корень уравнения есть частное определителя
                    // модифицированной квадратной матрицы с столбиком из свободных
                    // коофицентов и определится квадратной матрицы коофицентов.
                    ans += $"\nx{i + 1} = {Tools.Determinant(workMatrix) / delta}\n";
                }
            }
            // Красиво выводим ответ.
            Environment.Solve(ans);
        }

        /// <summary>
        /// Метод, находящий и выводящий основную квадратную матрицу и массив свободных коофицентов!
        /// </summary>
        /// <param name="matrix">Готовая матрица.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает квадратную матрицу типа double[,] с индексами переменных!</item>
        /// <item>Возвращает массив свободных членов уравнения из карайнего столбца матрицы.</item>
        /// </list>
        /// </returns>
        private static void WorkSolveData(double[,] matrix, out double[,] mainMatrix, out double[] freeNums)
        {
            // Создаём квадратную матрицу для коофицентов переменных.
            mainMatrix = new double[matrix.GetLength(0), matrix.GetLength(1) - 1];
            // Создаём массив свободных коофицентов.
            freeNums = new double[matrix.GetLength(0)];
            // Заполняем их.
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                freeNums[i] = matrix[i, matrix.GetLength(1) - 1];
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    mainMatrix[i, j] = matrix[i, j];
                }
            }
        }

        /// <summary>
        /// Метод, клонирующий указанную матрицу!
        /// </summary>
        /// <param name="matrix">Матрица.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает матрицу типа double[,], клон данной!</item>
        /// </list>
        /// </returns>
        public static double[,] CloneMatrix(double[,] matrix)
        {
            // Просто клонируем каждый элемент.
            double[,] clone = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    clone[i, j] = matrix[i, j];
            return clone;
        }

        /// <summary>
        /// Метод, производящий генерацию матрицы по заданным парметрам!
        /// </summary>
        /// <param name="doubleNum">Использовать ли дробные числа.</param>
        /// <param name="controlNum">Контрольные границы генерации.</param>
        /// <param name="height">Колличество строк в матрице.</param>
        /// <param name="width">Колличество столбцов в матрице.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает матрицу типа double[,] со сгенерированными элементами!</item>
        /// </list>
        /// </returns>
        public static void GenerateMatrix(bool doubleNum, int controlNum, int height, int width, out double[,] matrix)
        {
            var rnd = new Random();
            matrix = new double[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // В зависимости от заданных начальных парметров (границы и тип чисел), мы генерируем значения.
                    if (doubleNum) matrix[i, j] = rnd.Next(-controlNum, controlNum) + rnd.NextDouble();
                    else matrix[i, j] = rnd.Next(-controlNum, controlNum);
                }
            }
        }

        /// <summary>
        /// Метод, производящий генерацию числа по заданным парметрам!
        /// </summary>
        /// <param name="doubleNum">Использовать ли дробные числа.</param>
        /// <param name="controlNum">Контрольные границы генерации.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает число типа double[].</item>
        /// </list>
        /// </returns>
        public static void GenerateNum(bool doubleNum, int controlNum, out double num)
        {
            var rnd = new Random();
            // В зависимости от заданных начальных парметров (границы и тип чисел), мы генерируем значение числа.
            if (doubleNum) num = rnd.Next(-controlNum, controlNum) + rnd.NextDouble();
            else num = rnd.Next(-controlNum, controlNum);
        }
    }

}
