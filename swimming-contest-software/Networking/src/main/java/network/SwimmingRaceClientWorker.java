package network;

import observer.SwimmingRaceObserver;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import protocol.requests.*;
import protocol.responses.*;
import services.ServicesException;
import services.SwimmingRaceServices;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.security.NoSuchAlgorithmException;

public class SwimmingRaceClientWorker implements Runnable, SwimmingRaceObserver {

    private final SwimmingRaceServices services;
    private final Socket clientSocket;
    private ObjectInputStream objectInputStream;
    private ObjectOutputStream objectOutputStream;
    private volatile boolean connected;
    private static final Logger logger = LogManager.getLogger();

    public SwimmingRaceClientWorker(SwimmingRaceServices services, Socket clientSocket) {
        this.services = services;
        this.clientSocket = clientSocket;
        try {
            objectOutputStream = new ObjectOutputStream(this.clientSocket.getOutputStream());
            objectOutputStream.flush();
            objectInputStream = new ObjectInputStream(this.clientSocket.getInputStream());
            connected = true;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void run() {
        while (connected) {
            try {
                Object request = objectInputStream.readObject();
                Object response = handleRequest((Request) request);
                if (response != null) {
                    sendResponse((Response) response);
                    logger.info("Response sent.");
                }
                else {
                    logger.info("Unknown request type, cannot be handled!");
                }
            } catch (IOException | ClassNotFoundException e) {
                e.printStackTrace();
            }

            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }

        try {
            objectInputStream.close();
            objectOutputStream.close();
            clientSocket.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void racesUpdated() {
        try {
            sendResponse(new RacesUpdatedResponse());
            logger.info("RacesUpdateResponse sent to client: {}", clientSocket);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private Response handleRequest(Request request) {
        if (request instanceof LoginRequest loginRequest) {
            logger.traceEntry("handleRequest(request = LoginRequest)");
            var adminDTO = loginRequest.getAdmin();
            try {
                services.login(adminDTO.getUsername(), adminDTO.getPassword(), this);
                return logger.traceExit("Result: response = OkResponse", new OkResponse());
            } catch (NoSuchAlgorithmException e) {
                e.printStackTrace();
            } catch (ServicesException e) {
                connected = false;
                return logger.traceExit("Result: response = ErrorResponse", new ErrorResponse(e.getMessage()));
            }
        }

        if (request instanceof LogoutRequest logoutRequest) {
            logger.traceEntry("handleRequest(request = LogoutRequest)");
            var adminUsername = logoutRequest.getUsername();
            try {
                services.logout(adminUsername);
                connected = false;
                return logger.traceExit("Result: response = LogoutRequest", new OkResponse());
            } catch (ServicesException e) {
                return logger.traceExit("Result: response = ErrorResponse", new ErrorResponse(e.getMessage()));
            }
        }

        if (request instanceof FindAllRacesDetailsRequest findAllRacesDetailsRequest) {
            logger.traceEntry("handleRequest(request = FindAllRacesDetailsRequest)");
            var allRacesDetails = services.findAllRacesDetails();
            return logger.traceExit("Result: response = FindAllRacesDetailsResponse",
                    new FindAllRacesDetailsResponse(allRacesDetails));
        }

        if (request instanceof FindAllSwimmersDetailsForRaceRequest findAllSwimmersDetailsForRaceRequest) {
            logger.traceEntry("handleRequest(request = FindAllSwimmersDetailsForRaceRequest)");
            var raceDetailsDTO = findAllSwimmersDetailsForRaceRequest.getRaceDetailsDTO();
            var allSwimmersDetailsForRace = services.findAllSwimmersDetailsForRace(raceDetailsDTO.getSwimmingDistance(),
                    raceDetailsDTO.getSwimmingStyle());
            return logger.traceExit("Result: response = FindAllSwimmersDetailsForRaceResponse",
                    new FindAllSwimmersDetailsForRaceResponse(allSwimmersDetailsForRace));
        }

        if (request instanceof AddSwimmerRequest addSwimmerRequest) {
            logger.traceEntry("handleRequest(request = AddSwimmerRequest)");
            var swimmerDTO = addSwimmerRequest.getSwimmerDTO();
            services.addSwimmer(swimmerDTO.getSwimmer().getFirstName(),
                    swimmerDTO.getSwimmer().getLastName(),
                    swimmerDTO.getSwimmer().getAge(),
                    swimmerDTO.getRaceDetailsDTOs());
            return logger.traceExit("Result: response = OkResponse", new OkResponse());
        }
        return null;
    }

    private void sendResponse(Response response) throws IOException {
        synchronized (objectOutputStream) {
            objectOutputStream.writeObject(response);
            objectOutputStream.flush();
        }
    }
}
