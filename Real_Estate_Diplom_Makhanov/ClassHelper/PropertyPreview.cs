using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Real_Estate_Diplom_Makhanov.ClassHelper
{
    public class PropertyPreview
    {
        string ImageId { get; set; }
        public byte[] Photo { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Price { get; set; }
        public string PricePerMeterSqr { get; set; }
        public string Description { get; set; }
        public string IdProperty { get; set; }

        public PropertyPreview(string Json)
        {
            string[] args = Json.Substring(1, Json.Length - 2).Split('^');
            string name = args[0].Split(':')[1];
            Name = name.Substring(1, name.Length - 2);
            string adress = args[1].Split(':')[1];
            Adress = adress.Substring(1, adress.Length - 2);
            string price = args[2].Split(':')[1];
            Price = price.Substring(1, price.Length - 2);
            string pricePerMeterSqr = args[3].Split(':')[1];
            PricePerMeterSqr = pricePerMeterSqr.Substring(1, pricePerMeterSqr.Length - 2);
            string description = args[4].Split(':')[1];
            Description = description.Substring(1, description.Length - 2);
            string link = args[5].Split(':')[1];
            link = link.Substring(1, link.Length - 2);
            ImageId = link == "null" ? null : link;
            if (ImageId != null)
            {
                Photo = LinkHandler.GetImage(ImageId);
            }
            string idproperty = args[6].Split(':')[1];
            IdProperty = idproperty.Substring(1, idproperty.Length - 2);
        }
        public static PropertyPreview[] arrayFromJson(string Json)
        {
            if (Json == "{}") return new PropertyPreview[0];
            string[] args = Json.Substring(1, Json.Length - 2).Split(';');
            PropertyPreview[] res = new PropertyPreview[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                res[i] = new PropertyPreview(args[i]);
            }
            return res;
        }
    }
}
