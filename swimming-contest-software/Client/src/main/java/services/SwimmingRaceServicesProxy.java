package services;

import domain.dtos.AdminDTO;
import domain.dtos.RaceDTO;
import domain.dtos.RaceDetailsDTO;
import domain.dtos.SwimmerDTO;
import domain.entities.Swimmer;
import domain.enums.SwimmingDistances;
import domain.enums.SwimmingStyles;
import javafx.application.Platform;
import observer.SwimmingRaceObserver;
import protocol.requests.*;
import protocol.responses.*;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
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
    private BlockingQueue<Response> qresponses;
    private volatile boolean finished;

    public SwimmingRaceServicesProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses = new LinkedBlockingQueue<>();
    }

    @Override
    public void login(String username, String password, SwimmingRaceObserver client) throws NoSuchAlgorithmException, ServicesException {
        initializeConnection();
        AdminDTO adminDTO = new AdminDTO(username, password);
        sendRequest(new LoginRequest(adminDTO));
        Response response = readResponse();
        if (response instanceof OkResponse) {
            this.client = client;
        }
        if (response instanceof ErrorResponse errorResponse) {
            closeConnection();
            throw new ServicesException(errorResponse.getErrorMessage());
        }
    }

    private void initializeConnection() {
        try {
            socket = new Socket(host, port);
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
        tw.start();
    }

    private void closeConnection() {
        finished = true;
        try {
            inputStream.close();
            outputStream.close();
            socket.close();
            client = null;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void sendRequest(Request request) {
        try {
            outputStream.writeObject(request);
            outputStream.flush();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private Response readResponse() {
        Response response = null;
        try {
            response = qresponses.take();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }

    @Override
    public void logout(String username) throws ServicesException {
        sendRequest(new LogoutRequest(username));
        Response response = readResponse();
        closeConnection();
        if (response instanceof ErrorResponse errorResponse) {
            throw new ServicesException(errorResponse.getErrorMessage());
        }
    }

    @Override
    public List<RaceDTO> findAllRacesDetails() {
        sendRequest(new FindAllRacesDetailsRequest());
        Response response = readResponse();
        return ((FindAllRacesDetailsResponse) response).getAllRacesDetails();
    }

    @Override
    public List<SwimmerDTO> findAllSwimmersDetailsForRace(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle) {
        RaceDetailsDTO raceDetailsDTO = new RaceDetailsDTO(swimmingDistance, swimmingStyle);
        sendRequest(new FindAllSwimmersDetailsForRaceRequest(raceDetailsDTO));
        Response response = readResponse();
        return ((FindAllSwimmersDetailsForRaceResponse) response).getAllSwimmersDetailsForRace();
    }

    @Override
    public void addSwimmer(String firstName, String lastName, Integer age, List<RaceDetailsDTO> raceDetailsDTOS) {
        SwimmerDTO swimmerDTO = new SwimmerDTO(new Swimmer(firstName, lastName, age), raceDetailsDTOS);
        sendRequest(new AddSwimmerRequest(swimmerDTO));
        Response response = readResponse();
        if (response instanceof OkResponse) {
            //TODO logger
            return;
        }
    }

    private void handleUpdate(UpdateResponse updateResponse) {
        Platform.runLater(() -> {
            if (updateResponse instanceof RacesUpdatedResponse) {
                client.racesUpdated();
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
                        handleUpdate((UpdateResponse) response);
                    }
                    else {
                        try {
                            qresponses.put((Response) response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException | ClassNotFoundException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
