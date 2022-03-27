using System.Collections.Generic;
using System.Linq;

namespace MPP_lab_project.Domain.DTOs;

public class SwimmerDTO
{
    private Swimmer _swimmer { get; }
    private List<RaceDetailsDTO> _raceDetailsDTOs { get; }

    public string FirstName => _swimmer.FirstName;

    public string LastName => _swimmer.LastName;

    public int Age => _swimmer.Age;

    public string Races
    {
        get
        {
            return _raceDetailsDTOs.Aggregate("", (subtotal, element) => subtotal + element);
        }
    }

    public SwimmerDTO(Swimmer swimmer, List<RaceDetailsDTO> raceDetailsDtOs)
    {
        _swimmer = swimmer;
        _raceDetailsDTOs = raceDetailsDtOs;
    }
}