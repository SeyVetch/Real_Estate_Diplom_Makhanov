using Server.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Server.Classes
{
    internal class TransactionClass
    {
        public int IdClient { get; set; }
        public int? IdAgent { get; set; }
        public int? IdPP { get; set; }
        public int? IdRP { get; set; }
        public int IdProperty { get; set; }
        public string ClientName { get; set; }
        public string PropertyName { get; set; }
        public string AgentName { get; set; }
        public string ClosureDate { get; set; }
        public string Price { get; set; }
        public TransactionClass(PropertyTransaction t)
        {
            IdClient = t.IdClientBuyer;
            IdAgent = t.IdAgent;
            IdPP = t.IdPurchasableProperty;
            IdRP = t.IdRentalProperty;
            Client c = AppData.context.Client.FirstOrDefault(i => i.IdClient == IdClient);
            Agent? a = AppData.context.Agent.Where(i => i.IdAgent == IdAgent).FirstOrDefault();
            ClientName = c.LastName + " " + c.FirstName + " " + c.Patronymic;
            if (a == null)
            {
                AgentName = "";
            }
            else
            {
                AgentName = a.LastName + " " + a.FirstName + " " + a.Patronymic;
            }
            if (IdRP == null)
            {
                PurchasableProperty pp = AppData.context.PurchasableProperty.First(i => i.IdPurchasableProperty == IdPP);
                IdProperty = pp.IdProperty;
                Price = Math.Floor(pp.Price) + " Р";
            }
            else
            {
                RentalProperty rp = AppData.context.RentalProperty.First(i => i.IdRentalProperty == IdRP);
                IdProperty = rp.IdProperty;
                Price = Math.Floor(rp.Rent) + " Р " + AppData.context.PaymentPeriod.First(i => i.IdPaymentPeriod == rp.IdPaymentPeriod).Name;
            }
            Property p = AppData.context.Property.First(i => i.IdProperty == IdProperty);
            PropertyName = p.Name;
            ClosureDate = t.DateClosure.ToString();
        }
        public string ToJson()
        {
            string content = "\"IdClient\":\"" + IdClient + "\"^";
            content += "\"IdAgent\":\"" + IdAgent + "\"^";
            content += "\"IdProperty\":\"" + IdProperty + "\"^";
            content += "\"IdPRP\":\"" + IdRP + "\"^";
            content += "\"IdPPP\":\"" + IdPP + "\"^";
            content += "\"ClientName\":\"" + ClientName + "\"^";
            content += "\"PropertyName\":\"" + PropertyName + "\"^";
            content += "\"AgentName\":\"" + AgentName + "\"^";
            content += "\"ClosureDate\":\"" + ClosureDate + "\"^";
            content += "\"Price\":\"" + Price + "\"^";
            return "{" + content + "}";
        }
        public static List<TransactionClass> transformList(List<PropertyTransaction> T)
        {
            List<TransactionClass> res = new List<TransactionClass>();
            foreach (PropertyTransaction t in T)
            {
                res.Add(new TransactionClass(t));
            }
            return res;
        }
        public static string ListToJson(List<TransactionClass> T)
        {
            string res = "{";
            List<string> ts = new List<string>();
            foreach (TransactionClass t in T)
            {
                ts.Add(t.ToJson());
            }
            res += string.Join(";", ts);
            res += "}";
            return res;
        }
    }
}
