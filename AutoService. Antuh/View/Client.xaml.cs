using AutoService.Antuh.Model;
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

namespace AutoService.Antuh.View
{
    /// <summary>
    /// Логика взаимодействия для Client.xaml
    /// </summary>
    public partial class Client : Window
    {
        public Client()
        {
            InitializeComponent();
        }
        Entities m = new Entities();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var query =
            from product in m.Product
            from suppliers in m.Suppliers
            select new { product.ProductName, product.ProductDescription, suppliers.Supplier, product.ProductCost };

            dataGrid1.ItemsSource = query.ToList();

            var query2 =
            from product in m.Product
            select new { product.ProductDiscountAmount };

            dataGrid2.ItemsSource = query2.ToList();
            //DataGridRow row = (DataGridRow)dataGrid1.ItemContainerGenerator.ContainerFromIndex(0);
            //row.Background = Brushes.Aqua;
        }
    }
}



