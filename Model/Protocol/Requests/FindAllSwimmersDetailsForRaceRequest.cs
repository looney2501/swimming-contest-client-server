using System;
using Model.Domain.DTOs;

namespace Model.Protocol.Requests;

[Serializable]
public class FindAllSwimmersDetailsForRaceRequest : IRequest
{
    public FindAllSwimmersDetailsForRaceRequest(RaceDetailsDTO raceDetailsDto)
    {
        RaceDetailsDTO = raceDetailsDto;
    }

    public RaceDetailsDTO RaceDetailsDTO { get; }
}