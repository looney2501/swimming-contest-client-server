package swimmingApp.repository.interfaces;

import swimmingApp.domain.Race;
import swimmingApp.domain.Swimmer;
import swimmingApp.domain.SwimmerRace;

import java.util.List;

public interface SwimmerRaceRepository extends Repository<Integer, SwimmerRace> {
    Integer getNumberOfSwimmersForRace(Race race);
    List<Swimmer> findAllSwimmersForRace(Race race);
    List<Race> findAllRacesForSwimmer(Swimmer swimmer);
}
