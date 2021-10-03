using System;
using static System.Console;

namespace Peergrade002
{
    /// <summary>
    /// Класс, заведующий запуском действий калькулятора.
    /// </summary>
    class Actions
    {
        /// <summary>
        /// Метод, находящий след матрицы.
        /// </summary>
        public static void FindMatrixTrace()
        {
            Environment.Interface("trace");
            // Делаем запрос на метод ввода данных.
            Input.AskForInputMethod(out ushort method);
            // Выполняем запрос.
            TraceSwitcher(method);

        }

        /// <summary>
        /// Метод, создающий задачи по типу метода ввода пользовователя.
        /// </summary>
        /// <param name="method">Число типа ushort, означающие тип ввода.</param>
        private static void TraceSwitcher(ushort method)
        {
            switch (method)
            {
                case 1:
                    // Просим пользователя ввести данные через консоль.
                    WorkInputConstruct.Trace.TraceConsole(out double[,] matrix1);
                    // Вычисляем след.
                    Tools.Trace(matrix1);
                    break;
                case 2:
                    // Просим пользователя дать информацию о генерации и сгенерировать информацию для вычислений.
                    WorkInputConstruct.Trace.TraceGenerator(out double[,] matrix2);
                    // Вычисляем след.
                    Tools.Trace(matrix2);
                    break;
                case 3:
                    // Считываем данные из файла. Если действие оказалось провальным - ошибка.
                    if (WorkInputConstruct.Trace.TraceFile(out double[,] matrix3) &&
                        MatrixVerification.MatrixTrustSquare(matrix3.GetLength(0), matrix3.GetLength(1)))
                    {
                        // Немного информации для пользователя.
                        Environment.ConstructInfo("one_matrix_received");
                        Environment.PrintMatrix(matrix3);
                        // Вычисляем след.
                        Tools.Trace(matrix3);
                    }
                    else
                    {
                        Environment.FileError("file_error");
                    }
                    break;
            }
        }

        /// <summary>
        /// Метод, находящий транспозицию матрицы.
        /// </summary>
        public static void MatrixTransposition()
        {
            Environment.Interface("trans");
            // Делаем запрос на метод ввода данных.
            Input.AskForInputMethod(out ushort method);
            // Выполняем запрос.
            TranspositionSwitcher(method);

        }

        /// <summary>
        /// Метод, создающий задачи по типу метода ввода пользовователя.
        /// </summary>
        /// <param name="method">Число типа ushort, означающие тип ввода.</param>
        private static void TranspositionSwitcher(ushort method)
        {
            switch (method)
            {
                case 1:
                    // Просим пользователя ввести данные через консоль.
                    WorkInputConstruct.Transposition.TranspositionConsole(out double[,] matrix1);
                    // Вычисляем транспозицию матрицы.
                    Tools.Transposition(matrix1);
                    break;
                case 2:
                    // Просим пользователя дать информацию о генерации и сгенерировать информацию для вычислений.
                    WorkInputConstruct.Transposition.TranspositionGenerator(out double[,] matrix2);
                    // Вычисляем транспозицию матрицы.
                    Tools.Transposition(matrix2);
                    break;
                case 3:
                    // Считываем данные из файла. Если действие оказалось провальным - ошибка.
                    if (WorkInputConstruct.Transposition.TranspositionFile(out double[,] matrix3))
                    {
                        // Немного информации для пользователя.
                        Environment.ConstructInfo("one_matrix_received");
                        Environment.PrintMatrix(matrix3);
                        // Вычисляем транспозицию матрицы.
                        Tools.Transposition(matrix3);
                    }
                    else
                    {
                        Environment.FileError("file_error");
                    }
                    break;
            }
        }

        /// <summary>
        /// Метод, находящий сумму матриц.
        /// </summary>
        public static void MatrixSum()
        {
            Environment.Interface("sum");
            // Делаем запрос на метод ввода данных.
            Input.AskForInputMethod(out ushort method);
            // Выполняем запрос.
            SumSwitcher(method);
        }

