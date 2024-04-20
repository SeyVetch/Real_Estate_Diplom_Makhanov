using Real_Estate_Diplom_Makhanov.ClassHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Server.Classes
{
    public class TransactionClass
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
        public TransactionClass(string Json)
        {
            string[] args = Json.Substring(1, Json.Length - 2).Split('^');
            string temp = args[0].Split(':')[1];
            temp = temp.Substring(1, temp.Length - 2);
            IdClient = Convert.ToInt32(temp);
            temp = args[1].Split(':')[1];
            temp = temp.Substring(1, temp.Length - 2);
            if (temp != "")
            {
                IdAgent = Convert.ToInt32(temp);
            }
            temp = args[2].Split(':')[1];
            temp = temp.Substring(1, temp.Length - 2);
            IdProperty = Convert.ToInt32(temp);
            temp = args[3].Split(':')[1];
            temp = temp.Substring(1, temp.Length - 2);
            if (temp != "")
            {
                IdRP = Convert.ToInt32(temp);
                IdPP = -1;
            }
            temp = args[4].Split(':')[1];
            temp = temp.Substring(1, temp.Length - 2);
            if (temp != "")
            {
                IdRP = -1;
                IdPP = Convert.ToInt32(temp);
            }
            temp = args[5].Split(':')[1];
            ClientName = temp.Substring(1, temp.Length - 2);
            temp = args[6].Split(':')[1];
            PropertyName = temp.Substring(1, temp.Length - 2);
            temp = args[7].Split(':')[1];
            AgentName = temp.Substring(1, temp.Length - 2);
            temp = args[8].Split(':')[1];
            ClosureDate = temp.Substring(1, temp.Length - 2);
            temp = args[9].Split(':')[1];
            Price = temp.Substring(1, temp.Length - 2);
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
        public static TransactionClass[] arrayFromJson(string Json)
        {
            if (Json == null || Json.Length == 0) return null;
            string[] args = Json.Substring(1, Json.Length - 2).Split(';');
            TransactionClass[] res = new TransactionClass[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                res[i] = new TransactionClass(args[i]);
            }
            return res;
        }
    }
}
