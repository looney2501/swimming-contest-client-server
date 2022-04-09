package dbRepository;

import domain.entities.Admin;
import org.junit.jupiter.api.Test;
import repository.dbRepository.AdminDBRepository;
import repository.dbRepository.JdbcUtils;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.util.Properties;

import static org.junit.jupiter.api.Assertions.*;

class AdminDBRepositoryTest {

    @Test
    void findByUsernameAndPassword() {
        Properties properties = PropertiesDB.getDBProperties("bd_test.config");
        JdbcUtils jdbcUtils = new JdbcUtils(properties);
        AdminDBRepository adminDBRepository = new AdminDBRepository(properties);

        assertNull(adminDBRepository.findByUsernameAndPassword("giig", "aaa"));

        Connection connection = jdbcUtils.getConnection();
        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "insert into main.Admins (username, password) values ('gigi', 'ursu');"
        )) {
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }

        assertEquals(new Admin("gigi", "ursu"),
                adminDBRepository.findByUsernameAndPassword("gigi", "ursu"));

        try (PreparedStatement preparedStatement = connection.prepareStatement(
                "delete from main.Admins;"
        )) {
            preparedStatement.executeUpdate();
        } catch (SQLException e) {
            e.printStackTrace();
        }
    }
}