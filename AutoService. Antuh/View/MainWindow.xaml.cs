using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using AutoService.Antuh.Model;
using AutoService.Antuh.View;

namespace AutoService.Antuh.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frmMain.Navigate(new Authorization()); //Переход на страницу авторизации
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            frmMain.GoBack();
        }

        private void frmMain_ContentRendered(object sender, System.EventArgs e)
        {
            if (frmMain.CanGoBack)
            {
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBack.Visibility = Visibility.Hidden;
            }
        }
    }
}


