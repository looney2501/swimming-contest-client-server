package repository.dbRepository;

import domain.Swimmer;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import repository.interfaces.SwimmerRepository;
import utils.JdbcUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.Properties;

public class SwimmerDBRepository implements SwimmerRepository {

    private final JdbcUtils jdbcUtils;
    private static final Logger logger = LogManager.getLogger();

    public SwimmerDBRepository(Properties properties) {
        logger.info("Initialising SwimmerDBRepository with properties {}", properties);
        this.jdbcUtils = new JdbcUtils(properties);
    }

    @Override
    public void add(Swimmer elem) {
        logger.traceEntry("saving swimmer {}", elem);
        Connection connection = jdbcUtils.getConnection();

        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "insert into Swimmers (firstName, lastName, age) values (?, ?, ?)"
        ))
        {
            preparedStatement.setString(1, elem.getFirstName());
            preparedStatement.setString(2, elem.getLastName());
            preparedStatement.setInt(3, elem.getAge());
            int result = preparedStatement.executeUpdate();

            logger.trace("Saved {} instances", result);
        }
        catch (SQLException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void delete(Swimmer elem) {
    }

    @Override
    public void update(Swimmer elem, Integer id) {
    }

    @Override
    public Swimmer findById(Integer id) {
        return null;
    }
}
