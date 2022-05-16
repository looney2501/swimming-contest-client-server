using System;
using System.Collections.Generic;
using Model.Domain.DTOs;

namespace Model.Protocol.Responses;

[Serializable]
public class FindAllSwimmersDetailsForRaceResponse : IResponse
{
    public FindAllSwimmersDetailsForRaceResponse(List<SwimmerDTO> allSwimmersDetailsForRace)
    {
        AllSwimmersDetailsForRace = allSwimmersDetailsForRace;
    }

    public List<SwimmerDTO> AllSwimmersDetailsForRace { get; }
}