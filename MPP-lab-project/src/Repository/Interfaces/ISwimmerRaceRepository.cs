using System;
using System.Collections.Generic;
using MPP_lab_project.Domain;

namespace MPP_lab_project.Repository.Interfaces;

public interface ISwimmerRaceRepository: IRepository<Int32, SwimmerRace>
{
    Int32 GetNumberOfSwimmersForRace(Race race);
    List<Swimmer> FindAllSwimmersForRace(Race race);
    List<Race> FindAllRacesForSwimmer(Swimmer swimmer);
}