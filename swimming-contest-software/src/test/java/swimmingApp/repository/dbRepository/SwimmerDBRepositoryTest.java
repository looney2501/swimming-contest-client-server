package swimmingApp.repository.dbRepository;

import swimmingApp.domain.Swimmer;
import org.junit.jupiter.api.Test;
import swimmingApp.utils.JdbcUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Properties;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertNull;

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

    @Test
    void findById() {
        Properties properties = PropertiesDB.getDBProperties("bd_test.config");
        SwimmerDBRepository swimmerDBRepository = new SwimmerDBRepository(properties);
        JdbcUtils jdbcUtils = new JdbcUtils(properties);

        assertNull(swimmerDBRepository.findById(-2));

        swimmerDBRepository.add(new Swimmer("Gigi", "Ursu", 43));

        Integer id = null;
        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "select id from main.Swimmers;"
        )) {
            ResultSet resultSet = preparedStatement.executeQuery();
            resultSet.next();
            id = resultSet.getInt("id");
        } catch (SQLException e) {
            e.printStackTrace();
        }

        assertEquals(new Swimmer(id, "Gigi", "Ursu", 43), swimmerDBRepository.findById(id));

        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "delete from main.Swimmers;"
        )) {
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}