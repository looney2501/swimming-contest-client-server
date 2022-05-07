package main;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;

import java.io.IOException;
import java.util.Properties;

@ComponentScan({"services", "repository"})
@SpringBootApplication
public class StartRestServices {
    public static void main(String[] args) {
        SpringApplication.run(StartRestServices.class, args);
    }

    @Bean(name = "properties")
    public Properties loadProperties() {
        Properties properties = new Properties();
        try {
            properties.load(StartRestServices.class.getResourceAsStream("/db.config"));
        } catch (IOException e) {
            e.printStackTrace();
        }
        return properties;
    }
}
