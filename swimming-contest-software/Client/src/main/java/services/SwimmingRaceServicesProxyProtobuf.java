package services;

import com.google.protobuf.DescriptorProtos;
import com.google.protobuf.Descriptors;
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
import protocol.protobuf.ProtobufUtils;
import protocol.protobuf.SwimmingContestProtobuf;
import protocol.requests.*;
import protocol.responses.*;

import java.io.*;
import java.net.Socket;
import java.net.SocketException;
import java.security.NoSuchAlgorithmException;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class SwimmingRaceServicesProxyProtobuf implements SwimmingRaceServices {

    private final String host;
    private final int port;
    private SwimmingRaceObserver client;
    private OutputStream outputStream;
    private InputStream inputStream;
    private Socket socket;
    private final BlockingQueue<SwimmingContestProtobuf.Response> qresponses;
    private volatile boolean finished;
    private final static Logger logger = LogManager.getLogger();

    public SwimmingRaceServicesProxyProtobuf(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses = new LinkedBlockingQueue<>();
    }

    @Override
    public void login(String username, String password, SwimmingRaceObserver client) throws NoSuchAlgorithmException, ServicesException {
        initializeConnection();
        AdminDTO adminDTO = new AdminDTO(username, password);
        logger.info("Sending login request: username: {}, password: {}...", username, password);
        sendRequest(ProtobufUtils.createLoginRequest(adminDTO));
        logger.info("Waiting for response...");
        SwimmingContestProtobuf.Response response = readResponse();
        if (response.hasOkResponse()) {
            logger.info("OkResponse received!");
            this.client = client;
        }
        if (response.hasErrorResponse()) {
            logger.info("ErrorResponse received!");
            closeConnection();
            throw new ServicesException(response.getErrorResponse().getErrorMessage());
        }
    }

    private void initializeConnection() {
        try {
            logger.info("Connection details: host: {}, port: {}", host, port);
            socket = new Socket(host, port);
            logger.info("Successfully connected!");
            outputStream = socket.getOutputStream();
            inputStream = socket.getInputStream();
            finished = false;
            startReader();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void startReader() {
        Thread tw = new Thread(new SwimmingRaceServicesProxyProtobuf.ReaderThread());
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

    private void sendRequest(SwimmingContestProtobuf.Request request) {
        try {
            request.writeDelimitedTo(outputStream);
            logger.info("Request sent successfully!");
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private SwimmingContestProtobuf.Response readResponse() {
        SwimmingContestProtobuf.Response response = null;
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
        sendRequest(ProtobufUtils.createLogoutRequest(username));
        logger.info("Waiting for response...");
        SwimmingContestProtobuf.Response response = readResponse();
        if (response.hasOkResponse()) {
            logger.info("OkResponse received!");
            closeConnection();
        }
        if (response.hasErrorResponse()) {
            logger.info("ErrorResponse received!");
            throw new ServicesException(response.getErrorResponse().getErrorMessage());
        }
    }

    @Override
    public List<RaceDTO> findAllRacesDetails() {
        logger.info("Sending FindAllRacesDetailsRequest...");
        sendRequest(ProtobufUtils.createFindAllRacesDetailsRequest());
        logger.info("Waiting for response...");
        SwimmingContestProtobuf.Response response = readResponse();
        if (response.hasFindAllRacesDetailsResponse()) {
            logger.info("FindAllRacesDetailsResponse received!");
            //TODO
            return response.getFindAllRacesDetailsResponse().getAllRacesDetailsList().stream()
                    .map(ProtobufUtils::raceDTOFromProtobuf)
                    .toList();
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
        sendRequest(ProtobufUtils.createFindAllSwimmersDetailsForRaceRequest(raceDetailsDTO));
        logger.info("Waiting for response...");
        SwimmingContestProtobuf.Response response = readResponse();
        if (response.hasFindAllSwimmersDetailsForRaceResponse()) {
            //TODO
            return response.getFindAllSwimmersDetailsForRaceResponse().getAllSwimmersDetailsForRaceList().stream()
                    .map(ProtobufUtils::swimmerDTOFromProtobuf)
                    .toList();
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
        sendRequest(ProtobufUtils.createAddSwimmerRequest(swimmerDTO));
        logger.info("Waiting for response...");
        SwimmingContestProtobuf.Response response = readResponse();
        if (response.hasOkResponse()) {
            logger.info("OkResponse received!");
        }
        else {
            logger.info("Wrong response received!");
        }
    }

    private void handleUpdate(SwimmingContestProtobuf.Response updateResponse) {
        Platform.runLater(() -> {
            if (updateResponse.hasRacesUpdatedResponse()) {
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
                    SwimmingContestProtobuf.Response response = SwimmingContestProtobuf.Response.parseDelimitedFrom(inputStream);
                    if (response.hasRacesUpdatedResponse()) {
                        logger.info("UpdateResponse received. Handling response...");
                        handleUpdate(response);
                    }
                    else {
                        try {
                            qresponses.put(response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                }
                catch (SocketException e) {
                    //e.printStackTrace();
                }
                catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }
}
