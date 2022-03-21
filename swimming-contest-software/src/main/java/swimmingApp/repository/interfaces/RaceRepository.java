package swimmingApp.repository.interfaces;

import swimmingApp.domain.Race;
import swimmingApp.domain.SwimmingDistances;
import swimmingApp.domain.SwimmingStyles;

import java.util.List;

public interface RaceRepository extends Repository<Integer, Race> {
    Race findRaceByDistanceAndStyle(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle);
    List<Race> findAllRaces();
}
