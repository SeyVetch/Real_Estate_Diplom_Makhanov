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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Real_Estate_Diplom_Makhanov.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientViewPage.xaml
    /// </summary>
    public partial class ClientViewPage : Page
    {
        Frame CurPage;
        ClientClass C;
        public ClientViewPage(ClientClass c, Frame curPage)
        {
            InitializeComponent();
            this.DataContext = c;
            CurPage = curPage;
            TxtLastName.Text = c.LastName;
            TxtFirstName.Text = c.FirstName;
            TxtPatronymic.Text = c.Patronymic;
            TxtGender.Text = c.Gender;
            TxtBirthday.Text = c.Birthday;
            TxtPhone.Text = c.Phone;
            TxtEmail.Text = c.Email;
            TxtOrganisationName.Text = c.OrganisationName;
            TxtINN.Text = c.INN;
            TxtKPP.Text = c.KPP;
            CurPage = curPage;
            C = c;
        }

        private void BtnOwner_Click(object sender, RoutedEventArgs e)
        {
            CurPage.Content = new PropertyPage(C.IdClient, true, CurPage);
        }

        private void BtnBuyer_Click(object sender, RoutedEventArgs e)
        {
            CurPage.Content = new PropertyPage(C.IdClient, false, CurPage);
        }
    }
}
