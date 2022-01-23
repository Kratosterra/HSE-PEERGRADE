using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Point = System.Drawing.PointF;



namespace Peergrade005
{
    /// <summary>
    /// Класс, являюшиеся родительским для всех фракталов.
    /// </summary>
    class Fractal
    {
        // Длина начального отрезка фрактала.
        private double s_lenghtOfSegment;
        // Глубина рекурсии.
        private ushort s_depthOfRecursion;
        // Наследуемый обьект графики.
        protected Graphics GraphicsFractal;

        // Начальный цвет градиента.
        private Color s_startColor;
        // Конечный цвет градиента.
        private Color s_endColor;
        // Массив цветов градиента.
        private Color[] s_colorsGradient;

        // Своство длинны начального отрезка фрактала.
        public double LengthOfSegment
        {
            get => s_lenghtOfSegment;
            private set => s_lenghtOfSegment = value;
        }

        // Cвойство глубины рекурсии.
        public ushort RecursionDepth
        {
            get => s_depthOfRecursion;
            set
            {
                s_depthOfRecursion = value;
                // Cоздаём новый градиент в зависимости от глубины рекурсии.
                CreateNewGradient(RecursionDepth);
            }
        }

        // Поле для хранения и получения битовой карты фрактала.
        public Bitmap FractalBitmap { get; set; }

        /// <summary>
        /// Родительский конструктор класса Фрактал.
        /// </summary>
        /// <param name="segmentLength">Длина начального отрезка фрактала.</param>
        /// <param name="recursionDepth">Глубина рекурсии.</param>
        /// <param name="startColor">Начальный цвет градиента фрактала.</param>
        /// <param name="endColor">Конечный цвет градиента фрактала.</param>
        public Fractal(double segmentLength, ushort recursionDepth, Color startColor, Color endColor)
        {
            // Установка длины отрезка и глубины рекурсии.
            LengthOfSegment = segmentLength;
            RecursionDepth = recursionDepth;
            // Установка цветов и создание нового градиента.
            this.s_startColor = startColor;
            this.s_endColor = endColor;
            CreateNewGradient(RecursionDepth);
        }

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

        /// <summary>
        /// Метод создания градиента фрактала.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        public virtual void CreateFractal(Point[] points, int recursion)
        {
            GraphicsFractal = Graphics.FromImage(FractalBitmap);
            // В зависимости от типа фрактала устанавливаем начальную точку создания фрактала.
            if (this is Tree || this is Snowflake || this is Triangle)
            {
                Tools.CreateLine(points, recursion, ref GraphicsFractal, s_colorsGradient);
            }
            else
            {
                Tools.CreateRectangle(points, recursion, ref GraphicsFractal, s_colorsGradient);
            }
        }


    }

    class Tree : Fractal
    {
        private double s_nowAngle;
        private double s_firstAngle;
        private double s_secondAngle;

        private Point s_startPoint;
        private Point s_endPoint;

        private double Relation { get; set; }

        private double FirstAngle
        {
            get => s_firstAngle;
            set => s_firstAngle = value * Math.PI / 180;
        }

        private double SecondAngle
        {
            get => s_secondAngle;
            set => s_secondAngle = value * Math.PI / 180;
        }


        public Tree(float segmentLength, ushort recursionDepth, Color startColor, Color endColor,
            double firstAngle, double secondAngle, double relation) : base(segmentLength, recursionDepth, startColor,
            endColor)
        {
            FirstAngle = firstAngle;
            SecondAngle = secondAngle;
            Relation = relation;
        }


        public override void CreateFractal(Point[] points, int recursion)
        {
            if (recursion == 0) return;
            base.CreateFractal(points, recursion);
            WorkWithPoints(points, recursion);
            points[0] = points[1];
            CreateSomeFractals(points, recursion);
        }

        private void WorkWithPoints(Point[] points, int recursion)
        {
            if (recursion == RecursionDepth)
            {
                s_startPoint = new Point(-1, -1);
                s_endPoint = new Point(-1, -1);
            }

            foreach (Point point in points)
            {
                s_startPoint.X = (point.X < s_startPoint.X || s_startPoint.X == -1) ? point.X : s_startPoint.X;
                s_endPoint.X = (point.X > s_endPoint.X || s_endPoint.X == -1) ? point.X : s_endPoint.X;
                s_startPoint.Y = (point.Y < s_startPoint.Y || s_startPoint.Y == -1) ? point.Y : s_startPoint.Y;
                s_endPoint.Y = (point.Y > s_endPoint.Y || s_endPoint.Y == -1) ? point.Y : s_endPoint.Y;
            }
        }

