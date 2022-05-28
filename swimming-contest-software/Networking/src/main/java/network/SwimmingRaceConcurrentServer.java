package network;

import services.SwimmingRaceServices;

import java.net.Socket;

public class SwimmingRaceConcurrentServer extends AbstractConcurrentServer {

    private final SwimmingRaceServices services;

    public SwimmingRaceConcurrentServer(int port, SwimmingRaceServices swimmingRaceServices) {
        super(port);
        logger.info("Server name: SwimmingRacesServer; Server type: concurrent; Port: {}", port);
        this.services = swimmingRaceServices;
    }

    @Override
    protected Thread createWorker(Socket client) {
        logger.info("Creating worker to handle client...");
        SwimmingRaceClientWorker clientWorker = new SwimmingRaceClientWorker(services, client);
        return new Thread(clientWorker);
    }
}
