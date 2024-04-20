using Real_Estate_Diplom_Makhanov.ClassHelper;
using Server.Classes;
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

namespace Real_Estate_Diplom_Makhanov.Pages
{
    /// <summary>
    /// Логика взаимодействия для TransactionPage.xaml
    /// </summary>
    public partial class TransactionPage : Page
    {
        Frame CurPage;
        TransactionClass[]? transactions;
        int orderNum = 1;
        bool orderInc = true;
        bool rentable;
        public TransactionPage(Frame curPage, bool rentable)
        {
            InitializeComponent();
            transactions = LinkHandler.GetTransactions();
            LvTransactions.ItemsSource = transactions;
            CurPage = curPage;
            this.rentable = rentable;
            Sort();
        }

        private void LvTransactions_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TransactionClass ts = LvTransactions.SelectedItem as TransactionClass;
            CurPage.Content = new TransactionViewPage(ts, CurPage);
        }
        
        public void Sort()
        {
            TransactionClass[] ts;
            if (TBPrice.Text != "")
            {
                ts = transactions.Where(i => i.ClientName.ToLower().Contains(TBClientName.Text.ToLower()) &&
                                             i.AgentName.ToLower().Contains(TBAgentName.Text.ToLower()) &&
                                             i.PropertyName.ToLower().Contains(TBProperty.Text.ToLower()) &&
                                             i.ClosureDate.ToLower().Contains(TBDateClosure.Text.ToLower()) &&
                                             Convert.ToDecimal(i.Price.Split(' ')[0]) <=
                                             Convert.ToDecimal(TBPrice.Text)).ToArray();
            }
            else
            {
                ts = transactions.Where(i => i.ClientName.ToLower().Contains(TBClientName.Text.ToLower()) &&
                                             i.AgentName.ToLower().Contains(TBAgentName.Text.ToLower()) &&
                                             i.PropertyName.ToLower().Contains(TBProperty.Text.ToLower()) &&
                                             i.ClosureDate.ToLower().Contains(TBDateClosure.Text.ToLower())).ToArray();
            }
            if (rentable)
            {
                ts = ts.Where(i => i.IdRP > -1).ToArray();
            }
            else
            {
                ts = ts.Where(i => i.IdPP > -1).ToArray();
            }
            switch (orderNum)
            {
                case 1:
                    if (orderInc)
                    {
                        ts = ts.OrderBy(i => i.ClientName).ToArray();
                    }
                    else
                    {
                        ts = ts.OrderByDescending(i => i.ClientName).ToArray();
                    }
                    break;
                case 2:
                    if (orderInc)
                    {
                        ts = ts.OrderBy(i => i.PropertyName).ToArray();
                    }
                    else
                    {
                        ts = ts.OrderByDescending(i => i.PropertyName).ToArray();
                    }
                    break;
                case 3:
                    if (orderInc)
                    {
                        ts = ts.OrderBy(i => i.AgentName).ToArray();
                    }
                    else
                    {
                        ts = ts.OrderByDescending(i => i.AgentName).ToArray();
                    }
                    break;
                case 4:
                    if (orderInc)
                    {
                        ts = ts.OrderBy(i => Convert.ToDateTime(i.ClosureDate)).ToArray();
                    }
                    else
                    {
                        ts = ts.OrderByDescending(i => Convert.ToDateTime(i.ClosureDate)).ToArray();
                    }
                    break;
                case 5:
                    if (orderInc)
                    {
                        ts = ts.OrderBy(i => Convert.ToInt32(i.Price.Split(' ')[0])).ToArray();
                    }
                    else
                    {
                        ts = ts.OrderByDescending(i => Convert.ToInt32(i.Price.Split(' ')[0])).ToArray();
                    }
                    break;
            }
            LvTransactions.ItemsSource = ts;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sort();
        }

        private void BtnClUp_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 1;
            orderInc = true;
            Sort();
        }

        private void BtnClDown_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 1;
            orderInc = false;
            Sort();
        }

        private void BtnPtUp_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 2;
            orderInc = true;
            Sort();
        }

        private void BtnPtDown_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 2;
            orderInc = false;
            Sort();
        }

        private void BtnAgUp_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 3;
            orderInc = true;
            Sort();
        }

        private void BtnAgDown_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 3;
            orderInc = false;
            Sort();
        }

        private void BtnPrUp_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 4;
            orderInc = true;
            Sort();
        }

        private void BtnPrDown_Click(object sender, RoutedEventArgs e)
        {
            orderNum = 5;
            orderInc = true;
            Sort();
        }

        private void TB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Sort();
        }
    }
}