        private void CreateSomeFractals(Point[] points, int recursion)
        {
            CreateIterrationOfFractal(
                points[1],
                LengthOfSegment * Math.Pow(Relation, RecursionDepth - recursion + 1),
                SecondAngle, 1, recursion);
            CreateIterrationOfFractal(
                points[1],
                LengthOfSegment * Math.Pow(Relation, RecursionDepth - recursion + 1),
                FirstAngle, -1, recursion);
        }


        private void CreateIterrationOfFractal(Point startPoint, double segmentLength, double angleOfVector,
            int directionOfCreation, int recursion)
        {
            s_nowAngle += angleOfVector * directionOfCreation;
            CreateFractal(
                new Point[]
                {
                    startPoint, new Point(
                        (float) (startPoint.X - directionOfCreation *
                            (segmentLength * Math.Sin(s_nowAngle * directionOfCreation))),
                        (float) (startPoint.Y - (segmentLength * Math.Cos(s_nowAngle * directionOfCreation))))
                },
                recursion - 1);
            s_nowAngle -= angleOfVector * directionOfCreation;
        }

    }

    class Snowflake : Fractal
    {
        private const float SegmentFraction = 3;

        private const double SnowflakeRotateAngle = Math.PI / 3;

        public Snowflake(float segmentLength, ushort recursionDepth, Color startColor, Color endColor)
            : base(segmentLength, recursionDepth, startColor, endColor)
        {
        }

        public override void CreateFractal(Point[] points, int recursionLevel)
        {
            float displacementSegmentX = (points[1].X - points[0].X) / SegmentFraction;
            float displacementSegmentY = (points[1].Y - points[0].Y) / SegmentFraction;

            WorkWithPoints(points, displacementSegmentX, displacementSegmentY, out var pointOfFirstSegment,
                out var pointOfThridSegment, out var pointOfSecondSegment);
            if (recursionLevel == RecursionDepth)
                base.CreateFractal(new Point[] {points[0], points[1]}, recursionLevel);
            if (recursionLevel > 1)
            {
                CreateSomeFractals(points, recursionLevel, pointOfFirstSegment,
                    pointOfSecondSegment, pointOfThridSegment);
            }
        }

        private void CreateSomeFractals(Point[] points, int recursionLevel, Point pointOfFirstSegment,
            Point pointOfSecondSegment, Point pointOfThridSegment)
        {
            base.CreateFractal(new Point[] {pointOfFirstSegment, pointOfSecondSegment}, recursionLevel - 1);
            base.CreateFractal(new Point[] {pointOfSecondSegment, pointOfThridSegment}, recursionLevel - 1);
            GraphicsFractal.DrawLine(new Pen(Color.White, 2f), pointOfFirstSegment, pointOfThridSegment);
            CreateFractal(new Point[] {points[0], pointOfFirstSegment}, recursionLevel - 1);
            CreateFractal(new Point[] {pointOfFirstSegment, pointOfSecondSegment}, recursionLevel - 1);
            CreateFractal(new Point[] {pointOfSecondSegment, pointOfThridSegment}, recursionLevel - 1);
            CreateFractal(new Point[] {pointOfThridSegment, points[1]}, recursionLevel - 1);
        }

        private static void WorkWithPoints(Point[] points, float displacementSegmentX, float displacementSegmentY,
            out Point pointOfFirstSegment, out Point pointOfThridSegment, out Point pointOfSecondSegment)
        {
            pointOfFirstSegment = new Point(points[0].X + displacementSegmentX, points[0].Y + displacementSegmentY);
            pointOfThridSegment = new Point(points[0].X + displacementSegmentX * 2,
                points[0].Y + displacementSegmentY * 2);
            float platformX = (float) ((pointOfThridSegment.X - pointOfFirstSegment.X) * Math.Cos(SnowflakeRotateAngle)
                                       + (pointOfThridSegment.Y - pointOfFirstSegment.Y) *
                                       Math.Sin(SnowflakeRotateAngle));
            float platformY = (float) ((pointOfThridSegment.Y - pointOfFirstSegment.Y) * Math.Cos(SnowflakeRotateAngle)
                                       - (pointOfThridSegment.X - pointOfFirstSegment.X) *
                                       Math.Sin(SnowflakeRotateAngle));
            pointOfSecondSegment = new Point(pointOfFirstSegment.X + platformX, pointOfFirstSegment.Y + platformY);
        }
    }

    class Carpet : Fractal
    {

        private int s_nowColor;

        public Carpet(float segmentLength, ushort recursionDepth, Color startColor, Color endColor)
            : base(segmentLength, recursionDepth, startColor, endColor)
        {
        }

