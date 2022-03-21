package swimmingApp.repository.dbRepository;

import swimmingApp.domain.Race;
import swimmingApp.domain.SwimmingDistances;
import swimmingApp.domain.SwimmingStyles;
import org.junit.jupiter.api.Test;
import swimmingApp.utils.JdbcUtils;

import java.util.List;
import java.util.Properties;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNull;

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

    @Test
    void findById() {
        Properties properties = PropertiesDB.getDBProperties("bd_test.config");
        JdbcUtils jdbcUtils = new JdbcUtils(properties);

        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);
        assertNull(raceDBRepository.findById(30));

        Race race = new Race(SwimmingDistances._50, SwimmingStyles._MIXED, 0);
        Race race1 = raceDBRepository.findById(1);
        assertEquals(race, raceDBRepository.findById(1));
    }
}