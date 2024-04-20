using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RealEstate_Diplom_Makhanov.ClassHelper;
using Server.EF;

namespace Server.Classes
{
    internal class TcpServer
    {
        private TcpListener _server;
        private Boolean _isRunning;
        private int count = 0;
        private int userId = -1;

        public TcpServer(IPEndPoint port)
        {
            _server = new TcpListener(port);
            _server.Start();

            _isRunning = true;

            LoopClients();
        }

        public void LoopClients()
        {
            while (_isRunning)
            {
                // wait for client connection
                if (_server.Pending())
                {
                    TcpClient newClient = _server.AcceptTcpClient();

                    // client found.
                    // create a thread to handle communication
                    Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                    t.Start(newClient);
                }
            }
        }

        public void SendImage(byte[] img, TcpClient client)
        {
            Stream stream = client.GetStream();
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.Unicode);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.Unicode);

            int bufferSize = 1024 * 2; //2MB
            int imgSize = img.Length;
            int packages = imgSize / bufferSize;
            string imgSpecs = bufferSize + " " + imgSize;
            sWriter.WriteLine(imgSpecs);
            sWriter.Flush();

            int pakageNum = Convert.ToInt32(sReader.ReadLine());
            while (pakageNum < packages)
            {
                stream.Write(img, pakageNum * bufferSize, bufferSize);
                stream.Flush();
                Console.WriteLine(pakageNum + " Package sent");

                pakageNum = Convert.ToInt32(sReader.ReadLine());
            }
            count++;
        }

        public void HandleClient(object obj)
        {
            // retrieve client from parameter passed to thread
            TcpClient client = (TcpClient)obj;

            // sets two streams
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.Unicode);
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.Unicode);
            // you could use the NetworkStream to read and write, 
            // but there is no forcing flush, even when requested

            Boolean bClientConnected = true;
            String sData = null;

            bClientConnected = client.Connected;
            // reads from stream
            sData = sReader.ReadLine();

            // shows content on the console.
            Console.WriteLine("Client > " + sData);
            string[] args = sData.Substring(1).Split('/');
            string json = "";
            if (args[0].ToLower() == "login")
            {
                string[] loginData = args[1].Split('&');
                string l = loginData[0].Split('=')[1];
                string p = loginData[1].Split('=')[1];
                User u = AppData.context.User.Where(i => i.Login == l && i.Password == p).FirstOrDefault();
                if (u != null)
                {
                    json = new UserClass(u).ToJson();
                    Console.WriteLine("User " + u.Login + " logged in");
                    userId = u.IdUser;
                }
                else
                {
                    Agent a = AppData.context.Agent.Where(i => i.Email == l).FirstOrDefault();
                    if (a != null)
                    {
                        u = AppData.context.User.Where(i => i.IdUser == a.IdUser).FirstOrDefault();
                        if (u != null)
                        {
                            if (u.Password == p)
                            {
                                json = new UserClass(u).ToJson();
                                Console.WriteLine("User " + u.Login + " logged in");
                                userId = u.IdUser;
                            }
                        }
                    }
                }
                sWriter.WriteLine(json);
                Console.WriteLine("Response > " + json);
            }
            if (args[0].ToLower() == "createtransaction")
            {
                string[] transactionData = args[1].Split('&');
                string p = transactionData[0].Split('=')[1];
                string c = transactionData[1].Split('=')[1];
                string l = transactionData[1].Split('=')[1];
                PropertyTransaction pt = new PropertyTransaction();
                Agent A;
                User u = AppData.context.User.Where(i => i.Login == l).FirstOrDefault();
                if (u != null)
                {
                    A = AppData.context.Agent.Where(i => i.IdUser == u.IdUser).FirstOrDefault();
                }
                else
                {
                    A = AppData.context.Agent.Where(i => i.Email == l).FirstOrDefault();
                }
                if (A != null)
                {
                    int a = A.IdAgent;
                    pt.IdAgent = a;
                }
                pt.DateClosure = DateTime.Now;
                pt.IdClientBuyer = Convert.ToInt32(c);
                int idp = Convert.ToInt32(p);
                RentalProperty rp = AppData.context.RentalProperty.Where(i => i.IdProperty == idp).FirstOrDefault();
                PurchasableProperty pp = AppData.context.PurchasableProperty.Where(i => i.IdProperty == idp).FirstOrDefault();
                if (rp == null)
                {
                    pt.PurchasableProperty = pp;
                }
                else
                {
                    pt.RentalProperty = rp;
                }
                AppData.context.PropertyTransaction.Add(pt);
                AppData.context.SaveChanges();
                json = "{\"successful transaction\"}";
                sWriter.WriteLine(json);
                Console.WriteLine("Response > " + json);
            }
            if (args[0].ToLower() == "logout")
            {
                json = "User logged out";
                User u = AppData.context.User.Where(i => i.IdUser == userId).FirstOrDefault();
                userId = -1;
                Console.WriteLine("User " + u.Login + " logged out");
                Console.WriteLine("Response > " + json);
            }
            if (AppData.context.User.Where(i => i.IdUser == userId).FirstOrDefault() != null)
            {
                if (args[0].ToLower() == "proppreviews")
                {
                    string[] previewData = args[1].Split('&');
                    string mode = previewData[0].Split('=')[1];
                    string transactionType = previewData[1].Split('=')[1];
                    List<PropertyPreview> p;
                    if (mode == "Standart")
                    {
                        if (transactionType == "rent")
                        {
                            p = PropertyPreview.GetRentalProperty();
                        }
                        else
                        {
                            p = PropertyPreview.GetPurchasableProperty();
                        }
                    }
                    else
                    {
                        string owner = previewData[1].Split('=')[1];
                        int id = Convert.ToInt32(previewData[2].Split('=')[1]);
                        List<Property> P = new List<Property>();
                        List<RentalProperty> RP;
                        List<PurchasableProperty> PP;
                        if (owner == "Owner")
                        {
                            RP = AppData.context.RentalProperty.Where(i => i.IdClientOwner == id).ToList();
                            PP = AppData.context.PurchasableProperty.Where(i => i.IdClientOwner == id).ToList();
                        }
                        else
                        {
                            List<PropertyTransaction> trans = AppData.context.PropertyTransaction.Where(i => i.IdClientBuyer == id).ToList();
                            RP = new List<RentalProperty>();
                            PP = new List<PurchasableProperty>();
                            foreach (PropertyTransaction pt in trans)
                            {
                                if (pt.IdPurchasableProperty == null)
                                {
                                    RP.Add(AppData.context.RentalProperty.Where(i => i.IdRentalProperty == pt.IdRentalProperty).FirstOrDefault());
                                }
                                else
                                {
                                    PP.Add(AppData.context.PurchasableProperty.Where(i => i.IdPurchasableProperty == pt.IdPurchasableProperty).FirstOrDefault());
                                }
                            }
                        }
                        foreach (RentalProperty rp in RP)
                        {
                            P.Add(AppData.context.Property.Where(i => i.IdProperty == rp.IdProperty).FirstOrDefault());
                        }
                        foreach (PurchasableProperty pp in PP)
                        {
                            P.Add(AppData.context.Property.Where(i => i.IdProperty == pp.IdProperty).FirstOrDefault());
                        }
                        p = PropertyPreview.transformList(P);
                    }
                    json = PropertyPreview.ListToJson(p);
                    sWriter.WriteLine(json);
                    sWriter.Flush();
                    Console.WriteLine("Response > " + json);
                }
                if (args[0].ToLower() == "getclients")
                {
                    List<ClientClass> c = ClientClass.transformList(AppData.context.Client.ToList());
                    json = ClientClass.ListToJson(c);
                    sWriter.WriteLine(json);
                    sWriter.Flush();
                    Console.WriteLine("Response > " + json);
                }
                if (args[0].ToLower() == "gettransactions")
                {
                    List<TransactionClass> t = TransactionClass.transformList(AppData.context.PropertyTransaction.ToList());
                    json = TransactionClass.ListToJson(t);
                    sWriter.WriteLine(json);
                    sWriter.Flush();
                    Console.WriteLine("Response > " + json);
                }
                if (args[0].ToLower() == "getpropertypreview")
                {
                    int idProperty = Convert.ToInt32(args[1].Split('=')[1]);
                    PropertyPreview p = new PropertyPreview(AppData.context.Property.Where(i => i.IdProperty == idProperty).FirstOrDefault());
                    json = p.ToJson();
                    sWriter.WriteLine(json);
                    sWriter.Flush();
                    Console.WriteLine("Response > " + json);
                }
                if (args[0].ToLower() == "getproperty")
                {
                    int idProperty = Convert.ToInt32(args[1].Split('=')[1]);
                    PropertyClass p = new PropertyClass(AppData.context.Property.Where(i => i.IdProperty == idProperty).FirstOrDefault());
                    json = p.ToJson();
                    sWriter.WriteLine(json);
                    sWriter.Flush();
                    Console.WriteLine("Response > " + json);
                }
                if (args[0].ToLower() == "getclient")
                {
                    int idClient = Convert.ToInt32(args[1].Split('=')[1]);
                    ClientClass c = new ClientClass(AppData.context.Client.Where(i => i.IdClient == idClient).FirstOrDefault());
                    json = c.ToJson();
                    sWriter.WriteLine(json);
                    sWriter.Flush();
                    Console.WriteLine("Response > " + json);
                }
                if (args[0].ToLower() == "image")
                {
                    byte[]? img = ImageLinkHandler.HandleImageRequest(args[1]);
                    if (img == null)
                    {
                        sWriter.WriteLine("null");
                        sWriter.Flush();
                        Console.WriteLine("response > " + "null");
                    }
                    else
                    {
                        SendImage(img, client);
                        Console.WriteLine("response > " + img);
                        sWriter.Flush();
                    }
                }
            }
            count++;

            if (count >= 50)
            {
                Console.Clear();
                count = 0;
            }

            sWriter.Flush();
            client.Close();
        }
    }
}
