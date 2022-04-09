using System;
using System.Collections.Generic;
using Model.Domain.Entities;

namespace Server.Repository;

public interface ISwimmerRaceRepository: IRepository<Int32, SwimmerRace>
{
    Int32 GetNumberOfSwimmersForRace(Race race);
    List<Swimmer> FindAllSwimmersForRace(Race race);
    List<Race> FindAllRacesForSwimmer(Swimmer swimmer);
}