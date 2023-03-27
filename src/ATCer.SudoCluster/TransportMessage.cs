// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2023  版权所有 
// -----------------------------------------------------------------------------

namespace ATCer.SudoCluster;

public readonly struct TransportMessage
{
    public TransportMessage(ReadOnlyMemory<byte> body)
    {
        Body = body;
    }

    public readonly ReadOnlyMemory<byte> Body { get; }

    public byte[] GetBytes()
    {
        return Body.ToArray();
    }
}
