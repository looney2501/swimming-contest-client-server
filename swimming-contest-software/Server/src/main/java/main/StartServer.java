package main;

import services.SwimmingRaceServices;
import network.AbstractServer;
import network.SwimmingRacesConcurrentServer;
import repository.dbRepository.AdminDBRepository;
import repository.dbRepository.RaceDBRepository;
import repository.dbRepository.SwimmerDBRepository;
import repository.dbRepository.SwimmerRaceDBRepository;
import repository.AdminRepository;
import repository.RaceRepository;
import repository.SwimmerRaceRepository;
import repository.SwimmerRepository;
import services.SwimmingRaceServicesServer;

import java.io.IOException;
import java.util.Properties;

public class StartServer {

    public static void main(String[] args) {
        Properties serverProperties = loadProperties();
        int serverPort = Integer.parseInt(serverProperties.getProperty("server.port"));
        SwimmingRaceServices swimmingRaceServices = loadServices(serverProperties);
        AbstractServer server = new SwimmingRacesConcurrentServer(serverPort, swimmingRaceServices);
        server.start();
    }

    private static Properties loadProperties() {
        Properties properties = new Properties();
        try {
            properties.load(StartServer.class.getResourceAsStream("/server.config"));
        } catch (IOException e) {
            e.printStackTrace();
        }
        return properties;
    }

    private static SwimmingRaceServices loadServices(Properties properties) {
        AdminRepository adminRepository = new AdminDBRepository(properties);
        SwimmerRepository swimmerRepository = new SwimmerDBRepository(properties);
        RaceRepository raceRepository = new RaceDBRepository(properties);
        SwimmerRaceRepository swimmerRaceRepository = new SwimmerRaceDBRepository(properties, swimmerRepository, raceRepository);

        SwimmingRaceServicesServer swimmingRaceServices = new SwimmingRaceServicesServer();
        swimmingRaceServices.setRaceRepository(raceRepository);
        swimmingRaceServices.setAdminRepository(adminRepository);
        swimmingRaceServices.setSwimmerRepository(swimmerRepository);
        swimmingRaceServices.setSwimmerRaceRepository(swimmerRaceRepository);

        return swimmingRaceServices;
    }


}
