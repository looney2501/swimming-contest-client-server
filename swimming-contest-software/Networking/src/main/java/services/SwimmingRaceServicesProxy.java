package services;

import domain.dtos.AdminDTO;
import domain.dtos.RaceDTO;
import domain.dtos.RaceDetailsDTO;
import domain.dtos.SwimmerDTO;
import domain.entities.Swimmer;
import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;
import javafx.application.Platform;
import observer.SwimmingRaceObserver;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import protocol.requests.*;
import protocol.responses.*;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.net.SocketException;
import java.security.NoSuchAlgorithmException;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class SwimmingRaceServicesProxy implements SwimmingRaceServices {

    private final String host;
    private final int port;
    private SwimmingRaceObserver client;
    private ObjectOutputStream outputStream;
    private ObjectInputStream inputStream;
    private Socket socket;
    private final BlockingQueue<Response> qresponses;
    private volatile boolean finished;
    private final static Logger logger = LogManager.getLogger();

    public SwimmingRaceServicesProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses = new LinkedBlockingQueue<>();
    }

    @Override
    public void login(String username, String password, SwimmingRaceObserver client) throws NoSuchAlgorithmException, ServicesException {
        initializeConnection();
        AdminDTO adminDTO = new AdminDTO(username, password);
        logger.info("Sending login request: username: {}, password: {}...", username, password);
        sendRequest(new LoginRequest(adminDTO));
        logger.info("Waiting for response...");
        Response response = readResponse();
        if (response instanceof OkResponse) {
            logger.info("OkResponse received!");
            this.client = client;
        }
        if (response instanceof ErrorResponse errorResponse) {
            logger.info("ErrorResponse received!");
            closeConnection();
            throw new ServicesException(errorResponse.getErrorMessage());
        }
    }

    private void initializeConnection() {
        try {
            logger.info("Connection details: host: {}, port: {}", host, port);
            socket = new Socket(host, port);
            logger.info("Successfully connected!");
            outputStream = new ObjectOutputStream(socket.getOutputStream());
            outputStream.flush();
            inputStream = new ObjectInputStream(socket.getInputStream());
            finished = false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void startReader() {
        Thread tw = new Thread(new ReaderThread());
        logger.info("Starting response reader thread...");
        tw.start();
    }

    private void closeConnection() {
        logger.info("Closing connection...");
        finished = true;
        try {
            inputStream.close();
            outputStream.close();
            socket.close();
            client = null;
            logger.info("Connection closed successfully!");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void sendRequest(Request request) {
        try {
            outputStream.writeObject(request);
            outputStream.flush();
            logger.info("Request sent successfully!");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private Response readResponse() {
        Response response = null;
        try {
            response = qresponses.take();
            logger.info("Response read successfully!");
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }

    @Override
    public void logout(String username) throws ServicesException {
        logger.info("Sending logout request: username: {}", username);
        sendRequest(new LogoutRequest(username));
        logger.info("Waiting for response...");
        Response response = readResponse();
        if (response instanceof OkResponse) {
            logger.info("OkResponse received!");
            closeConnection();
        }
        if (response instanceof ErrorResponse errorResponse) {
            logger.info("ErrorResponse received!");
            throw new ServicesException(errorResponse.getErrorMessage());
        }
    }

    @Override
    public List<RaceDTO> findAllRacesDetails() {
        logger.info("Sending FindAllRacesDetailsRequest...");
        sendRequest(new FindAllRacesDetailsRequest());
        logger.info("Waiting for response...");
        Response response = readResponse();
        if (response instanceof FindAllRacesDetailsResponse findAllRacesDetailsResponse) {
            logger.info("FindAllRacesDetailsResponse received!");
            return findAllRacesDetailsResponse.getAllRacesDetails();
        }
        else {
            logger.info("Wrong response received!");
            return null;
        }
    }

    @Override
    public List<SwimmerDTO> findAllSwimmersDetailsForRace(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle) {
        RaceDetailsDTO raceDetailsDTO = new RaceDetailsDTO(swimmingDistance, swimmingStyle);
        logger.info("Sending FindAllSwimmersDetailsForRaceRequest: raceDetails: {}", raceDetailsDTO);
        sendRequest(new FindAllSwimmersDetailsForRaceRequest(raceDetailsDTO));
        logger.info("Waiting for response...");
        Response response = readResponse();
        if (response instanceof FindAllSwimmersDetailsForRaceResponse findAllSwimmersDetailsForRaceResponse) {
            return findAllSwimmersDetailsForRaceResponse.getAllSwimmersDetailsForRace();
        }
        else {
            logger.info("Wrong response received!");
            return null;
        }
    }

    @Override
    public void addSwimmer(String firstName, String lastName, Integer age, List<RaceDetailsDTO> raceDetailsDTOs) {
        SwimmerDTO swimmerDTO = new SwimmerDTO(new Swimmer(firstName, lastName, age), raceDetailsDTOs);
        logger.info("Sending AddSwimmerRequest: swimmerDetails: {}", swimmerDTO);
        sendRequest(new AddSwimmerRequest(swimmerDTO));
        logger.info("Waiting for response...");
        Response response = readResponse();
        if (response instanceof OkResponse) {
            logger.info("OkResponse received!");
        }
        else {
            logger.info("Wrong response received!");
        }
    }

    private void handleUpdate(UpdateResponse updateResponse) {
        Platform.runLater(() -> {
            if (updateResponse instanceof RacesUpdatedResponse) {
                logger.info("Updating UI...");
                client.racesUpdated();
                logger.info("UI updated successfully!");
            }
        });
    }

    private class ReaderThread implements Runnable {
        @Override
        public void run() {
            while (!finished) {
                try {
                    Object response = inputStream.readObject();
                    if (response instanceof UpdateResponse) {
                        logger.info("UpdateResponse received. Handling response...");
                        handleUpdate((UpdateResponse) response);
                    }
                    else {
                        try {
                            qresponses.put((Response) response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                }
                catch (SocketException e) {
                    //e.printStackTrace();
                }
                catch (IOException | ClassNotFoundException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
