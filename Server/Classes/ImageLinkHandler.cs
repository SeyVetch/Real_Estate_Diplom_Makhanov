using Server.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Classes
{
    internal class ImageLinkHandler
    {
        public static byte[]? HandleImageRequest(string url)
        {
            Uri imageSource = new Uri("C:/Users/4797588/source/repos/Real_Estate_Diplom_Makhanov/Server/Images/" + url, UriKind.Absolute);
            Console.WriteLine(imageSource.ToString());
            byte[]? result;
            try
            {
                result = File.ReadAllBytes("C:/Users/4797588/source/repos/Real_Estate_Diplom_Makhanov/Server/Images/" + url);
            }
            catch 
            {
                result = File.ReadAllBytes("C:/Users/4797588/source/repos/Real_Estate_Diplom_Makhanov/Server/Images/" + "img1.jpg"); ;
            }
            return result;
        }
    }
}
