﻿using System;
using System.Collections.Generic;
using Model.Domain.Entities;
using Model.Domain.Enums;

namespace Server.Repository;

public interface IRaceRepository: IRepository<Int32, Race>
{
    Race FindRaceByDistanceAndStyle(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle);
    List<Race> FindAllRaces();
}