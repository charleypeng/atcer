using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ATCer.UdpHost
{
    public class ATCUdpEndpoint
    {
        private readonly UdpClient udpClient;
        //private readonly IPEndPoint iPEndPoint;
        Thread task;
        bool flag = true;
        private readonly MqSender mq;
        private CancellationToken _cancellationToken;
        private HostOptions _options;
        public ATCUdpEndpoint(HostOptions options, RabbitMqOptions mqOptions, CancellationToken cancellationToken = default)
        {
            if (options == null) throw new ArgumentNullException("options");
            if (mqOptions == null) throw new ArgumentNullException();

            _options = options;
            _cancellationToken = cancellationToken;
            udpClient = new UdpClient(options.Port);
            udpClient.JoinMulticastGroup(IPAddress.Parse(options.Ip));

            mq = new MqSender(mqOptions, exchange:options.ExchangeName);
            task = new Thread(async () =>
            {
                while (flag)
                {
                    try
                    {
                        if (udpClient.Available <= 0) continue;
                        if (udpClient.Client == null) return;

                        var data = await udpClient.ReceiveAsync(_cancellationToken);
                        var str = Encoding.UTF8.GetString(data.Buffer);
                        this.mq.Send(data.Buffer);
                        //Console.WriteLine(str);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogInfomation(ex.ToString());
                    }
                }
            });
        }
        public void Start()
        {
            Logger.LogInfomation($"正在启动{_options.ExchangeName}");
            flag = true;
            task.Start();
        }

        public void Stop()
        {
            flag = false;
            if(task.ThreadState == ThreadState.Running) 
            {
                task.Abort();
            }
            udpClient.Close();
            Logger.LogInfomation($"{_options.ExchangeName}已经停止");
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
