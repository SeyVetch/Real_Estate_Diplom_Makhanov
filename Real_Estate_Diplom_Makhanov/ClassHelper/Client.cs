using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Real_Estate_Diplom_Makhanov.ClassHelper
{
    public class Client
    {
        private StreamReader _sReader;
        private StreamWriter _sWriter;

        public Boolean _isConnected = true;
        private TcpClient _client;

        public Client(IPEndPoint port)
        {
            _client = new TcpClient();
            _client.Connect(port);

            //HandleCommunication();
        }

        public byte[]? GetImage(string url)
        {
            Stream stream = _client.GetStream();
            _sReader = new StreamReader(_client.GetStream(), Encoding.Unicode);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.Unicode);
            _sWriter.WriteLine(url);
            _sWriter.Flush();

            string imgSpecs = _sReader.ReadLine();
            if (imgSpecs == "null")
            {
                return null;
            }

            string[] args = imgSpecs.Split();
            int imgSize = Convert.ToInt32(args[1]);
            int bufferSize = Convert.ToInt32(args[0]);
            byte[] res = new byte[imgSize];
            int pakages = imgSize / bufferSize;
            int pakageNum = 0;

            while (pakageNum < pakages)
            {
                _sWriter.WriteLine(pakageNum);
                _sWriter.Flush();

                int read = stream.Read(res, pakageNum * bufferSize, bufferSize);
                if (read == bufferSize)
                {
                    pakageNum++;
                }
            }
            _sWriter.WriteLine(pakageNum);
            _sWriter.Flush();
            _client.Close();
            return res;
        }

        public void SendImage(byte[] img)
        {
            Stream stream = _client.GetStream();
            _sReader = new StreamReader(_client.GetStream(), Encoding.Unicode);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.Unicode);

            int bufferSize = 1024 * 2; //2MB
            int imgSize = img.Length;
            int packages = imgSize / bufferSize;
            string imgSpecs = bufferSize + " " + imgSize;
            _sWriter.WriteLine(imgSpecs);
            _sWriter.Flush();

            int pakageNum = Convert.ToInt32(_sReader.ReadLine());
            while (pakageNum < packages)
            {
                stream.Write(img, pakageNum * bufferSize, bufferSize);
                stream.Flush();

                pakageNum = Convert.ToInt32(_sReader.ReadLine());
            }
        }
        public string GetJson(string url)
        {
            _sReader = new StreamReader(_client.GetStream(), Encoding.Unicode);
            _sWriter = new StreamWriter(_client.GetStream(), Encoding.Unicode);

            string res = "";

            _sWriter.WriteLine(url);
            _sWriter.Flush();

            res = _sReader.ReadLine();
            _client.Close();
            return res;
        }
    }
}
