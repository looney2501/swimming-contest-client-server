package repository.dbRepository;

import domain.Race;
import domain.SwimmingDistances;
import domain.SwimmingStyles;
import org.junit.jupiter.api.Test;
import utils.JdbcUtils;

import java.util.List;
import java.util.Properties;

import static org.junit.jupiter.api.Assertions.assertEquals;

class RaceDBRepositoryTest {

    @Test
    void findRaceByDistanceAndStyle() {
        Properties properties = PropertiesDB.getDBProperties("bd_test.config");

        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);

        Race race = raceDBRepository.findRaceByDistanceAndStyle(SwimmingDistances._50, SwimmingStyles._FREESTYLE);
        assertEquals(SwimmingDistances._50, race.getDistance());
        assertEquals(SwimmingStyles._FREESTYLE, race.getStyle());
    }

    @Test
    void findAllRaces() {
        Properties properties = PropertiesDB.getDBProperties("bd_test.config");

        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);
        List<Race> races = raceDBRepository.findAllRaces();

        assertEquals(16, races.size());
    }
}