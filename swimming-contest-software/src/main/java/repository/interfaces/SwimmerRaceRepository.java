package repository.interfaces;

import domain.Race;
import domain.Swimmer;
import domain.SwimmerRace;

import java.util.List;

public interface SwimmerRaceRepository extends Repository<Integer, SwimmerRace> {
    Integer getNumberOfSwimmersForRace(Race race);
    List<Swimmer> findAllSwimmersForRace(Race race);
}
