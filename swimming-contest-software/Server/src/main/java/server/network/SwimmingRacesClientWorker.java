package server.network;

import model.observer.SwimmingRaceObserver;
import model.protocol.*;
import model.service.ServiceException;
import model.service.SwimmingRaceServices;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.security.NoSuchAlgorithmException;

public class SwimmingRacesClientWorker implements Runnable, SwimmingRaceObserver {

    private SwimmingRaceServices swimmingRaceServices;
    private Socket clientSocket;
    private ObjectInputStream objectInputStream;
    private ObjectOutputStream objectOutputStream;
    private volatile boolean connected;

    public SwimmingRacesClientWorker(SwimmingRaceServices swimmingRaceServices, Socket socket) {
        this.swimmingRaceServices = swimmingRaceServices;
        this.clientSocket = socket;
        try {
            objectOutputStream = new ObjectOutputStream(clientSocket.getOutputStream());
            objectOutputStream.flush();
            objectInputStream = new ObjectInputStream(clientSocket.getInputStream());
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
                }
            } catch (IOException e) {
                e.printStackTrace();
            } catch (ClassNotFoundException e) {
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
    public void raceUpdated() {
        var allRacesDetails = swimmingRaceServices.findAllRacesDetails();
        try {
            sendResponse(new RacesUpdatedResponse(allRacesDetails));
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private Response handleRequest(Request request) {
        if (request instanceof LoginRequest loginRequest) {
            var adminDTO = loginRequest.getAdmin();
            try {
                swimmingRaceServices.login(adminDTO.getUsername(), adminDTO.getPassword(), this);
                return new OkResponse();
            } catch (NoSuchAlgorithmException e) {
                e.printStackTrace();
            } catch (ServiceException e) {
                connected = false;
                return new ErrorResponse(e.getMessage());
            }
        }

        if (request instanceof LogoutRequest loginRequest) {
            var adminUsername = loginRequest.getUsername();
            try {
                swimmingRaceServices.logout(adminUsername);
                return new OkResponse();
            } catch (ServiceException e) {
                connected = false;
                return new ErrorResponse(e.getMessage());
            }
        }

        if (request instanceof FindAllRacesDetailsRequest findAllRacesDetailsRequest) {
            var allRacesDetails = swimmingRaceServices.findAllRacesDetails();
            return new FindAllRacesDetailsResponse(allRacesDetails);
        }

        if (request instanceof FindAllSwimmersDetailsForRaceRequest findAllSwimmersDetailsForRaceRequest) {
            var raceDetailsDTO = findAllSwimmersDetailsForRaceRequest.getRaceDetailsDTO();
            var allSwimmersDetailsForRace = swimmingRaceServices.findAllSwimmersDetailsForRace(raceDetailsDTO.getSwimmingDistance(),
                    raceDetailsDTO.getSwimmingStyle());
            return new FindAllSwimmersDetailsForRaceResponse(allSwimmersDetailsForRace);
        }

        if (request instanceof AddSwimmerRequest addSwimmerRequest) {
            var swimmerDTO = addSwimmerRequest.getSwimmerDTO();
            swimmingRaceServices.addSwimmer(swimmerDTO.getSwimmer().getFirstName(),
                    swimmerDTO.getSwimmer().getLastName(),
                    swimmerDTO.getSwimmer().getAge(),
                    swimmerDTO.getRaceDetailsDTOS());
            return new OkResponse();
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
