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

        /// <summary>
        /// Своство длины начального отрезка фрактала.
        /// </summary>
        public double LengthOfSegment
        {
            get => s_lenghtOfSegment;
            private set => s_lenghtOfSegment = value;
        }

        /// <summary>
        /// Cвойство глубины рекурсии.
        /// </summary>
        public ushort RecursionDepth
        {
            get => s_depthOfRecursion;
            set => s_depthOfRecursion = value;
        }

        /// <summary>
        /// Свойство битовой карты фрактала
        /// </summary>
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
        /// Метод создания фрактала.
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        public virtual void CreateFractal(Point[] points, int recursion)
        {
            GraphicsFractal = Graphics.FromImage(FractalBitmap);
            // В зависимости от типа фрактала устанавливаем начальную фигуру для создания фрактала.
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

    /// <summary>
    /// Класс Фрактального дерева, содержащий методы для его прорисовки.
    /// </summary>
    class Tree : Fractal
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

            foreach (var point in points)
            {

                s_startPoint.X = (point.X < s_startPoint.X || s_startPoint.X == -1) ? point.X : s_startPoint.X;

                s_endPoint.X = (point.X > s_endPoint.X || s_endPoint.X == -1) ? point.X : s_endPoint.X;

                s_startPoint.Y = (point.Y < s_startPoint.Y || s_startPoint.Y == -1) ? point.Y : s_startPoint.Y;

                s_endPoint.Y = (point.Y > s_endPoint.Y || s_endPoint.Y == -1) ? point.Y : s_endPoint.Y;
            }
        }

        private void CreateSomeFractals(Point[] points, int recursion)
        {
            double segmentLength = LengthOfSegment * Math.Pow(Relation, RecursionDepth - recursion + 1);
            CreateIterrationOfFractal(points[1], segmentLength, SecondAngle, 1, recursion);
            CreateIterrationOfFractal(points[1], segmentLength, FirstAngle, -1, recursion);
        }


        private void CreateIterrationOfFractal(Point startPoint, double segmentLength, double angleOfVector,
            int treeDirection, int recursion)
        {
            s_nowAngle += angleOfVector * treeDirection;

            float endPointX = (float) (startPoint.X - treeDirection * (segmentLength * Math.Sin(s_nowAngle * treeDirection)));
            float endPointY = (float) (startPoint.Y - (segmentLength * Math.Cos(s_nowAngle * treeDirection)));

            Point endPoint = new Point(endPointX, endPointY);

            CreateFractal(
                new Point[] {startPoint, endPoint},
                recursion - 1);

            s_nowAngle -= angleOfVector * treeDirection;
        }

    }

    /// <summary>
    /// Класс кривой Коха (снежинки), содержащий методы для её прорисовки.
    /// </summary>
    class Snowflake : Fractal
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
        {}

        /// <summary>
        /// Метод создания фрактала Кривая Коха (снежинки).
        /// </summary>
        /// <param name="points">Начальные точки фрактала.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        public override void CreateFractal(Point[] points, int recursion)
        {
            float displacementSegmentX = (points[1].X - points[0].X) / 3;
            float displacementSegmentY = (points[1].Y - points[0].Y) / 3;

            WorkWithPoints(
                points,
                displacementSegmentX,
                displacementSegmentY,
                out var pointOfFirstSegment,
                out var pointOfThridSegment,
                out var pointOfSecondSegment);

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

        private void CreateSomeFractals(Point[] points, int recursion, Point pointOfFirstSegment,
            Point pointOfSecondSegment, Point pointOfThridSegment)
        {
            base.CreateFractal(new Point[] {pointOfFirstSegment, pointOfSecondSegment}, recursion - 1);
            base.CreateFractal(new Point[] {pointOfSecondSegment, pointOfThridSegment}, recursion - 1);

            GraphicsFractal.DrawLine(new Pen(Color.White, 2f), pointOfFirstSegment, pointOfThridSegment);

            CreateFractal(new Point[] {points[0], pointOfFirstSegment}, recursion - 1);
            CreateFractal(new Point[] {pointOfFirstSegment, pointOfSecondSegment}, recursion - 1);
            CreateFractal(new Point[] {pointOfSecondSegment, pointOfThridSegment}, recursion - 1);
            CreateFractal(new Point[] {pointOfThridSegment, points[1]}, recursion - 1);
        }

        private static void WorkWithPoints(Point[] points, float displacementSegmentX, float displacementSegmentY,
            out Point pointOfFirstSegment, out Point pointOfThridSegment, out Point pointOfSecondSegment)
        {
            pointOfFirstSegment = new Point(
                points[0].X + displacementSegmentX, 
                points[0].Y + displacementSegmentY);

            pointOfThridSegment = new Point(
                points[0].X + displacementSegmentX * 2,
                points[0].Y + displacementSegmentY * 2);

            float platformX = (float) (
                (pointOfThridSegment.X - pointOfFirstSegment.X) * Math.Cos(Math.PI / 3)
                + (pointOfThridSegment.Y - pointOfFirstSegment.Y) * Math.Sin(Math.PI / 3));

            float platformY = (float) (
                (pointOfThridSegment.Y - pointOfFirstSegment.Y) * Math.Cos(Math.PI / 3)
                - (pointOfThridSegment.X - pointOfFirstSegment.X) * Math.Sin(Math.PI / 3));

            pointOfSecondSegment = new Point(
                pointOfFirstSegment.X + platformX,
                pointOfFirstSegment.Y + platformY);
        }
    }

    /// <summary>
    /// Класс Ковра Серпинского, содержащий методы для его прорисовки.
    /// </summary>
    class Carpet : Fractal
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
            
            float currentSegmentLength = (float) LengthOfSegment / (float) Math.Pow(3, RecursionDepth - recursion);

            if (recursion == RecursionDepth)
            {
                s_nowControlColor = 1;
                CreateNewGradient((int) Math.Pow(8, RecursionDepth));
            }
            if (recursion == 0)
            {
                CreateZeroRecursionFractal(points, currentSegmentLength);
                return;
            }

            WorkWithPoints(
                points,
                currentSegmentLength,
                out var startPoint,
                out var midPoint,
                out var endingPoint);

            CreateSomeFractals(
                recursion,
                startPoint,
                midPoint,
                endingPoint);
        }

        private void CreateZeroRecursionFractal(PointF[] points, float currentSegmentLength)
        {
            Point endPoint = new Point(
                points[0].X + currentSegmentLength,
                points[0].Y + currentSegmentLength);

            base.CreateFractal(
                new Point[] {points[0], endPoint},
                RecursionDepth == 1 ? 1 : s_nowControlColor++);
        }

        private void CreateSomeFractals(int recursion, Point startPoint, Point midPoint, Point endingPoint)
        {
            CreateFractal(new Point[] {new Point(startPoint.X, startPoint.Y)}, recursion - 1);
            CreateFractal(new Point[] {new Point(midPoint.X, startPoint.Y)}, recursion - 1);
            CreateFractal(new Point[] {new Point(endingPoint.X, startPoint.Y)}, recursion - 1);
            CreateFractal(new Point[] {new Point(startPoint.X, midPoint.Y)}, recursion - 1);
            CreateFractal(new Point[] {new Point(endingPoint.X, midPoint.Y)}, recursion - 1);
            CreateFractal(new Point[] {new Point(startPoint.X, endingPoint.Y)}, recursion - 1);
            CreateFractal(new Point[] {new Point(midPoint.X, endingPoint.Y)}, recursion - 1);
            CreateFractal(new Point[] {new Point(endingPoint.X, endingPoint.Y)}, recursion - 1);
        }

        private static void WorkWithPoints(Point[] points, float currentSegmentLength, out Point startPoint,
            out Point midPoint, out Point endingPoint)
        {
            startPoint = new Point(points[0].X, points[0].Y);

            midPoint = new Point(
                startPoint.X + currentSegmentLength / 3,
                startPoint.Y + currentSegmentLength / 3);

            endingPoint = new Point(
                startPoint.X + currentSegmentLength * 2 / 3,
                startPoint.Y + currentSegmentLength * 2 / 3);
        }
    }

    /// <summary>
    /// Класс Треугольника Серпинского, содержащий методы для его прорисовки.
    /// </summary>
    class Triangle : Fractal
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
            if (recursion == 0)
            {
                return;
            }

            WorkWithPoints(
                points, 
                out var bottom,
                out var left,
                out var right);

            CreateSomeFractals(
                points,
                recursion,
                bottom,
                left,
                right);
        }

        private void CreateSomeFractals(Point[] points, int recursion, Point bottom, Point left,
            Point right)
        {
            CreateFractal(new Point[] {points[0], bottom, left}, recursion - 1);
            CreateFractal(new Point[] {bottom, points[1], right}, recursion - 1);
            CreateFractal(new Point[] {left, right, points[2]}, recursion - 1);

            for (int i = 0; i <= points.Length - 1; i++)
            {
                base.CreateFractal(
                    new Point[] {points[i % points.Length],
                    points[(i + 1) % points.Length]}, recursion);
            }
        }

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
    class Set : Fractal
    {
        // Поле для хранения длины одной стороны.
        private float s_sideLength;
        // поле для хранения расстояния между ячейками множества.
        private float s_betweenDistance;

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
            if (recursion == 0)
            {
                return;
            }

            float currentSegmentLength = (float)LengthOfSegment / (float)Math.Pow(3, RecursionDepth - recursion);

            WorkWithPoints(points, currentSegmentLength, out var endPoint);
            CreateSomeFractals(points, recursion, endPoint, currentSegmentLength);
        }

        private void WorkWithPoints(PointF[] points, float currentSegmentLength, out PointF endPoint)
        {
            endPoint = new Point(
                points[0].X + currentSegmentLength,
                points[0].Y + s_sideLength);
        }

        private void CreateSomeFractals(PointF[] points, int recursion, PointF endPoint, float currentSegmentLength)
        {
            base.CreateFractal(
                new Point[] {points[0], endPoint},
                recursion);

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

