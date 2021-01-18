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
using Engine;

namespace RPGBasico
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private Player _player;
        public MainWindow()
        {
            InitializeComponent();

            _player = new Player("Kevin",1,2,3,4,5);



             lblVida.Content = _player.Vida.ToString(); ;
             lblLevel.Content = _player.Level.ToString(); ;
             lblNome.Content = _player.Nome.ToString(); ;
             lblDinheiro.Content = _player.Dinheiro.ToString(); ;
        }

        private void BtnNorte_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnOeste_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSul_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnLeste_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUsePotion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUseWeapon_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
