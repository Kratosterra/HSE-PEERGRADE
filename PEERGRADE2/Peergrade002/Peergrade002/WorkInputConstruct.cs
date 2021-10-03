using System;
using static System.Console;

namespace Peergrade002
{
    /// <summary>
    /// Класс, зведующий получением данных для вычислений.
    /// </summary>
    class WorkInputConstruct
    {
        /// <summary>
        /// Класс, отвечающий за получение информации для поиска следа матрицы.
        /// </summary>
        public class Trace
        {
            /// <summary>
            /// Метод, возвращающий матрицу согласно вводу пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// </list>
            /// </returns>
            public static void TraceConsole(out double[,] matrix)
            {
                // Запрос размерности матрицы.
                Input.AskForSize(true, out int size);
                // Запрос самой матрицы согласно размерности.
                Input.AskForMatrix(size, size, out matrix);
                // Вывод матрицы на экран.
                Environment.ConstructInfo("one_matrix_received");
                Environment.PrintMatrix(matrix);
            }
            /// <summary>
            /// Метод, возвращающий матрицу которая была сгенерированна согласно
            /// предпочтениям пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// </list>
            /// </returns>
            public static void TraceGenerator(out double[,] matrix)
            {
                // Запрос размерности матрицы.
                Input.AskForSize(true, out int size);
                // Запрос данных для генерации.
                Environment.ConstructInfo("start_gen");
                Input.AskForDouble(out bool needDouble);
                Input.AskForControlNum(out int controlNum);
                // Генерация.
                Tools.GenerateMatrix(needDouble, controlNum, size, size, out matrix);
                // Вывод матрицы на экран.
                Environment.ConstructInfo("one_matrix_gen");
                Environment.PrintMatrix(matrix);
            }

            /// <summary>
            /// Метод, возвращающий матрицу которая была прочитана из файла Input.txt.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа bool означающее успех/неуспех чтения файла.</item>
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// </list>
            /// </returns>
            public static bool TraceFile(out double[,] matrix)
            {
                // Пытаемся прочитать файл.
                return Input.TryToReadFileOne(out matrix);
            }
        }

        /// <summary>
        /// Класс, отвечающий за получение информации длятранспозиции матрицы.
        /// </summary>
        public class Transposition
        {
            /// <summary>
            /// Метод, возвращающий матрицу согласно вводу пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// </list>
            /// </returns>
            public static void TranspositionConsole(out double[,] matrix)
            {
                // Запрос размерности матрицы.
                Input.AskForSizes(out int size1, out int size2);
                // Запрос самой матрицы согласно размерности.
                Input.AskForMatrix(size1, size2, out matrix);
                // Вывод матрицы на экран.
                Environment.ConstructInfo("one_matrix_received");
                Environment.PrintMatrix(matrix);
            }

            /// <summary>
            /// Метод, возвращающий матрицу которая была сгенерированна согласно
            /// предпочтениям пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// </list>
            /// </returns>
            public static void TranspositionGenerator(out double[,] matrix)
            {
                // Запрос размерности матрицы.
                Input.AskForSizes(out int size1, out int size2);
                // Запрос данных для генерации.
                Environment.ConstructInfo("start_gen");
                Input.AskForDouble(out bool needDouble);
                Input.AskForControlNum(out int controlNum);
                // Генерация.
                Tools.GenerateMatrix(needDouble, controlNum, size1, size2, out matrix);
                // Вывод матрицы на экран.
                Environment.ConstructInfo("one_matrix_gen");
                Environment.PrintMatrix(matrix);
            }

            /// <summary>
            /// Метод, возвращающий матрицу которая была прочитана из файла Input.txt.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа bool означающее успех/неуспех чтения файла.</item>
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// </list>
            /// </returns>
            public static bool TranspositionFile(out double[,] matrix)
            {
                // Пытаемся прочитать файл.
                return Input.TryToReadFileOne(out matrix);
            }
        }

