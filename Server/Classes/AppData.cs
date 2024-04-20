using Server.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Classes
{
    internal class AppData
    {
        public static Entities context { get; } = new Entities();
    }
}
