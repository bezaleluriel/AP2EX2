using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf
{
    class ViewModelMultiPlayerMenu : ViewModelBase
    {
        private ModelMultiPlayerMenu model;

        public ViewModelMultiPlayerMenu(ModelMultiPlayerMenu model)
        {
            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        public string MazeName
        {
            get { return model.MazeName; }
            set
            {
                model.MazeName = value;
                NotifyPropertyChanged("MazeName");
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

        public ObservableCollection<string> GamesList
        {
            get { return model.GamesList; }
            set
            {
                model.GamesList = value;
                NotifyPropertyChanged("GamesList");
            }
        }

        public void list()
        {
            string command = "list";
            model.start(command);
        }

        public void start()
        {
            string command = "start ";
            command += MazeName;
            command += " ";
            command += MazeRows;
            command += " ";
            command += MazeCols;
            model.start(command);

        }
    }
}
