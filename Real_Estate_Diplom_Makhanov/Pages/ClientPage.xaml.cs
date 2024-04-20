using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
using Real_Estate_Diplom_Makhanov.ClassHelper;

namespace Real_Estate_Diplom_Makhanov.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientPage.xaml
    /// </summary>
    public partial class ClientPage : Page
    {
        Frame curPage;
        ClientClass[]? clients;
        int orderNum = 1;
        bool orderInc = true;
        PropertyClass p;
        bool creatingTransaction = false;
        public ClientPage(Frame CurPage)
        {
            InitializeComponent();
            clients = LinkHandler.GetClients();
            LvClients.ItemsSource = clients;
            this.curPage = CurPage;
        }
        public ClientPage(Frame curPage, PropertyClass P)
        {
            InitializeComponent();
            clients = LinkHandler.GetClients();
            LvClients.ItemsSource = clients;
            this.curPage = curPage;
            p = P;
            creatingTransaction = true;
        }

        private void LvClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClientClass c = LvClients.SelectedItem as ClientClass;
            if (creatingTransaction)
            {
                MessageBoxResult Result = MessageBox.Show("Вы подтверждаете транзакцию с недвижимостью \"" + p.Name + "\" и клиентом \"" + c.GetName() +"\"", "Подтвердите", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (Result == MessageBoxResult.OK)
                {
                    bool flag = LinkHandler.CreateTransaction(p, c);
                    if (flag)
                    {
                        MessageBox.Show("Операция выполнена успешно", "Уведомление", MessageBoxButton.OK);
                        curPage.Content = new PropertyPage(Convert.ToInt32(c), false, curPage);
                    }
                    else
                    {
                        MessageBox.Show("Операция не была выполнена повторите попытку", "Уведомление", MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                curPage.Content = new ClientViewPage(c, curPage);
            }
        }
        private void Sort()
        {
            ClientClass[] c = clients.Where(i => i.FirstName.ToLower().StartsWith(TBFirstName.Text.ToLower()) &&
                                                 i.LastName.ToLower().StartsWith(TBLastName.Text.ToLower()) &&
                                                 i.Patronymic.ToLower().StartsWith(TBPatronymic.Text.ToLower()) &&
                                                 i.Gender.ToLower().StartsWith(TBGender.Text.ToLower()) &&
                                                 i.Birthday.ToLower().Contains(TBBirthday.Text.ToLower()) &&
                                                 i.Email.ToLower().StartsWith(TBEmail.Text.ToLower()) &&
                                                 i.Phone.ToLower().StartsWith(TBPhone.Text.ToLower())).ToArray();
            switch (orderNum)
            {
                case 1:
                    if (orderInc)
                    {
                        c = c.OrderBy(i => i.LastName).ToArray();
                    }
                    else
                    {
                        c = c.OrderByDescending(i => i.LastName).ToArray();
                    }
                    break;
                case 2:
                    if (orderInc)
                    {
                        c = c.OrderBy(i => i.FirstName).ToArray();
                    }
                    else
                    {
                        c = c.OrderByDescending(i => i.FirstName).ToArray();
                    }
                    break;
                case 3:
                    if (orderInc)
                    {
                        c = c.OrderBy(i => i.Patronymic).ToArray();
                    }
                    else
                    {
                        c = c.OrderByDescending(i => i.Patronymic).ToArray();
                    }
                    break;
                case 4:
                    if (orderInc)
                    {
                        c = c.OrderBy(i => i.Gender).ToArray();
                    }
                    else
                    {
                        c = c.OrderByDescending(i => i.Gender).ToArray();
                    }
                    break;
                case 5:
                    if (orderInc)
                    {
                        c = c.OrderBy(i => Convert.ToDateTime(i.Birthday)).ToArray();
                    }
                    else
                    {
                        c = c.OrderByDescending(i => Convert.ToDateTime(i.Birthday)).ToArray();
                    }
                    break;
            }
            LvClients.ItemsSource = c;
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => Sort();

        private void BtnLNUp_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 1;
            orderInc = true;
            Sort();
        }

        private void BtnLNDown_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 1;
            orderInc = false;
            Sort();
        }

        private void BtnFNUp_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 2;
            orderInc = true;
            Sort();
        }

        private void BtnFNDown_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 2;
            orderInc = false;
            Sort();
        }

        private void BtnPUp_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 3;
            orderInc = true;
            Sort();
        }

        private void BtnPDown_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 3;
            orderInc = false;
            Sort();
        }

        private void BtnGUp_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 4;
            orderInc = true;
            Sort();
        }

        private void BtnGDown_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 4;
            orderInc = false;
            Sort();
        }

        private void BtnBDUp_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 5;
            orderInc = true;
            Sort();
        }

        private void BtnBDDown_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 5;
            orderInc = false;
            Sort();
        }

        private void TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sort();
        }
    }
}
