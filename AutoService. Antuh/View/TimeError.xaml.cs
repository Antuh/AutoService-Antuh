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
using System.Windows.Threading;

namespace AutoService.Antuh.View
{
    /// <summary>
    /// Логика взаимодействия для TimeError.xaml
    /// </summary>
    public partial class TimeError : Window
    {
        private int time = 15;
        private DispatcherTimer Timer;
        public TimeError()
        {
            InitializeComponent();
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
        }
        void Timer_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                if (time <= 10)
                {
                    if (time % 2 == 0)
                    {
                        tb_count.Foreground = Brushes.Red;
                    }
                    else
                    {
                        tb_count.Foreground = Brushes.White;
                    }
                    time--;
                    tb_count.Text = string.Format("00:0{0}:0{1}", time / 60, time % 60);

                }
                else
                {
                    time--;
                    tb_count.Text = string.Format("00:0{0}:{1}", time / 60, time % 60);
                }
            }
            else
            {
                Timer.Stop();
                MessageBox.Show("Вы можете продолжить попытку авторизации");
                Authorization f = new Authorization();
                f.Show();
                this.Close();
            }
        }
    }
}
