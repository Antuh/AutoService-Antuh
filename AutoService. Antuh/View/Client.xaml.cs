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
        public Client(string name)
        {
            InitializeComponent();
            Name = name;
            MessageBox.Show(Name);

            var product = Entities.GetContex().Product.ToList();
            LViewProduct.ItemsSource = product;
            DataContext = this;

            textAllAmount.Text = product.Count().ToString();

            UpdateData();
        }
        public string[] SortingList { get; set; } =
        {
            "Без сортировки",
            "Стоимость по возрастанию",
            "Стоимость по убыванию"
        };
        public string[] FilterList { get; set; } =
        {
            "Все диапазоны",
            "0%-9,99%",
            "10%-14,99%",
            "15% и более"
        };
        private void UpdateData()
        {
            var result = Entities.GetContex().Product.ToList(); //Принимаем данные из таблицы product в переменную

            if (cmbSorting.SelectedIndex == 1)// Реализация сортировки 
                result = result.OrderBy(x => x.ProductCost).ToList();
            if (cmbSorting.SelectedIndex == 2)
                result = result.OrderByDescending(x => x.ProductCost).ToList();

            if (cmbFilter.SelectedIndex == 1)//Реализация фильтрации
                result = result.Where(x => x.ProductDiscountAmount >= 0 && x.ProductDiscountAmount < 10).ToList();
            if (cmbFilter.SelectedIndex == 2)
                result = result.Where(x => x.ProductDiscountAmount >= 10 && x.ProductDiscountAmount < 15).ToList();
            if (cmbFilter.SelectedIndex == 3)
                result = result.Where(x => x.ProductDiscountAmount >= 15).ToList();

            result = result.Where(x => x.ProductName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();//Реализация поиска
            LViewProduct.ItemsSource = result; //Передаём результат в ListView

            textResultAmount.Text = result.Count().ToString();
        }
        private void cmbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void textSearch_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateData();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            Authorization f = new Authorization();
            f.Show();
            this.Close();
        }
    }
}



