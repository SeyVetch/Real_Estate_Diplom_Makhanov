using Real_Estate_Diplom_Makhanov.ClassHelper;
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

namespace Real_Estate_Diplom_Makhanov
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            string l = TBLog.Text;
            string p = TBPas.Text;
            UserClass? u = LinkHandler.LogIn(l, p);
            if (u != null)
            {
                Window win = new MainWindow(u.Name, u.IsAdmin == 1, TBLog.Text);
                win.Show();
                Close();
            }
            else
            {
                BtnLog.Background = Brushes.Firebrick;
            }
        }
    }
}
