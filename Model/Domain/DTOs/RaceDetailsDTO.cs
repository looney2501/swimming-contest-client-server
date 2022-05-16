using System;
using Model.Domain.Enums;

namespace Model.Domain.DTOs;

[Serializable]
public class RaceDetailsDTO
{
    public RaceDetailsDTO(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle)
    {
        SwimmingDistance = swimmingDistance;
        SwimmingStyle = swimmingStyle;
    }

    public SwimmingDistance SwimmingDistance { get; }
    public SwimmingStyle SwimmingStyle { get; }

    public override string ToString()
    {
        return SwimmingDistance + ", " + SwimmingStyle + "; ";
    }
}