using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Security.Permissions;
namespace Yuan.Net.Socket
{
    /// <summary>
    /// 建立一個TCP通訊器(伺服器端)
    /// </summary>
    
    public class Server
    {
        //Event
        [HostProtection(SharedState = true)]
        
        public delegate void ReceiveEvent(object sender, ReceiveEventArgs eventArgs);

        /// <summary>
        /// 當接收到一個封包則會觸發該event。此Event觸發時，屬於背景執行緒，與Windows Form的控制向所處在不同執行緒，請使用Control.Invoke來控制Windows Form的控制項
        /// </summary>
        public  event ReceiveEvent Receive;
        private TcpListener tl;
        private bool stop = false;
        /// <summary>
        /// 建立一個TCP通訊器(伺服器端)
        /// </summary>
        /// <param name="IP">IP位置(需是本機端)，客戶端將使用該IP進行一TCP通訊</param>
        /// <param name="port">所監聽的端口</param>
        public Server(byte[] IP,int port)
        {
            tl = new TcpListener(new IPAddress(IP), port);
            Enable = false;
        }
        public bool Enable { get; set; }
        private Thread thread;
        public void Start()
        {
            Enable = true;
            tl.Start();
            thread = new Thread(() => { Background_Work(); });
            thread.IsBackground = true;
            thread.Start();
            stop = false;
        }
        [STAThread]
        
        private void ReceiveData()
        {
            if (stop)
            {
                Enable = false;
            }
            System.Net.Sockets.Socket socket = tl.AcceptSocket();
            byte[] data = new byte[socket.ReceiveBufferSize];
            socket.Receive(data);
            Receive(this, new ReceiveEventArgs(data, ((IPEndPoint)socket.RemoteEndPoint).Address));
            
        }
        public void Stop()
        {
            stop = true;
            while (Enable)
            {

            }
            tl.Stop();
        }
        private void Background_Work()
        {
            while (Enable)
            {
                if (tl.Pending())
                {
                    ReceiveData();
                }
            }
            Enable = false;
        }
        public class ReceiveEventArgs
        {
            public byte[] Data { get; set; }
            public IPAddress sourceIP { get; set; }
            public ReceiveEventArgs(byte[] Data,IPAddress sourceIP)
            {
                this.Data = Data;
                this.sourceIP = sourceIP;
            }
        }
        ~Server()
        {
            Stop();
        }
    }
    
}