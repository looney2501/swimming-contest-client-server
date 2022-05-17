package repository.dbRepository;

import domain.entities.Race;
import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.stereotype.Repository;
import repository.RaceRepository;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

@Repository
public class RaceDBRepository implements RaceRepository {

    private final JdbcUtils jdbcUtils;
    private static final Logger logger =  LogManager.getLogger();

    public RaceDBRepository(Properties properties) {
        logger.info("Initialising RaceDBRepository with properties {}", properties);
        jdbcUtils = new JdbcUtils(properties);
    }

    @Override
    public Race findRaceByDistanceAndStyle(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle) {
        logger.traceEntry("findRaceByDistanceAndStyle(swimmingDistance = {}, swimmingStyle = {})",
                swimmingDistance,
                swimmingStyle);

        Race race = null;

        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "select id, swimmersNumber from main.Races where distance = ? and style = ?;"
        )) {
            preparedStatement.setInt(1, SwimmingDistance.integerFromDistance(swimmingDistance));
            preparedStatement.setInt(2, SwimmingStyle.integerFromStyle(swimmingStyle));
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
                        SwimmingDistance.distanceFromInteger(swimmingDistance),
                        SwimmingStyle.styleFromInteger(swimmingStyle),
                        swimmersNumber);
                races.add(race);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }

        return logger.traceExit("Result: allRaces  = {}", races);
    }

    @Override
    public Integer add(Race elem) {
        logger.traceEntry("add(race = {})", elem);

        Connection connection = jdbcUtils.getConnection();
        Integer id = null;

        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "insert into Races (distance, style, swimmersNumber) values (?, ?, ?);"
        )) {
            preparedStatement.setInt(1, SwimmingDistance.integerFromDistance(elem.getDistance()));
            preparedStatement.setInt(2, SwimmingStyle.integerFromStyle(elem.getStyle()));
            preparedStatement.setInt(3, elem.getSwimmersNumber());
            preparedStatement.executeUpdate();
            ResultSet resultSet = preparedStatement.getGeneratedKeys();
            resultSet.next();
            id = resultSet.getInt(1);

        } catch (SQLException e) {
            throw new RuntimeException(e);
        }
        return logger.traceExit("Result: id = {}", id);
    }

    @Override
    public Race delete(Integer id) {
        logger.traceEntry("delete(id = {})");

        Race deletedRace = null;

        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "select distance, style, swimmersNumber from main.Races where id = ?;"
        )) {
            preparedStatement.setInt(1, id);
            ResultSet resultSet = preparedStatement.executeQuery();
            if (resultSet.next()) {
                Integer distance = resultSet.getInt("distance");
                Integer style = resultSet.getInt("style");
                Integer swimmersNo = resultSet.getInt("swimmersNumber");
                deletedRace = new Race(id, SwimmingDistance.distanceFromInteger(distance), SwimmingStyle.styleFromInteger(style), swimmersNo);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }

        if (deletedRace != null) {
            try (PreparedStatement preparedStatement = connection.prepareStatement(
                    "delete from Races where id = ?"
            )) {
                preparedStatement.setInt(1, id);
                preparedStatement.executeUpdate();
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }

        return logger.traceExit("Result: deletedRace = {}", deletedRace);
    }

    @Override
    public Race update(Race elem, Integer id) {
        logger.traceEntry("update(elem = {}, id = {})", elem, id);

        Race oldRace = null;

        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "select distance, style, swimmersNumber from main.Races where id = ?;"
        )) {
            preparedStatement.setInt(1, id);
            ResultSet resultSet = preparedStatement.executeQuery();
            if (resultSet.next()) {
                Integer distance = resultSet.getInt("distance");
                Integer style = resultSet.getInt("style");
                Integer swimmersNo = resultSet.getInt("swimmersNumber");
                oldRace = new Race(id, SwimmingDistance.distanceFromInteger(distance), SwimmingStyle.styleFromInteger(style), swimmersNo);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }

        if (oldRace != null) {
            try (PreparedStatement preparedStatement = connection.prepareStatement(
                    "update Races set distance = ?, style = ?, swimmersNumber = ? where id = ?"
            )) {
                preparedStatement.setInt(1, SwimmingDistance.integerFromDistance(elem.getDistance()));
                preparedStatement.setInt(2, SwimmingStyle.integerFromStyle(elem.getStyle()));
                preparedStatement.setInt(3, elem.getSwimmersNumber());
                preparedStatement.setInt(4, elem.getId());
                preparedStatement.executeUpdate();
            } catch (SQLException e) {
                e.printStackTrace();
            }
        }

        return logger.traceExit("Result: oldRace = {}", oldRace);
    }

    @Override
    public Race findById(Integer id) {
        logger.traceEntry("findById(id = {})", id);
        Race race = null;

        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "select distance, style, swimmersNumber from main.Races where id = ?;"
        )) {
            preparedStatement.setInt(1, id);
            ResultSet resultSet = preparedStatement.executeQuery();
            if (resultSet.next()) {
                Integer distance = resultSet.getInt("distance");
                Integer style = resultSet.getInt("style");
                Integer swimmersNo = resultSet.getInt("swimmersNumber");
                race = new Race(id, SwimmingDistance.distanceFromInteger(distance), SwimmingStyle.styleFromInteger(style), swimmersNo);
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }

        return logger.traceExit("Result: race = {}", race);
    }
}
