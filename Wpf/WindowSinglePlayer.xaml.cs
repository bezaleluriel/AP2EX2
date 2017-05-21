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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using TextBox = System.Windows.Controls.TextBox;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for WindowSinglePlayer.xaml
    /// </summary>
    /// 

    public partial class WindowSinglePlayer : Window
    {
        ViewModelSinglePlayer VM;
        public WindowSinglePlayer()
        {
            InitializeComponent();
            ModelSinglePlayer model = new ModelSinglePlayer();
            VM = new ViewModelSinglePlayer(model);
            this.DataContext = VM;
            this.KeyDown += mazeBoard.UserControl_KeyDown;
            VM.Generate();
            str = str.Substring(32);
            this.Title = str;


        }

        private static string str;
        string isSolvePressedToThisName;
        private void Solve_Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("solve button pressed");

            if (isSolvePressedToThisName != null)
            {
                if (!isSolvePressedToThisName.Equals(str))
                {
                    VM.Solve();
                }
                else if (isSolvePressedToThisName.Equals(str))
                {

                    mazeBoard.drawMovingSolution();


                }
            }
            else if (isSolvePressedToThisName == null)
            {
                VM.Solve();
                isSolvePressedToThisName = str;
            }

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            // System.Windows.MessageBox.Show("are you sure you want restart?");
            //VM.Restart();
            MessageBoxResult m = MessageBox.Show("Are you sure you want a restart?", "restart game?", MessageBoxButton.YesNo);
            if (m == MessageBoxResult.Yes)
            {
                //  mazeBoard.CurrentPosition = mazeBoard.StartPosition;
                // Do something
                mazeBoard.Restart();
            }
            else if (m == MessageBoxResult.No)
            {

                // Do something else
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {



            MessageBoxResult sure = MessageBox.Show("?בטוח/ה רוצה לחזור לתפריט הראשי", "תפריט ראשי", MessageBoxButton.YesNo);
            if (sure == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else if (sure == MessageBoxResult.No)
            {

                // Do something else
            }

            ///////////////////////////////////////////////
          //  MainWindow mainWindow = new MainWindow();
          //  mainWindow.Show();
            ///////////////////////////////////
          //  this.Close();
        }

        public static void updateName(TextBox txtMazeName)
        {
            str = txtMazeName.ToString();
            // str.Substring(32, str.Length - 32);

            System.Console.WriteLine("in the single player:" + str);

        }
    }
}