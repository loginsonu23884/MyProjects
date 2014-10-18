using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Config
/// </summary>
public class Config
{
    public enum RuleDataType
    {
        Text ,
        Persent
    }

    public enum ConditionType
    {
        Equals,
        LowerThan,
        GreaterThan,
        LowerThanOrEqual,
        GreaterThanOrEqual
    }
    public enum FontStyleType
    {
        Tahoma,
        Cambria,
        Verdana
    }
    public enum TextAlignmentType
    {
        Left,
        Center,
        Right
    }
}