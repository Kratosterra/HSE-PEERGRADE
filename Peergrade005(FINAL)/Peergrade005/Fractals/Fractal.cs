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
            s_startColor = startColor;
            s_endColor = endColor;
            CreateNewGradient(RecursionDepth);
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

}

