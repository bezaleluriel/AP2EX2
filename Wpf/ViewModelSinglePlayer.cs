using MazeLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf
{
    class ViewModelSinglePlayer : ViewModelBase
    {
        ModelSinglePlayer model;

        public Maze maze
        {
            get { return model.Maze; }
            set
            {             
                model.Maze = value;
                NotifyPropertyChanged("maze");
            }
        }

        public string mazeInStr
        {
            get { return model.Maze.ToString(); }
            set
            {
                NotifyPropertyChanged("mazeInStr");
            }
        }

        public string SolutionInStr
        {
            get { return model.SolutionInStr; }
            set
            {
                NotifyPropertyChanged("SolutionInStr");
            }
        }

        public ViewModelSinglePlayer(ModelSinglePlayer model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged(e.PropertyName);
                };
        }

        public void Generate()
        {
            string command = "generate ";
            command += Properties.Settings.Default.MazeName + " ";
            command += Properties.Settings.Default.MazeRows + " ";
            command += Properties.Settings.Default.MazeCols;
            model.start(command);
        }

        public void Solve()
        {
            Console.WriteLine("in view model solve func");
            string command = "solve" + " ";
            command += Properties.Settings.Default.MazeName + " ";
            command += Properties.Settings.Default.SearchAlgorithm + " ";
            Console.Write("the solve command string is:");
            Console.WriteLine(command);
            model.start(command);       
        }


       
    }
}
