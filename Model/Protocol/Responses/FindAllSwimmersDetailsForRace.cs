using System;
using System.Collections.Generic;
using Model.Domain.DTOs;

namespace Model.Protocol.Responses;

[Serializable]
public class FindAllSwimmersDetailsForRace: IResponse
{
    public List<SwimmerDTO> AllSwimmersDetailsForRace { get; }

    public FindAllSwimmersDetailsForRace(List<SwimmerDTO> allSwimmersDetailsForRace)
    {
        AllSwimmersDetailsForRace = allSwimmersDetailsForRace;
    }
}