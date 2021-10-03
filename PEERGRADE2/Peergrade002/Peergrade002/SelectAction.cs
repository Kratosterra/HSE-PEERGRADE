using System;
using static System.Console;

namespace Peergrade002
{
    /// <summary>
    /// Класс, заведующий началом исполнения калькулятора, получение действия и последующее начало выполнения.
    /// </summary>
    class SelectAction
    {
        /// <summary>
        /// Метод, возвращающий номер действия, полученное от пользователя.
        /// </summary>
        /// <returns>
        /// <list type="bullet">
        /// <item>Возвращает значение типа ushort, означающее номер действия.</item>
        /// </list>
        /// </returns>
        public static void Choice(out ushort actionChoice)
        {
            string actionChoiceData;
            // До того момента пока пользователь не введет подходящее действие, просим ввести его снова. 
            do
            {
                // Предлагаем выбрать действие из меню.
                Environment.Information("choice_start");
                actionChoiceData = ReadLine();

            } while (!Input.GetAction(actionChoiceData, out actionChoice));
        }

        /// <summary>
        /// Метод, запускающий действие калькулятора.
        /// </summary>
        /// <param name="actionChoice">Число типа ushort, означающие действие.</param>
        public static void Start(ushort actionChoice)
        {
            switch (actionChoice)
            {
                case 1:
                    // Запускаем нахождение следа матрицы.
                    Actions.FindMatrixTrace();
                    break;
                case 2:
                    // Транспонируем матрицу.
                    Actions.MatrixTransposition();
                    break;
                case 3:
                    // Суммируем матрицы.
                    Actions.MatrixSum();
                    break;
                case 4:
                    // Разность матриц.
                    Actions.MatrixSubSum();
                    break;
                case 5:
                    // Произведение матриц.
                    Actions.MatrixMultiplication();
                    break;
                case 6:
                    // Произведение матрицы и числа.
                    Actions.MatrixNumMultiplication();
                    break;
                case 7:
                    // Находим определитель матрицы.
                    Actions.MatrixDet();
                    break;
                case 8:
                    // Решаем СЛАУ.
                    Actions.MatrixSolve();
                    break;
            }
        }

    }

}
