using System;
using System.Drawing;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Point = System.Drawing.PointF;

namespace Peergrade005
{
    /// <summary>
    /// Класс Ковра Серпинского, содержащий методы для его прорисовки.
    /// </summary>
    partial class Carpet : Fractal
    {
        // Поле для контроля за цветовой составляющей фрактала.
        private int s_nowControlColor;

        /// <summary>
        /// Конструктор класса Ковра Серпинского, наследуемого от класса Фрактал.
        /// </summary>
        /// <param name="segmentLength">Длина начального отрезка фрактала.</param>
        /// <param name="recursionDepth">Глубина рекурсии.</param>
        /// <param name="startColor">Начальный цвет градиента фрактала.</param>
        /// <param name="endColor">Конечный цвет градиента фрактала.</param>
        public Carpet(float segmentLength, ushort recursionDepth, Color startColor, Color endColor)
            : base(segmentLength, recursionDepth, startColor, endColor)
        {
        }

        /// <summary>
        /// Метод создания фрактала Ковёр Серпинского.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        public override void CreateFractal(Point[] points, int recursion)
        {
            // Вычисляем текущую длину отрезка.
            float currentSegmentLength = (float)LengthOfSegment / (float)Math.Pow(3, RecursionDepth - recursion);

            if (recursion == RecursionDepth)
            {
                s_nowControlColor = 1;
                CreateNewGradient((int)Math.Pow(8, RecursionDepth));
            }
            if (recursion == 0)
            {
                // Cоздаём ковер нулевой рекурсии.
                CreateZeroRecursionFractal(points, currentSegmentLength);
                return;
            }
            // Получаем три основные точки построения.
            WorkWithPoints(
                points,
                currentSegmentLength,
                out var startPoint,
                out var midPoint,
                out var endingPoint);
            // Cоздаём фрактальный рисунок.
            CreateSomeFractals(
                recursion,
                startPoint,
                midPoint,
                endingPoint);
        }

    }
}
