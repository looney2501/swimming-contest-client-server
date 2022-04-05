package network;

import service.SwimmingRaceServices;

import java.net.Socket;

public class SwimmingRacesConcurrentServer extends AbstractConcurrentServer {

    private final SwimmingRaceServices services;

    public SwimmingRacesConcurrentServer(int port, SwimmingRaceServices swimmingRaceServices) {
        super(port);
        this.services = swimmingRaceServices;
    }

    @Override
    protected Thread createWorker(Socket client) {
        SwimmingRacesClientWorker clientWorker = new SwimmingRacesClientWorker(services, client);
        return new Thread(clientWorker);
    }
}
