using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Protocol.Protobuf;

public static class ProtoUtils
{
    public static Domain.Enums.SwimmingDistance
        SwimmingDistanceFromProtobuf(SwimmingDistance swimmingDistanceProtobuf)
    {
        return swimmingDistanceProtobuf switch
        {
            SwimmingDistance._50M => Domain.Enums.SwimmingDistance._50m,
            SwimmingDistance._200M => Domain.Enums.SwimmingDistance._200m,
            SwimmingDistance._800M => Domain.Enums.SwimmingDistance._800m,
            SwimmingDistance._1500M => Domain.Enums.SwimmingDistance._1500m,
            _ => throw new ArgumentOutOfRangeException(nameof(swimmingDistanceProtobuf), swimmingDistanceProtobuf, null)
        };
    }

    public static SwimmingDistance SwimmingDistanceToProtobuf(Domain.Enums.SwimmingDistance swimmingDistance)
    {
        return swimmingDistance switch
        {
            Domain.Enums.SwimmingDistance._50m => SwimmingDistance._50M,
            Domain.Enums.SwimmingDistance._200m => SwimmingDistance._200M,
            Domain.Enums.SwimmingDistance._800m => SwimmingDistance._800M,
            Domain.Enums.SwimmingDistance._1500m => SwimmingDistance._1500M,
            _ => throw new ArgumentOutOfRangeException(nameof(swimmingDistance), swimmingDistance, null)
        };
    }

    public static Domain.Enums.SwimmingStyle SwimmingStyleFromProtobuf(SwimmingStyle swimmingStyleProtobuf)
    {
        return swimmingStyleProtobuf switch
        {
            SwimmingStyle.Mixed => Domain.Enums.SwimmingStyle.Mixed,
            SwimmingStyle.Butterfly => Domain.Enums.SwimmingStyle.Butterfly,
            SwimmingStyle.Freestyle => Domain.Enums.SwimmingStyle.Freestyle,
            SwimmingStyle.Backstroke => Domain.Enums.SwimmingStyle.Backstroke,
            _ => throw new ArgumentOutOfRangeException(nameof(swimmingStyleProtobuf), swimmingStyleProtobuf, null)
        };
    }

    public static SwimmingStyle SwimmingStyleToProtobuf(Domain.Enums.SwimmingStyle swimmingStyle)
    {
        return swimmingStyle switch
        {
            Domain.Enums.SwimmingStyle.Mixed => SwimmingStyle.Mixed,
            Domain.Enums.SwimmingStyle.Butterfly => SwimmingStyle.Butterfly,
            Domain.Enums.SwimmingStyle.Freestyle => SwimmingStyle.Freestyle,
            Domain.Enums.SwimmingStyle.Backstroke => SwimmingStyle.Backstroke,
            _ => throw new ArgumentOutOfRangeException(nameof(swimmingStyle), swimmingStyle, null)
        };
    }

    public static Domain.DTOs.AdminDTO AdminDTOFromProtobuf(AdminDTO adminDTOProtobuf)
    {
        return new Domain.DTOs.AdminDTO(adminDTOProtobuf.Username, adminDTOProtobuf.Password);
    }

    public static RaceDTO RaceDTOToProtobuf(Domain.DTOs.RaceDTO raceDTO)
    {
        return new RaceDTO
        {
            SwimmingDistance = SwimmingDistanceToProtobuf(raceDTO.SwimmingDistance),
            SwimmingStyle = SwimmingStyleToProtobuf(raceDTO.SwimmingStyle),
            NoSwimmers = raceDTO.NoSwimmers
        };
    }

    public static Domain.DTOs.RaceDetailsDTO RaceDetailsDTOFromProtobuf(RaceDetailsDTO raceDetailsDTOProtobuf)
    {
        var swimmingDistance =
            SwimmingDistanceFromProtobuf(raceDetailsDTOProtobuf.SwimmingDistance);
        var swimmingStyle = SwimmingStyleFromProtobuf(raceDetailsDTOProtobuf.SwimmingStyle);
        return new Domain.DTOs.RaceDetailsDTO(swimmingDistance, swimmingStyle);
    }

    public static RaceDetailsDTO RaceDetailsDTOToProtobuf(Domain.DTOs.RaceDetailsDTO raceDetailsDTO)
    {
        return new RaceDetailsDTO
        {
            SwimmingDistance = SwimmingDistanceToProtobuf(raceDetailsDTO.SwimmingDistance),
            SwimmingStyle = SwimmingStyleToProtobuf(raceDetailsDTO.SwimmingStyle)
        };
    }

    public static Domain.Entities.Swimmer SwimmerFromProtobuf(Swimmer swimmer)
    {
        return new Domain.Entities.Swimmer(swimmer.ID, swimmer.FirstName, swimmer.LastName, swimmer.Age);
    }

    public static Swimmer SwimmerToProtobuf(Domain.Entities.Swimmer swimmer)
    {
        return new Swimmer
        {
            ID = swimmer.id,
            FirstName = swimmer.FirstName,
            LastName = swimmer.LastName,
            Age = swimmer.Age
        };
    }

    public static Domain.DTOs.SwimmerDTO SwimmerDTOFromProtobuf(SwimmerDTO swimmerDTOProtobuf)
    {
        var swimmer = SwimmerFromProtobuf(swimmerDTOProtobuf.Swimmer);
        var allRacesDetails =
            swimmerDTOProtobuf.RaceDetailsDTOs.Select(RaceDetailsDTOFromProtobuf).ToList();
        return new Domain.DTOs.SwimmerDTO(swimmer, allRacesDetails);
    }

    public static SwimmerDTO SwimmerDTOToProtobuf(Domain.DTOs.SwimmerDTO swimmerDTO)
    {
        var swimmer = SwimmerToProtobuf(swimmerDTO.Swimmer);
        var allRaceDetails = swimmerDTO.RaceDetailsDTOs.Select(RaceDetailsDTOToProtobuf).ToList();
        return new SwimmerDTO
        {
            Swimmer = swimmer,
            RaceDetailsDTOs = {allRaceDetails}
        };
    }

    public static Response CreateRacesUpdatedResponse()
    {
        var racesUpdatedResponse = new RacesUpdatedResponse();
        return new Response
        {
            RacesUpdatedResponse = racesUpdatedResponse
        };
    }

    public static Response CreateOkResponse()
    {
        var okResponse = new OkResponse();
        return new Response
        {
            OkResponse = okResponse
        };
    }

    public static Response CreateErrorResponse(string message)
    {
        var errorResponse = new ErrorResponse
        {
            ErrorMessage = message
        };
        return new Response
        {
            ErrorResponse = errorResponse
        };
    }

    public static Response CreateFindAllRacesDetailsReponse(List<Domain.DTOs.RaceDTO> allRacesDetails)
    {
        return new Response
        {
            FindAllRacesDetailsResponse = new FindAllRacesDetailsResponse
            {
                AllRacesDetails = {allRacesDetails.Select(RaceDTOToProtobuf).ToList()}
            }
        };
    }

    public static Response CreateFindAllSwimmersDetailsForRaceResponse(
        List<Domain.DTOs.SwimmerDTO> allSwimmersDetailsForRace)
    {
        return new Response
        {
            FindAllSwimmersDetailsForRaceResponse = new FindAllSwimmersDetailsForRaceResponse
            {
                AllSwimmersDetailsForRace = {allSwimmersDetailsForRace.Select(SwimmerDTOToProtobuf).ToList()}
            }
        };
    }
}