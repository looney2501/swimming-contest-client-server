package swimmingApp.repository.interfaces;

import swimmingApp.domain.entities.Race;
import swimmingApp.domain.enums.SwimmingDistances;
import swimmingApp.domain.enums.SwimmingStyles;

import java.util.List;

public interface RaceRepository extends Repository<Integer, Race> {
    Race findRaceByDistanceAndStyle(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle);
    List<Race> findAllRaces();
}
