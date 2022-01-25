using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;
using Point = System.Drawing.PointF;

namespace Peergrade005
{
    /// <summary>
    /// Класс, содержащий вспомогательные методы.
    /// </summary>
    class Tools
    {
        /// <summary>
        /// Метод, сохраняющий фрактал без папки сохранения в формате .png.
        /// </summary>
        /// <param name="fractal">Фрактал.</param>
        /// <param name="nowFractal">Номер, репрезентующий тип фрактала.</param>
        /// <param name="typesFractal">Массив названий фракталов.</param>
        public static void SaveWithNoDictionary(Fractal fractal, int nowFractal, string[] typesFractal)
        {
            MessageBox.Show($"Папка для сохранений не была создана или была удалена!{Environment.NewLine}" +
                            $"Будет произведена попытка сохранения фрактала в родительской папке",
                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            try
            {
                // Сохраняем фрактал.
                fractal.FractalBitmap.Save($"{typesFractal[nowFractal]}_" +
                                           $"({DateTime.Now.Day}_{DateTime.Now.Month})_" +
                                           $"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.png");
                MessageBox.Show($"Файл удалось сохранить!" +
                                $"{Environment.NewLine}Файл находится внутри родительской папки!",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сохранить файл",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод, создающий начальную линию для создания фрактала.
        /// </summary>
        /// <param name="points">Точки.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        /// <param name="graphics">Графика для создания фрактала.</param>
        /// <param name="colorsGradient">Массив цветов градиента.</param>
        public static void CreateLine(Point[] points, int recursion, ref Graphics graphics, Color[] colorsGradient)
        {
            // Задаём ручку с цветом, получаемым из итерации рекурсии.
            Pen nowPen = new Pen(colorsGradient[recursion - 1], 2f);
            // Рисуем линию.
            graphics.DrawLine(nowPen, points[0], points[1]);
        }

        /// <summary>
        /// Метод, создающий треугольник для создания фрактала.
        /// </summary>
        /// <param name="points">Точки.</param>
        /// <param name="recursion">Глубина рекурсии.</param>
        /// <param name="graphics">Графика для создания фрактала.</param>
        /// <param name="colorsGradient">Массив цветов градиента.</param>
        public static void CreateRectangle(Point[] points, int recursion, ref Graphics graphics,
            Color[] colorsGradient)
        {
            // Cоздаём треугольник.
            RectangleF rectangle = new RectangleF(
                points[0],
                new SizeF(points[1].X - points[0].X, points[1].Y - points[0].Y));
            // Получаем кисточку с цветом.
            SolidBrush solidBrush = new SolidBrush(colorsGradient[recursion - 1]);
            // Закрашиваем треугольник.
            graphics.FillRectangle(solidBrush, rectangle);
        }

        /// <summary>
        /// Метод, для создания градиента из двух цветов.
        /// </summary>
        /// <param name="recursionDepth">Глубина рекурсии.</param>
        /// <param name="startColor">Начальный цвет.</param>
        /// <param name="endColor">Конечный цвет.</param>
        /// <param name="colorsGradient">Массив цветов для градиента.</param>
        public static void ExecuteColorsGradient(int recursionDepth, ref Color[] colorsGradient,
            Color startColor, Color endColor)
        {
            // Если глубина рекурсии 1, то начальный цвет и будет всем градиентом.
            if (recursionDepth == 1)
            {
                colorsGradient[0] = startColor;
                return;
            }
            // Иначе производим классическое вычисление градиента по двум цветам.
            for (int i = 0; i < recursionDepth; i++)
            {
                colorsGradient[i] = Color.FromArgb(
                    endColor.R + (int)((startColor.R - endColor.R) * i / (double)recursionDepth),
                    endColor.G + (int)((startColor.G - endColor.G) * i / (double)recursionDepth),
                    endColor.B + (int)((startColor.B - endColor.B) * i / (double)recursionDepth));
            }
        }

        /// <summary>
        /// Метод, производящий сохранение фрактала в папке Saves в формате .png.
        /// </summary>
        /// <param name="fractal">Фрактал.</param>
        /// <param name="nowFractal">Число, репрезентующее тип фрактала.</param>
        /// <param name="typesFractal">Массив названий фрактала.</param>
        public static void ClearSave(Fractal fractal, int nowFractal, string[] typesFractal)
        {
            try
            {
                // Сохраняем файл.
                fractal.FractalBitmap.Save(
                    $"Saves{Path.DirectorySeparatorChar}{typesFractal[nowFractal]}_" +
                    $"({DateTime.Now.Day}_{DateTime.Now.Month})_" +
                    $"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.png");
                MessageBox.Show(
                    $"Файл удалось сохранить!{Environment.NewLine}Файл находится по пути: {Path.GetFullPath("Saves")}",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сохранить файл",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод, создающий ImageSource из Bitmap.
        /// </summary>
        /// <param name="bmp">Битовая карта.</param>
        public static ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            // Производим получение ImageSource.
            var handle = bmp.GetHbitmap();
            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero,
                Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        /// <summary>
        /// Метод, получающий начальные точки для построения фракталов.
        /// </summary>
        /// <param name="fractal">Фрактал.</param>
        /// <param name="MainWindowKernel">Обьект главного окна.</param>
        public static Point[] GetStartPointsForFractalCreation(Fractal fractal, MainWindow MainWindowKernel)
        {
            Point[] points = new Point[0];
            // Получаем центр экрана.
            float centerX = (float)MainWindowKernel.MaxWidth / 2f;
            float centerY = (float)MainWindowKernel.MaxHeight / 2f;
            // Вычисляем точки для построения фрактала в зависимости от его типа.
            switch (fractal)
            {
                case Tree:
                    points = new Point[] {
                        new Point(centerX, centerY + (float)fractal.LengthOfSegment),
                        new Point(centerX, centerY)
                    };
                    break;
                case Snowflake:
                    points = new Point[] {
                        new Point(centerX - (float)fractal.LengthOfSegment / 2, centerY),
                        new Point(centerX + (float)fractal.LengthOfSegment / 2, centerY)
                    };
                    break;
                case Carpet:
                    points = new Point[] {
                        new Point(centerX - (float)fractal.LengthOfSegment / 2, centerY - (float)fractal.LengthOfSegment / 2)
                    };
                    break;
                case Triangle:
                    points = new Point[] {
                        new Point(centerX - (float)fractal.LengthOfSegment / 2, centerY + (float)fractal.LengthOfSegment / 2),
                        new Point(centerX + (float)fractal.LengthOfSegment / 2, centerY + (float)fractal.LengthOfSegment / 2),
                        new Point(centerX, centerY + (float)fractal.LengthOfSegment / 2 - (float)fractal.LengthOfSegment / 2
                            * (float)Math.Tan(Math.PI / 3))
                    };
                    break;
                case Set:
                    points = new Point[] { new Point(centerX - (float)fractal.LengthOfSegment / 2, centerY/2) };
                    break;
            }
            return points;
        }
    }
}
