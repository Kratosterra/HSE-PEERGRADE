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
    /// Взаимодействие с логикой для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Переменная для хранения фрактала.
        private Fractal s_fractal;
        // Переменная для хранения информации о текущем фрактале.
        private ushort s_nowFractal;
        // Массив для хранения названий фракталов.
        private readonly string[] s_typesFractal =
            { "Фрактальное_дерево", "Снежинка", "Ковёр_Серпинского", "Треугольник_Серпинского", "Множество_Кантора" };
        // Кортеж для хранения информации о стартовом цвете градиента.
        private (int, int, int) s_startRGB = (0, 0, 0);
        // Кортеж для хранения информации о конечном цвете градиента.
        private (int, int, int) s_endRGB = (0, 0, 0);
        // Переменная для хранения текущей глубины рекурсии.
        private int s_nowRecursion = 1;

        /// <summary>
        /// Метод, вызываемый при запуске главного окна.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            SetNewEnvironment();
        }

        /// <summary>
        /// Метод, устанавливающий размеры окна и создающий папку сохранения фракталов.
        /// </summary>
        private void SetNewEnvironment()
        {
            // Устанавливаем размеры окна.
            MainWindowKernel.MaxHeight = SystemParameters.PrimaryScreenHeight;
            MainWindowKernel.MaxWidth = SystemParameters.PrimaryScreenWidth;
            MainWindowKernel.MinHeight = SystemParameters.PrimaryScreenHeight / 2;
            MainWindowKernel.MinWidth = SystemParameters.PrimaryScreenWidth / 2;
            MainWindowKernel.Width = SystemParameters.PrimaryScreenWidth / 2;
            MainWindowKernel.Height = SystemParameters.PrimaryScreenHeight / 2;
            // Устанавливаем положение ползунков окна просмотра.
            ScrollViewer.ScrollToHorizontalOffset(SystemParameters.PrimaryScreenWidth / 3.5);
            ScrollViewer.ScrollToVerticalOffset(SystemParameters.PrimaryScreenHeight / 3.5);
            // Создаём папку для сохранения фракталов.
            try
            {
                if (!Directory.Exists("Saves")) Directory.CreateDirectory("Saves");
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Метод, задающий видимость элементов интерфейса.
        /// </summary>
        /// <param name="fractalTreeVisible">Переменая задающая видимость специального
        /// меню фрактального дерева.</param>
        /// <param name="kantorSetVisible">Переменая задающая видимость специального меню множества Кантора.</param>
        private void SetVisibilityMenu(bool fractalTreeVisible, bool kantorSetVisible)
        {
            Visibility visibilityFractalTree = fractalTreeVisible ? Visibility.Visible : Visibility.Hidden;
            Visibility visibilityKantorSet = kantorSetVisible ? Visibility.Visible : Visibility.Hidden;
            // Видимость элементов меню фрактального дерева.
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
            // Видимость элементов меню множества Кантора.
            this.SetKantorBox.Visibility = visibilityKantorSet;
            this.SideText.Visibility = visibilityKantorSet;
            this.SideSlider.Visibility = visibilityKantorSet;
            this.DistanceText.Visibility = visibilityKantorSet;
            this.DistanceSlider.Visibility = visibilityKantorSet;
            this.SideImage.Visibility = visibilityKantorSet;
            this.DistanceImage.Visibility = visibilityKantorSet;

        }

        /// <summary>
        /// Метод, задающий элементы интерфейса каждого фрактала, а также настраивающий слайдеры.
        /// </summary>
        /// <param name="maxLengthSlider">Максимальное значение слайдера длины отрезка.</param>
        /// <param name="maxRecursionSlider">Максимальное значение слайдера глубины рекурсии.</param>
        /// <param name="infoPicture">Информация об изображении.</param>
        private void SetFractalInterface(int maxLengthSlider, int maxRecursionSlider, Uri infoPicture)
        {
            s_fractal = null;
            // Настраиваем слайдеры.
            this.SectionLengthSlider.Value = 1;
            this.SectionLengthSlider.Maximum = maxLengthSlider;
            this.RecursionDeepSlider.Value = 1;
            this.RecursionDeepSlider.Maximum = maxRecursionSlider;
            // Выводим декоративное изображение.
            this.MainImage.Source = null;
            this.ImageInfo.Visibility = Visibility.Visible;
            this.ImageInfo.Source = new BitmapImage(infoPicture);
            this.ScrollViewer.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Метод, заполняющий пример-элипс требуемым цветом.
        /// </summary>
        /// <param name="RGB">Представление цвета в виде кортежа.</param>
        /// <param name="isThisStartColor">Является ли цвет конечным или начальным.</param>
        private void FillExampleColor((int, int, int) RGB, bool isThisStartColor)
        {
            // Создаём кисточку для покраски объекта.
            var brush = new SolidColorBrush(color: System.Windows.Media.Color.FromArgb(100,
                (byte)RGB.Item1,
                (byte)RGB.Item2,
                (byte)RGB.Item3));
            // Красим объект.
            if (isThisStartColor) this.ShowStartColor.Fill = brush;
            else this.ShowEndColor.Fill = brush;

        }

        /// <summary>
        /// Метод, выводящий фрактал на экран.
        /// </summary>
        private void OutputNewFractal()
        {
            try
            {
                // Создаём битовую карту для прорисовки фрактала.
                s_fractal.FractalBitmap = new Bitmap((int)MainWindowKernel.MaxWidth, (int)MainWindowKernel.MaxHeight);
                // Заполняем битовую карту, прорисовывая фрактал.
                s_fractal.CreateFractal(Tools.GetStartPoints(s_fractal, MainWindowKernel), s_fractal.RecursionDepth);
                // Устанавливаем битовую карту в качестве источника изображения.
                MainImage.Source = Tools.ImageSourceFromBitmap(s_fractal.FractalBitmap);
            }
            catch (Exception e)
            {
                // При ошибке выводим сообщение.
                MessageBox.Show($"Ошибка при прорисовке фрактала: {e.Message}",
                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Метод, создающий требуемый обьект фрактал.
        /// </summary>
        private void SetNewFractal()
        {
            switch (s_nowFractal)
            {
                case 0:
                    s_fractal = new Tree((int)SectionLengthSlider.Value, (ushort)RecursionDeepSlider.Value,
                        Color.FromArgb(s_startRGB.Item1, s_startRGB.Item2, s_startRGB.Item3),
                        Color.FromArgb(s_endRGB.Item1, s_endRGB.Item2, s_endRGB.Item3),
                        (int)FirstAngleSlider.Value, (int)SecondAngleSlider.Value,
                        ((int)RelationSlider.Value) / (double)1000);
                    break;
                case 1:
                    s_fractal = new Snowflake(
                        (int)SectionLengthSlider.Value, (ushort)(RecursionDeepSlider.Value + 1),
                        Color.FromArgb(s_startRGB.Item1, s_startRGB.Item2, s_startRGB.Item3),
                        Color.FromArgb(s_endRGB.Item1, s_endRGB.Item2, s_endRGB.Item3));
                    break;
                case 2:
                    s_fractal = new Carpet((int)SectionLengthSlider.Value, (ushort)(RecursionDeepSlider.Value),
                        Color.FromArgb(s_startRGB.Item1, s_startRGB.Item2, s_startRGB.Item3),
                        Color.FromArgb(s_endRGB.Item1, s_endRGB.Item2, s_endRGB.Item3));
                    break;
                case 3:
                    s_fractal = new Triangle((int)SectionLengthSlider.Value, (ushort)(RecursionDeepSlider.Value),
                        Color.FromArgb(s_startRGB.Item1, s_startRGB.Item2, s_startRGB.Item3),
                        Color.FromArgb(s_endRGB.Item1, s_endRGB.Item2, s_endRGB.Item3));
                    break;
                case 4:
                    s_fractal = new Set(
                        (int)SectionLengthSlider.Value, (ushort)(RecursionDeepSlider.Value),
                        Color.FromArgb(s_startRGB.Item1, s_startRGB.Item2, s_startRGB.Item3),
                        Color.FromArgb(s_endRGB.Item1, s_endRGB.Item2, s_endRGB.Item3),
                        (int)SideSlider.Value, (int)DistanceSlider.Value);
                    break;
            }
        }

        /// <summary>
        /// Метод, который устанавливает положение ползунков просмотра холста.
        /// </summary>
        private void SetNewViewMod()
        {
            ScrollViewer.ScrollToHorizontalOffset(ScrollViewer.ScrollableWidth / 2);
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.ScrollableHeight / 2);
            if (ScrollViewer.ScrollableWidth == 0)
            {
                ScrollViewer.ScrollToHorizontalOffset(MainWindowKernel.MaxWidth / 3);
                ScrollViewer.ScrollToVerticalOffset(MainWindowKernel.MaxHeight / 3);
            }
        }

        /// <summary>
        /// Событие, вызываемое при необходимости сменить тип фрактала на фрактальное дерево.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void TreeButton_Click(object sender, RoutedEventArgs e)
        {
            // Устанавливаем тип фрактала.
            s_nowFractal = 0;
            s_nowRecursion = 1;
            this.TypeFractalTextBlock.Text = "Тип фрактала: Фрактальное Дерево";
            // Устанавливаем текущую видимость меню настроек фракталов.
            SetVisibilityMenu(true, false);
            // Устанавливаем ограничения настроек и обновляем интерфейс.
            SetFractalInterface(350, 12, 
                new Uri("/FRACTAL-TREE.png", UriKind.Relative));
        }

        /// <summary>
        /// Событие, вызываемое при необходимости сменить тип фрактала на кривую Коха.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>        
        private void CurvedKochButton_Click(object sender, RoutedEventArgs e)
        {
            // Устанавливаем тип фрактала.
            s_nowFractal = 1;
            s_nowRecursion = 1;
            this.TypeFractalTextBlock.Text = "Тип фрактала: Кривая Коха";
            // Устанавливаем текущую видимость меню настроек фракталов.
            SetVisibilityMenu(false, false);
            // Устанавливаем ограничения настроек и обновляем интерфейс.
            SetFractalInterface(1500, 6, 
                new Uri("/KOCH-SNOWFLAKE.png", UriKind.Relative));
        }

        /// <summary>
        /// Событие, вызываемое при необходимости сменить тип фрактала на ковёр Серпинского.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void CarpetButton_Click(object sender, RoutedEventArgs e)
        {
            // Устанавливаем тип фрактала.
            s_nowFractal = 2;
            s_nowRecursion = 1;
            this.TypeFractalTextBlock.Text = "Тип фрактала: Ковер Серпинского";
            // Устанавливаем текущую видимость меню настроек фракталов.
            SetVisibilityMenu(false, false);
            // Устанавливаем ограничения настроек и обновляем интерфейс.
            SetFractalInterface(1000, 5, 
                new Uri("/SIERPINSKI-CARPET.png", UriKind.Relative));
        }

        /// <summary>
        /// Событие, вызываемое при необходимости сменить тип фрактала на треугольник Серпинского.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void TriangleButton_Click(object sender, RoutedEventArgs e)
        {
            // Устанавливаем тип фрактала.
            s_nowFractal = 3;
            s_nowRecursion = 1;
            this.TypeFractalTextBlock.Text = "Тип фрактала: Треугольник Серпинского";
            // Устанавливаем текущую видимость меню настроек фракталов.
            SetVisibilityMenu(false, false);
            // Устанавливаем ограничения настроек и обновляем интерфейс.
            SetFractalInterface(1000, 6, 
                new Uri("/SIERPINSKI-TRIANGLE.png", UriKind.Relative));
        }

        /// <summary>
        /// Событие, вызываемое при необходимости сменить тип фрактала на множество Кантора.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            // Устанавливаем тип фрактала.
            s_nowFractal = 4;
            s_nowRecursion = 1;
            this.TypeFractalTextBlock.Text = "Тип фрактала: Множество Кантора";
            // Устанавливаем текущую видимость меню настроек фракталов.
            SetVisibilityMenu(false, true);
            // Устанавливаем ограничения настроек и обновляем интерфейс.
            SetFractalInterface(600, 10, 
                new Uri("/CANTOR-SET.png", UriKind.Relative));
        }

        /// <summary>
        /// Событие, вызываемое при изменении зума.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.ZoomTextBlock.Text = $"Масштаб: x{(int)(ZoomSlider.Value)}";
            // Приближаем изображение с помощью трансформации элемента Image.
            this.MainImage.LayoutTransform =
                new ScaleTransform( (int) (ZoomSlider.Value),  (int) (ZoomSlider.Value));
            // Устанавливаем положение ползунков.
            SetNewViewMod();
        }

        /// <summary>
        /// Событие, вызываемое при смене ползунка длины отрезка.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void SectionLengthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.SectionLengthText.Text = $"Длина отрезка: {(int) SectionLengthSlider.Value}";
        }

        /// <summary>
        /// Событие, вызываемое при смене ползунка рекурсии.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void RecursionDeepSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.RecursionDeepText.Text = $"Глубина рекурсии: {(int)RecursionDeepSlider.Value}";
            // Вызываем перерисовку фракталов.
            if(s_fractal != null && (int)RecursionDeepSlider.Value != s_nowRecursion) CreateFractalButton_Click(this, new RoutedEventArgs());
            s_nowRecursion = (int) RecursionDeepSlider.Value;
        }

        /// <summary>
        /// Событие, вызываемое при изменении ползунка начального цвета градиента (R).
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void StartColourRSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_startRGB.Item1 = (int) StartColourRSlider.Value;
            this.StartColourText.Text = $"({s_startRGB.Item1},{s_startRGB.Item2},{s_startRGB.Item3})";
            // Заполняем пример необходимым цветом.
            FillExampleColor(s_startRGB, true);
        }

        /// <summary>
        /// Событие, вызываемое при изменении ползунка начального цвета градиента (G).
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void StartColourGSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_startRGB.Item2 = (int)StartColourGSlider.Value;
            this.StartColourText.Text = $"({s_startRGB.Item1},{s_startRGB.Item2},{s_startRGB.Item3})";
            // Заполняем пример необходимым цветом.
            FillExampleColor(s_startRGB, true);
        }

        /// <summary>
        /// Событие, вызываемое при изменении ползунка начального цвета градиента (B).
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void StartColourBSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_startRGB.Item3 = (int)StartColourBSlider.Value;
            this.StartColourText.Text = $"({s_startRGB.Item1},{s_startRGB.Item2},{s_startRGB.Item3})";
            // Заполняем пример необходимым цветом.
            FillExampleColor(s_startRGB, true);
        }

        /// <summary>
        /// Событие, вызываемое при изменении ползунка конечного цвета градиента (R).
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void EndColourRSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_endRGB.Item1 = (int)EndColourRSlider.Value;
            this.EndColourText.Text = $"({s_endRGB.Item1},{s_endRGB.Item2},{s_endRGB.Item3})";
            // Заполняем пример необходимым цветом.
            FillExampleColor(s_endRGB, false);
        }

        /// <summary>
        /// Событие, вызываемое при изменении ползунка конечного цвета градиента (G).
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void EndColourGSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_endRGB.Item2 = (int)EndColourGSlider.Value;
            this.EndColourText.Text = $"({s_endRGB.Item1},{s_endRGB.Item2},{s_endRGB.Item3})";
            // Заполняем пример необходимым цветом.
            FillExampleColor(s_endRGB, false);
        }

        /// <summary>
        /// Событие, вызываемое при изменении ползунка конечного цвета градиента (B).
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void EndColourBSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            s_endRGB.Item3 = (int)EndColourBSlider.Value;
            this.EndColourText.Text = $"({s_endRGB.Item1},{s_endRGB.Item2},{s_endRGB.Item3})";
            // Заполняем пример необходимым цветом.
            FillExampleColor(s_endRGB, false);
        }

        /// <summary>
        /// Событие, вызываемое при изменении ползунка отношения длин.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void RelationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.RelationText.Text = $"Отношение длин: {((int)RelationSlider.Value)/(double)1000}";
        }

        /// <summary>
        /// Событие, вызываемое при изменении ползунка первого угла для фрактального дерева.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void FirstAngleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.FirstAngleText.Text = $"Первый угол наклона: {(int)FirstAngleSlider.Value}";
        }

        /// <summary>
        /// Событие, вызываемое при изменении ползунка второго угла фрактального дерева.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void SecondAngleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.SecondAngleText.Text = $"Второй угол наклона: {(int)SecondAngleSlider.Value}";
        }

        /// <summary>
        /// Событие, вызываемое при изменении ползунка стороны прямоугольника множества Кантора.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void SideSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.SideText.Text = $"Сторона прямоугольника: {(int)SideSlider.Value}";
        }

        /// <summary>
        /// Событие, вызываемое при изменении ползунка расстояния множества Кантора.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void DistanceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.DistanceText.Text = $"Расстояние: {(int)DistanceSlider.Value}";
        }

        /// <summary>
        /// Событие, вызываемое при необходимости прорисовки фрактала.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void CreateFractalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.ImageInfo.Visibility = Visibility.Hidden;
                this.ScrollViewer.Visibility = Visibility.Visible;
                // Устанавливаем фрактал.
                SetNewFractal();
                // Выводим фрактал на экран.
                OutputNewFractal();
                // Устанавливаем вид на холст в центр экрана.
                SetNewViewMod();
            }
            catch (OutOfMemoryException)
            {
                MessageBox.Show($"Использовано слишком много памяти, измените входные данные!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show($"Произошло переполнение, измените входные данные!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (StackOverflowException)
            {
                MessageBox.Show($"Произошло переполнение стека, измените глубину рекурсивных вызовов!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show($"Произошла ошибка при прорисовке фрактала, проверьте входные данные!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        /// <summary>
        /// Событие, вызываемое при необходимости сохранить фрактал.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (s_fractal == null)
            {
                MessageBox.Show($"Создайте фрактал, чтобы его сохранить", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if(!Directory.Exists("Saves"))
            {
                // Сохраняем фрактал без папки сохранения.
                Tools.SaveWithNoDictionary(s_fractal, s_nowFractal, s_typesFractal);
            }
            else
            {
                // Сохраняем фрактал с папкой сохранения.
                Tools.ClearSave(s_fractal, s_nowFractal, s_typesFractal);
            }
        }

    }
}
