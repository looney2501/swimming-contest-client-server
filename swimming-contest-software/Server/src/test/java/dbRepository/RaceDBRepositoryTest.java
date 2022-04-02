package dbRepository;

import model.domain.entities.Race;
import model.domain.enums.SwimmingDistances;
import model.domain.enums.SwimmingStyles;
import org.junit.jupiter.api.Test;
import server.repository.dbRepository.JdbcUtils;
import server.repository.dbRepository.RaceDBRepository;

import java.util.List;
import java.util.Properties;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNull;

class RaceDBRepositoryTest {

    @Test
    void findRaceByDistanceAndStyle() {
        Properties properties = PropertiesDB.getDBProperties("bd_test.config");

        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);

        Race race = raceDBRepository.findRaceByDistanceAndStyle(SwimmingDistances._50m, SwimmingStyles._FREESTYLE);
        assertEquals(SwimmingDistances._50m, race.getDistance());
        assertEquals(SwimmingStyles._FREESTYLE, race.getStyle());
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

        Race race = new Race(SwimmingDistances._50m, SwimmingStyles._MIXED, 0);
        assertEquals(race, raceDBRepository.findById(1));
    }
}