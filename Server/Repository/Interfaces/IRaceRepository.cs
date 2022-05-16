using System.Collections.Generic;
using Model.Domain.Entities;
using Model.Domain.Enums;

namespace Server.Repository;

public interface IRaceRepository : IRepository<int, Race>
{
    Race FindRaceByDistanceAndStyle(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle);
    List<Race> FindAllRaces();
}