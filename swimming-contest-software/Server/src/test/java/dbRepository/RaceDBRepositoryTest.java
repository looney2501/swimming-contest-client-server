package dbRepository;

import domain.entities.Race;
import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;
import org.junit.jupiter.api.Test;
import repository.dbRepository.JdbcUtils;
import repository.dbRepository.RaceDBRepository;

import java.util.List;
import java.util.Properties;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNull;

class RaceDBRepositoryTest {

    @Test
    void findRaceByDistanceAndStyle() {
        Properties properties = PropertiesDB.getDBProperties("bd_test.config");

        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);

        Race race = raceDBRepository.findRaceByDistanceAndStyle(SwimmingDistance._50m, SwimmingStyle.Freestyle);
        assertEquals(SwimmingDistance._50m, race.getDistance());
        assertEquals(SwimmingStyle.Freestyle, race.getStyle());
    }

    @Test
    void findAllRaces() {
        Properties properties = PropertiesDB.getDBProperties("bd_test.config");

        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);
        List<Race> races = raceDBRepository.findAllRaces();

        assertEquals(16, races.size());
    }

    @Test
    void findById() {
        Properties properties = PropertiesDB.getDBProperties("bd_test.config");
        JdbcUtils jdbcUtils = new JdbcUtils(properties);

        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);
        assertNull(raceDBRepository.findById(30));

        Race race = new Race(SwimmingDistance._50m, SwimmingStyle.Mixed, 0);
        assertEquals(race, raceDBRepository.findById(1));
    }
}