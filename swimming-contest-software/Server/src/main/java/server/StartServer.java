package server;

import model.service.SwimmingRaceServices;
import server.network.AbstractServer;
import server.network.SwimmingRacesConcurrentServer;
import server.repository.dbRepository.AdminDBRepository;
import server.repository.dbRepository.RaceDBRepository;
import server.repository.dbRepository.SwimmerDBRepository;
import server.repository.dbRepository.SwimmerRaceDBRepository;
import server.repository.interfaces.AdminRepository;
import server.repository.interfaces.RaceRepository;
import server.repository.interfaces.SwimmerRaceRepository;
import server.repository.interfaces.SwimmerRepository;
import server.service.ServerSwimmingRaceServices;

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

        ServerSwimmingRaceServices swimmingRaceServices = new ServerSwimmingRaceServices();
        swimmingRaceServices.setRaceRepository(raceRepository);
        swimmingRaceServices.setAdminRepository(adminRepository);
        swimmingRaceServices.setSwimmerRepository(swimmerRepository);
        swimmingRaceServices.setSwimmerRaceRepository(swimmerRaceRepository);

        return swimmingRaceServices;
    }


}
