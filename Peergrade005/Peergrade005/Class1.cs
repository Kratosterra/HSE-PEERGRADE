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
        protected Graphics Graphics;

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

            if (RecursionDepth == 1)
            {
                colorsGradient[0] = startColor;
                return;
            }

            for (int i = 0; i < recursionDepth; i++)
            {
                colorsGradient[i] = Color.FromArgb(
                    endColor.R + (int)((startColor.R - endColor.R) * i / (double)recursionDepth),
                    endColor.G + (int)((startColor.G - endColor.G) * i / (double)recursionDepth),
                    endColor.B + (int)((startColor.B - endColor.B) * i / (double)recursionDepth));
            }
        }

        public virtual void CreateFractal(PointF[] points, int recursionLevel)
        {
            Graphics = Graphics.FromImage(FractalBitmap);

            if (this is Tree)
            {
                Pen nowPen = new Pen(colorsGradient[recursionLevel - 1], 2f);
                Graphics.DrawLine(nowPen, points[0], points[1]);
            }
            else
            {
                RectangleF rectangle = new RectangleF(points[0],
                    new SizeF(points[1].X - points[0].X, points[1].Y - points[0].Y)
                    );
                SolidBrush solidBrush = new SolidBrush(colorsGradient[recursionLevel - 1]);
                Graphics.FillRectangle(solidBrush, rectangle);
            }
        }
    }

    class Tree:Fractal
    {
        private double nowAngle;
        private double firstAngle;
        private double secondAngle;

        private PointF startPoint;
        private PointF endPoint;

        private double Coefficient { get; set; }

        private double RightAngle
        {
            get => firstAngle;
            set => firstAngle = value * Math.PI / 180;
        }

        private double LeftAngle
        {
            get => secondAngle;
            set => secondAngle = value * Math.PI / 180;
        }


        public Tree(float segmentLength, ushort recursionDepth, Color startColor, Color endColor,
            double rightAngle, double leftAngle, double coefficient) : base(segmentLength, recursionDepth, startColor, endColor)
        {
            RightAngle = rightAngle;
            LeftAngle = leftAngle;
            Coefficient = coefficient;
        }


        public override void CreateFractal(PointF[] points, int recursionLevel)
        {
            if (recursionLevel == 0) return;
            base.CreateFractal(points, recursionLevel);
            if (recursionLevel == RecursionDepth)
            {
                startPoint = new PointF(-1, -1);
                endPoint = new PointF(-1, -1);
            }
            foreach (PointF point in points)
            {
                if (point.X < startPoint.X || startPoint.X == -1) startPoint.X = point.X;
                if (point.X > endPoint.X || endPoint.X == -1) endPoint.X = point.X;
                if (point.Y < startPoint.Y || startPoint.Y == -1) startPoint.Y = point.Y;
                if (point.Y > endPoint.Y || endPoint.Y == -1) endPoint.Y = point.Y;
            }
            points[0] = points[1];
            CreateOneSide(points[1],
                LengthOfSegment * Math.Pow(Coefficient, RecursionDepth - recursionLevel + 1), LeftAngle, 1, recursionLevel);
            CreateOneSide(points[1],
                LengthOfSegment * Math.Pow(Coefficient, RecursionDepth - recursionLevel + 1), RightAngle, -1, recursionLevel);
        }


        private void CreateOneSide(PointF startPoint, double segmentLength, double angle, int direction, int recursionLevel)
        {
            nowAngle += angle * direction;

            CreateFractal(
                new PointF[] { startPoint, new PointF(
                    (float)(startPoint.X - direction * (segmentLength * Math.Sin(nowAngle * direction))),
                    (float)(startPoint.Y - (segmentLength * Math.Cos(nowAngle * direction)))) },
                recursionLevel - 1);

            nowAngle -= angle * direction;
        }

    }
}

