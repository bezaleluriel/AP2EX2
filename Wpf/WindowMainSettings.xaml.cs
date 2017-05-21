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
using Newtonsoft.Json;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for WindowMainSettings.xaml
    /// </summary>
    public partial class WindowMainSettings : Window
    {
        ViewModelMainSettings VM ;

        public WindowMainSettings()
        {
            InitializeComponent();
            ModelMainSettings model = new ModelMainSettings();
            VM = new ViewModelMainSettings(model);
            this.DataContext = VM;
        }

        private void OK_button_Click(object sender, RoutedEventArgs e)
        {
            VM.SaveSettings();
          //  MainWindow win = (MainWindow)Application.Current.MainWindow;
          //  win.Show();
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
