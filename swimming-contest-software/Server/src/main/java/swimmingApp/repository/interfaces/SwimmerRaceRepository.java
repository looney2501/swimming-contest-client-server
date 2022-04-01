package swimmingApp.repository.interfaces;

import swimmingApp.domain.entities.Race;
import swimmingApp.domain.entities.Swimmer;
import swimmingApp.domain.entities.SwimmerRace;

import java.util.List;

public interface SwimmerRaceRepository extends Repository<Integer, SwimmerRace> {
    Integer getNumberOfSwimmersForRace(Race race);
    List<Swimmer> findAllSwimmersForRace(Race race);
    List<Race> findAllRacesForSwimmer(Swimmer swimmer);
}
