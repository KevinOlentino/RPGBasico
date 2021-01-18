using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RPGBasico.Views
{
    /// <summary>
    /// Lógica interna para DevelopmentWindow.xaml
    /// </summary>
    public partial class DevelopmentWindow : Window
    {
        public DevelopmentWindow()
        {
            InitializeComponent();

            LocationUI.Children.Add(new Ellipse
            {
                Height = 10,
                Width = 10,
                Fill = new SolidColorBrush(Colors.Red),
            });

            Canvas.SetLeft(LocationUI.Children[0], LocationUI.Width / 2 - 5);
            Canvas.SetTop(LocationUI.Children[0], LocationUI.Height / 2 - 5);
        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button b)
            {
                switch (b.Name)
                {
                    case "Up":
                        break;
                    case "Down":
                        break;
                    case "Left":
                        break;
                    case "Right":
                        break;
                }
            }
        }
    }
}
