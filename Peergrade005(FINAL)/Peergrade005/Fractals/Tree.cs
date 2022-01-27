using System;
using System.Drawing;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Point = System.Drawing.PointF;

namespace Peergrade005
{
    /// <summary>
    /// Класс Фрактального дерева, содержащий методы для его прорисовки.
    /// </summary>
    partial class Tree : Fractal
    {
        // Угол поворота сейчас.
        private double s_nowAngle;
        // Первый угол поворота ветви.
        private double s_firstAngle;
        // Второй угол поворота ветви.
        private double s_secondAngle;

        // Начальная точка построения.
        private Point s_startPoint;
        // Конечная точка построения.
        private Point s_endPoint;

        /// <summary>
        /// Свойство отношения длин отрезков фрактала.
        /// </summary>
        private double Relation { get; set; }

        /// <summary>
        /// Свойство первого угла поворота ветви.
        /// </summary>
        private double FirstAngle
        {
            get => s_firstAngle;
            set => s_firstAngle = value * Math.PI / 180;
        }

        /// <summary>
        /// Свойство второго угла поворота ветви.
        /// </summary>
        private double SecondAngle
        {
            get => s_secondAngle;
            set => s_secondAngle = value * Math.PI / 180;
        }

        /// <summary>
        /// Конструктор класса Фрактальное дерево, наследуемый от класса Фрактал.
        /// </summary>
        /// <param name="segmentLength">Длина начального отрезка фрактала.</param>
        /// <param name="recursionDepth">Глубина рекурсии.</param>
        /// <param name="startColor">Начальный цвет градиента фрактала.</param>
        /// <param name="endColor">Конечный цвет градиента фрактала.</param>
        /// <param name="firstAngle">Первый угол поворота ветви.</param>
        /// <param name="secondAngle">Второй угол поворота ветви.</param>
        /// <param name="relation">Отношение длин отрезков фрактала.</param>
        public Tree(float segmentLength, ushort recursionDepth, Color startColor, Color endColor,
            double firstAngle, double secondAngle, double relation) : base(segmentLength, recursionDepth, startColor,
            endColor)
        {
            // Устанавливаем первый угол.
            FirstAngle = firstAngle;
            // Задаём второй угол поворота.
            SecondAngle = secondAngle;
            // Устанавливаем отношения отрезков разных итераций.
            Relation = relation;
        }

        /// <summary>
        /// Метод создания фрактала Фрактальное дерево.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        public override void CreateFractal(Point[] points, int recursion)
        {
            if (recursion == 0) return;
            // Создаём отрезок.
            base.CreateFractal(points, recursion);
            // Получаем точки.
            WorkWithPoints(points, recursion);
            // Производим замену.
            points[0] = points[1];
            // Создаём фрактальный рисунок.
            CreateSomeFractals(points, recursion);
        }

    }
}
