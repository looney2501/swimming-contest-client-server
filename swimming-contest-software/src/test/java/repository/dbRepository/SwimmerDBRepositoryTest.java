package repository.dbRepository;

import domain.Swimmer;
import org.junit.jupiter.api.Test;
import utils.JdbcUtils;

import java.io.FileReader;
import java.io.IOException;
import java.sql.*;
import java.util.Properties;

import static org.junit.jupiter.api.Assertions.assertEquals;

class SwimmerDBRepositoryTest {
    @Test
    void add() {
        Properties properties = PropertiesDB.getDBProperties("bd_test.config");
        SwimmerDBRepository swimmerDBRepository = new SwimmerDBRepository(properties);
        swimmerDBRepository.add(new Swimmer("Gigi", "Ursu", 43));

        JdbcUtils jdbcUtils = new JdbcUtils(properties);
        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "select * from main.Swimmers;"
        )) {
            ResultSet resultSet = preparedStatement.executeQuery();
            resultSet.next();
            assertEquals("Gigi", resultSet.getString("firstName"));
            assertEquals("Ursu", resultSet.getString("lastName"));
            assertEquals(43, resultSet.getInt("age"));
        } catch (SQLException e) {
            e.printStackTrace();
        }
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "select count(*) from main.Swimmers;"
        )) {
            ResultSet resultSet = preparedStatement.executeQuery();
            resultSet.next();
            assertEquals(1, resultSet.getInt(1));
        } catch (SQLException e) {
            e.printStackTrace();
        }

        swimmerDBRepository.add(new Swimmer("aaa", "aaa", 21));
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "select count(*) from main.Swimmers;"
        )) {
            ResultSet resultSet = preparedStatement.executeQuery();
            resultSet.next();
            assertEquals(2, resultSet.getInt(1));
        } catch (SQLException e) {
            e.printStackTrace();
        }

        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "delete from main.Swimmers;"
        )) {
            assertEquals(2, preparedStatement.executeUpdate());
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}