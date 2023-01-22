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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
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
                int idpost = user.Role.RoleID;
                MessageBox.Show("Авторизация выполнена успешно");
                switch (idpost)
                {
                    case 1:
                        Authorization f = new Authorization();
                        f.Show();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует");
                String allowchar = " ";
                allowchar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
                allowchar += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,y,z";
                allowchar += "1,2,3,4,5,6,7,8,9,0";
                char[] a = { ',' };
                String[] ar = allowchar.Split(a);
                String pwd = " ";
                string temp = " ";
                Random r = new Random();
                for (int i = 0; i < 6; i++)
                {
                    temp = ar[(r.Next(0, ar.Length))];
                    pwd += temp;
                }
                tb_capcha.Text = pwd;
            }
        }
    }
}




