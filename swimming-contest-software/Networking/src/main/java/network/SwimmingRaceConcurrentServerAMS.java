package network;

import services.SwimmingRaceServices;
import services.SwimmingRaceServicesAMS;

import java.net.Socket;

public class SwimmingRaceConcurrentServerAMS extends AbstractConcurrentServer {

    private final SwimmingRaceServicesAMS services;

    public SwimmingRaceConcurrentServerAMS(int port, SwimmingRaceServicesAMS swimmingRaceServices) {
        super(port);
        logger.info("Server name: SwimmingRacesServer; Server type: concurrent; Port: {}", port);
        this.services = swimmingRaceServices;
    }

    @Override
    protected Thread createWorker(Socket client) {
        logger.info("Creating worker to handle client...");
        SwimmingRaceClientWorkerAMS clientWorker = new SwimmingRaceClientWorkerAMS(services, client);
        return new Thread(clientWorker);
    }
}
