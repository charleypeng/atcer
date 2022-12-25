// -----------------------------------------------------------------------------
// ATCer 全平台综合性空中交通管理系统
//  作者：彭磊  
//  CopyRight(C) 2022  版权所有 
// -----------------------------------------------------------------------------

using OneOf;

namespace ATCer;

public class StringNumber : OneOfBase<string, int, double>
{
    StringNumber(OneOf<string, int, double> _) : base(_) { }

    // optionally, define implicit conversions
    // you could also make the constructor public
    public static implicit operator StringNumber(string _) => new(_);
    public static implicit operator StringNumber(int _) => new(_);
    public static implicit operator StringNumber(double _) => new(_);

    public (bool isNumber, double number) TryGetNumber() =>
        Match(
            s => (double.TryParse(s, out var n), n),
            i => (true, i),
            d => (true, d)
        );

    public int ToInt32() => Match(
        t0 =>
        {
            string[] strs = t0.Split("px");
            return int.TryParse(strs[0], out var val) ? val : 0;
        },
        t1 => t1,
        t2 => Convert.ToInt32(t2)
        );

    public double ToDouble() => Match(
        t0 =>
        {
            string[] strs = t0.Split("px");
            return double.TryParse(strs[0], out var val) ? val : 0D;
        },
        t1 => Convert.ToDouble(t1),
        t2 => t2
        );

    public override string ToString()
    {
        return Value?.ToString();
    }

    public static bool operator ==(StringNumber left, StringNumber right)
    {
        if (Equals(left, right))
        {
            return true;
        }

        if (left is null || right is null)
        {
            return false;
        }

        return left.Value == right.Value;
    }

    public static bool operator !=(StringNumber left, StringNumber right)
    {
        if (Equals(left, right))
        {
            return false;
        }

        if (left is null || right is null)
        {
            return true;
        }

        return left.Value != right.Value;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}