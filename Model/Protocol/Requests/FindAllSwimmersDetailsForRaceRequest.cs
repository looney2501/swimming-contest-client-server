using System;
using Model.Domain.DTOs;

namespace Model.Protocol.Requests;

[Serializable]
public class FindAllSwimmersDetailsForRaceRequest: IRequest
{
    public RaceDetailsDTO RaceDetailsDTO { get; }

    public FindAllSwimmersDetailsForRaceRequest(RaceDetailsDTO raceDetailsDto)
    {
        RaceDetailsDTO = raceDetailsDto;
    }
}