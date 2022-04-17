using System;
using Model.Domain.Enums;

namespace Model.Domain.DTOs;

[Serializable]
public class RaceDetailsDTO
{
    public SwimmingDistance SwimmingDistance { get; }
    public SwimmingStyle SwimmingStyle { get; }

    public RaceDetailsDTO(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle)
    {
        SwimmingDistance = swimmingDistance;
        SwimmingStyle = swimmingStyle;
    }

    public override string ToString()
    {
        return SwimmingDistance + ", " + SwimmingStyle + "; ";
    }
}