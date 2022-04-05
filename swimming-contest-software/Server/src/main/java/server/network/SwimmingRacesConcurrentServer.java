package server.network;

import model.service.SwimmingRaceServices;

import java.net.Socket;

public class SwimmingRacesConcurrentServer extends AbstractConcurrentServer {

    private SwimmingRaceServices server;

    public SwimmingRacesConcurrentServer(int port, SwimmingRaceServices swimmingRaceServices) {
        super(port);
        this.server = swimmingRaceServices;
    }

    @Override
    protected Thread createWorker(Socket client) {
        SwimmingRacesClientWorker clientWorker = new SwimmingRacesClientWorker(server, client);
        Thread thread = new Thread(clientWorker);
        return thread;
    }
}
