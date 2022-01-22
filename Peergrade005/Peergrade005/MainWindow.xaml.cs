using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Color = System.Drawing.Color;
using Size = System.Windows.Size;

namespace Peergrade005
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string[] typesFractal = { "FractalTree", "KochCurved", "Carpet", "Triangle", "CantorSet" };
        private ushort nowFractal = 0;
        private (int, int, int) startRGB = (0, 0, 0);
        private (int, int, int) endRGB = (0, 0, 0);
        private Fractal fractal;

        public MainWindow()
        {
            InitializeComponent();
            MainWindowKernel.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            MainWindowKernel.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            MainWindowKernel.MinHeight = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
            MainWindowKernel.MinWidth = System.Windows.SystemParameters.PrimaryScreenWidth / 2;

            ScrollViewer.ScrollToHorizontalOffset(System.Windows.SystemParameters.PrimaryScreenWidth / 3.5);
            ScrollViewer.ScrollToVerticalOffset(System.Windows.SystemParameters.PrimaryScreenHeight / 3.5);
        }

        private void SetVisibilityMenu(bool fractalTreeVisible, bool kantorSetVisible)
        {
            Visibility visibilityFractalTree = fractalTreeVisible ? Visibility.Visible : Visibility.Hidden;
            Visibility visibilityKantorSet = kantorSetVisible ? Visibility.Visible : Visibility.Hidden;

            this.FractalTreeSettingsBox.Visibility = visibilityFractalTree;
            this.RelationText.Visibility = visibilityFractalTree;
            this.RelationSlider.Visibility = visibilityFractalTree;
            this.FirstAngleText.Visibility = visibilityFractalTree;
            this.FirstAngleSlider.Visibility = visibilityFractalTree;
            this.SecondAngleText.Visibility = visibilityFractalTree;
            this.SecondAngleSlider.Visibility = visibilityFractalTree;

            this.SetKantorBox.Visibility = visibilityKantorSet;
            this.SideText.Visibility = visibilityKantorSet;
            this.SideSlider.Visibility = visibilityKantorSet;
            this.DistanceText.Visibility = visibilityKantorSet;
            this.DistanceSlider.Visibility = visibilityKantorSet;
        }

        private void TreeButton_Click(object sender, RoutedEventArgs e)
        {
            this.TypeFractalTextBlock.Text = "Тип фрактала: Фрактальное Дерево";
            SetVisibilityMenu(true, false);
            nowFractal = 0;
            this.SectionLengthSlider.Value = 1;
            this.SectionLengthSlider.Maximum = 350;
            this.RecursionDeepSlider.Value = 1;
            this.RecursionDeepSlider.Maximum = 12;
        }

        private void CurvedKochButton_Click(object sender, RoutedEventArgs e)
        {
            this.TypeFractalTextBlock.Text = "Тип фрактала: Кривая Коха";
            SetVisibilityMenu(false, false);
            nowFractal = 1;
            this.SectionLengthSlider.Value = 1;
            this.SectionLengthSlider.Maximum = 1500;
            this.RecursionDeepSlider.Value = 1;
            this.RecursionDeepSlider.Maximum = 6;
        }

        private void CarpetButton_Click(object sender, RoutedEventArgs e)
        {
            this.TypeFractalTextBlock.Text = "Тип фрактала: Ковер Серпинского";
            SetVisibilityMenu(false, false);
            nowFractal = 2;
            this.SectionLengthSlider.Value = 1;
            this.SectionLengthSlider.Maximum = 1000;
            this.RecursionDeepSlider.Value = 1;
            this.RecursionDeepSlider.Maximum = 5;
        }

        private void TriangleButton_Click(object sender, RoutedEventArgs e)
        {
            this.TypeFractalTextBlock.Text = "Тип фрактала: Треугольник Серпинского";
            SetVisibilityMenu(false, false);
            nowFractal = 3;
            this.SectionLengthSlider.Value = 1;
            this.SectionLengthSlider.Maximum = 1000;
            this.RecursionDeepSlider.Value = 1;
            this.RecursionDeepSlider.Maximum = 6;
        }

        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            this.TypeFractalTextBlock.Text = "Тип фрактала: Множество Кантора";
            SetVisibilityMenu(false, true);
            nowFractal = 4;
            this.SectionLengthSlider.Value = 1;
            this.SectionLengthSlider.Maximum = 350;
            this.RecursionDeepSlider.Value = 1;
            this.RecursionDeepSlider.Maximum = 10;
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.ZoomTextBlock.Text = $"Масштаб: x{(int)(ZoomSlider.Value)}";
            this.MainImage.LayoutTransform =
                new ScaleTransform((double) (int) (ZoomSlider.Value), (double) (int) (ZoomSlider.Value));
        }

        private void SectionLengthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.SectionLengthText.Text = $"Длина отрезка: {(int) SectionLengthSlider.Value}";
        }

        private void RecursionDeepSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.RecursionDeepText.Text = $"Глубина рекурсии: {(int)RecursionDeepSlider.Value}";
        }

        private void StartColourRSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            startRGB.Item1 = (int) StartColourRSlider.Value;
            this.StartColourText.Text = $"({startRGB.Item1},{startRGB.Item2},{startRGB.Item3})";
        }

        private void StartColourGSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            startRGB.Item2 = (int)StartColourGSlider.Value;
            this.StartColourText.Text = $"({startRGB.Item1},{startRGB.Item2},{startRGB.Item3})";
        }

        private void StartColourBSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            startRGB.Item3 = (int)StartColourBSlider.Value;
            this.StartColourText.Text = $"({startRGB.Item1},{startRGB.Item2},{startRGB.Item3})";
        }

        private void EndColourRSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            endRGB.Item1 = (int)EndColourRSlider.Value;
            this.EndColourText.Text = $"({endRGB.Item1},{endRGB.Item2},{endRGB.Item3})";
        }

        private void EndColourGSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            endRGB.Item2 = (int)EndColourGSlider.Value;
            this.EndColourText.Text = $"({endRGB.Item1},{endRGB.Item2},{endRGB.Item3})";
        }

        private void EndColourBSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            endRGB.Item3 = (int)EndColourBSlider.Value;
            this.EndColourText.Text = $"({endRGB.Item1},{endRGB.Item2},{endRGB.Item3})";
        }

        private void RelationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.RelationText.Text = $"Отношение длин: {((int)RelationSlider.Value)/(double)1000}";
        }

        private void FirstAngleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.FirstAngleText.Text = $"Первый угол наклона: {(int)FirstAngleSlider.Value}";
        }

        private void SecondAngleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.SecondAngleText.Text = $"Второй угол наклона: {(int)SecondAngleSlider.Value}";
        }

        private void SideSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.SideText.Text = $"Сторона прямоугольника: {(int)SideSlider.Value}";
        }

        private void DistanceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.DistanceText.Text = $"Расстояние: {(int)DistanceSlider.Value}";
        }

        private void CreateFractalButton_Click(object sender, RoutedEventArgs e)
        {
            switch (nowFractal)
            {
                case 0:
                    fractal = new Tree(
                        (int) SectionLengthSlider.Value,
                        (ushort)RecursionDeepSlider.Value,
                        Color.FromArgb(startRGB.Item1, startRGB.Item2, startRGB.Item3),
                        Color.FromArgb(endRGB.Item1, endRGB.Item2, endRGB.Item3),
                        (int)FirstAngleSlider.Value,
                        (int)SecondAngleSlider.Value,
                        ((int)RelationSlider.Value) / (double)1000
                        );
                    break;
                case 1:
                    fractal = new Snowflake(
                        (int) SectionLengthSlider.Value,
                        (ushort) (RecursionDeepSlider.Value + 1),
                        Color.FromArgb(startRGB.Item1, startRGB.Item2, startRGB.Item3),
                        Color.FromArgb(endRGB.Item1, endRGB.Item2, endRGB.Item3)
                    );
                    break;
                case 2:
                    fractal = new Carpet(
                        (int)SectionLengthSlider.Value,
                        (ushort)(RecursionDeepSlider.Value),
                        Color.FromArgb(startRGB.Item1, startRGB.Item2, startRGB.Item3),
                        Color.FromArgb(endRGB.Item1, endRGB.Item2, endRGB.Item3)
                    );
                    break;
                case 3:
                    fractal = new Triangle(
                        (int)SectionLengthSlider.Value,
                        (ushort)(RecursionDeepSlider.Value),
                        Color.FromArgb(startRGB.Item1, startRGB.Item2, startRGB.Item3),
                        Color.FromArgb(endRGB.Item1, endRGB.Item2, endRGB.Item3)
                    );
                    break;
                case 4:
                    fractal = new Set(
                        (int)SectionLengthSlider.Value,
                        (ushort)(RecursionDeepSlider.Value),
                        Color.FromArgb(startRGB.Item1, startRGB.Item2, startRGB.Item3),
                        Color.FromArgb(endRGB.Item1, endRGB.Item2, endRGB.Item3),
                        (int)SideSlider.Value,
                        (int)DistanceSlider.Value
                    );
                    break;
            }
            DrawFractal();

            ScrollViewer.ScrollToHorizontalOffset(System.Windows.SystemParameters.PrimaryScreenWidth / 3.5);
            ScrollViewer.ScrollToVerticalOffset(System.Windows.SystemParameters.PrimaryScreenHeight / 3.5);
        }

        public ImageSource ImageSourceFromBitmap(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        private void DrawFractal()
        {

            try
            {
                fractal.FractalBitmap = new Bitmap((int)MainWindowKernel.MaxWidth, (int)MainWindowKernel.MaxHeight);
                fractal.CreateFractal(GetStartPoints(), fractal.RecursionDepth);
                MainImage.Source = ImageSourceFromBitmap(fractal.FractalBitmap);
                fractal.FractalBitmap.Save("ddddddd.png");
                
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка {e}");
            }

        }

        private PointF[] GetStartPoints()
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

        private void MainWindowKernel_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