        /// <summary>
        /// Класс, отвечающий за получение информации для поиска суммы матриц.
        /// </summary>
        public class Sum
        {
            /// <summary>
            /// Метод, возвращающий матрицы согласно вводу пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает два значения типа double[,] то есть две матрицы.</item>
            /// </list>
            /// </returns>
            public static void SumConsole(out double[,] matrix1, out double[,] matrix2)
            {
                int size1, size2, size3, size4;
                do
                {
                    // Запрос размерности матриц.
                    Environment.ConstructInfo("first_matrix");
                    Input.AskForSizes(out size1, out size2);
                    Environment.ConstructInfo("second_matrix");
                    Input.AskForSizes(out size3, out size4);
                } while (!MatrixVerification.TwoMatrixTrustSum(size1, size2, size3, size4));
                Environment.ConstructInfo("first_matrix");
                // Запрос самой матрицы согласно размерности.
                Input.AskForMatrix(size1, size2, out matrix1);
                Environment.ConstructInfo("second_matrix");
                // Запрос самой матрицы согласно размерности.
                Input.AskForMatrix(size3, size4, out matrix2);
                // Вывод матриц на экран.
                Environment.ConstructInfo("two_matrix_received");
                Environment.PrintMatrix(matrix1);
                Environment.PrintMatrix(matrix2);
            }

            /// <summary>
            /// Метод, возвращающий матрицы которые были сгенерированны согласно
            /// предпочтениям пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает два значения типа double[,] то есть матрицы.</item>
            /// </list>
            /// </returns>
            public static void SumGenerator(out double[,] matrix1, out double[,] matrix2)
            {
                int size1, size2, size3, size4;
                do
                {
                    // Запрос размерности матриц.
                    Environment.ConstructInfo("first_matrix");
                    Input.AskForSizes(out size1, out size2);
                    Environment.ConstructInfo("second_matrix");
                    Input.AskForSizes(out size3, out size4);
                } while (!MatrixVerification.TwoMatrixTrustSum(size1, size2, size3, size4));
                // Запрос данных для генерации.
                Environment.ConstructInfo("start_gen");
                Input.AskForDouble(out bool needDouble);
                Input.AskForControlNum(out int controlNum);
                // Генерация.
                Tools.GenerateMatrix(needDouble, controlNum, size1, size2, out matrix1);
                Tools.GenerateMatrix(needDouble, controlNum, size3, size4, out matrix2);
                // Вывод матриц на экран.
                Environment.ConstructInfo("two_matrix_gen");
                Environment.PrintMatrix(matrix1);
                Environment.PrintMatrix(matrix2);
            }

            /// <summary>
            /// Метод, возвращающий матрицы которые были прочитаны из файла Input.txt.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа bool означающее успех/неуспех чтения файла.</item>
            /// <item>Возвращает два значения типа double[,] то есть матрицы.</item>
            /// </list>
            /// </returns>
            public static bool SumFile(out double[,] matrix1, out double[,] matrix2)
            {
                // Пытаемся прочитать файл.
                return Input.TryToReadFileTwo(out matrix1, out matrix2);
            }

        }

        /// <summary>
        /// Класс, отвечающий за получение информации для нахождения разности матриц.
        /// </summary>
        public class SubSum
        {
            /// <summary>
            /// Метод, возвращающий матрицы согласно вводу пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает два значения типа double[,] то есть две матрицы.</item>
            /// </list>
            /// </returns>
            public static void SubSumConsole(out double[,] matrix1, out double[,] matrix2)
            {
                int size1, size2, size3, size4;
                do
                {
                    // Запрос размерности матриц.
                    Environment.ConstructInfo("first_matrix");
                    Input.AskForSizes(out size1, out size2);
                    Environment.ConstructInfo("second_matrix");
                    Input.AskForSizes(out size3, out size4);
                } while (!MatrixVerification.TwoMatrixTrustSum(size1, size2, size3, size4));
                Environment.ConstructInfo("first_matrix");
                // Запрос самой матрицы согласно размерности.
                Input.AskForMatrix(size1, size2, out matrix1);
                Environment.ConstructInfo("second_matrix");
                // Запрос самой матрицы согласно размерности.
                Input.AskForMatrix(size3, size4, out matrix2);
                // Вывод матриц на экран.
                Environment.ConstructInfo("two_matrix_received");
                Environment.PrintMatrix(matrix1);
                Environment.PrintMatrix(matrix2);
            }


