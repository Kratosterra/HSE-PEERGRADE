using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Peergrade005
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string[] typesFractal = { "FractalTree", "KochCurved", "Carpet", "Triangle", "CantorSet" };
        private ushort nowFractal = 0;
        public MainWindow()
        {
            InitializeComponent();
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
        }

        private void CurvedKochButton_Click(object sender, RoutedEventArgs e)
        {
            this.TypeFractalTextBlock.Text = "Тип фрактала: Кривая Коха";
            SetVisibilityMenu(false, false);
            nowFractal = 1;
        }

        private void CarpetButton_Click(object sender, RoutedEventArgs e)
        {
            this.TypeFractalTextBlock.Text = "Тип фрактала: Ковер Серпинского";
            SetVisibilityMenu(false, false);
            nowFractal = 2;
        }

        private void TriangleButton_Click(object sender, RoutedEventArgs e)
        {
            this.TypeFractalTextBlock.Text = "Тип фрактала: Треугольник Серпинского";
            SetVisibilityMenu(false, false);
            nowFractal = 3;
        }

        private void SetButton_Click(object sender, RoutedEventArgs e)
        {
            this.TypeFractalTextBlock.Text = "Тип фрактала: Множество Кантора";
            SetVisibilityMenu(false, true);
            nowFractal = 4;
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.ZoomTextBlock.Text = $"Масштаб: x{(int)(ZoomSlider.Value)}";
            this.MainImage.RenderTransform =
                new ScaleTransform((double) (int) (ZoomSlider.Value), (double) (int) (ZoomSlider.Value));
        }
    }
}
