namespace MPP_lab_project.Domain.DTOs;

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