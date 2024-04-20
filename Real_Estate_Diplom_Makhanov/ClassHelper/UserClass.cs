using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Diplom_Makhanov.ClassHelper
{
    public class UserClass
    {
        public int IsAdmin { get; set; }
        public string Name { get; set; }
        public UserClass(string Json)
        {
            string[] args = Json.Substring(1, Json.Length - 2).Split(',');
            string id = args[0].Split(':')[1];
            id = id.Substring(1, id.Length - 2);
            IsAdmin = Convert.ToInt32(id);
            string name = args[1].Split(':')[1];
            Name = name.Substring(1, name.Length - 2);
        }
    }
}
