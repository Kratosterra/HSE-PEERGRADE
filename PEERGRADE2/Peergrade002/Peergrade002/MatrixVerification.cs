using System;

namespace Peergrade002
{
    /// <summary>
    /// Метод проверки матриц с учётом их размера.
    /// </summary>
    class MatrixVerification
    {
        /// <summary>
        /// Метод, проверяющий, можно ли перемножить две матрицы.
        /// </summary>
        /// <param name="height1">Колличество строк в первой матрице.</param>
        /// <param name="width1">Колличество столбцов в первой матрице.</param>
        /// <param name="height2">Колличество строк во второй матрице.</param>
        /// <param name="width2">Колличество столбцов во второй матрице.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, можно\нельзя перемножить матрицы.</item>
        /// </list>
        /// </returns>
        public static bool TwoMatrixTrustMultiplication(int height1, int width1, int height2, int width2)
        {
            // Если колличество столбцов в первой матрице совпадает совпадает с колличесвтом строк во второй,
            // то перемножать матрицы можно! Иначе - нельзя.
            if (width1 == height2)
            {
                return true;
            }
            else
            {
                Environment.MatrixError("matrix_multiplication");
                return false;
            }
        }

        /// <summary>
        /// Метод, проверяющий, можно сложить или вычесть матрицы.
        /// </summary>
        /// <param name="height1">Колличество строк в первой матрице.</param>
        /// <param name="width1">Колличество столбцов в первой матрице.</param>
        /// <param name="height2">Колличество строк во второй матрице.</param>
        /// <param name="width2">Колличество столбцов во второй матрице.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, можно\нельзя сложить или вычесть матрицы.</item>
        /// </list>
        /// </returns>
        public static bool TwoMatrixTrustSum(int height1, int width1, int height2, int width2)
        {
            // Складывать и вычитать можно только матрицы с одинаковой размерностью.
            if (height1 == height2 && width1 == width2)
            {
                return true;
            }
            else
            {
                Environment.MatrixError("matrix_sum");
                return false;
            }
        }

        /// <summary>
        /// Метод, проверяющий, является ли матрица квадратной.
        /// </summary>
        /// <param name="height">Колличество строк в матрице.</param>
        /// <param name="width">Колличество столбцов в матрице.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, можно\нельзя сложить или вычесть матрицы.</item>
        /// </list>
        /// </returns>
        public static bool MatrixTrustSquare(int height, int width)
        {
            // Если колличество строк и столбцов совпадает, то матрица квадратная.
            if (height == width)
            {
                return true;
            }
            else
            {
                Environment.MatrixError("square_matrix");
                return false;
            }

        }

        /// <summary>
        /// Метод, проверяющий, возможно ли полноценно решить СЛАУ.
        /// </summary>
        /// <param name="height">Колличество строк в матрице.</param>
        /// <param name="width">Колличество столбцов в матрице.</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа bool, можно\нельзя решить СЛАУ с таким колличеством переменных.</item>
        /// </list>
        /// </returns>
        public static bool MatrixTrustSolve(int height, int width)
        {
            // Для численого решения СЛАУ требуется, чтобы колличество строк совпадало с колличеством переменных,
            // a колличество столбцов было на одну больше, чем строк, ведь в последнем столбце записываются своподные коофиценты,
            // которые заапиисаны в уравнении после знака равно.
            if (height == width - 1)
            {
                return true;
            }
            else
            {
                Environment.MatrixError("sove_matrix");
                return false;
            }

        }
    }

}
