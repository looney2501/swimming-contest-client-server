using System;
using Model.Domain.DTOs;

namespace Model.Protocol.Requests;

[Serializable]
public class FindAllSwimmersDetailsForRace: IRequest
{
    public RaceDetailsDTO RaceDetailsDTO { get; }

    public FindAllSwimmersDetailsForRace(RaceDetailsDTO raceDetailsDto)
    {
        RaceDetailsDTO = raceDetailsDto;
    }
}