            /// <summary>
            /// Метод, возвращающий матрицы которые были сгенерированны согласно
            /// предпочтениям пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает два значения типа double[,] то есть матрицы.</item>
            /// </list>
            /// </returns>
            public static void SubSumGenerator(out double[,] matrix1, out double[,] matrix2)
            {
                int size1, size2, size3, size4;
                do
                {
                    // Запрос размерности матриц.
                    Environment.ConstructInfo("first_matrix");
                    Input.AskForSizes(out size1, out size2);
                    Environment.ConstructInfo("second_matrix");
                    Input.AskForSizes(out size3, out size4);
                } while (!MatrixVerification.TwoMatrixTrustSum(size1, size2, size3, size4));
                // Запрос данных для генерации.
                Environment.ConstructInfo("start_gen");
                Input.AskForDouble(out bool needDouble);
                Input.AskForControlNum(out int controlNum);
                // Генерация.
                Tools.GenerateMatrix(needDouble, controlNum, size1, size2, out matrix1);
                Tools.GenerateMatrix(needDouble, controlNum, size3, size4, out matrix2);
                // Вывод матриц на экран.
                Environment.ConstructInfo("two_matrix_gen");
                Environment.PrintMatrix(matrix1);
                Environment.PrintMatrix(matrix2);
            }

            /// <summary>
            /// Метод, возвращающий матрицы которые были прочитаны из файла Input.txt.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа bool означающее успех/неуспех чтения файла.</item>
            /// <item>Возвращает два значения типа double[,] то есть матрицы.</item>
            /// </list>
            /// </returns>
            public static bool SubSumFile(out double[,] matrix1, out double[,] matrix2)
            {
                // Пытаемся прочитать файл.
                return Input.TryToReadFileTwo(out matrix1, out matrix2);
            }

        }

        /// <summary>
        /// Класс, отвечающий за получение информации для нахождения произведения матриц.
        /// </summary>
        public class Multiplication
        {
            /// <summary>
            /// Метод, возвращающий матрицы согласно вводу пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает два значения типа double[,] то есть две матрицы.</item>
            /// </list>
            /// </returns>
            public static void MultiplicationConsole(out double[,] matrix1, out double[,] matrix2)
            {
                int size1, size2, size3, size4;
                do
                {
                    // Запрос размерности матриц.
                    Environment.ConstructInfo("first_matrix");
                    Input.AskForSizes(out size1, out size2);
                    Environment.ConstructInfo("second_matrix");
                    Input.AskForSizes(out size3, out size4);
                } while (!MatrixVerification.TwoMatrixTrustMultiplication(size1, size2, size3, size4));
                Environment.ConstructInfo("first_matrix");
                // Запрос самой матрицы согласно размерности.
                Input.AskForMatrix(size1, size2, out matrix1);
                Environment.ConstructInfo("second_matrix");
                // Запрос самой матрицы согласно размерности.
                Input.AskForMatrix(size3, size4, out matrix2);
                // Вывод матриц на экран.
                Environment.ConstructInfo("two_matrix_received");
                Environment.PrintMatrix(matrix1);
                Environment.PrintMatrix(matrix2);
            }

