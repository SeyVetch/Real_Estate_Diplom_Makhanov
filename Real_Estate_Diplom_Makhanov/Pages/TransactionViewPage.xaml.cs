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
    /// Логика взаимодействия для TransactionViewPage.xaml
    /// </summary>
    public partial class TransactionViewPage : Page
    {
        Frame CurPage;
        TransactionClass TS;
        public TransactionViewPage(TransactionClass ts, Frame curPage)
        {
            InitializeComponent();
            TxtAgentName.Text = ts.AgentName;
            TxtClientName.Text = ts.ClientName;
            TxtClosureDate.Text = ts.ClosureDate;
            TxtPrice.Text = ts.Price;
            TxtPropertyName.Text = ts.PropertyName;
            TS = ts;
            CurPage = curPage;
        }

        private void TxtClientName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ClientClass c = LinkHandler.GetClient(TS.IdClient.ToString());
            CurPage.Content = new ClientViewPage(c, CurPage);
        }
    }
}