        /// <summary>
        /// Метод, создающий задачи по типу метода ввода пользовователя.
        /// </summary>
        /// <param name="method">Число типа ushort, означающие тип ввода.</param>
        private static void SumSwitcher(ushort method)
        {
            switch (method)
            {
                case 1:
                    // Просим пользователя ввести данные через консоль.
                    WorkInputConstruct.Sum.SumConsole(out double[,] matrix11, out double[,] matrix21);
                    // Вычисляем сумму матриц.
                    Tools.Sum(matrix11, matrix21);
                    break;
                case 2:
                    // Просим пользователя дать информацию о генерации и сгенерировать информацию для вычислений.
                    WorkInputConstruct.Sum.SumGenerator(out double[,] matrix12, out double[,] matrix22);
                    // Вычисляем сумму матриц.
                    Tools.Sum(matrix12, matrix22);
                    break;
                case 3:
                    // Считываем данные из файла. Если действие оказалось провальным - ошибка.
                    if (WorkInputConstruct.Sum.SumFile(out double[,] matrix13, out double[,] matrix23) &&
                        MatrixVerification.TwoMatrixTrustSum(matrix13.GetLength(0), matrix13.GetLength(1),
                        matrix23.GetLength(0), matrix23.GetLength(1)))
                    {
                        // Немного информации для пользователя.
                        Environment.ConstructInfo("two_matrix_received");
                        Environment.PrintMatrix(matrix13);
                        Environment.PrintMatrix(matrix23);
                        // Вычисляем сумму матриц.
                        Tools.Sum(matrix13, matrix23);
                    }
                    else
                    {
                        Environment.FileError("file_error");
                    }
                    break;
            }
        }

        /// <summary>
        /// Метод, находящий разность матриц.
        /// </summary>
        /// <param name="method">Число типа ushort, означающие тип ввода.</param>
        public static void MatrixSubSum()
        {
            Environment.Interface("sub");
            // Делаем запрос на метод ввода данных.
            Input.AskForInputMethod(out ushort method);
            // Выполняем запрос.
            SubSumSwitcher(method);
        }

        /// <summary>
        /// Метод, создающий задачи по типу метода ввода пользовователя.
        /// </summary>
        /// <param name="method">Число типа ushort, означающие тип ввода.</param>
        private static void SubSumSwitcher(ushort method)
        {
            switch (method)
            {
                case 1:
                    // Просим пользователя ввести данные через консоль.
                    WorkInputConstruct.SubSum.SubSumConsole(out double[,] matrix11, out double[,] matrix21);
                    // Вычисляем разность матриц.
                    Tools.SubSum(matrix11, matrix21);
                    break;
                case 2:
                    // Просим пользователя дать информацию о генерации и сгенерировать информацию для вычислений.
                    WorkInputConstruct.SubSum.SubSumGenerator(out double[,] matrix12, out double[,] matrix22);
                    // Вычисляем разность матриц.
                    Tools.SubSum(matrix12, matrix22);
                    break;
                case 3:
                    // Считываем данные из файла. Если действие оказалось провальным - ошибка.
                    if (WorkInputConstruct.SubSum.SubSumFile(out double[,] matrix13, out double[,] matrix23) &&
                        MatrixVerification.TwoMatrixTrustSum(matrix13.GetLength(0), matrix13.GetLength(1),
                        matrix23.GetLength(0), matrix23.GetLength(1)))
                    {
                        // Немного информации для пользователя.
                        Environment.ConstructInfo("two_matrix_received");
                        Environment.PrintMatrix(matrix13);
                        Environment.PrintMatrix(matrix23);
                        // Вычисляем разность матриц.
                        Tools.SubSum(matrix13, matrix23);
                    }
                    else
                    {
                        Environment.FileError("file_error");
                    }
                    break;
            }
        }

