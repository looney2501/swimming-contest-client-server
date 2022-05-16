using System;

namespace Model.Domain.Enums;

[Serializable]
public enum SwimmingStyle
{
    Mixed,
    Freestyle,
    Butterfly,
    Backstroke
}

public static class SwimmingStylesMethods
{
    public static SwimmingStyle StyleFromInteger(int value)
    {
        return value switch
        {
            1 => SwimmingStyle.Mixed,
            2 => SwimmingStyle.Freestyle,
            3 => SwimmingStyle.Butterfly,
            4 => SwimmingStyle.Backstroke,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };
    }

    public static int IntegerFromStyle(SwimmingStyle style)
    {
        return style switch
        {
            SwimmingStyle.Mixed => 1,
            SwimmingStyle.Freestyle => 2,
            SwimmingStyle.Butterfly => 3,
            SwimmingStyle.Backstroke => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(style), style, null)
        };
    }
}