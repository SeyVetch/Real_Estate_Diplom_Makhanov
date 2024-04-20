using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Diplom_Makhanov.ClassHelper
{
    public class PropertyClass
    {
        public string ImageId { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Price { get; set; }
        public string PricePerMeterSqr { get; set; }
        public string Description { get; set; }
        public string L_NL { get; set; }
        public string TypeProperty { get; set; }
        public string SharedAreaSquareMeters { get; set; }
        public string LivingAreaSquareMeters { get; set; }
        public string KitchenAreaSquareMeters { get; set; }
        public string RoomQuantity { get; set; }
        public string CeilingHeight { get; set; }
        public string FloorNumber { get; set; }
        public string FloorsAmount { get; set; }
        public string HasHeating { get; set; }
        public string HasParking { get; set; }
        public string BathroomQuantity { get; set; }
        public string AreaSquareMeters { get; set; }
        public string ParkingSpaces { get; set; }
        public string Tags { get; set; }
        public string IdClient { get; set; }
        public string IdProperty { get; set; }
        public string IsAvailible { get; set; }

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
            if (ImageId != null)
            {
                Photo = LinkHandler.GetImage(ImageId);
            }
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
                res += "\"" + key + "\":\"" + dict[key];
                if (i < keys.Length - 1)
                {
                    res += "\"^";
                }
            }
            return "{" + res + "}";
        }
    }
}
