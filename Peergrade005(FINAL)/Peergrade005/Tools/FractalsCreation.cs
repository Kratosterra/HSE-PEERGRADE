
using System;
using System.Drawing;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Point = System.Drawing.PointF;

namespace Peergrade005
{
    /// <summary>
    /// Класс, являюшиеся родительским для всех фракталов.
    /// </summary>
    partial class Fractal
    {
        /// <summary>
        /// Метод создания градиента фрактала.
        /// </summary>
        /// <param name="recursionDepth">Глубина рекурсии.</param>
        protected void CreateNewGradient(int recursionDepth)
        {
            s_colorsGradient = new Color[recursionDepth];
            // Создаём градиент.
            Tools.ExecuteColorsGradient(recursionDepth, ref s_colorsGradient, s_startColor, s_endColor);
        }
    }

    /// <summary>
    /// Класс Фрактального дерева, содержащий методы для его прорисовки.
    /// </summary>
    partial class Tree
    {
        /// <summary>
        /// Метод, устанавливающий координаты необходимых для построения фрактала точек.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        private void WorkWithPoints(Point[] points, int recursion)
        {
            if (recursion == RecursionDepth)
            {
                s_startPoint = new Point(-1, -1);
                s_endPoint = new Point(-1, -1);
            }

            foreach (var point in points)
            {

                s_startPoint.X = (point.X < s_startPoint.X || s_startPoint.X == -1) ? point.X : s_startPoint.X;

                s_endPoint.X = (point.X > s_endPoint.X || s_endPoint.X == -1) ? point.X : s_endPoint.X;

                s_startPoint.Y = (point.Y < s_startPoint.Y || s_startPoint.Y == -1) ? point.Y : s_startPoint.Y;

                s_endPoint.Y = (point.Y > s_endPoint.Y || s_endPoint.Y == -1) ? point.Y : s_endPoint.Y;
            }
        }

        /// <summary>
        /// Метод, создающий необходимые части фрактала.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        private void CreateSomeFractals(Point[] points, int recursion)
        {
            // Вычисляем относительную длину отрезка построения.
            double segmentLength = LengthOfSegment * Math.Pow(Relation, RecursionDepth - recursion + 1);
            // Создаём два сегмента фрактала из текущей точки с учётом направления и угла наклона.
            CreateIterrationOfFractal(points[1], segmentLength, SecondAngle, 1, recursion);
            CreateIterrationOfFractal(points[1], segmentLength, FirstAngle, -1, recursion);
        }

