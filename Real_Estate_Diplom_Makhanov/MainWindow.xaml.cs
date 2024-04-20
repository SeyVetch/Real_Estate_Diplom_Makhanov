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
using Real_Estate_Diplom_Makhanov.ClassHelper;
using Real_Estate_Diplom_Makhanov.Pages;

namespace Real_Estate_Diplom_Makhanov
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PropertyPreview soldProperty;
        ClientClass clientBuyer;
        bool isAdmin;
        public static string userLogin;
        public MainWindow()
        {
            InitializeComponent();
            BtnBack.Content = "<";
        }
        public MainWindow(string UserName, bool IsAdmin, string login)
        {
            InitializeComponent();
            BtnBack.Content = "<";
            TxtUserName.Text = UserName;
            isAdmin = IsAdmin;
            userLogin = login;
        }
        private void NavRealEstate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RealEstateOptionsProperty.Visibility = Visibility.Visible;
        }

        private void NavClients_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurrentPage.Content = new ClientPage(CurrentPage);
            BtnBack.Visibility = Visibility.Visible;
        }

        private void NavSales_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RealEstateOptionsTransaction.Visibility = Visibility.Visible;
        }

        private void BtnOptionRentProperty_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage.Content = new PropertyPage("Rent", CurrentPage);
            RealEstateOptionsProperty.Visibility = Visibility.Hidden;
            BtnBack.Visibility = Visibility.Visible;
        }

        private void BtnOptionSaleProperty_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage.Content = new PropertyPage("Sale", CurrentPage);
            RealEstateOptionsProperty.Visibility = Visibility.Hidden;
            BtnBack.Visibility = Visibility.Visible;
        }

        private void UserExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LinkHandler.LogOut();
            Window win = new LoginWindow();
            win.Show();
            Close();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentPage.GoBack();
            }
            catch 
            {
                CurrentPage.Content = null;
                CurrentPage.NavigationService.RemoveBackEntry();
                BtnBack.Visibility = Visibility.Hidden;
            }
        }

        private void CurrentPage_Navigated(object sender, NavigationEventArgs e)
        {
            if(CurrentPage.Content is ClientViewPage)
            {

            }
            else
            {

            }
            if(CurrentPage.Content is PropertyViewPage)
            {

            }
            else
            {

            }
            if (isAdmin)
            {
                if (CurrentPage.Content is PropertyPage)
                {

                }
                else
                {

                }
            }
        }

        private void BtnOptionRentTransaction_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage.Content = new TransactionPage(CurrentPage, true);
            RealEstateOptionsTransaction.Visibility = Visibility.Hidden;
            BtnBack.Visibility = Visibility.Visible;
        }

        private void BtnOptionSaleTransaction_Click(object sender, RoutedEventArgs e)
        {
            CurrentPage.Content = new TransactionPage(CurrentPage, false);
            RealEstateOptionsTransaction.Visibility = Visibility.Hidden;
            BtnBack.Visibility = Visibility.Visible;
        }
    }
}