// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ATCer.DataRecorder
{
    public class ATCUdpEndpoint
    {
        private readonly UdpClient udpClient;
        private readonly IPEndPoint iPEndPoint;
        /// <summary>
        /// Event to fire when a datagram is received.
        /// </summary>
        public event EventHandler<Datagram> DatagramReceived;
        Thread task;
        bool flag = true;
        public ATCUdpEndpoint(string ipAddress, int port, string senderName = null, CancellationToken cancellationToken = default)
        {
            udpClient = new UdpClient(port);
            udpClient.JoinMulticastGroup(IPAddress.Parse(ipAddress));

            task = new Thread(async () =>
            {
                while (flag)
                {
                    try
                    {
                        if (udpClient.Available <= 0) continue;
                        if (udpClient.Client == null) return;

                        var data = await udpClient.ReceiveAsync(cancellationToken);

                        DatagramReceived?.Invoke(this, new Datagram(ipAddress, port, data.Buffer));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            });
        }

        public void Start()
        {
            flag = true;
            task.Start();
        }

        public void Stop()
        {
            flag = false;
            if (task.ThreadState == ThreadState.Running)
            {
                task.Abort();
            }
            udpClient.Close();
        }

        public void Dispose()
        {
            this.Stop();
            GC.SuppressFinalize(this);
        }

        ~ATCUdpEndpoint()
        {
            this.Dispose();
        }
    }
}
