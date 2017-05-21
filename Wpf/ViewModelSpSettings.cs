using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf;

namespace Wpf
{
    class ViewModelSpSettings : ViewModelBase
    {
        private ModelSPSettings model;
        public ViewModelSpSettings(ModelSPSettings model)
        {
            this.model = model;
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

        public void SaveSettings()
        {
            model.SaveSettings();
        }
    }
}
