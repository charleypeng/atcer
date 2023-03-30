using System.Text.Json;

namespace ATCer.UdpHost
{
    internal class Program
    {
        static string filePath = "options.json";
        static void Main(string[] args)
        { 
            var opt = LoadOptions(filePath);

            if (opt.HostOptions != null && opt.HostOptions.Count != 0 && opt.RabbitMqOptions != null)
            {
                foreach (var item in opt.HostOptions)
                {
                    var app = new ATCUdpEndpoint(item, opt.RabbitMqOptions);
                    app.Start();
                }
            }
            else
            {
                Logger.LogInfomation("配置文件中UDP Hosts 为空");
            }
            Console.ReadLine();
        }


        static UdpHostOptions LoadOptions(string filePath) 
        {
            Logger.LogInfomation("读取配置文件");
            if (!File.Exists(filePath))
            {
                Logger.LogInfomation("配置文件不存在，将为你添加新的配置，请更改后重启程序");
                var options = GetNewOptions();
                File.WriteAllText(filePath, JsonSerializer.Serialize(options));
                return options;
            }
            else
            {
                try
                {
                    var fileStr = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<UdpHostOptions>(fileStr)!;
                }
                catch (Exception)
                {
                    Logger.LogInfomation("配置文件解析错误请检查");
                }
                return GetNewOptions();
            }
        }

        static UdpHostOptions GetNewOptions()
        {
            var option = new UdpHostOptions();
            option.HostOptions = new List<HostOptions> { new HostOptions() };
            option.RabbitMqOptions = new RabbitMqOptions();
            return option;
        }
    }

    public static class Logger
    {
        public static void LogInfomation(string info, string type = "INFO")
        {
            Console.WriteLine($"[{type}]:[{DateTime.Now}] {info}");
        }
    }
}