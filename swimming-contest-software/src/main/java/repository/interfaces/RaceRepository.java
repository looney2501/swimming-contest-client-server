package repository.interfaces;

import domain.Race;
import domain.SwimmingDistances;
import domain.SwimmingStyles;

public interface RaceRepository extends Repository<Integer, Race> {
    Race findRaceByDistanceAndStyle(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle);
}
