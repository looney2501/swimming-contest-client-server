using System;

namespace Model.Domain.Enums;

[Serializable]
public enum SwimmingDistance
{
    _50m,
    _200m,
    _800m,
    _1500m
}

public static class SwimmingDistancesMethods
{
    public static SwimmingDistance DistanceFromInteger(Int32 value) =>
        value switch 
        {
            1 => SwimmingDistance._50m,
            2 => SwimmingDistance._200m,
            3 => SwimmingDistance._800m,
            4 => SwimmingDistance._1500m,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };

    public static Int32 IntegerFromDistance(SwimmingDistance swimmingDistance) =>
        swimmingDistance switch
        {
            SwimmingDistance._50m => 1,
            SwimmingDistance._200m => 2,
            SwimmingDistance._800m => 3,
            SwimmingDistance._1500m => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(swimmingDistance), swimmingDistance, null)
        };
}
