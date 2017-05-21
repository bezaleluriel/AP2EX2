using MazeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Wpf;

namespace Wpf
{
    /// <summary>
    /// Interaction logic for mazeBoard.xaml
    /// </summary>
    public partial class mazeBoard : UserControl
    {
        public mazeBoard()
        {
            InitializeComponent();
        }

        private Rectangle[,] rectArray;

        BitmapImage joint;

        public Maze MazeInStr
        {
            get { return (Maze)GetValue(MazeProperty); }
            set
            {
                SetValue(MazeProperty, value);
            }
        }
        public int Rows {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        public string SolutionInStr
        {
            get { return GetValue(SolutionInStrProperty).ToString(); }
            set { SetValue(SolutionInStrProperty, value); }
        }
        Position startPosition;
        public Position StartPosition
        {
            get { return startPosition; }
            set { startPosition = value; }
        }

        Position endPosition;
        public Position EndPosition
        {
            get { return endPosition; }
            set { endPosition = value; }
        }

        Position currentPosition;
        public Position CurrentPosition
        {
            get { return currentPosition; }
            set { currentPosition = value; }
        }

        
        private string[,] coolorArray;
        public void drawBoard()
        {
            currentPosition = MazeInStr.InitialPos;
            int x = Rows;
            int y = Cols;           
            rectArray = new Rectangle[x,y];
            coolorArray = new string[x, y];
            string dummy = null;
            dummy = MazeInStr.ToString();

            if (dummy == null)
            {
                return;
            }

            int i, j;
            int counter = 0;
            int sizeOfBlock = Math.Min(300/Rows, 300/Cols);
            for (i = 0; i < Rows; i++)
            {
                for (j = 0 ; j < Cols; j++)
                {   
                    Rectangle rect = new Rectangle();
                    rect.Height = sizeOfBlock;
                    rect.Width = sizeOfBlock;
                    rectArray[i, j] = rect;
                    switch (dummy[counter])
                    {
                        case '0':
                            rectArray[i, j].Fill = new SolidColorBrush(Colors.AntiqueWhite);
                            coolorArray[i, j] = "AntiqueWhite";
                            break;
                        case '1':
                            rectArray[i, j].Fill = new SolidColorBrush(Colors.Black);
                            coolorArray[i, j] = "Black";
                            break;
                        case '*':
                            rectArray[i, j].Fill = new SolidColorBrush(Colors.AntiqueWhite);
                            coolorArray[i, j] = "AntiqueWhite";
                            startPosition = currentPosition;

                            break;
                        case '#':
                            rectArray[i, j].Fill = new SolidColorBrush(Colors.Red);
                            coolorArray[i, j] = "Red";
                            endPosition = new Position(i,j);
                            break;
                        case '\n':
                            break;
                        default:
                            Console.WriteLine("error in switch");
                            break;                      
                    }
                    myCanvas.Children.Add(rectArray[i, j]);
                    Canvas.SetLeft(rectArray[i, j], (j * sizeOfBlock));
                    Canvas.SetTop(rectArray[i, j], (i * sizeOfBlock));
                    counter++;
                }
                counter += 2;
            }
            rectArray[currentPosition.Row, currentPosition.Col].Fill = new ImageBrush(joint);
        }

        public void drawMovingSolution()
        {
            rectArray[currentPosition.Row, currentPosition.Col].Fill = new SolidColorBrush(Colors.AntiqueWhite);
            rectArray[endPosition.Row, endPosition.Col].Fill = new SolidColorBrush(Colors.Red);
            rectArray[startPosition.Row, startPosition.Col].Fill = new SolidColorBrush(Colors.GreenYellow);
            currentPosition = MazeInStr.InitialPos;
            for (int i = 0; i < SolutionInStr.Count(); i++)
            {
                myCanvas.Dispatcher.Invoke(delegate { }, DispatcherPriority.Render);
                System.Threading.Thread.Sleep(20);
                rectArray[startPosition.Row, startPosition.Col].Fill = new SolidColorBrush(Colors.GreenYellow);
                myCanvas.Dispatcher.Invoke(delegate { }, DispatcherPriority.Render);
                switch (SolutionInStr[i])
                {
                    case '0'://left
                        rectArray[currentPosition.Row, currentPosition.Col].Fill = new SolidColorBrush(Colors.AntiqueWhite);
                        currentPosition.Col -= 1;
                        rectArray[currentPosition.Row, currentPosition.Col].Fill = new ImageBrush(joint);
                        myCanvas.Dispatcher.Invoke(delegate { }, DispatcherPriority.Render);
                        break;
                    case '1'://right
                        rectArray[currentPosition.Row, currentPosition.Col].Fill = new SolidColorBrush(Colors.AntiqueWhite);
                        currentPosition.Col += 1;
                        rectArray[currentPosition.Row, currentPosition.Col].Fill = new ImageBrush(joint);
                        myCanvas.Dispatcher.Invoke(delegate { }, DispatcherPriority.Render);
                        break;
                    case '2'://up
                        rectArray[currentPosition.Row, currentPosition.Col].Fill = new SolidColorBrush(Colors.AntiqueWhite);
                        currentPosition.Row -= 1;
                        rectArray[currentPosition.Row, currentPosition.Col].Fill = new ImageBrush(joint);
                        myCanvas.Dispatcher.Invoke(delegate { }, DispatcherPriority.Render);
                        break;
                    case '3'://down
                        rectArray[currentPosition.Row, currentPosition.Col].Fill = new SolidColorBrush(Colors.AntiqueWhite);
                        currentPosition.Row += 1;
                        rectArray[currentPosition.Row, currentPosition.Col].Fill = new ImageBrush(joint);
                        myCanvas.Dispatcher.Invoke(delegate { }, DispatcherPriority.Render);
                        break;
                }
            }
        }


        //we need to make there properties a dependency property because
        //user control doesnt do it automaticly and we need them to be dependency for the binding.

        public static readonly DependencyProperty RowsProperty = 
            DependencyProperty.Register("Rows", typeof(int), typeof(mazeBoard), new PropertyMetadata(onRowsPropertyChanged));

        public static readonly DependencyProperty ColsProperty =
        DependencyProperty.Register("Cols", typeof(int), typeof(mazeBoard), new PropertyMetadata(onColsPropertyChanged));

        public static readonly DependencyProperty MazeProperty =
        DependencyProperty.Register("MazeInStr", typeof(Maze), typeof(mazeBoard), new PropertyMetadata(onMazeInStrPropertyChanged));

        public static readonly DependencyProperty SolutionInStrProperty =
        DependencyProperty.Register("SolutionInStr", typeof(string), typeof(mazeBoard), new PropertyMetadata(onSolutionInStrPropertyChanged));

        private static void onColsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //((mazeBoard)d).drawBoard();
        }
        private static void onRowsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //((mazeBoard)d).drawBoard();
        }
        private static void onMazeInStrPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           // ((mazeBoard)d).drawBoard();
        }
        private static void onSolutionInStrPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((mazeBoard)d).drawMovingSolution();
        }




        private void MazeBoard_OnLoaded(object sender, RoutedEventArgs e)
        {
            joint = new BitmapImage(new Uri("pack://application:,,,/Images/joint.jpeg"));
            drawBoard();
        }

        public void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up :
                    try
                    {
                        if (coolorArray[currentPosition.Row - 1, currentPosition.Col] == "Black")
                        {
                            break;
                        }
                        else if (coolorArray[currentPosition.Row - 1, currentPosition.Col] == "Red")
                        {
                            System.Windows.MessageBox.Show("You Won!");
                        }
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                        break;
                    }
                    rectArray[currentPosition.Row, currentPosition.Col].Fill = new SolidColorBrush(Colors.AntiqueWhite);
                    currentPosition.Row -= 1;
                    rectArray[currentPosition.Row, currentPosition.Col].Fill = new ImageBrush(joint);
                    break;
                case Key.Down:
                    try
                    {
                        if (coolorArray[currentPosition.Row + 1, currentPosition.Col] == "Black")
                        {
                            break;
                        }
                        else if (coolorArray[currentPosition.Row + 1, currentPosition.Col] == "Red")
                        {

                            System.Windows.MessageBox.Show("You Won!");
                        }
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                        break;
                    }
                    rectArray[currentPosition.Row, currentPosition.Col].Fill = new SolidColorBrush(Colors.AntiqueWhite);
                    currentPosition.Row += 1;
                    rectArray[currentPosition.Row, currentPosition.Col].Fill = new ImageBrush(joint);
                    break;
                case Key.Right:
                    try
                    {
                        if (coolorArray[currentPosition.Row, currentPosition.Col + 1] == "Black")
                        {
                            break;
                        }
                        else if (coolorArray[currentPosition.Row, currentPosition.Col + 1] == "Red")
                        {

                            System.Windows.MessageBox.Show("You Won!");
                        }
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                        break;
                    }
                    rectArray[currentPosition.Row, currentPosition.Col].Fill = new SolidColorBrush(Colors.AntiqueWhite);
                    currentPosition.Col += 1;
                    rectArray[currentPosition.Row, currentPosition.Col].Fill = new ImageBrush(joint);
                    break;
                case Key.Left:
                    try
                    {
                        if (coolorArray[currentPosition.Row, currentPosition.Col - 1] == "Black")
                        {
                            break;
                        }
                        else if (coolorArray[currentPosition.Row, currentPosition.Col - 1] == "Red")
                        {

                            System.Windows.MessageBox.Show("You Won!");

                        }
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                        break;
                    }
                    rectArray[currentPosition.Row, currentPosition.Col].Fill = new SolidColorBrush(Colors.AntiqueWhite);
                    currentPosition.Col -= 1;
                    rectArray[currentPosition.Row, currentPosition.Col].Fill = new ImageBrush(joint);
                    break;
            }
        }

        public void Restart()
        {
            rectArray[currentPosition.Row, currentPosition.Col].Fill = new SolidColorBrush(Colors.AntiqueWhite);
            currentPosition = startPosition;
             rectArray[startPosition.Row, startPosition.Col].Fill = new ImageBrush(joint);
            rectArray[endPosition.Row, endPosition.Col].Fill = new SolidColorBrush(Colors.Red);
            myCanvas.Dispatcher.Invoke(delegate { }, DispatcherPriority.Render);

        }

       
    }
}

