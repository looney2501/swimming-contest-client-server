using System;

namespace Model.Domain.Enums;

public enum SwimmingStyles
{
    Mixed,
    Freestyle,
    Butterfly,
    Backstroke
}

public static class SwimmingStylesMethods
{
    public static SwimmingStyles StyleFromInteger(Int32 value) =>
        value switch
        {
            1 => SwimmingStyles.Mixed,
            2 => SwimmingStyles.Freestyle,
            3 => SwimmingStyles.Butterfly,
            4 => SwimmingStyles.Backstroke,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };

    public static Int32 IntegerFromStyle(SwimmingStyles style) =>
        style switch
        {
            SwimmingStyles.Mixed => 1,
            SwimmingStyles.Freestyle => 2,
            SwimmingStyles.Butterfly => 3,
            SwimmingStyles.Backstroke => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(style), style, null)
        };
}

