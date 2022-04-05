package network;

import java.net.Socket;

public abstract class AbstractConcurrentServer extends AbstractServer{

    public AbstractConcurrentServer(int port) {
        super(port);
        logger.info("Server type: concurrent");
    }

    public void processRequest(Socket client) {
        Thread thread = createWorker(client);
        thread.start();
    }

    protected abstract Thread createWorker(Socket client);
}