        /// <summary>
        /// Метод, находящий произведение матриц.
        /// </summary>
        public static void MatrixMultiplication()
        {
            Environment.Interface("multi");
            // Делаем запрос на метод ввода данных.
            Input.AskForInputMethod(out ushort method);
            // Выполняем запрос.
            MultiplicationSwitcher(method);
        }

        /// <summary>
        /// Метод, создающий задачи по типу метода ввода пользовователя.
        /// </summary>
        /// <param name="method">Число типа ushort, означающие тип ввода.</param>
        private static void MultiplicationSwitcher(ushort method)
        {
            switch (method)
            {
                case 1:
                    // Просим пользователя ввести данные через консоль.
                    WorkInputConstruct.Multiplication.MultiplicationConsole(out double[,] matrix11,
                        out double[,] matrix21);
                    // Вычисляем произведение матриц.
                    Tools.MultiplicateMatrix(matrix11, matrix21);
                    break;
                case 2:
                    // Просим пользователя дать информацию о генерации и сгенерировать информацию для вычислений.
                    WorkInputConstruct.Multiplication.MultiplicationGenerator(out double[,] matrix12,
                        out double[,] matrix22);
                    // Вычисляем произведение матриц.
                    Tools.MultiplicateMatrix(matrix12, matrix22);
                    break;
                case 3:
                    // Считываем данные из файла. Если действие оказалось провальным - ошибка.
                    if (WorkInputConstruct.Multiplication.MultiplicationFile(out double[,] matrix13,
                        out double[,] matrix23) &&
                        MatrixVerification.TwoMatrixTrustMultiplication(matrix13.GetLength(0), matrix13.GetLength(1),
                        matrix23.GetLength(0), matrix23.GetLength(1)))
                    {
                        // Немного информации для пользователя.
                        Environment.ConstructInfo("two_matrix_received");
                        Environment.PrintMatrix(matrix13);
                        Environment.PrintMatrix(matrix23);
                        // Вычисляем произведение матриц.
                        Tools.MultiplicateMatrix(matrix13, matrix23);
                    }
                    else
                    {
                        Environment.FileError("file_error");
                    }
                    break;
            }
        }

        /// <summary>
        /// Метод, находящий произведение числа и матрицы.
        /// </summary>
        public static void MatrixNumMultiplication()
        {
            Environment.Interface("multi_num");
            // Делаем запрос на метод ввода данных.
            Input.AskForInputMethod(out ushort method);
            // Выполняем запрос.
            NumMultiplicationSwitcher(method);
        }

        /// <summary>
        /// Метод, создающий задачи по типу метода ввода пользовователя.
        /// </summary>
        /// <param name="method">Число типа ushort, означающие тип ввода.</param>
        private static void NumMultiplicationSwitcher(ushort method)
        {
            switch (method)
            {
                case 1:
                    // Просим пользователя ввести данные через консоль.
                    WorkInputConstruct.MultiplicationNum.MultiplicationNumConsole(out double[,] matrix1,
                        out double num1);
                    // Вычисляем произведение числа и матрицы.
                    Tools.MultiplicateNum(matrix1, num1);
                    break;
                case 2:
                    // Просим пользователя дать информацию о генерации и сгенерировать информацию для вычислений.
                    WorkInputConstruct.MultiplicationNum.MultiplicationNumGenerator(out double[,] matrix2,
                        out double num2);
                    // Вычисляем произведение числа и матрицы.
                    Tools.MultiplicateNum(matrix2, num2);
                    break;
                case 3:
                    // Считываем данные из файла. Если действие оказалось провальным - ошибка.
                    if (WorkInputConstruct.MultiplicationNum.MultiplicationNumFile(out double[,] matrix3,
                        out double num3))
                    {
                        // Немного информации для пользователя.
                        Environment.NumReceived(num3);
                        Environment.ConstructInfo("one_matrix_received");
                        Environment.PrintMatrix(matrix3);
                        // Вычисляем произведение числа и матрицы.
                        Tools.MultiplicateNum(matrix3, num3);
                    }
                    else
                    {
                        Environment.FileError("file_error");
                    }
                    break;
            }
        }

