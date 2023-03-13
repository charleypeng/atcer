// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊
//  CopyRight(C) 2023  版权所有
// -----------------------------------------------------------------------------

using ATCer.DataCenter.Domains;
using ATCer.DataCenter.Domains.MH4016_9;

namespace ATCer.DataCenter;

public class IndexNames
{
    public static string[] Names = new[] {"metdata.press","metdata.pv","metdata.vis"};

    public static Dictionary<string, Type> IndexDict
    {
        get => new Dictionary<string, Type>
        {
            {Names[0], typeof(PRESS)},
            {Names[1], typeof(PV)}
        };
    }
    
}