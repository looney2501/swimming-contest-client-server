using System;
using System.Collections.Generic;
using Model.Domain.DTOs;

namespace Model.Protocol.Responses;

[Serializable]
public class FindAllRacesDetailsResponse: IResponse
{
    public List<RaceDTO> AllRacesDetails { get; }

    public FindAllRacesDetailsResponse(List<RaceDTO> allRacesDetails)
    {
        AllRacesDetails = allRacesDetails;
    }
}