using System;
using Model.Domain.Enums;

namespace Model.Domain.DTOs;

[Serializable]
public class RaceDTO
{
    public SwimmingDistances SwimmingDistance { get; }
    public SwimmingStyles SwimmingStyle { get; }
    public int NoSwimmers { get; }

    public RaceDTO(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle, int noSwimmers)
    {
        SwimmingDistance = swimmingDistance;
        SwimmingStyle = swimmingStyle;
        NoSwimmers = noSwimmers;
    }
}