        /// <summary>
        /// Метод, создающий необходимые части фрактала.
        /// </summary>
        /// <param name="startPoint">Стартовая точка построения.</param>
        /// <param name="segmentLength">Длина отрезка построения.</param>
        /// <param name="angleOfVector">Наклон отрезка.</param>
        /// <param name="treeDirection">Направление наклона отрезка построения фрактала</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        private void CreateIterrationOfFractal(Point startPoint, double segmentLength, double angleOfVector,
            int treeDirection, int recursion)
        {
            s_nowAngle += angleOfVector * treeDirection;
            // Вычисляем координаты конечной точки. 
            float endPointX = (float)(startPoint.X - treeDirection *
                (segmentLength * Math.Sin(s_nowAngle * treeDirection)));
            float endPointY = (float)(startPoint.Y - (segmentLength * Math.Cos(s_nowAngle * treeDirection)));
            // Cоздаём конечную точку.
            Point endPoint = new Point(endPointX, endPointY);
            // Снова вызываем построения фрактала исходя из текущей позиции построения.
            CreateFractal(new Point[] { startPoint, endPoint }, recursion - 1);
            s_nowAngle -= angleOfVector * treeDirection;
        }
    }


    /// <summary>
    /// Класс кривой Коха (снежинки), содержащий методы для её прорисовки.
    /// </summary>
    partial class Snowflake
    {
        /// <summary>
        /// Метод, создающий необходимые части фрактала.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        /// <param name="pointOfFirstSegment">Точка первого отрезка построения.</param>
        /// <param name="pointOfSecondSegment">Точка второго отрезка построения.</param>
        /// <param name="pointOfThridSegment">Точка третьего отрезка построения.</param>
        private void CreateSomeFractals(Point[] points, int recursion, Point pointOfFirstSegment,
         Point pointOfSecondSegment, Point pointOfThridSegment)
        {
            // Создаём зубчатый массив точек.
            Point[][] creationPoints = new[]
            {
                new Point[] {points[0], pointOfFirstSegment},
                new Point[] {pointOfFirstSegment, pointOfSecondSegment},
                new Point[] {pointOfSecondSegment, pointOfThridSegment},
                new Point[] {pointOfThridSegment, points[1]}
            };

            // Создаём два отрезка.
            base.CreateFractal(creationPoints[1], recursion - 1);
            base.CreateFractal(creationPoints[2], recursion - 1);
            // Закрашиваем линию построения белым
            GraphicsFractal.DrawLine(new Pen(Color.White, 2f), pointOfFirstSegment, pointOfThridSegment);

            // Производим вызов рекурсивной функции в цикле. 
            for (int i = 0; i < creationPoints.Length; i++)
            {
                CreateFractal(creationPoints[i], recursion - 1);
            }
        }

        /// <summary>
        /// Метод, устанавливающий координаты необходимых для построения фрактала точек.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        private static void WorkWithPoints(Point[] points, float displacementSegmentX, float displacementSegmentY,
            out Point pointOfFirstSegment, out Point pointOfThridSegment, out Point pointOfSecondSegment)
        {
            // Создаём контрольные точки построения.
            pointOfFirstSegment = new Point(
                points[0].X + displacementSegmentX,
                points[0].Y + displacementSegmentY);

            pointOfThridSegment = new Point(
                points[0].X + displacementSegmentX * 2,
                points[0].Y + displacementSegmentY * 2);

            // Вычисляем кординаты контрольной точки второго отрезка.
            float platformX = (float)(
                (pointOfThridSegment.X - pointOfFirstSegment.X) * Math.Cos(Math.PI / 3)
                + (pointOfThridSegment.Y - pointOfFirstSegment.Y) * Math.Sin(Math.PI / 3));

            float platformY = (float)(
                (pointOfThridSegment.Y - pointOfFirstSegment.Y) * Math.Cos(Math.PI / 3)
                - (pointOfThridSegment.X - pointOfFirstSegment.X) * Math.Sin(Math.PI / 3));
            // получаем контрольную точку второго отрезка.
            pointOfSecondSegment = new Point(
                pointOfFirstSegment.X + platformX,
                pointOfFirstSegment.Y + platformY);
        }
    }


    /// <summary>
    /// Класс Ковра Серпинского, содержащий методы для его прорисовки.
    /// </summary>
    partial class Carpet
    {
        /// <summary>
        /// Метод, создающий фрактал нулевой рекурсии.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="currentSegmentLength">Текущая длина отрезка построения.</param>
        private void CreateZeroRecursionFractal(Point[] points, float currentSegmentLength)
        {
            // Вычисляем опорную точку.
            Point endPoint = new Point(
                points[0].X + currentSegmentLength,
                points[0].Y + currentSegmentLength);
            // Строим многоугольник.
            base.CreateFractal(
                new Point[] { points[0], endPoint },
                RecursionDepth == 1 ? 1 : s_nowControlColor++);
        }

        /// <summary>
        /// Метод, создающий необходимые части фрактала.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="startPoint">Глубина рекурсии.</param>
        /// <param name="midPoint">Глубина рекурсии.</param>
        /// <param name="endingPoint">Глубина рекурсии.</param>
        private void CreateSomeFractals(int recursion, Point startPoint, Point midPoint, Point endingPoint)
        {
            // Создаём зубчатый массив точек.
            Point[][] creationPoints =
            {
                new Point[] {new Point(startPoint.X, startPoint.Y)},
                new Point[] {new Point(midPoint.X, startPoint.Y)},
                new Point[] {new Point(endingPoint.X, startPoint.Y)},
                new Point[] {new Point(startPoint.X, midPoint.Y)},
                new Point[] {new Point(endingPoint.X, midPoint.Y)},
                new Point[] {new Point(startPoint.X, endingPoint.Y)},
                new Point[] {new Point(midPoint.X, endingPoint.Y)},
                new Point[] { new Point(endingPoint.X, endingPoint.Y) }
            };
            // Вызываем рекурсивную функцию создания фрактала в цикле.
            for (int i = 0; i < creationPoints.Length; i++)
            {
                CreateFractal(creationPoints[i], recursion - 1);
            }
        }

        /// <summary>
        /// Метод, устанавливающий координаты необходимых для построения фрактала точек.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="currentSegmentLength">Текущая длина отрезка построения.</param>
        /// <param name="startPoint">Начальная точка.</param>
        /// <param name="midPoint">Серединная точка.</param>
        /// <param name="endingPoint">Конечная точка.</param>
        private static void WorkWithPoints(Point[] points, float currentSegmentLength, out Point startPoint,
            out Point midPoint, out Point endingPoint)
        {
            // Устанавливаем начальную точку.
            startPoint = new Point(points[0].X, points[0].Y);
            // Устанавливаем серединную точку.
            midPoint = new Point(
                startPoint.X + currentSegmentLength / 3,
                startPoint.Y + currentSegmentLength / 3);
            // Устанавливаем конечную точку.
            endingPoint = new Point(
                startPoint.X + currentSegmentLength * 2 / 3,
                startPoint.Y + currentSegmentLength * 2 / 3);
        }
    }

    /// <summary>
    /// Класс Треугольника Серпинского, содержащий методы для его прорисовки.
    /// </summary>
    partial class Triangle
    {
        /// <summary>
        /// Метод, создающий необходимые части фрактала.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        /// <param name="bottom">Нижняя точка треугольника.</param>
        /// <param name="left">Левая точка треугольника.</param>
        /// <param name="right">Правая точка треугольника.</param>
        private void CreateSomeFractals(Point[] points, int recursion, Point bottom, Point left,
            Point right)
        {
            // Вызываем построение три раза, чтобы создать треугольник.
            CreateFractal(new Point[] { points[0], bottom, left }, recursion - 1);
            CreateFractal(new Point[] { bottom, points[1], right }, recursion - 1);
            CreateFractal(new Point[] { left, right, points[2] }, recursion - 1);
            // Реализуем построение основной части фрактала.
            for (int i = 0; i <= points.Length - 1; i++)
            {
                base.CreateFractal(
                    new Point[] {points[i % points.Length],
                        points[(i + 1) % points.Length]}, recursion);
            }
        }

        /// <summary>
        /// Метод, устанавливающий координаты необходимых для построения фрактала точек.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="bottom">Нижняя точка треугольника.</param>
        /// <param name="left">Левая точка треугольника.</param>
        /// <param name="right">Правая точка треугольника.</param>
        private static void WorkWithPoints(Point[] points, out Point bottom, out Point left, out Point right)
        {
            bottom = new Point(
                (points[0].X + points[1].X) / 2,
                (points[0].Y + points[1].Y) / 2);

            left = new Point(
                (points[0].X + points[2].X) / 2,
                (points[0].Y + points[2].Y) / 2);

            right = new Point(
                (points[1].X + points[2].X) / 2,
                (points[1].Y + points[2].Y) / 2);
        }
    }

    /// <summary>
    /// Класс Множества Кантора, содержащий методы для его прорисовки.
    /// </summary>
    partial class Set
    {

        /// <summary>
        /// Метод, устанавливающий координаты необходимых для построения фрактала точек.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="currentSegmentLength">Текущая длина отрезка построения.</param>
        /// <param name="endPoint">Конечная контрольная точка построения.</param>
        private void WorkWithPoints(Point[] points, float currentSegmentLength, out Point endPoint)
        {
            // Вычисляем точку.
            endPoint = new Point(
                points[0].X + currentSegmentLength,
                points[0].Y + s_sideLength);
        }

        /// <summary>
        /// Метод, создающий необходимые части фрактала.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        /// <param name="currentSegmentLength">Текущая длина отрезка построения.</param>
        /// <param name="endPoint">Конечная контрольная точка построения.</param>
        private void CreateSomeFractals(Point[] points, int recursion, Point endPoint, float currentSegmentLength)
        {
            // Вызываем создание основы фрактала.
            base.CreateFractal(
                new Point[] { points[0], endPoint },
                recursion);
            // Производим создание прямоугольников, с учётом парметров пользователя.
            CreateFractal(
                new Point[]
                {
                    new Point(
                        points[0].X,
                        points[0].Y + s_betweenDistance + s_sideLength)
                },
                recursion - 1);

            CreateFractal(
                new Point[]
                {
                    new Point(
                        points[0].X + currentSegmentLength * 2 / 3,
                        points[0].Y + s_betweenDistance + s_sideLength)
                },
                recursion - 1);
        }
    }
}
