package network;

import services.SwimmingRaceServices;

import java.net.Socket;

public class SwimmingRacesConcurrentServer extends AbstractConcurrentServer {

    private final SwimmingRaceServices services;

    public SwimmingRacesConcurrentServer(int port, SwimmingRaceServices swimmingRaceServices) {
        super(port);
        logger.info("Server name: SwimmingRacesServer; Server type: concurrent");
        this.services = swimmingRaceServices;
    }

    @Override
    protected Thread createWorker(Socket client) {
        logger.info("Creating worker to handle request...");
        SwimmingRacesClientWorker clientWorker = new SwimmingRacesClientWorker(services, client);
        return new Thread(clientWorker);
    }
}
