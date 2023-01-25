using AutoService.Antuh.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public string use;
        public Authorization()
        {
            InitializeComponent();
        }

        private void btn_voity_Click(object sender, RoutedEventArgs e)
        {
            string login = tb_login.Text;
            string password = tb_password.Password;

            Entities m = new Entities();
            var authorization = m.User;
            



            var user = authorization.Where(x => x.UserLogin == login && x.UserPassword == password).FirstOrDefault();
            if (user != null)
            {
                string use = user.UserName;
                int idpost = user.Role.RoleID;
                MessageBox.Show("Авторизация выполнена успешно");
                
                switch (idpost)
                {
                    case 1:
                        var name = use;
                        Client f = new Client(name);
                        f.Show();
                        this.Close();
                        break;
                }
            }
            else
            {
                TimeError f = new TimeError();
                f.Show();
                this.Close();
                
            }
        }
        private void btn_seeclient_Click(object sender, RoutedEventArgs e)
        {
            var name = use;
            Client f = new Client(name);
            f.Show();
            this.Close();
        }

        private void btn_exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}




