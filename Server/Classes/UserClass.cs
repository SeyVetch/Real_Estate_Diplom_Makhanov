using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.EF;
using static System.Net.Mime.MediaTypeNames;

namespace Server.Classes
{
    internal class UserClass
    {
        public int IsAdmin { get; set; }
        public string Name { get; set; }
        public static User FindUserByEmail(string Email)
        {
            Agent a = AppData.context.Agent.Where(i => i.Email == Email).First();
            return AppData.context.User.Where(i => i.IdUser == a.IdUser).First();
        }
        public UserClass(User u) 
        {
            if (u.IsAdmin)
            {
                IsAdmin = 1;
            }
            else
            {
                IsAdmin = 2;
            }
            Agent a = AppData.context.Agent.Where(i => i.IdUser == u.IdUser).FirstOrDefault();
            if (a == null) 
            {
                Name = u.Login;
            }
            else
            {
                Name = a.LastName + " " + a.FirstName[0] + "." + a.Patronymic[0] + ".";
            }
        }
        public string ToJson()
        {
            string id = "\"IsAdmin\":\"" + IsAdmin + "\",";
            string name = "\"Name\":\"" + Name + "\",";
            return "{" + id + name + "}";
        }
    }
}
