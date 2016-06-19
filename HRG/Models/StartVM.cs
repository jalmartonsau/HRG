using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRG.Models
{

    class StartVM : MainWindow
    {
        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;

        private long _Progress;
        public long Progress
        {
            get { return _Progress; }
            set
            {
                _Progress = value;
                var handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs("Progress"));
            }
        }

        private string _Task;
        public string Task
        {
            get { return _Task; }
            set
            {
                _Task = value;
                var handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs("Task"));
            }
        }

        #endregion

    }
}
