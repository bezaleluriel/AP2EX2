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
using System.Windows.Shapes;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for WindowMultiPlayerMenu.xaml
    /// </summary>
    public partial class WindowMultiPlayerMenu : Window
    {

        ViewModelMultiPlayerMenu VM;

        public WindowMultiPlayerMenu()
        {
            InitializeComponent();
            ModelMultiPlayerMenu model = new ModelMultiPlayerMenu();
            VM = new ViewModelMultiPlayerMenu(model);
            this.DataContext = VM;
            VM.list();
            listOfGames.ItemsSource = model.GamesList;                        
        }

        private void listOfGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            VM.start();
        }

        private void joinButton_Click(object sender, RoutedEventArgs e)
        {
            //VM.join();
        }
    }
}
