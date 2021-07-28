using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Monitoring_WPF.Models
{
    class SettingsModel : INotifyPropertyChanged
    {
        private string path;
        private int maxCount;
        private double temperature;

        public string Path
        {
            get { return path; }
            set
            {
                path = value;
                OnPropertyChanged("Path");
            }
        }
        public int MaxCount
        {
            get { return maxCount; }
            set
            {
                if (value == 0)
                    maxCount = int.MaxValue;
                else
                    maxCount = value;
                OnPropertyChanged("MaxCount");
            }
        }
        public double Temperature
        {
            get { return temperature; }
            set
            {
                temperature = Math.Round(value,1);
                OnPropertyChanged("Temperature");
            }
        }

        public SettingsModel()
        {
            path = Properties.Settings.Default.PathToFolder;
            maxCount = Properties.Settings.Default.MaxCount;
            temperature = Properties.Settings.Default.Temp;
        }
        public void SaveSettingsInProperty()
        {
            Properties.Settings.Default.PathToFolder = path ;
            Properties.Settings.Default.MaxCount = maxCount;
            Properties.Settings.Default.Temp = temperature;
            Properties.Settings.Default.Save();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
