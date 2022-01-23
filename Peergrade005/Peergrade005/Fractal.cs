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
using Point = System.Drawing.Point;


namespace Peergrade005
{
    class Fractal
    {
        private double lenghtOfSegment;
        private ushort depthOfRecursion;
        protected Graphics GraphicsFractal;

        private Color startColor;
        private Color endColor;
        private Color[] colorsGradient;

        public double LengthOfSegment
        {
            get => lenghtOfSegment;
            private set => lenghtOfSegment = value;
        }

        public ushort RecursionDepth
        {
            get => depthOfRecursion;
            set
            {
                depthOfRecursion = value;
                CreateNewGradient(RecursionDepth);
            }
        }

        public Bitmap FractalBitmap { get; set; }

        public Fractal(double segmentLength, ushort recursionDepth, Color startColor, Color endColor)
        {
            LengthOfSegment = segmentLength;
            RecursionDepth = recursionDepth;
            this.startColor = startColor;
            this.endColor = endColor;
            CreateNewGradient(RecursionDepth);
        }

        protected void CreateNewGradient(int recursionDepth)
        {
            colorsGradient = new Color[recursionDepth];
            Tools.ExecuteColorsGradient(recursionDepth, ref colorsGradient, startColor, endColor);
        }

        public virtual void CreateFractal(PointF[] points, int recursion)
        {
            GraphicsFractal = Graphics.FromImage(FractalBitmap);

            if (this is Tree || this is Snowflake || this is Triangle)
            {
                Tools.CreateLine(points, recursion, ref GraphicsFractal, colorsGradient);
            }
            else
            {
                Tools.CreateRectangle(points, recursion, ref GraphicsFractal, colorsGradient);
            }
        }


    }

    class Tree : Fractal
    {
        private double nowAngle;
        private double firstAngle;
        private double secondAngle;

        private PointF startPoint;
        private PointF endPoint;

        private double Relation { get; set; }

        private double FirstAngle
        {
            get => firstAngle;
            set => firstAngle = value * Math.PI / 180;
        }

        private double SecondAngle
        {
            get => secondAngle;
            set => secondAngle = value * Math.PI / 180;
        }


        public Tree(float segmentLength, ushort recursionDepth, Color startColor, Color endColor,
            double firstAngle, double secondAngle, double relation) : base(segmentLength, recursionDepth, startColor,
            endColor)
        {
            FirstAngle = firstAngle;
            SecondAngle = secondAngle;
            Relation = relation;
        }


        public override void CreateFractal(PointF[] points, int recursion)
        {
            if (recursion == 0) return;
            base.CreateFractal(points, recursion);
            WorkWithPoints(points, recursion);
            points[0] = points[1];
            CreateSomeFractals(points, recursion);
        }

        private void WorkWithPoints(PointF[] points, int recursion)
        {
            if (recursion == RecursionDepth)
            {
                startPoint = new PointF(-1, -1);
                endPoint = new PointF(-1, -1);
            }

            foreach (PointF point in points)
            {
                startPoint.X = (point.X < startPoint.X || startPoint.X == -1) ? point.X : startPoint.X;
                endPoint.X = (point.X > endPoint.X || endPoint.X == -1) ? point.X : endPoint.X;
                startPoint.Y = (point.Y < startPoint.Y || startPoint.Y == -1) ? point.Y : startPoint.Y;
                endPoint.Y = (point.Y > endPoint.Y || endPoint.Y == -1) ? point.Y : endPoint.Y;
            }
        }

        private void CreateSomeFractals(PointF[] points, int recursion)
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


        private void CreateIterrationOfFractal(PointF startPoint, double segmentLength, double angleOfVector,
            int directionOfCreation, int recursion)
        {
            nowAngle += angleOfVector * directionOfCreation;

            CreateFractal(
                new PointF[]
                {
                    startPoint, new PointF(
                        (float) (startPoint.X - directionOfCreation *
                            (segmentLength * Math.Sin(nowAngle * directionOfCreation))),
                        (float) (startPoint.Y - (segmentLength * Math.Cos(nowAngle * directionOfCreation))))
                },
                recursion - 1);

            nowAngle -= angleOfVector * directionOfCreation;
        }

    }

    class Snowflake : Fractal
    {
        private const float segmentFraction = 3;

        private const double snowflakeRotateAngle = Math.PI / 3;

