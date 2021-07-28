using Monitoring_WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Monitoring_WPF.ViewModels
{
    class SettingsViewModel : INotifyPropertyChanged
    {
        
        private SettingsModel settingsModel;
        public SettingsModel SettingsModel
        {
            get => settingsModel;
            set
            {
                settingsModel = value;
                OnPropertyCahnged("SettingsModel");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyCahnged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public SettingsViewModel()
        {
            SettingsModel = new SettingsModel();
        }
        public bool CheckFolder(string path)
        {
            return Directory.Exists(path);
        }
        public void SaveProperty()
        {
            settingsModel.SaveSettingsInProperty();
        }
    }
}