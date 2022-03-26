namespace MPP_lab_project.Domain.DTOs;

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