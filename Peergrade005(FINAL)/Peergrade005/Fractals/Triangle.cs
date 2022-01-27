using System;
using System.Drawing;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Point = System.Drawing.PointF;

namespace Peergrade005
{
    /// <summary>
    /// Класс Треугольника Серпинского, содержащий методы для его прорисовки.
    /// </summary>
    partial class Triangle : Fractal
    {
        /// <summary>
        /// Конструктор класса Треугольника Серпинского, наследуемого от класса Фрактал.
        /// </summary>
        /// <param name="segmentLength">Длина начального отрезка фрактала.</param>
        /// <param name="recursionDepth">Глубина рекурсии.</param>
        /// <param name="startColor">Начальный цвет градиента фрактала.</param>
        /// <param name="endColor">Конечный цвет градиента фрактала.</param>
        public Triangle(float segmentLength, ushort recursionDepth, Color startColor, Color endColor)
            : base(segmentLength, recursionDepth, startColor, endColor) { }


        /// <summary>
        /// Метод создания фрактала Треугольник Серпинского.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        public override void CreateFractal(Point[] points, int recursion)
        {
            if (recursion == 0) return;
            // Получаем три точки построения треугольника.
            WorkWithPoints(
                points,
                out var bottom,
                out var left,
                out var right);
            // Создаём фрактальный рисунок.
            CreateSomeFractals(
                points,
                recursion,
                bottom,
                left,
                right);
        }
    }
}
