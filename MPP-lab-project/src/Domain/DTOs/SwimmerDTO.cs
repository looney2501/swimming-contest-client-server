using System.Collections.Generic;

namespace MPP_lab_project.Domain.DTOs;

public class SwimmerDTO
{
    public Swimmer Swimmer { get; }
    public List<RaceDetailsDTO> RaceDetailsDTOs { get; }

    public SwimmerDTO(Swimmer swimmer, List<RaceDetailsDTO> raceDetailsDtOs)
    {
        Swimmer = swimmer;
        RaceDetailsDTOs = raceDetailsDtOs;
    }
}