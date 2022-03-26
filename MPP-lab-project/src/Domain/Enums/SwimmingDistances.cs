using System;

namespace MPP_lab_project.Domain;

public enum SwimmingDistances
{
    _50m,
    _200m,
    _800m,
    _1500m
}

public static class SwimmingDistancesMethods
{
    public static SwimmingDistances DistanceFromInteger(Int32 value) =>
        value switch 
        {
            1 => SwimmingDistances._50m,
            2 => SwimmingDistances._200m,
            3 => SwimmingDistances._800m,
            4 => SwimmingDistances._1500m,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };

    public static Int32 IntegerFromDistance(SwimmingDistances swimmingDistances) =>
        swimmingDistances switch
        {
            SwimmingDistances._50m => 1,
            SwimmingDistances._200m => 2,
            SwimmingDistances._800m => 3,
            SwimmingDistances._1500m => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(swimmingDistances), swimmingDistances, null)
        };
}
