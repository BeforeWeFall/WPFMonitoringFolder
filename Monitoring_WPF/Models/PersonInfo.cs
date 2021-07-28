using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Monitoring_WPF
{
    class PersonInfo : INotifyPropertyChanged 
    {
        private byte[] image;
        private string nameAndDate;
        private string temp;
        public string Path { get; set; }
        
        public byte[] Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }
        public string NameAndDate
        {
            get { return nameAndDate; }
            set
            {
                nameAndDate = value;
                OnPropertyChanged("NameAndDate");
            }
        }
        public string Temp
        {
            get { return temp; }
            set
            {
                temp = Round(value);
                OnPropertyChanged("Temp");
            }
        }

        public PersonInfo(byte[] image, string name, string temp, string path)
        {
            Image = image;
            NameAndDate = name;
            Temp = temp;
            Path = path;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private string Round(string value)
        {
            return Math.Round(Convert.ToDouble(value),1).ToString();
        }

    }
}
