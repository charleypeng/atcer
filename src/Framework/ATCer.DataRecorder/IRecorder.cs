// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.DataRecorder
{
    public interface IRecorder
    {
        event EventHandler<Datagram> DataReceived;
        Datagram RecordData { get; }
        bool IsConnected { get;}
        void Start(Action action = null);
        void Stop();
        void StartWithRetry(int interval);
    }
}
