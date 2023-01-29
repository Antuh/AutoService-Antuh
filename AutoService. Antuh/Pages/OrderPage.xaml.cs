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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoService.Antuh.View;
using AutoService.Antuh.Model;



namespace AutoService.Antuh.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        List<Product> productList = new List<Product>();
        public OrderPage(List<Product> products, User user)
        {
            InitializeComponent();
            DataContext = this; // Привязываем контекст к данному классу
            productList = products; // Передача списка товаров в пустой лист
            lViewOrder.ItemsSource = productList; // Вывод товаров в ListView

            cmbPickupPouint.ItemsSource = Entities.GetContex().PickupPoint.ToList();//Ввод пунктов выдачи в список

            if (user != null) // Проверка на наличие пользователя
                textUser.Text = user.UserSurname.ToString() + " " + user.UserName.ToString() + " " + user.UserPatronymic.ToString();
        }

        public string Total
        {
            get
            {
                var total = productList.Sum(x => Convert.ToDouble(x.ProductCost) - Convert.ToDouble(x.ProductCost) * Convert.ToDouble(x.ProductDiscountAmount / 100.00));
                return total.ToString();
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот элемент?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                productList.Remove(lViewOrder.SelectedItems as Product);
        }

        private void btnOrderSave_Click(object sender, RoutedEventArgs e)
        {
            var productArticle = productList.Select(x => x.ProductArticleNumber).ToArray(); // Производим поиск товаров по артикулу, добавляя каждый отдельным элементом массива 
            Random random = new Random(); //Для случайного числа в коде получения
            var date = DateTime.Now; //Добавляем переменную, в которой хранится сегодняшняя дата
            if (productList.Any(x => x.ProductQuantityInStock < 3))
                date = date.AddDays(6);
            else                        //В зависимости от количества товара
                date = date.AddDays(3); //назначется время доставки

            if (cmbPickupPouint.SelectedItem == null)
            {   // Проверка на возможность добавления в бд без выбранного пункта выдачи
                MessageBox.Show("Выберите пункт выдачи!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                Order newOrder = new Order()
                {
                    IDOrderStatus = 1,
                    OrderDate = DateTime.Now,
                    OrderPickupPoint = cmbPickupPouint.SelectedIndex + 1,
                    OrderDeliveryDate = date,
                    ReceiptCode = random.Next(100, 1000),
                    ClientFullName = textUser.Text
                };
                Entities.GetContex().Order.Add(newOrder); //Передаём добавленные данные в таблицу Order

                for (int i = 0; i < productArticle.Count(); i++)  // Счётчик, который будет добавлять записи до того как не закончатся артикулы
                {
                    OrderProduct newOrderProduct = new OrderProduct()
                    {
                        OrderID = newOrder.OrderID,
                        ProductArticleNumber = productArticle[i],
                        CountProduct = 1
                    };
                    Entities.GetContex().OrderProduct.Add(newOrderProduct); //Передаём параметры доя созранения в базу
                }
                Entities.GetContex().SaveChanges();//Сохраняем записи в БД
                MessageBox.Show("Заказ Оформлен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new OrderTicketPage(newOrder, productList)); //Переходим на страницу талона заказа
                //< ListView x: Name = "lViewOrder" d: ItemsSource = "{d:SampleData ItemCount=1}" >
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
