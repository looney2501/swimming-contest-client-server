package repository;

import domain.entities.Race;
import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;

import java.util.List;

public interface RaceRepository extends Repository<Integer, Race> {
    Race findRaceByDistanceAndStyle(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle);
    List<Race> findAllRaces();
}
