package dbRepository;

import org.junit.jupiter.api.Test;
import domain.entities.Race;
import domain.entities.Swimmer;
import domain.entities.SwimmerRace;
import domain.enums.SwimmingDistances;
import domain.enums.SwimmingStyles;
import repository.dbRepository.JdbcUtils;
import repository.dbRepository.RaceDBRepository;
import repository.dbRepository.SwimmerDBRepository;
import repository.dbRepository.SwimmerRaceDBRepository;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Properties;

import static org.junit.jupiter.api.Assertions.assertEquals;

class SwimmerRaceDBRepositoryTest {

    Properties properties = PropertiesDB.getDBProperties("bd_test.config");
    JdbcUtils jdbcUtils = new JdbcUtils(properties);

    @Test
    void add() {
        SwimmerDBRepository swimmerDBRepository = new SwimmerDBRepository(properties);
        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);
        SwimmerRaceDBRepository swimmerRaceDBRepository = new SwimmerRaceDBRepository(properties,
                swimmerDBRepository,
                raceDBRepository);

        Swimmer swimmer = new Swimmer("Gigi", "Ursu", 32);
        Integer id = swimmerDBRepository.add(swimmer);
        swimmer.setID(id);

        Race race = raceDBRepository.findRaceByDistanceAndStyle(SwimmingDistances._200m, SwimmingStyles._BACKSTROKE);

        SwimmerRace swimmerRace = new SwimmerRace(swimmer, race);
        swimmerRaceDBRepository.add(swimmerRace);

        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "select id_swimmer, id_race from main.SwimmersRaces;"
        )) {
            ResultSet resultSet = preparedStatement.executeQuery();
            resultSet.next();
            Integer id_swimmer = resultSet.getInt(1);
            Integer id_race = resultSet.getInt(2);
            assertEquals(swimmer.getID(), id_swimmer);
            assertEquals(race.getID(), id_race);
        } catch (SQLException e) {
            e.printStackTrace();
        }

        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "delete from main.SwimmersRaces;"
        )) {
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }

        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "delete from main.Swimmers;"
        )) {
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    @Test
    void getNumberOfSwimmersForRace() {
        SwimmerDBRepository swimmerDBRepository = new SwimmerDBRepository(properties);
        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);
        SwimmerRaceDBRepository swimmerRaceDBRepository = new SwimmerRaceDBRepository(properties,
                swimmerDBRepository,
                raceDBRepository);

        Swimmer swimmer = new Swimmer("Gigi", "Ursu", 32);
        Integer id = swimmerDBRepository.add(swimmer);
        swimmer.setID(id);

        Race race = raceDBRepository.findRaceByDistanceAndStyle(SwimmingDistances._200m, SwimmingStyles._BACKSTROKE);

        assertEquals(0, swimmerRaceDBRepository.getNumberOfSwimmersForRace(race));

        SwimmerRace swimmerRace = new SwimmerRace(swimmer, race);
        swimmerRaceDBRepository.add(swimmerRace);

        assertEquals(1, swimmerRaceDBRepository.getNumberOfSwimmersForRace(race));

        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "delete from main.SwimmersRaces;"
        )) {
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }

        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "delete from main.Swimmers;"
        )) {
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    @Test
    void findAllSwimmersForRace() {
        SwimmerDBRepository swimmerDBRepository = new SwimmerDBRepository(properties);
        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);
        SwimmerRaceDBRepository swimmerRaceDBRepository = new SwimmerRaceDBRepository(properties,
                swimmerDBRepository,
                raceDBRepository);

        Swimmer swimmer1 = new Swimmer("Gigi", "Ursu", 32);
        Integer id1 = swimmerDBRepository.add(swimmer1);
        swimmer1.setID(id1);
        Swimmer swimmer2 = new Swimmer("Gigi2", "Ursu2", 33);
        Integer id2 = swimmerDBRepository.add(swimmer2);
        swimmer2.setID(id2);

        Race race = raceDBRepository.findRaceByDistanceAndStyle(SwimmingDistances._200m, SwimmingStyles._BACKSTROKE);

        SwimmerRace swimmerRace1 = new SwimmerRace(swimmer1, race);
        swimmerRaceDBRepository.add(swimmerRace1);
        assertEquals(1, swimmerRaceDBRepository.findAllSwimmersForRace(race).size());

        SwimmerRace swimmerRace2 = new SwimmerRace(swimmer2, race);
        swimmerRaceDBRepository.add(swimmerRace1);
        assertEquals(2, swimmerRaceDBRepository.findAllSwimmersForRace(race).size());

        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "delete from main.SwimmersRaces;"
        )) {
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }

        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "delete from main.Swimmers;"
        )) {
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }

    @Test
    void findAllRacesForSwimmer() {
        SwimmerDBRepository swimmerDBRepository = new SwimmerDBRepository(properties);
        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);
        SwimmerRaceDBRepository swimmerRaceDBRepository = new SwimmerRaceDBRepository(properties,
                swimmerDBRepository,
                raceDBRepository);

        Swimmer swimmer1 = new Swimmer("Gigi", "Ursu", 32);
        Integer id1 = swimmerDBRepository.add(swimmer1);
        swimmer1.setID(id1);

        Race race1 = raceDBRepository.findRaceByDistanceAndStyle(SwimmingDistances._200m, SwimmingStyles._BACKSTROKE);
        Race race2 = raceDBRepository.findRaceByDistanceAndStyle(SwimmingDistances._50m, SwimmingStyles._BACKSTROKE);

        SwimmerRace swimmerRace1 = new SwimmerRace(swimmer1, race1);
        swimmerRaceDBRepository.add(swimmerRace1);
        assertEquals(1, swimmerRaceDBRepository.findAllRacesForSwimmer(swimmer1).size());

        SwimmerRace swimmerRace2 = new SwimmerRace(swimmer1, race2);
        swimmerRaceDBRepository.add(swimmerRace1);
        assertEquals(2, swimmerRaceDBRepository.findAllRacesForSwimmer(swimmer1).size());

        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "delete from main.SwimmersRaces;"
        )) {
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }

        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "delete from main.Swimmers;"
        )) {
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}