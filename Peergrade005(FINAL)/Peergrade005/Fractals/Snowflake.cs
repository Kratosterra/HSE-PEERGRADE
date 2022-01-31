using Color = System.Drawing.Color;
using Point = System.Drawing.PointF;


namespace Peergrade005
{
    /// <summary>
    /// Класс кривой Коха (снежинки), содержащий методы для её прорисовки.
    /// </summary>
    partial class Snowflake : Fractal
    {
        /// <summary>
        /// Конструктор класса Кривой Коха (снежинки), наследуемой от класса Фрактал.
        /// </summary>
        /// <param name="segmentLength">Длина начального отрезка фрактала.</param>
        /// <param name="recursionDepth">Глубина рекурсии.</param>
        /// <param name="startColor">Начальный цвет градиента фрактала.</param>
        /// <param name="endColor">Конечный цвет градиента фрактала.</param>
        public Snowflake(float segmentLength, ushort recursionDepth, Color startColor, Color endColor)
            : base(segmentLength, recursionDepth, startColor, endColor)
        { }

        /// <summary>
        /// Метод создания фрактала Кривая Коха (снежинки).
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        public override void CreateFractal(Point[] points, int recursion)
        {
            // Вычисляем координаты новой точки засчёт вычисления смещения координат точек прародителей.
            float displacementSegmentX = (points[1].X - points[0].X) / 3;
            float displacementSegmentY = (points[1].Y - points[0].Y) / 3;
            // Получаем контрольные точки трёх отрезков построения основного фрактала.
            WorkWithPoints(
                points,
                displacementSegmentX,
                displacementSegmentY,
                out var pointOfFirstSegment,
                out var pointOfThridSegment,
                out var pointOfSecondSegment);
            // Создаём фрактальный рисунок.
            if (recursion == RecursionDepth)
            {
                base.CreateFractal(new Point[] { points[0], points[1] }, recursion);
            }
            if (recursion > 1)
            {
                CreateSomeFractals(
                    points,
                    recursion,
                    pointOfFirstSegment,
                    pointOfSecondSegment,
                    pointOfThridSegment);
            }
        }

    }
}
