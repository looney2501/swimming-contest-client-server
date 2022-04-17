using System;
using Model.Domain.Enums;

namespace Model.Domain.DTOs;

[Serializable]
public class RaceDTO
{
    public SwimmingDistance SwimmingDistance { get; }
    public SwimmingStyle SwimmingStyle { get; }
    public int NoSwimmers { get; }

    public RaceDTO(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle, int noSwimmers)
    {
        SwimmingDistance = swimmingDistance;
        SwimmingStyle = swimmingStyle;
        NoSwimmers = noSwimmers;
    }
}