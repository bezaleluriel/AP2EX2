using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Wpf;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for WindowSPSettings.xaml
    /// </summary>
    public partial class WindowSPSettings : Window
    {

        ViewModelSpSettings VM;

        public WindowSPSettings()
        {
            InitializeComponent();
            ModelSPSettings model = new ModelSPSettings();
            VM = new ViewModelSpSettings(model);
            this.DataContext = VM;

             
        }

       
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            VM.SaveSettings();
            WindowSinglePlayer windowSinglePlayer = new WindowSinglePlayer();
            windowSinglePlayer.Show();
            this.Close();
            
        }

        private void txtMazeName_TextChanged(object sender, TextChangedEventArgs e)
        {
            WindowSinglePlayer.updateName(txtMazeName);
        }
    }
}
