package repository.dbRepository;

import domain.Race;
import domain.SwimmingDistances;
import domain.SwimmingStyles;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import repository.interfaces.RaceRepository;
import utils.JdbcUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class RaceDBRepository implements RaceRepository {

    private final JdbcUtils jdbcUtils;
    private static final Logger logger =  LogManager.getLogger();

    public RaceDBRepository(Properties properties) {
        logger.info("Initialising RaceDBRepository with properties {}", properties);
        jdbcUtils = new JdbcUtils(properties);
    }

    @Override
    public Race findRaceByDistanceAndStyle(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle) {
        logger.traceEntry("findRaceByDistanceAndStyle(swimmingDistance = {}, swimmingStyle = {})",
                swimmingDistance,
                swimmingStyle);

        Race race = null;

        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "select id, swimmersNumber from main.Races where distance = ? and style = ?"
        )) {
            preparedStatement.setInt(1, SwimmingDistances.integerFromDistance(swimmingDistance));
            preparedStatement.setInt(2, SwimmingStyles.integerFromStyle(swimmingStyle));
            ResultSet resultSet = preparedStatement.executeQuery();
            resultSet.next();
            Integer id = resultSet.getInt("id");
            Integer swimmersNumber = resultSet.getInt("swimmersNumber");
            race = new Race(id, swimmingDistance, swimmingStyle, swimmersNumber);
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return logger.traceExit("Result: race = {}", race);
    }

    @Override
    public List<Race> findAllRaces() {
        logger.traceEntry("findAllRaces()");

        List<Race> races = new ArrayList<>();

        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "select * from main.Races;"
        )) {
            ResultSet resultSet = preparedStatement.executeQuery();
            while(resultSet.next()) {
                Integer id = resultSet.getInt("id");
                Integer swimmingDistance = resultSet.getInt("distance");
                Integer swimmingStyle = resultSet.getInt("style");
                Integer swimmersNumber = resultSet.getInt("swimmersNumber");
                Race race = new Race(id,
                        SwimmingDistances.distanceFromInteger(swimmingDistance),
                        SwimmingStyles.styleFromInteger(swimmingStyle),
                        swimmersNumber);
                races.add(race);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return logger.traceExit("Result: allRaces  = {}", races);
    }

    @Override
    public void add(Race elem) {

    }

    @Override
    public void delete(Race elem) {

    }

    @Override
    public void update(Race elem, Integer id) {

    }

    @Override
    public Race findById(Integer id) {
        return null;
    }
}
