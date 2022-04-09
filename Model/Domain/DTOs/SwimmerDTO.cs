using System;
using System.Collections.Generic;
using System.Linq;
using Model.Domain.Entities;

namespace Model.Domain.DTOs;

[Serializable]
public class SwimmerDTO
{
    private readonly Swimmer _swimmer;
    private readonly List<RaceDetailsDTO> _raceDetailsDTOs;
    public string FirstName => _swimmer.FirstName;
    public string LastName => _swimmer.LastName;
    public int Age => _swimmer.Age;
    public List<RaceDetailsDTO> RaceDetailsDTOs => _raceDetailsDTOs;

    public SwimmerDTO(Swimmer swimmer, List<RaceDetailsDTO> raceDetailsDtOs)
    {
        _swimmer = swimmer;
        _raceDetailsDTOs = raceDetailsDtOs;
    }

    public string Races
    {
        get
        {
            return _raceDetailsDTOs.Aggregate("", (subtotal, element) => subtotal + element);
        }
    }

    public override string ToString()
    {
        return $"{nameof(_swimmer)}: {_swimmer}, {nameof(_raceDetailsDTOs)}: {_raceDetailsDTOs}";
    }
}