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
using Wpf;


namespace Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

        }

        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            WindowSPSettings SPSettings = new WindowSPSettings();            
            SPSettings.Show();
            this.Close();
            //windowSinglePlayer.Close

        }

        private void MultiPlayer_Click(object sender, RoutedEventArgs e)
        {
            WindowMultiPlayerMenu multiPlayerMenu = new WindowMultiPlayerMenu();
            multiPlayerMenu.Show();
            this.Close();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            WindowMainSettings mainSettings = new WindowMainSettings();
            mainSettings.Show();
           // this.Close();
        }
    }
}