        public override void CreateFractal(Point[] points, int recursionLevel)
        {
            float currentSegmentLength = (float) LengthOfSegment / (float) Math.Pow(3, RecursionDepth - recursionLevel);
            if (recursionLevel == RecursionDepth)
            {
                s_nowColor = 1;
                CreateNewGradient((int) Math.Pow(8, RecursionDepth));
            }
            if (recursionLevel == 0)
            {
                Point endPoint = new Point(points[0].X + currentSegmentLength, points[0].Y + currentSegmentLength);
                base.CreateFractal(new Point[] {points[0], endPoint}, RecursionDepth == 1 ? 1 : s_nowColor++);
                return;
            }
            WorkWithPoints(points, currentSegmentLength, out var startPoint, out var midPoint, out var endingPoint);
            CreateSomeFractals(recursionLevel, startPoint, midPoint, endingPoint);
        }

        private void CreateSomeFractals(int recursionLevel, Point startPoint, Point midPoint, Point endingPoint)
        {
            CreateFractal(new Point[] {new Point(startPoint.X, startPoint.Y)}, recursionLevel - 1);
            CreateFractal(new Point[] {new Point(midPoint.X, startPoint.Y)}, recursionLevel - 1);
            CreateFractal(new Point[] {new Point(endingPoint.X, startPoint.Y)}, recursionLevel - 1);
            CreateFractal(new Point[] {new Point(startPoint.X, midPoint.Y)}, recursionLevel - 1);
            CreateFractal(new Point[] {new Point(endingPoint.X, midPoint.Y)}, recursionLevel - 1);
            CreateFractal(new Point[] {new Point(startPoint.X, endingPoint.Y)}, recursionLevel - 1);
            CreateFractal(new Point[] {new Point(midPoint.X, endingPoint.Y)}, recursionLevel - 1);
            CreateFractal(new Point[] {new Point(endingPoint.X, endingPoint.Y)}, recursionLevel - 1);
        }

        private static void WorkWithPoints(Point[] points, float currentSegmentLength, out Point startPoint,
            out Point midPoint, out Point endingPoint)
        {
            startPoint = new Point(points[0].X, points[0].Y);
            midPoint = new Point(startPoint.X + currentSegmentLength / 3,
                startPoint.Y + currentSegmentLength / 3);
            endingPoint = new Point(startPoint.X + currentSegmentLength * 2 / 3,
                startPoint.Y + currentSegmentLength * 2 / 3);
        }
    }

    class Triangle : Fractal
    {

        public Triangle(float segmentLength, ushort recursionDepth, Color startColor, Color endColor)
            : base(segmentLength, recursionDepth, startColor, endColor) { }


        public override void CreateFractal(Point[] points, int recursionLevel)
        {
            if (recursionLevel == 0)
            {
                return;
            }
            WorkWithPoints(points, out var bottom, out var left, out var right);
            CreateSomeFractals(points, recursionLevel, bottom, left, right);
        }

        private void CreateSomeFractals(Point[] points, int recursionLevel, Point bottom, Point left,
            Point right)
        {
            CreateFractal(new Point[] {points[0], bottom, left}, recursionLevel - 1);
            CreateFractal(new Point[] {bottom, points[1], right}, recursionLevel - 1);
            CreateFractal(new Point[] {left, right, points[2]}, recursionLevel - 1);

            for (int i = 0; i <= points.Length - 1; i++)
            {
                base.CreateFractal(new Point[] {points[i % points.Length],
                    points[(i + 1) % points.Length]}, recursionLevel);
            }
        }

        private static void WorkWithPoints(Point[] points, out Point bottom, out Point left, out Point right)
        {
            bottom = new Point((points[0].X + points[1].X) / 2, (points[0].Y + points[1].Y) / 2);
            left = new Point((points[0].X + points[2].X) / 2, (points[0].Y + points[2].Y) / 2);
            right = new Point((points[1].X + points[2].X) / 2, (points[1].Y + points[2].Y) / 2);
        }
    }

    class Set : Fractal
    {
        private float s_sideLength;

        private float s_betweenDistance;

        public Set(float segmentLength, ushort recursionDepth, Color startColor, Color endColor, float height, float distance)
            : base(segmentLength, recursionDepth, startColor, endColor)
        {
            s_sideLength = height;
            s_betweenDistance = distance;
        }

        public override void CreateFractal(Point[] points, int recursionLevel)
        {
            if (recursionLevel == 0)
            {
                return;
            }
            float currentSegmentLength = (float)LengthOfSegment / (float)Math.Pow(3, RecursionDepth - recursionLevel);

            var endPoint = new Point(points[0].X + currentSegmentLength, points[0].Y + s_sideLength);

            base.CreateFractal(new Point[] { points[0], endPoint }, recursionLevel);

            CreateFractal(new Point[] { new Point(points[0].X, 
                points[0].Y + s_betweenDistance + s_sideLength) }, recursionLevel - 1);
            CreateFractal(new Point[] { new Point(points[0].X + currentSegmentLength * 2 / 3,
                points[0].Y + s_betweenDistance + s_sideLength) }, recursionLevel - 1);
        }

    }
}