        public Snowflake(float segmentLength, ushort recursionDepth, Color startColor, Color endColor)
            : base(segmentLength, recursionDepth, startColor, endColor)
        {
        }

        public override void CreateFractal(PointF[] points, int recursionLevel)
        {
            float displacementSegmentX = (points[1].X - points[0].X) / segmentFraction;
            float displacementSegmentY = (points[1].Y - points[0].Y) / segmentFraction;

            WorkWithPoints(points, displacementSegmentX, displacementSegmentY, out var pointOfFirstSegment,
                out var pointOfThridSegment, out var pointOfSecondSegment);
            if (recursionLevel == RecursionDepth)
                base.CreateFractal(new PointF[] {points[0], points[1]}, recursionLevel);
            if (recursionLevel > 1)
            {
                CreateSomeFractals(points, recursionLevel, pointOfFirstSegment, pointOfSecondSegment, pointOfThridSegment);
            }
        }

        private void CreateSomeFractals(PointF[] points, int recursionLevel, PointF pointOfFirstSegment,
            PointF pointOfSecondSegment, PointF pointOfThridSegment)
        {
            base.CreateFractal(new PointF[] {pointOfFirstSegment, pointOfSecondSegment}, recursionLevel - 1);
            base.CreateFractal(new PointF[] {pointOfSecondSegment, pointOfThridSegment}, recursionLevel - 1);
            GraphicsFractal.DrawLine(new Pen(Color.White, 2f), pointOfFirstSegment, pointOfThridSegment);
            CreateFractal(new PointF[] {points[0], pointOfFirstSegment}, recursionLevel - 1);
            CreateFractal(new PointF[] {pointOfFirstSegment, pointOfSecondSegment}, recursionLevel - 1);
            CreateFractal(new PointF[] {pointOfSecondSegment, pointOfThridSegment}, recursionLevel - 1);
            CreateFractal(new PointF[] {pointOfThridSegment, points[1]}, recursionLevel - 1);
        }

        private static void WorkWithPoints(PointF[] points, float displacementSegmentX, float displacementSegmentY,
            out PointF pointOfFirstSegment, out PointF pointOfThridSegment, out PointF pointOfSecondSegment)
        {
            pointOfFirstSegment = new PointF(points[0].X + displacementSegmentX, points[0].Y + displacementSegmentY);
            pointOfThridSegment = new PointF(points[0].X + displacementSegmentX * 2,
                points[0].Y + displacementSegmentY * 2);

            float platformX = (float) ((pointOfThridSegment.X - pointOfFirstSegment.X) * Math.Cos(snowflakeRotateAngle)
                                       + (pointOfThridSegment.Y - pointOfFirstSegment.Y) *
                                       Math.Sin(snowflakeRotateAngle));
            float platformY = (float) ((pointOfThridSegment.Y - pointOfFirstSegment.Y) * Math.Cos(snowflakeRotateAngle)
                                       - (pointOfThridSegment.X - pointOfFirstSegment.X) *
                                       Math.Sin(snowflakeRotateAngle));

            pointOfSecondSegment = new PointF(pointOfFirstSegment.X + platformX, pointOfFirstSegment.Y + platformY);
        }
    }

    class Carpet : Fractal
    {

        private int nowColor;

        public Carpet(float segmentLength, ushort recursionDepth, Color startColor, Color endColor)
            : base(segmentLength, recursionDepth, startColor, endColor)
        {
        }

        public override void CreateFractal(PointF[] points, int recursionLevel)
        {
            float currentSegmentLength = (float) LengthOfSegment / (float) Math.Pow(3, RecursionDepth - recursionLevel);
            if (recursionLevel == RecursionDepth)
            {
                nowColor = 1;
                CreateNewGradient((int) Math.Pow(8, RecursionDepth));
            }
            if (recursionLevel == 0)
            {
                PointF endPoint = new PointF(points[0].X + currentSegmentLength, points[0].Y + currentSegmentLength);
                base.CreateFractal(new PointF[] {points[0], endPoint}, RecursionDepth == 1 ? 1 : nowColor++);
                return;
            }
            WorkWithPoints(points, currentSegmentLength, out var startPoint, out var midPoint, out var endingPoint);
            CreateSomeFractals(recursionLevel, startPoint, midPoint, endingPoint);
        }

