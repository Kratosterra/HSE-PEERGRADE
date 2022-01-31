using System;
using System.Drawing;
using Color = System.Drawing.Color;
using Point = System.Drawing.PointF;

namespace Peergrade005
{

    /// <summary>
    /// Класс Множества Кантора, содержащий методы для его прорисовки.
    /// </summary>
    partial class Set : Fractal
    {
        // Поле для хранения длины одной стороны.
        private readonly float s_sideLength;
        // поле для хранения расстояния между ячейками множества.
        private readonly float s_betweenDistance;

        /// <summary>
        /// Конструктор класса Множества Кантора, наследуемого от класса Фрактал.
        /// </summary>
        /// <param name="segmentLength">Длина начального отрезка фрактала.</param>
        /// <param name="recursionDepth">Глубина рекурсии.</param>
        /// <param name="startColor">Начальный цвет градиента фрактала.</param>
        /// <param name="endColor">Конечный цвет градиента фрактала.</param>
        /// <param name="height">Длина одной строный (высота).</param>
        /// <param name="distance">Расстояние между ячейками множества.</param>
        public Set(float segmentLength, ushort recursionDepth, Color startColor, Color endColor,
            float height, float distance) : base(segmentLength, recursionDepth, startColor, endColor)
        {
            s_sideLength = height;
            s_betweenDistance = distance;
        }

        /// <summary>
        /// Метод создания фрактала Множество Кантора.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        public override void CreateFractal(Point[] points, int recursion)
        {
            if (recursion == 0) return;
            // Вычисляем текущую длину отрезка построения.
            float currentSegmentLength = (float)LengthOfSegment / (float)Math.Pow(3, RecursionDepth - recursion);
            // Получаем конечную точку построения.
            WorkWithPoints(points, currentSegmentLength, out var endPoint);
            // Cоздаём фрактальный рисунок.
            CreateSomeFractals(points, recursion, endPoint, currentSegmentLength);
        }
    }
}
