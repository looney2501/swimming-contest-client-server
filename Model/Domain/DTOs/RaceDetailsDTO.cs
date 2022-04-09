using System;
using Model.Domain.Enums;

namespace Model.Domain.DTOs;

[Serializable]
public class RaceDetailsDTO
{
    public SwimmingDistances SwimmingDistance { get; }
    public SwimmingStyles SwimmingStyle { get; }

    public RaceDetailsDTO(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle)
    {
        SwimmingDistance = swimmingDistance;
        SwimmingStyle = swimmingStyle;
    }

    public override string ToString()
    {
        return SwimmingDistance + ", " + SwimmingStyle + "; ";
    }
}