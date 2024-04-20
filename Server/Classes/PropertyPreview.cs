using Server.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Server.Classes;
using Microsoft.SqlServer.Server;

namespace RealEstate_Diplom_Makhanov.ClassHelper
{
    internal class PropertyPreview
    {
        string ImageId { get; set; }
        string Name { get; set; }
        string Adress { get; set; }
        string Price { get; set; }
        string PricePerMeterSqr { get; set; }
        string Description { get; set; }
        string IdProperty { get; set; }

        public PropertyPreview(Property property)
        {
            Name = property.Name;
            PropertyView pw = AppData.context.PropertyView.FirstOrDefault(i => i.IdProperty == property.IdProperty);
            Adress = pw.Adress;
            Description = property.Description;
            RentalProperty rp = AppData.context.RentalProperty.FirstOrDefault(i => i.IdProperty == property.IdProperty);
            if (rp != null)
            {
                Price = rp.Rent + " ₽ " + AppData.context.PaymentPeriod.FirstOrDefault(i => i.IdPaymentPeriod == rp.IdPaymentPeriod).Name;
            }
            else
            {
                PurchasableProperty pp = AppData.context.PurchasableProperty.FirstOrDefault(i => i.IdProperty == property.IdProperty);
                Price = pp.Price + " ₽";
                decimal sqrMeter;
                LivingProperty lp = AppData.context.LivingProperty.FirstOrDefault(i => i.IdProperty == property.IdProperty);
                if (lp != null)
                {
                    sqrMeter = lp.KitchenAreaSquareMeters + lp.LivingAreaSquareMeters + lp.SharedAreaSquareMeters;
                }
                else
                {
                    NonLivingProperty nlp = AppData.context.NonLivingProperty.FirstOrDefault(i => i.IdProperty == property.IdProperty);
                    sqrMeter = nlp.AreaSquareMeters;
                }
                PricePerMeterSqr = Math.Floor(pp.Price / sqrMeter) + " ₽ за м²";
            }
            IdProperty = property.IdProperty.ToString();
            ImageId = AppData.context.PropertyImage.FirstOrDefault().Image;
        }
        public string ToJson()
        {
            string name = "\"Name\":\"" + Name + "\"^";
            string adress = "\"Adress\":\"" + Adress + "\"^";
            string price = "\"Price\":\"" + Price + "\"^";
            string ppms = "\"PricePerMeterSqr\":\"" + PricePerMeterSqr + "\"^";
            string description = "\"Description\":\"" + Description + "\"^";
            string imageid = "\"ImageId\":\"" + ImageId + "\"" + "\"^";
            string idproperty = "\"IdProperty\":\"" + IdProperty + "\"";
            return "{" + name + adress + price + ppms + description + imageid + idproperty + "}";
        }
        public static List<PropertyPreview> transformList(List<Property> P)
        {
            List<PropertyPreview> res = new List<PropertyPreview>();
            foreach (Property p in P)
            {
                res.Add(new PropertyPreview(p));
            }
            return res;
        }
        public static string ListToJson(List<PropertyPreview> P)
        {
            string res = "{";
            List<string> ps = new List<string>();
            foreach (PropertyPreview p in P)
            {
                ps.Add(p.ToJson());
            }
            res += string.Join(";", ps);
            res += "}";
            return res;
        }
        public static List<PropertyPreview> GetRentalProperty()
        {
            List<RentalProperty> rps = AppData.context.RentalProperty.ToList();
            List<PropertyPreview> res = new List<PropertyPreview>();
            foreach (RentalProperty rp in rps)
            {
                Property p = AppData.context.Property.First(i => i.IdProperty == rp.IdProperty);
                res.Add(new PropertyPreview(p));
            }
            return res;
        }
        public static List<PropertyPreview> GetPurchasableProperty()
        {
            List<PurchasableProperty> pps = AppData.context.PurchasableProperty.ToList();
            List<PropertyPreview> res = new List<PropertyPreview>();
            foreach (PurchasableProperty pp in pps)
            {
                Property p = AppData.context.Property.First(i => i.IdProperty == pp.IdProperty);
                res.Add(new PropertyPreview(p));
            }
            return res;
        }
    }
}
