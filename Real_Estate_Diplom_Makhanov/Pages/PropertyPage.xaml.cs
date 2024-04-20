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
    /// Логика взаимодействия для PropertyPage.xaml
    /// </summary>
    public partial class PropertyPage : Page
    {
        Frame CurPage;
        PropertyPreview[] property;
        public PropertyPage(string transactionType, Frame curPage)
        {
            InitializeComponent();
            PropertySale = transactionType == "Sale";
            if (PropertySale)
            {
                PropertyPreview[] p = LinkHandler.GetPreviews("sale");
                LvProperty.ItemsSource = p;
                property = p;
            }
            else
            {
                PropertyPreview[] p = LinkHandler.GetPreviews("rent");
                LvProperty.ItemsSource = p;
                property = p;
            }
            CurPage = curPage;
        }
        public PropertyPage(int idClient, bool owner, Frame curPage)
        {
            InitializeComponent();
            PropertyPreview[] p = LinkHandler.GetClientProperty(idClient.ToString(), owner);
            LvProperty.ItemsSource = p;
            CurPage = curPage;
        }
        public bool PropertySale = true;

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sort();
            if (SearchOverlay.Visibility == Visibility.Visible)
            {
                SearchOverlay.Visibility = Visibility.Collapsed;
            }
            else
            {
                SearchOverlay.Visibility = Visibility.Visible;
            } 
        }

        private void Sort()
        {
            PropertyClass[] P = new PropertyClass[property.Length];
            PropertyPreview[] p = property.Where(i => i.Name.ToLower().Contains(TBName.Text.ToLower()) &&
                                 Convert.ToDecimal(i.Price.Split(' ')[0]) <=
                                 Convert.ToDecimal(TBPrice.Text)).ToArray();
            foreach (PropertyPreview prop in p)
            {
                //P.add()
            }
            if (SearchLP.Visibility == Visibility.Visible)
            {
            }
            else
            {
                p = p.Where().ToArray();
            }
            LvProperty.ItemsSource = p;
        }

        private void LvProperty_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CurPage.Content = new PropertyViewPage(CurPage, ((PropertyPreview)LvProperty.SelectedItem).IdProperty);
        }

        private void BtnSwitchNlpLp_Click(object sender, RoutedEventArgs e)
        {
            if (SearchLP.Visibility == Visibility.Visible)
            {
                SearchLP.Visibility = Visibility.Collapsed;
                SearchNLP.Visibility = Visibility.Visible;
                BtnSwitchNlpLp.Content = "Переключить на жилую недвижимость";
            }
            else
            {
                SearchNLP.Visibility = Visibility.Collapsed;
                SearchLP.Visibility = Visibility.Visible;
                BtnSwitchNlpLp.Content = "Переключить на не жилую недвижимость";
            }
        }
    }
}
