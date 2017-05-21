using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf;

namespace Wpf
{
    public class ModelSPSettings : IModelSettings
    {
        public string MazeName
        {
            get { return Properties.Settings.Default.MazeName; }
            set
            {
                //Console.WriteLine("blablalalalaalaaa");
                Properties.Settings.Default.MazeName = value;
            }
        }
        public int MazeCols
        {
            get { return Properties.Settings.Default.MazeCols; }
            set {
                //Console.WriteLine("blablalalalaalaaa");
                Properties.Settings.Default.MazeCols = value;
                }
        }

        public int MazeRows
        {
            get { return Properties.Settings.Default.MazeRows; }
            set { Properties.Settings.Default.MazeRows = value; }
        }

        public string ServerIP
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int ServerPort
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int SearchAlgorithm
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void SaveSettings()
        {
            /*
            we check if the user dfs in main settings window if not then
            we want to set the search algorithm to be a default of 0 (BFS).
            */            
            if(Properties.Settings.Default.SearchAlgorithm != 1)
            {
                Properties.Settings.Default.SearchAlgorithm = 0;
            }
            Properties.Settings.Default.Save();
        }
    }
}
