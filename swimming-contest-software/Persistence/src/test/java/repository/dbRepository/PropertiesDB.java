package repository.dbRepository;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

public class PropertiesDB {
    public static Properties getDBProperties(String DBPath) {
        Properties properties = new Properties();
        try {
            properties.load(PropertiesDB.class.getResourceAsStream(DBPath));
        } catch (IOException e) {
            e.printStackTrace();
        }
        return properties;
    }
}
