using Server.EF;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using RealEstate_Diplom_Makhanov.ClassHelper;

namespace Server.Classes
{
    internal class ClientClass
    {
        public int IdClient { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrganisationName { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }

        public ClientClass(Client c)
        {
            IdClient = c.IdClient;
            LastName = c.LastName;
            FirstName = c.FirstName;
            Patronymic = c.Patronymic;
            Gender = AppData.context.Gender.First(i => i.IdGender == c.IdGender).Name;
            Birthday = c.Birthday.ToString();
            Phone = c.Phone;
            Email = c.Email;
            OrganisationName = c.OrganisationName == null ? "" : c.OrganisationName;
            INN = c.INN == null ? "" : c.INN;
            KPP = c.KPP == null ? "" : c.KPP;
        }
        public string ToJson()
        {
            string content = "\"IdClient\":\"" + IdClient + "\"^";
            content += "\"LastName\":\"" + LastName + "\"^";
            content += "\"FirstName\":\"" + FirstName + "\"^";
            content += "\"Patronymic\":\"" + Patronymic + "\"^";
            content += "\"Gender\":\"" + Gender + "\"^";
            content += "\"Birthday\":\"" + Birthday + "\"^";
            content += "\"Phone\":\"" + Phone + "\"^";
            content += "\"Email\":\"" + Email + "\"^";
            content += "\"OrganisationName\":\"" + OrganisationName + "\"^";
            content += "\"INN\":\"" + INN + "\"^";
            content += "\"KPP\":\"" + KPP + "\"";
            return "{" + content + "}";
        }
        public static List<ClientClass> transformList(List<Client> C)
        {
            List<ClientClass> res = new List<ClientClass>();
            foreach (Client c in C)
            {
                res.Add(new ClientClass(c));
            }
            return res;
        }
        public static string ListToJson(List<ClientClass> C)
        {
            string res = "{";
            List<string> cs = new List<string>();
            foreach (ClientClass c in C)
            {
                cs.Add(c.ToJson());
            }
            res += string.Join(";", cs);
            res += "}";
            return res;
        }
    }
}
