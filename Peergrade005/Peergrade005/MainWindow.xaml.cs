using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Cache;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Color = System.Drawing.Color;
using Path = System.IO.Path;
using Size = System.Windows.Size;

namespace Peergrade005
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string[] typesFractal = { "Фрактальное_дерево", "Снежинка", "Ковёр_Серпинского", "Треугольник_Серпинского", "Множество_Кантора" };
        private ushort nowFractal = 0;
        private (int, int, int) startRGB = (0, 0, 0);
        private (int, int, int) endRGB = (0, 0, 0);
        private Fractal fractal;

        public MainWindow()
        {
            InitializeComponent();
            SetNewEnvironment();
        }

        private void SetNewEnvironment()
        {
            MainWindowKernel.MaxHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            MainWindowKernel.MaxWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            MainWindowKernel.MinHeight = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
            MainWindowKernel.MinWidth = System.Windows.SystemParameters.PrimaryScreenWidth / 2;

            MainWindowKernel.Width = System.Windows.SystemParameters.PrimaryScreenWidth / 2;
            MainWindowKernel.Height = System.Windows.SystemParameters.PrimaryScreenHeight / 2;
            ScrollViewer.ScrollToHorizontalOffset(System.Windows.SystemParameters.PrimaryScreenWidth / 3.5);
            ScrollViewer.ScrollToVerticalOffset(System.Windows.SystemParameters.PrimaryScreenHeight / 3.5);

            try
            {
                if (!Directory.Exists("Saves")) Directory.CreateDirectory("Saves");
            }
            catch (Exception ex)
            {
            }
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
            this.RelationImage.Visibility = visibilityFractalTree;
            this.FirstAngleImage.Visibility = visibilityFractalTree;
            this.SecondAngleImage.Visibility = visibilityFractalTree;

            this.SetKantorBox.Visibility = visibilityKantorSet;
            this.SideText.Visibility = visibilityKantorSet;
            this.SideSlider.Visibility = visibilityKantorSet;
            this.DistanceText.Visibility = visibilityKantorSet;
            this.DistanceSlider.Visibility = visibilityKantorSet;
            this.SideImage.Visibility = visibilityKantorSet;
            this.DistanceImage.Visibility = visibilityKantorSet;

        }

        private void SetFractalInterface(int maxLengthSlider, int maxRecursionSlider, Uri infoPicture)
        {
            this.SectionLengthSlider.Value = 1;
            this.SectionLengthSlider.Maximum = maxLengthSlider;
            this.RecursionDeepSlider.Value = 1;
            this.RecursionDeepSlider.Maximum = maxRecursionSlider;
            fractal = null;
            this.MainImage.Source = null;
            this.ImageInfo.Visibility = Visibility.Visible;
            this.ImageInfo.Source = new BitmapImage(infoPicture);
            this.ScrollViewer.Visibility = Visibility.Hidden;
        }

        private void TreeButton_Click(object sender, RoutedEventArgs e)
        {
            nowFractal = 0;
            this.TypeFractalTextBlock.Text = "Тип фрактала: Фрактальное Дерево";
            SetVisibilityMenu(true, false);
            SetFractalInterface(350, 12, new Uri("/FRACTAL-TREE.png", UriKind.Relative));
        }

        private void CurvedKochButton_Click(object sender, RoutedEventArgs e)
        {
            nowFractal = 1;
            this.TypeFractalTextBlock.Text = "Тип фрактала: Кривая Коха";
            SetVisibilityMenu(false, false);
            SetFractalInterface(1500, 6, new Uri("/KOCH-SNOWFLAKE.png", UriKind.Relative));
        }

        private void CarpetButton_Click(object sender, RoutedEventArgs e)
        {
            nowFractal = 2;
            this.TypeFractalTextBlock.Text = "Тип фрактала: Ковер Серпинского";
            SetVisibilityMenu(false, false);
            SetFractalInterface(1000, 5, new Uri("/SIERPINSKI-CARPET.png", UriKind.Relative));
        }

        private void TriangleButton_Click(object sender, RoutedEventArgs e)
        {
            nowFractal = 3;
            this.TypeFractalTextBlock.Text = "Тип фрактала: Треугольник Серпинского";
            SetVisibilityMenu(false, false);
            SetFractalInterface(1000, 6, new Uri("/SIERPINSKI-TRIANGLE.png", UriKind.Relative));
        }

        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            nowFractal = 4;
            this.TypeFractalTextBlock.Text = "Тип фрактала: Множество Кантора";
            SetVisibilityMenu(false, true);
            SetFractalInterface(600, 10, new Uri("/CANTOR-SET.png", UriKind.Relative));
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.ZoomTextBlock.Text = $"Масштаб: x{(int)(ZoomSlider.Value)}";
            this.MainImage.LayoutTransform =
                new ScaleTransform((double) (int) (ZoomSlider.Value), (double) (int) (ZoomSlider.Value));
            ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.ScrollableWidth / 2);
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.ScrollableHeight / 2);
        }

        private void SectionLengthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.SectionLengthText.Text = $"Длина отрезка: {(int) SectionLengthSlider.Value}";
        }

        private void RecursionDeepSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.RecursionDeepText.Text = $"Глубина рекурсии: {(int)RecursionDeepSlider.Value}";
            if(fractal != null) CreateFractalButton_Click(this, new RoutedEventArgs());
        }

        private void StartColourRSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            startRGB.Item1 = (int) StartColourRSlider.Value;
            this.StartColourText.Text = $"({startRGB.Item1},{startRGB.Item2},{startRGB.Item3})";
            var brush = new SolidColorBrush(color: System.Windows.Media.Color.FromArgb(100,
                (byte)startRGB.Item1,
                (byte)startRGB.Item2,
                (byte)startRGB.Item3));
            this.ShowStartColor.Fill = brush;
        }

        private void StartColourGSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            startRGB.Item2 = (int)StartColourGSlider.Value;
            this.StartColourText.Text = $"({startRGB.Item1},{startRGB.Item2},{startRGB.Item3})";
            var brush = new SolidColorBrush(color: System.Windows.Media.Color.FromArgb(100,
                (byte)startRGB.Item1,
                (byte)startRGB.Item2,
                (byte)startRGB.Item3));
            this.ShowStartColor.Fill = brush;
        }

        private void StartColourBSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            startRGB.Item3 = (int)StartColourBSlider.Value;
            this.StartColourText.Text = $"({startRGB.Item1},{startRGB.Item2},{startRGB.Item3})";
            var brush = new SolidColorBrush(color: System.Windows.Media.Color.FromArgb(100,
                (byte)startRGB.Item1,
                (byte)startRGB.Item2,
                (byte)startRGB.Item3));
            this.ShowStartColor.Fill = brush;
        }

        private void EndColourRSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            endRGB.Item1 = (int)EndColourRSlider.Value;
            this.EndColourText.Text = $"({endRGB.Item1},{endRGB.Item2},{endRGB.Item3})";
            var brush = new SolidColorBrush(color: System.Windows.Media.Color.FromArgb(100,
                (byte)endRGB.Item1,
                (byte)endRGB.Item2,
                (byte)endRGB.Item3));
            this.ShowEndColor.Fill = brush;
        }

        private void EndColourGSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            endRGB.Item2 = (int)EndColourGSlider.Value;
            this.EndColourText.Text = $"({endRGB.Item1},{endRGB.Item2},{endRGB.Item3})";
            var brush = new SolidColorBrush(color: System.Windows.Media.Color.FromArgb(100,
                (byte)endRGB.Item1,
                (byte)endRGB.Item2,
                (byte)endRGB.Item3));
            this.ShowEndColor.Fill = brush;
        }

        private void EndColourBSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            endRGB.Item3 = (int)EndColourBSlider.Value;
            this.EndColourText.Text = $"({endRGB.Item1},{endRGB.Item2},{endRGB.Item3})";
            var brush = new SolidColorBrush(color: System.Windows.Media.Color.FromArgb(100,
                (byte)endRGB.Item1,
                (byte)endRGB.Item2,
                (byte)endRGB.Item3));
            this.ShowEndColor.Fill = brush;
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
            this.ImageInfo.Visibility = Visibility.Hidden;
            this.ScrollViewer.Visibility = Visibility.Visible;
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
            ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.ScrollableWidth / 2);
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.ScrollableHeight / 2);
            if (ScrollViewer.ScrollableWidth == 0)
            {
                ScrollViewer.ScrollToHorizontalOffset(MainWindowKernel.MaxWidth / 3);
                ScrollViewer.ScrollToVerticalOffset(MainWindowKernel.MaxHeight/ 3);
            }
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
            }
            catch (Exception e)
            {
                MessageBox.Show($"Ошибка при прорисовке фрактала: {e.Message}");
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (fractal == null)
            {
                MessageBox.Show($"Создайте фрактал, чтобы его сохранить", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if(!Directory.Exists("Saves"))
            {
                MessageBox.Show($"Папка для сохранений не была создана или была удалена!{Environment.NewLine}" +
                                $"Будет произведена попытка сохранения фрактала в родительской папке",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                try
                {
                    fractal.FractalBitmap.Save($"{typesFractal[nowFractal]}_({DateTime.Now.Day}_{DateTime.Now.Month})_" +
                                               $"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.png");
                    MessageBox.Show($"Файл удалось сохранить!{Environment.NewLine}Файл находится внутри родительской папки!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось сохранить файл", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    fractal.FractalBitmap.Save($"Saves{Path.DirectorySeparatorChar}{typesFractal[nowFractal]}_({DateTime.Now.Day}_{DateTime.Now.Month})_" +
                                               $"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}.png");
                    MessageBox.Show($"Файл удалось сохранить!{Environment.NewLine}Файл находится по пути: {Path.GetFullPath("Saves")}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось сохранить файл", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