        private void CreateSomeFractals(int recursionLevel, PointF startPoint, PointF midPoint, PointF endingPoint)
        {
            CreateFractal(new PointF[] {new PointF(startPoint.X, startPoint.Y)}, recursionLevel - 1);
            CreateFractal(new PointF[] {new PointF(midPoint.X, startPoint.Y)}, recursionLevel - 1);
            CreateFractal(new PointF[] {new PointF(endingPoint.X, startPoint.Y)}, recursionLevel - 1);
            CreateFractal(new PointF[] {new PointF(startPoint.X, midPoint.Y)}, recursionLevel - 1);
            CreateFractal(new PointF[] {new PointF(endingPoint.X, midPoint.Y)}, recursionLevel - 1);
            CreateFractal(new PointF[] {new PointF(startPoint.X, endingPoint.Y)}, recursionLevel - 1);
            CreateFractal(new PointF[] {new PointF(midPoint.X, endingPoint.Y)}, recursionLevel - 1);
            CreateFractal(new PointF[] {new PointF(endingPoint.X, endingPoint.Y)}, recursionLevel - 1);
        }

        private static void WorkWithPoints(PointF[] points, float currentSegmentLength, out PointF startPoint,
            out PointF midPoint, out PointF endingPoint)
        {
            startPoint = new PointF(points[0].X, points[0].Y);
            midPoint = new PointF(startPoint.X + currentSegmentLength / 3,
                startPoint.Y + currentSegmentLength / 3);
            endingPoint = new PointF(startPoint.X + currentSegmentLength * 2 / 3,
                startPoint.Y + currentSegmentLength * 2 / 3);
        }
    }

    class Triangle : Fractal
    {

        public Triangle(float segmentLength, ushort recursionDepth, Color startColor, Color endColor)
            : base(segmentLength, recursionDepth, startColor, endColor) { }


        public override void CreateFractal(PointF[] points, int recursionLevel)
        {
            if (recursionLevel == 0)
            {
                return;
            }
            WorkWithPoints(points, out var bottomApex, out var leftApex, out var rightApex);
            CreateSomeFractals(points, recursionLevel, bottomApex, leftApex, rightApex);
        }

        private void CreateSomeFractals(PointF[] points, int recursionLevel, PointF bottomApex, PointF leftApex,
            PointF rightApex)
        {
            CreateFractal(new PointF[] {points[0], bottomApex, leftApex}, recursionLevel - 1);
            CreateFractal(new PointF[] {bottomApex, points[1], rightApex}, recursionLevel - 1);
            CreateFractal(new PointF[] {leftApex, rightApex, points[2]}, recursionLevel - 1);

            for (int i = 0; i <= points.Length - 1; i++)
            {
                base.CreateFractal(new PointF[] {points[i % points.Length], points[(i + 1) % points.Length]}, recursionLevel);
            }
        }

        private static void WorkWithPoints(PointF[] points, out PointF bottomApex, out PointF leftApex, out PointF rightApex)
        {
            bottomApex = new PointF((points[0].X + points[1].X) / 2, (points[0].Y + points[1].Y) / 2);
            leftApex = new PointF((points[0].X + points[2].X) / 2, (points[0].Y + points[2].Y) / 2);
            rightApex = new PointF((points[1].X + points[2].X) / 2, (points[1].Y + points[2].Y) / 2);
        }
    }

    class Set : Fractal
    {
        private float sideLength;

        private float betweenDistance;


        public float Height
        {
            get => sideLength;
            set
            {
                sideLength = value;
            }
        }

        public float Distance
        {
            get => betweenDistance;
            set
            {
                betweenDistance = value;
            }
        }

        public Set(float segmentLength, ushort recursionDepth, Color startColor, Color endColor, float height, float distance)
            : base(segmentLength, recursionDepth, startColor, endColor)
        {
            Height = height;
            Distance = distance;
        }

        public override void CreateFractal(PointF[] points, int recursionLevel)
        {
            if (recursionLevel == 0)
            {
                return;
            }
            float currentSegmentLength = (float)LengthOfSegment / (float)Math.Pow(3, RecursionDepth - recursionLevel);

            PointF endPoint = new PointF(points[0].X + currentSegmentLength, points[0].Y + Height);

            base.CreateFractal(new PointF[] { points[0], endPoint }, recursionLevel);

            CreateFractal(new PointF[] { new PointF(points[0].X, points[0].Y + Distance + Height) }, recursionLevel - 1);
            CreateFractal(new PointF[] { new PointF(points[0].X + currentSegmentLength * 2 / 3, points[0].Y + Distance + Height) }, recursionLevel - 1);
        }

    }
}