        /// <summary>
        /// Метод, находящий определитель матрицы.
        /// </summary>
        /// </summary>
        public static void MatrixDet()
        {
            Environment.Interface("det");
            // Делаем запрос на метод ввода данных.
            Input.AskForInputMethod(out ushort method);
            // Выполняем запрос.
            DetSwitcher(method);
        }

        /// <summary>
        /// Метод, создающий задачи по типу метода ввода пользовователя.
        /// </summary>
        /// <param name="method">Число типа ushort, означающие тип ввода.</param>
        private static void DetSwitcher(ushort method)
        {
            switch (method)
            {
                case 1:
                    // Просим пользователя ввести данные через консоль.
                    WorkInputConstruct.Determinant.DeterminantConsole(out double[,] matrix1);
                    // Вычисляем определитель матрицы.
                    Tools.DeterminantTool(matrix1);
                    break;
                case 2:
                    // Просим пользователя дать информацию о генерации и сгенерировать информацию для вычислений.
                    WorkInputConstruct.Determinant.DeterminantGenerator(out double[,] matrix2);
                    // Вычисляем определитель матрицы.
                    Tools.DeterminantTool(matrix2);
                    break;
                case 3:
                    // Считываем данные из файла. Если действие оказалось провальным - ошибка.
                    if (WorkInputConstruct.Determinant.DeterminantFile(out double[,] matrix3) &&
                        MatrixVerification.MatrixTrustSquare(matrix3.GetLength(0), matrix3.GetLength(1)))
                    {
                        // Немного информации для пользователя.
                        Environment.ConstructInfo("one_matrix_received");
                        Environment.PrintMatrix(matrix3);
                        // Вычисляем определитель матрицы.
                        Tools.DeterminantTool(matrix3);
                    }
                    else
                    {
                        Environment.FileError("file_error");
                    }
                    break;
            }
        }

        /// <summary>
        /// Метод, находящий решение СЛАУ по матрице.
        /// </summary>
        public static void MatrixSolve()
        {
            Environment.Interface("solve");
            // Делаем запрос на метод ввода данных.
            Input.AskForInputMethod(out ushort method);
            // Выполняем запрос.
            SolveSwitcher(method);
        }

        /// <summary>
        /// Метод, создающий задачи по типу метода ввода пользовователя.
        /// </summary>
        /// <param name="method">Число типа ushort, означающие тип ввода.</param>
        private static void SolveSwitcher(ushort method)
        {
            switch (method)
            {
                case 1:
                    // Просим пользователя ввести данные через консоль.
                    WorkInputConstruct.Solve.SolveConsole(out double[,] matrix1);
                    // Решаем СЛАУ методом Крамера.
                    Tools.SolveTool(matrix1);
                    break;
                case 2:
                    // Просим пользователя дать информацию о генерации и сгенерировать информацию для вычислений.
                    WorkInputConstruct.Solve.SolveGenerator(out double[,] matrix2);
                    // Решаем СЛАУ методом Крамера.
                    Tools.SolveTool(matrix2);
                    break;
                case 3:
                    // Считываем данные из файла. Если действие оказалось провальным - ошибка.
                    if (WorkInputConstruct.Solve.SolveFile(out double[,] matrix3) &&
                        MatrixVerification.MatrixTrustSolve(matrix3.GetLength(0), matrix3.GetLength(1)))
                    {
                        // Немного информации для пользователя.
                        Environment.ConstructInfo("one_matrix_received");
                        Environment.PrintMatrix(matrix3);
                        // Решаем СЛАУ методом Крамера.
                        Tools.SolveTool(matrix3);
                    }
                    else
                    {
                        Environment.FileError("file_error");
                    }
                    break;
            }
        }
    }

}
