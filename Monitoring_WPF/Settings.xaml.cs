using Monitoring_WPF.ViewModels;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Monitoring_WPF
{

    public partial class Settings : Page
    {
        SettingsViewModel settingsViewModel;
        public Settings()
        {
            InitializeComponent();
            settingsViewModel = new SettingsViewModel();
            DataContext = settingsViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {          
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                settingsViewModel.SettingsModel.Path = openFileDlg.SelectedPath;
            }
        }

        private void TempTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.Source as TextBox;
            textBox.Text = textBox.Text.Replace(',', '.');
        }

        private void Button_Add (object sender, RoutedEventArgs e)
        {
            var button = e.Source as Button;
            button.Focus();

            if ( settingsViewModel.CheckFolder(settingsViewModel.SettingsModel.Path))
            {
                settingsViewModel.SaveProperty();
                this.NavigationService.GoBack();
            }
            else
            {
                ErrorMessageBox("Папка не найдена, пожалуйста проверьте путь", "Ошибка данных");
            }           
        }
        public void ErrorMessageBox(string caption, string messageBoxText)
        {       
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBox.Show(messageBoxText, caption, button, icon);
        }
        private void Count_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = e.Source as TextBox;
            textBox.Text = Regex.Replace(textBox.Text, @"[^\d]", "");
            try
            {
                Convert.ToInt32(textBox.Text);
            }
            catch (ArithmeticException ArithE)
            {
                textBox.Text = int.MaxValue.ToString();
            }
        }
    }
}
