using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf
{
    class ViewModelMainSettings : ViewModelBase
    {
        private ModelMainSettings model;
        public ViewModelMainSettings(ModelMainSettings model)
        {
            this.model = model;
        }
        public string ServerIP
        {
            get { return model.ServerIP; }
            set
            {
                model.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }
        public int ServerPort
        {
            get { return model.ServerPort; }
            set
            {
                model.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }

        public int MazeRows
        {
            get { return model.MazeRows; }
            set
            {
                model.MazeRows = value;
                NotifyPropertyChanged("MazeRows");
            }
        }

        public int MazeCols
        {
            get { return model.MazeCols; }
            set
            {
                model.MazeCols = value;
                NotifyPropertyChanged("MazeCols");
            }
        }

        public int SearchAlgorithm
        {
            get { return model.SearchAlgorithm; }
            set
            {
                model.SearchAlgorithm = value;
                if (value == 0)
                {
                    model.SearchAlgorithm = 0;
                    Console.WriteLine("BFS!");
                }
                if (value == 1)
                {
                    model.SearchAlgorithm = 1;
                    Console.WriteLine("DFS!");
                }
                NotifyPropertyChanged("SearchAlgorithm");
            }
        }

        public void SaveSettings()
        {
            model.SaveSettings();
        }
    }
}
