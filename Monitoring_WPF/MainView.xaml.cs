using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Monitoring_WPF
{

    public partial class MainView : Page
    {
        private MainViewModel model;
        public MainView()
        {
            InitializeComponent();
            model = new MainViewModel();
            DataContext = model;
        }

        private void DataGridInfo_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.BorderThickness = new Thickness(5, 5, 5, 5);
            try
            {
                var personfon = e.Row.Item as PersonInfo;
                if (Convert.ToDouble(personfon.Temp) > Properties.Settings.Default.Temp)
                    e.Row.BorderBrush = new SolidColorBrush(Color.FromRgb(204, 0, 0));
                else
                    e.Row.BorderBrush = new SolidColorBrush(Color.FromRgb(240, 240, 240));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Settings page = new Settings();
            this.NavigationService.Navigate(page);
        }
    }
}