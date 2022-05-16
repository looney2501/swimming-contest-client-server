using System;
using System.Collections.Generic;
using Model.Domain.DTOs;

namespace Model.Protocol.Responses;

[Serializable]
public class FindAllRacesDetailsResponse : IResponse
{
    public FindAllRacesDetailsResponse(List<RaceDTO> allRacesDetails)
    {
        AllRacesDetails = allRacesDetails;
    }

    public List<RaceDTO> AllRacesDetails { get; }
}