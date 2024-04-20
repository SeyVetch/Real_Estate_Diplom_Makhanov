using Real_Estate_Diplom_Makhanov.ClassHelper;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для PropertyViewPage.xaml
    /// </summary>
    public partial class PropertyViewPage : Page
    {
        Frame CurPage;
        PropertyClass P;
        ClientClass C;
        public PropertyViewPage(Frame curPage, string idProperty)
        {
            InitializeComponent();
            CurPage = curPage;
            P = LinkHandler.GetProperty(idProperty);
            PropertyImage.Source = ToImage(P.Photo);
            TxtAdress.Text = P.Adress;
            C = LinkHandler.GetClient(P.IdClient);
            if (C == null)
            {
                PanelClient.Visibility = Visibility.Collapsed;
            }
            else
            {
                TxtClient.Text = C.LastName + " " + C.FirstName + " " + C.Patronymic;
            }
            TxtDescription.Text = P.Description;
            TxtName.Text = P.Name;
            TxtPrice.Text = P.Price;
            TxtPricePerMeter.Text = P.PricePerMeterSqr;
            TxtTags.Text = P.Tags;
            TxtType.Text = P.TypeProperty;
            if (P.L_NL == "LivingProperty")
            {
                LivingPropertyPanel.Visibility = Visibility.Visible;
                TxtSharedArea.Text = P.SharedAreaSquareMeters;
                TxtKitchenArea.Text = P.KitchenAreaSquareMeters;
                TxtLivingArea.Text = P.LivingAreaSquareMeters;
                if (P.HasParking == "1")
                {
                    TxtParking.Text = "Есть";
                }
                else
                {
                    TxtParking.Text = "Нет";
                }
                if (P.HasHeating == "1")
                {
                    TxtHeating.Text = "Есть";
                }
                else
                {
                    TxtHeating.Text = "Нет";
                }
                TxtFloor.Text = P.FloorNumber + " из " + P.FloorsAmount;
                TxtRooms.Text = P.RoomQuantity;
                TxtBathrooms.Text = P.BathroomQuantity;
                TxtCeilingHeight.Text = P.CeilingHeight;
            }
            else
            {
                NonLivingPropertyPanel.Visibility = Visibility.Visible;
                TxtParkingSpaces.Text = P.ParkingSpaces;
                TxtTotalArea.Text = P.AreaSquareMeters;
            }
            if (P.IsAvailible == "0")
            {
                BtnCreateTransaction.Visibility = Visibility.Hidden;
            }
        }
        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private void TxtClient_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CurPage.Content = new ClientViewPage(C, CurPage);
        }

        private void BtnCreateTransaction_Click(object sender, RoutedEventArgs e)
        {
            CurPage.Content = new ClientPage(CurPage, P);
        }
    }
}
