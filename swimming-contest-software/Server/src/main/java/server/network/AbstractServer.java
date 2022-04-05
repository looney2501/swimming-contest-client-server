package server.network;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;

public abstract class AbstractServer {
    private final int port;
    private ServerSocket server=null;

    public AbstractServer(int port) {
        this.port = port;
    }

    public void start() {
        try {
            server = new ServerSocket(port);
            while (true) {
                Socket client = server.accept();
                processRequest(client);
            }
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    public abstract void processRequest(Socket client);

    public void stop() {
        try {
            server.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
