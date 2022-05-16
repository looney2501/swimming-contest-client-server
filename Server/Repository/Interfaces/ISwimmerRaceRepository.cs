using System.Collections.Generic;
using Model.Domain.Entities;

namespace Server.Repository;

public interface ISwimmerRaceRepository : IRepository<int, SwimmerRace>
{
    int GetNumberOfSwimmersForRace(Race race);
    List<Swimmer> FindAllSwimmersForRace(Race race);
    List<Race> FindAllRacesForSwimmer(Swimmer swimmer);
}