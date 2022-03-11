using System;

namespace MPP_lab_project.Domain;

public enum SwimmingDistances
{
    _50,
    _200,
    _800,
    _1500
}

public static class SwimmingDistancesMethods
{
    public static SwimmingDistances DistanceFromInteger(Int32 value) =>
        value switch 
        {
            1 => SwimmingDistances._50,
            2 => SwimmingDistances._200,
            3 => SwimmingDistances._800,
            4 => SwimmingDistances._1500,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        };

    public static Int32 IntegerFromDistance(SwimmingDistances swimmingDistances) =>
        swimmingDistances switch
        {
            SwimmingDistances._50 => 1,
            SwimmingDistances._200 => 2,
            SwimmingDistances._800 => 3,
            SwimmingDistances._1500 => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(swimmingDistances), swimmingDistances, null)
        };
}
