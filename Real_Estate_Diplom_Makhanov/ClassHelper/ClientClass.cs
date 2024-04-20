using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Real_Estate_Diplom_Makhanov.ClassHelper
{
    public class ClientClass
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

        public ClientClass(string Json)
        {
            string[] args = Json.Substring(1, Json.Length - 2).Split('^');
            string id = args[0].Split(':')[1];
            id = id.Substring(1, id.Length - 2);
            IdClient = Convert.ToInt32(id);
            string temp = args[1].Split(':')[1];
            LastName = temp.Substring(1, temp.Length - 2);
            temp = args[2].Split(':')[1];
            FirstName = temp.Substring(1, temp.Length - 2);
            temp = args[3].Split(':')[1];
            Patronymic = temp.Substring(1, temp.Length - 2);
            temp = args[4].Split(':')[1];
            Gender = temp.Substring(1, temp.Length - 2);
            temp = args[5].Split(':')[1];
            Birthday = temp.Substring(1, temp.Length - 2);
            temp = args[6].Split(':')[1];
            Phone = temp.Substring(1, temp.Length - 2);
            temp = args[7].Split(':')[1];
            Email = temp.Substring(1, temp.Length - 2);
            temp = args[8].Split(':')[1];
            OrganisationName = temp.Substring(1, temp.Length - 2);
            temp = args[9].Split(':')[1];
            INN = temp.Substring(1, temp.Length - 2);
            temp = args[10].Split(':')[1];
            KPP = temp.Substring(1, temp.Length - 2);
        } 
        public string GetName()
        {
            if (Patronymic != "" && Patronymic != null)
            {
                return LastName + " " + FirstName + " " + Patronymic; 
            }
            return LastName + " " + FirstName;
        } 
        public static ClientClass[] arrayFromJson(string Json)
        {
            string[] args = Json.Substring(1, Json.Length - 2).Split(';');
            ClientClass[] res = new ClientClass[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                res[i] = new ClientClass(args[i]);
            }
            return res;
        }
    }
}