            /// <summary>
            /// Метод, возвращающий матрицы которые были сгенерированны согласно
            /// предпочтениям пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает два значения типа double[,] то есть матрицы.</item>
            /// </list>
            /// </returns>
            public static void MultiplicationGenerator(out double[,] matrix1, out double[,] matrix2)
            {
                int size1, size2, size3, size4;
                do
                {
                    // Запрос размерности матриц.
                    Environment.ConstructInfo("first_matrix");
                    Input.AskForSizes(out size1, out size2);
                    Environment.ConstructInfo("second_matrix");
                    Input.AskForSizes(out size3, out size4);
                } while (!MatrixVerification.TwoMatrixTrustMultiplication(size1, size2, size3, size4));
                // Запрос данных для генерации.
                Environment.ConstructInfo("start_gen");
                Input.AskForDouble(out bool needDouble);
                Input.AskForControlNum(out int controlNum);
                // Генерация.
                Tools.GenerateMatrix(needDouble, controlNum, size1, size2, out matrix1);
                Tools.GenerateMatrix(needDouble, controlNum, size3, size4, out matrix2);
                // Вывод матриц на экран.
                Environment.ConstructInfo("two_matrix_gen");
                Environment.PrintMatrix(matrix1);
                Environment.PrintMatrix(matrix2);
            }

            /// <summary>
            /// Метод, возвращающий матрицы которые были прочитаны из файла Input.txt.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа bool означающее успех/неуспех чтения файла.</item>
            /// <item>Возвращает два значения типа double[,] то есть матрицы.</item>
            /// </list>
            /// </returns>
            public static bool MultiplicationFile(out double[,] matrix1, out double[,] matrix2)
            {
                // Пытаемся прочитать файл.
                return Input.TryToReadFileTwo(out matrix1, out matrix2);
            }

        }

        /// <summary>
        /// Класс, отвечающий за получение информации для нахождения произведения мматрицы и числа.
        /// </summary>
        public class MultiplicationNum
        {
            /// <summary>
            /// Метод, возвращающий матрицу и число согласно вводу пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает одно значение типа double[,] то есть матрицу.</item>
            /// <item>Возвращает значение типа double, содержащее число для умножения.</item>
            /// </list>
            /// </returns>
            public static void MultiplicationNumConsole(out double[,] matrix, out double num)
            {
                int size1, size2;
                // Запрос размерности матрицы и значения числа.
                Input.AskForNum(out num);
                Input.AskForSizes(out size1, out size2);
                // Запрос самой матрицы согласно размерности.
                Input.AskForMatrix(size1, size2, out matrix);
                // Вывод матрицы и числа на экран.
                Environment.NumReceived(num);
                Environment.ConstructInfo("one_matrix_received");
                Environment.PrintMatrix(matrix);
            }

            /// <summary>
            /// Метод, возвращающий матрицу и число которые были сгенерированны согласно
            /// предпочтениям пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// <item>Возвращает значение типа double, содержащее число для умножения.</item>
            /// </list>
            /// </returns>
            public static void MultiplicationNumGenerator(out double[,] matrix, out double num)
            {
                int size1, size2;
                // Запрос размерности матрицы.
                Input.AskForSizes(out size1, out size2);
                // Запрос данных для генерации.
                Environment.ConstructInfo("start_gen");
                Input.AskForDouble(out bool needDouble);
                Input.AskForControlNum(out int controlNum);
                // Генерация.
                Tools.GenerateMatrix(needDouble, controlNum, size1, size2, out matrix);
                Tools.GenerateNum(needDouble, controlNum, out num);
                // Вывод матрицы и числа на экран.
                Environment.NumGen(num);
                Environment.ConstructInfo("one_matrix_gen");
                Environment.PrintMatrix(matrix);
            }

            /// <summary>
            /// Метод, возвращающий матрицу и число которые были прочитаны из файла Input.txt.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа bool означающее успех/неуспех чтения файла.</item>
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// <item>Возвращает значение типа double, то есть число для перемножения.</item>
            /// </list>
            /// </returns>
            public static bool MultiplicationNumFile(out double[,] matrix, out double num)
            {
                // Пытаемся прочитать файл.
                return Input.TryToReadFileMatrixNum(out matrix, out num);
            }

        }

