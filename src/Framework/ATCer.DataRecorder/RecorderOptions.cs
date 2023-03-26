// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using SimpleUdp;

namespace ATCer.DataRecorder
{
    public class RecorderOptions
    {
        /// <summary>
        /// The class name of the recorder
        /// </summary>
        public string RecorderName { get; set; }
        /// <summary>
        /// Default is 127.0.0.1
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// Default is 9999
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// Default is UTF8
        /// </summary>
        public DataEncodings Encoding { get; set; } = DataEncodings.UTF8;
        /// <summary>
        /// Set type of udp endpoint
        /// </summary>
        public EndpointType EndpointType { get; set; } = EndpointType.Unicast;
        public Action<Datagram>? JobAction { get; set; }
        public RecorderOptions()
        {
            Ip = "127.0.0.1";
            Port = 9999;
            Encoding = DataEncodings.UTF8;
            RecorderName = Guid.NewGuid().ToString("N");
        }
    }
}
