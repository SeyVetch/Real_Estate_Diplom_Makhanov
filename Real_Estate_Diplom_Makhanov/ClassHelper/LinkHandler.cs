using Server.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Diplom_Makhanov.ClassHelper
{
    internal class LinkHandler
    {
        static string adr = "http://127.0.0.1:9999";
        static IPAddress address = IPAddress.Parse("127.0.0.1");
        static IPEndPoint port = new IPEndPoint(address, 9999);

        static public Dictionary<string, byte[]?> images = new Dictionary<string, byte[]>();

        static Client client;
        private static bool connect(IPEndPoint p)
        {
            client = new Client(p);
            return client._isConnected;
        }
        public static byte[] GetImage(string url)
        {
            if (images.Keys.Contains(url))
            {
                return images[url];
            }
            bool connected = connect(port);
            if (!connected)
            {
                return null;
            }
            byte[] response = client.GetImage("/Image/" + url);
            images.Add(url, response);
            return response;
        }
        public static UserClass? LogIn(string log, string pas)
        {
            bool connected = connect(port);
            if (!connected)
            {
                return null;
            }
            string parameters = "/log=" + log + "&" + "pas=" + pas;
            string link = "/Login" + parameters;
            string response = client.GetJson(link);
            if (response == "")
            {
                return null;
            }
            UserClass res = new UserClass(response);
            return res;
        }
        public static bool LogOut()
        {
            bool connected = connect(port);
            if (!connected)
            {
                return false;
            }
            string link = "/Logout";
            string response = client.GetJson(link);
            if (response == "")
            {
                return false;
            }
            return true;
        }
        public static bool TryLogIn(string log, string pas)
        {
            bool connected = connect(port);
            string parameters = "/log=" + log + "&" + "pas=" + pas;
            string link = "/TryLogin" + parameters;
            string res = client.GetJson(link);
            if (res == "")
            {
                return false;
            }
            res = res.Substring(1, res.Length - 2).Split(':')[1];
            return Convert.ToBoolean(res.Substring(1, res.Length - 2));
        }
        public static bool LoginExists(string log)
        {
            bool connected = connect(port);
            string parameters = "/log=" + log;
            string link = "/LoginExists" + parameters;
            string res = client.GetJson(link);
            if (res == "")
            {
                return false;
            }
            res = res.Substring(1, res.Length - 2).Split(':')[1];
            return Convert.ToBoolean(res.Substring(1, res.Length - 2));
        }
        public static PropertyPreview[]? GetPreviews(string transactionType)
        {
            bool connected = connect(port);
            string parameters = "/mode=" + "Standart" + "&" + "type=" + transactionType;
            string link = "/PropPreviews" + parameters;
            string res = client.GetJson(link);
            if (res == "")
            {
                return null;
            }
            return PropertyPreview.arrayFromJson(res);
        }
        public static PropertyPreview[]? GetClientProperty(string idClient, bool owner)
        {
            bool connected = connect(port);
            string transactionType;
            if (owner)
            {
                transactionType = "Owner";
            }
            else
            {
                transactionType = "Client";
            }
            string parameters = "/mode=" + "ClientP" + "&" + "type=" + transactionType + "&" + "Id=" + idClient;
            string link = "/PropPreviews" + parameters;
            string res = client.GetJson(link);
            if (res == "")
            {
                return null;
            }
            return PropertyPreview.arrayFromJson(res);
        }
        public static PropertyPreview? GetPropertyPreview(string id)
        {
            bool connected = connect(port);
            string parameters = "/id=" + id;
            string link = "/GetPropertyPreview" + parameters;
            string res = client.GetJson(link);
            if (res == "")
            {
                return null;
            }
            return new PropertyPreview(res);
        }
        public static PropertyClass? GetProperty(string id)
        {
            bool connected = connect(port);
            string parameters = "/id=" + id;
            string link = "/GetProperty" + parameters;
            string res = client.GetJson(link);
            if (res == "")
            {
                return null;
            }
            return new PropertyClass(res);
        }
        public static ClientClass? GetClient(string id)
        {
            if (id == "")
            {
                return null;
            }
            bool connected = connect(port);
            string parameters = "/id=" + id;
            string link = "/GetClient" + parameters;
            string res = client.GetJson(link);
            if (res == "")
            {
                return null;
            }
            return new ClientClass(res);
        }
        public static ClientClass[]? GetClients()
        {
            bool connected = connect(port);
            string link = "/GetClients";
            string res = client.GetJson(link);
            if (res == "")
            {
                return null;
            }
            return ClientClass.arrayFromJson(res);
        }
        public static TransactionClass[]? GetTransactions()
        {
            bool connected = connect(port);
            string link = "/GetTransactions";
            string res = client.GetJson(link);
            if (res == "")
            {
                return null;
            }
            return TransactionClass.arrayFromJson(res);
        }
        public static bool CreateTransaction(PropertyClass p, ClientClass c)
        {
            bool connected = connect(port);
            if (connected)
            {
                string parameters = "/Pid=" + p.IdProperty + "&Cid=" + c.IdClient + "&Log=" + MainWindow.userLogin;
                string link = "/CreateTransaction" + parameters;
                string res = client.GetJson(link);
                if (res == "")
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