        /// <summary>
        /// Класс, отвечающий за получение информации для нахождения определителя.
        /// </summary>
        public class Determinant
        {
            /// <summary>
            /// Метод, возвращающий матрицу согласно вводу пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// </list>
            /// </returns>
            public static void DeterminantConsole(out double[,] matrix)
            {
                // Запрос размерности матрицы.
                Input.AskForSize(true, out int size);
                // Запрос самой матрицы согласно размерности.
                Input.AskForMatrix(size, size, out matrix);
                // Вывод матрицы на экран.
                Environment.ConstructInfo("one_matrix_received");
                Environment.PrintMatrix(matrix);
            }

            /// <summary>
            /// Метод, возвращающий матрицу которая была сгенерированна согласно
            /// предпочтениям пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// </list>
            /// </returns>
            public static void DeterminantGenerator(out double[,] matrix)
            {
                // Запрос размерности матрицы.
                Input.AskForSize(true, out int size);
                // Запрос данных для генерации.
                Environment.ConstructInfo("start_gen");
                Input.AskForDouble(out bool needDouble);
                Input.AskForControlNum(out int controlNum);
                // Генерация.
                Tools.GenerateMatrix(needDouble, controlNum, size, size, out matrix);
                // Вывод матрицы на экран.
                Environment.ConstructInfo("one_matrix_gen");
                Environment.PrintMatrix(matrix);
            }

            /// <summary>
            /// Метод, возвращающий матрицу которая была прочитана из файла Input.txt.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа bool означающее успех/неуспех чтения файла.</item>
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// </list>
            /// </returns>
            public static bool DeterminantFile(out double[,] matrix)
            {
                // Пытаемся прочитать файл.
                return Input.TryToReadFileOne(out matrix);
            }
        }

        /// <summary>
        /// Класс, отвечающий за получение информации для решения СЛАУ методом Крамера.
        /// </summary>
        public class Solve
        {
            /// <summary>
            /// Метод, возвращающий матрицу согласно вводу пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// </list>
            /// </returns>
            public static void SolveConsole(out double[,] matrix)
            {
                int size1, size2;
                do
                {
                    // Запрос размерности матрицы.
                    Input.AskForSizes(out size1, out size2);
                } while (!MatrixVerification.MatrixTrustSolve(size1, size2));
                // Запрос самой матрицы согласно размерности.
                Input.AskForMatrix(size1, size2, out matrix);
                // Вывод матрицы на экран.
                Environment.ConstructInfo("one_matrix_received");
                Environment.PrintMatrix(matrix);
            }

            /// <summary>
            /// Метод, возвращающий матрицу которая была сгенерированна согласно
            /// предпочтениям пользователя.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// </list>
            /// </returns>
            public static void SolveGenerator(out double[,] matrix)
            {
                int size1, size2;
                do
                {
                    // Запрос размерности матрицы.
                    Input.AskForSizes(out size1, out size2);
                } while (!MatrixVerification.MatrixTrustSolve(size1, size2));
                // Запрос данных для генерации.
                Environment.ConstructInfo("start_gen");
                Input.AskForDouble(out bool needDouble);
                Input.AskForControlNum(out int controlNum);
                // Генерация.
                Tools.GenerateMatrix(needDouble, controlNum, size1, size2, out matrix);
                // Вывод матрицы на экран.
                Environment.ConstructInfo("one_matrix_gen");
                Environment.PrintMatrix(matrix);
            }

            /// <summary>
            /// Метод, возвращающий матрицу которая была прочитана из файла Input.txt.
            /// </summary>
            /// <returns>
            /// <list type="bullet">
            /// <item>Возвращает значение типа bool означающее успех/неуспех чтения файла.</item>
            /// <item>Возвращает значение типа double[,] то есть матрицу.</item>
            /// </list>
            /// </returns>
            public static bool SolveFile(out double[,] matrix)
            {
                // Пытаемся прочитать файл.
                return Input.TryToReadFileOne(out matrix);
            }
        }
    }

}
