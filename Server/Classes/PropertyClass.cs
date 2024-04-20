using Microsoft.SqlServer.Server;
using Server.EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Classes
{
    public class PropertyClass
    {
        string ImageId { get; set; }
        string Name { get; set; }
        string Adress { get; set; }
        string Price { get; set; }
        string PricePerMeterSqr { get; set; }
        string Description { get; set; }
        string L_NL { get; set; }
        string TypeProperty { get; set; }
        string SharedAreaSquareMeters { get; set; }
        string LivingAreaSquareMeters { get; set; }
        string KitchenAreaSquareMeters { get; set; }
        string RoomQuantity { get; set; }
        string CeilingHeight { get; set; }
        string FloorNumber { get; set; }
        string FloorsAmount { get; set; }
        string HasHeating { get; set; }
        string HasParking { get; set; }
        string BathroomQuantity { get; set; }
        string AreaSquareMeters { get; set; }
        string ParkingSpaces { get; set; }
        string Tags { get; set; }
        string IdClient { get; set; }
        string IdProperty { get; set; }
        string IsAvailible { get; set; }

        public PropertyClass(Property property)
        {
            IdProperty = property.IdProperty.ToString();
            Name = property.Name;
            PropertyView pw = AppData.context.PropertyView.FirstOrDefault(i => i.IdProperty == property.IdProperty);
            Adress = pw.Adress;
            Description = property.Description;
            RentalProperty rp = AppData.context.RentalProperty.FirstOrDefault(i => i.IdProperty == property.IdProperty);
            if (rp != null)
            {
                Price = rp.Rent + " ₽ " + AppData.context.PaymentPeriod.FirstOrDefault(i => i.IdPaymentPeriod == rp.IdPaymentPeriod).Name;
                IdClient = rp.IdClientOwner.ToString();
                if (IdClient == "null")
                {
                    IdClient = "";
                }
                if (AppData.context.PropertyTransaction.FirstOrDefault(i => i.IdRentalProperty == rp.IdRentalProperty) == null)
                {
                    IsAvailible = "1";
                }
                else
                {
                    IsAvailible = "0";
                }
            }
            else
            {
                PurchasableProperty pp = AppData.context.PurchasableProperty.FirstOrDefault(i => i.IdProperty == property.IdProperty);
                Price = pp.Price + " ₽";
                decimal sqrMeter;
                LivingProperty Lp = AppData.context.LivingProperty.FirstOrDefault(i => i.IdProperty == property.IdProperty);
                if (Lp != null)
                {
                    sqrMeter = Lp.KitchenAreaSquareMeters + Lp.LivingAreaSquareMeters + Lp.SharedAreaSquareMeters;
                }
                else
                {
                    NonLivingProperty nlp = AppData.context.NonLivingProperty.FirstOrDefault(i => i.IdProperty == property.IdProperty);
                    sqrMeter = nlp.AreaSquareMeters;
                }
                PricePerMeterSqr = Math.Floor(pp.Price / sqrMeter) + " ₽ за м²";
                IdClient = pp.IdClientOwner.ToString();
                if (IdClient == "null")
                {
                    IdClient = "";
                }
                if (AppData.context.PropertyTransaction.FirstOrDefault(i => i.IdPurchasableProperty == pp.IdPurchasableProperty) == null)
                {
                    IsAvailible = "1";
                }
                else
                {
                    IsAvailible = "0";
                }
            }
            LivingProperty lp = AppData.context.LivingProperty.FirstOrDefault(i => i.IdProperty == property.IdProperty);
            if (lp != null)
            {
                L_NL = "LivingProperty";
                TypeProperty = AppData.context.LivingPropertyType.FirstOrDefault(i => i.IdLivingPropertyType == lp.IdLivingPropertyType).Name;
                SharedAreaSquareMeters = lp.SharedAreaSquareMeters.ToString() + "м²";
                LivingAreaSquareMeters = lp.LivingAreaSquareMeters.ToString() + "м²";
                KitchenAreaSquareMeters = lp.KitchenAreaSquareMeters.ToString() + "м²";
                RoomQuantity = lp.RoomQuantity.ToString();
                CeilingHeight = lp.CeilingHeight.ToString() + "м";
                FloorNumber = lp.FloorNumber.ToString();
                FloorsAmount = lp.FloorsAmount.ToString();
                if (lp.HasHeating)
                {
                    HasHeating = "Есть отопление";
                }
                else
                {
                    HasHeating = "Нет отопления";
                }
                if (lp.HasParking)
                {
                    HasParking = "Есть парковка";
                }
                else
                {
                    HasParking = "Нет  парковки";
                }
                BathroomQuantity = lp.BathroomQuantity.ToString();
            }
            else
            {
                NonLivingProperty nlp = AppData.context.NonLivingProperty.FirstOrDefault(i => i.IdProperty == property.IdProperty);
                L_NL = "NonLivingProperty";
                TypeProperty = AppData.context.NonLivingPropertyType.FirstOrDefault(i => i.IdNonLivingProperty == nlp.IdNonLivingPropertyType).Name;
                AreaSquareMeters = nlp.AreaSquareMeters.ToString() + " м²";
                ParkingSpaces = nlp.ParkingSpaces.ToString();
            }
            ImageId = AppData.context.PropertyImage.FirstOrDefault().Image;
        }
        public PropertyClass(string Json)
        {
            string[] args = Json.Substring(1, Json.Length - 2).Split('^');
            Dictionary<string, string> res = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                string key = arg.Split(':')[0];
                key = key.Substring(1, key.Length - 2);
                string val = arg.Split(':')[1];
                val = val.Substring(1, val.Length - 2);
                res[key] = val;
            }
            ImageId = res["ImageId"];
            Name = res["Name"];
            Adress = res["Adress"];
            Price = res["Price"];
            PricePerMeterSqr = res["PricePerMeterSqr"];
            Description = res["Description"];
            L_NL = res["L_NL"];
            TypeProperty = res["Type"];
            SharedAreaSquareMeters = res["SharedAreaSquareMeters"];
            LivingAreaSquareMeters = res["LivingAreaSquareMeters"];
            KitchenAreaSquareMeters = res["KitchenAreaSquareMeters"];
            RoomQuantity = res["RoomQuantity"];
            CeilingHeight = res["CeilingHeight"];
            FloorNumber = res["FloorNumber"];
            FloorsAmount = res["FloorsAmount"];
            HasHeating = res["HasHeating"];
            HasParking = res["HasParking"];
            BathroomQuantity = res["BathroomQuantity"];
            AreaSquareMeters = res["AreaSquareMeters"];
            ParkingSpaces = res["ParkingSpaces"];
            Tags = res["Tags"];
            IdClient = res["IdClient"];
            IdProperty = res["IdProperty"];
            IsAvailible = res["IsAvailible"];
        }
        public Dictionary<string, string> ToDict()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            res.Add("ImageId", ImageId);
            res.Add("Name", Name);
            res.Add("Adress", Adress);
            res.Add("Price", Price);
            res.Add("PricePerMeterSqr", PricePerMeterSqr);
            res.Add("Description", Description);
            res.Add("L_NL", L_NL);
            res.Add("Type", TypeProperty);
            res.Add("SharedAreaSquareMeters", SharedAreaSquareMeters);
            res.Add("LivingAreaSquareMeters", LivingAreaSquareMeters);
            res.Add("KitchenAreaSquareMeters", KitchenAreaSquareMeters);
            res.Add("RoomQuantity", RoomQuantity);
            res.Add("CeilingHeight", CeilingHeight);
            res.Add("FloorNumber", FloorNumber);
            res.Add("FloorsAmount", FloorsAmount);
            res.Add("HasHeating", HasHeating);
            res.Add("HasParking", HasParking);
            res.Add("BathroomQuantity", BathroomQuantity);
            res.Add("AreaSquareMeters", AreaSquareMeters);
            res.Add("ParkingSpaces", ParkingSpaces);
            res.Add("Tags", Tags);
            res.Add("IdClient", IdClient);
            res.Add("IdProperty", IdProperty);
            res.Add("IsAvailible", IsAvailible);
            return res;
        }
        public string ToJson()
        {
            Dictionary<string, string> dict = ToDict();
            string[] keys = dict.Keys.ToArray();
            string res = "";
            for (int i = 0; i < keys.Length; i++)
            {
                string key = keys[i];
                res += "\"" + key + "\":\"" + dict[key] + "\"";
                if (i < keys.Length - 1)
                {
                    res += "^";
                }
            }
            return "{" + res + "}";
        }
    }
}
