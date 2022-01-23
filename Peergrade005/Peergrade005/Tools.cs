using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;
using Pen = System.Drawing.Pen;

namespace Peergrade005
{
    class Tools
    {
        public static void SaveWithNoDictionary(Fractal fractal, int nowFractal, string[] typesFractal)
        {
            MessageBox.Show($"Папка для сохранений не была создана или была удалена!{Environment.NewLine}" +
                            $"Будет произведена попытка сохранения фрактала в родительской папке",
                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            try
            {
                fractal.FractalBitmap.Save($"{typesFractal[nowFractal]}_({DateTime.Now.Day}_{DateTime.Now.Month})_" +
                                           $"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.png");
                MessageBox.Show($"Файл удалось сохранить!{Environment.NewLine}Файл находится внутри родительской папки!",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сохранить файл", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void CreateLine(PointF[] points, int recursion, ref Graphics graphics, Color[] colorsGradient)
        {
            Pen nowPen = new Pen(colorsGradient[recursion - 1], 2f);
            graphics.DrawLine(nowPen, points[0], points[1]);
        }

        public static void CreateRectangle(PointF[] points, int recursion, ref Graphics graphics, Color[] colorsGradient)
        {
            RectangleF rectangle = new RectangleF(
                points[0],
                new SizeF(points[1].X - points[0].X, points[1].Y - points[0].Y));

            SolidBrush solidBrush = new SolidBrush(colorsGradient[recursion - 1]);
            graphics.FillRectangle(solidBrush, rectangle);
        }

        public static void ExecuteColorsGradient(int recursionDepth, ref Color[] colorsGradient, Color startColor, Color endColor)
        {
            if (recursionDepth == 1)
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

        public static void ClearSave(Fractal fractal, int nowFractal, string[] typesFractal)
        {
            try
            {
                fractal.FractalBitmap.Save(
                    $"Saves{Path.DirectorySeparatorChar}{typesFractal[nowFractal]}_({DateTime.Now.Day}_{DateTime.Now.Month})_" +
                    $"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.png");
                MessageBox.Show(
                    $"Файл удалось сохранить!{Environment.NewLine}Файл находится по пути: {Path.GetFullPath("Saves")}",
                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сохранить файл", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public static PointF[] GetStartPoints(Fractal fractal, MainWindow MainWindowKernel)
        {
            PointF[] points = new PointF[0];
            float centerX = (float)MainWindowKernel.MaxWidth / 2f;
            float centerY = (float)MainWindowKernel.MaxHeight / 2f;

            if (fractal is Tree)
            {
                points = new PointF[] {
                    new PointF(centerX, centerY + (float)fractal.LengthOfSegment),
                    new PointF(centerX, centerY)
                };
            }
            if (fractal is Snowflake)
            {
                points = new PointF[] {
                    new PointF(centerX - (float)fractal.LengthOfSegment / 2, centerY),
                    new PointF(centerX + (float)fractal.LengthOfSegment / 2, centerY)
                };
            }
            if (fractal is Carpet)
            {
                points = new PointF[] {
                    new PointF(centerX - (float)fractal.LengthOfSegment / 2, centerY - (float)fractal.LengthOfSegment / 2)
                };
            }
            if (fractal is Triangle)
            {
                points = new PointF[] {
                    new PointF(centerX - (float)fractal.LengthOfSegment / 2, centerY + (float)fractal.LengthOfSegment / 2),
                    new PointF(centerX + (float)fractal.LengthOfSegment / 2, centerY + (float)fractal.LengthOfSegment / 2),
                    new PointF(centerX, centerY + (float)fractal.LengthOfSegment / 2 - (float)fractal.LengthOfSegment / 2
                        * (float)Math.Tan(Math.PI / 3))
                };
            }
            if (fractal is Set)
            {
                points = new PointF[] { new PointF(centerX - (float)fractal.LengthOfSegment / 2, 10) };
            }
            return points;
        }
    }
}
