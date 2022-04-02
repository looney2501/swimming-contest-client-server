package server.repository.interfaces;

import model.domain.entities.Race;
import model.domain.enums.SwimmingDistances;
import model.domain.enums.SwimmingStyles;

import java.util.List;

public interface RaceRepository extends Repository<Integer, Race> {
    Race findRaceByDistanceAndStyle(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle);
    List<Race> findAllRaces();
}
