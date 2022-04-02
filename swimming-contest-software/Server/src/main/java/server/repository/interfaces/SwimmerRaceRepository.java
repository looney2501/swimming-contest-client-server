package server.repository.interfaces;

import model.domain.entities.Race;
import model.domain.entities.Swimmer;
import model.domain.entities.SwimmerRace;

import java.util.List;

public interface SwimmerRaceRepository extends Repository<Integer, SwimmerRace> {
    Integer getNumberOfSwimmersForRace(Race race);
    List<Swimmer> findAllSwimmersForRace(Race race);
    List<Race> findAllRacesForSwimmer(Swimmer swimmer);
}
