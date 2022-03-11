using System;
using System.Collections.Generic;
using MPP_lab_project.Domain;

namespace MPP_lab_project.Repository.Interfaces;

public interface IRaceRepository: IRepository<Int32, Race>
{
    Race FindRaceByDistanceAndStyle(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle);
    List<Race> FindAllRaces();
}