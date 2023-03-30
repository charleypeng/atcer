// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.UdpMq;

public class MqUdpClient
{
    public int Port { get; }
    public string Ip { get; }

    public MqUdpClient(string ipAddress, 
        int port, 
        string senderName)
    {

    }
}
