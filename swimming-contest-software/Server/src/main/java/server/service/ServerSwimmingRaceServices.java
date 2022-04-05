package server.service;

import model.domain.dtos.RaceDTO;
import model.domain.dtos.RaceDetailsDTO;
import model.domain.dtos.SwimmerDTO;
import model.domain.entities.Admin;
import model.domain.entities.Race;
import model.domain.entities.Swimmer;
import model.domain.entities.SwimmerRace;
import model.domain.enums.SwimmingDistances;
import model.domain.enums.SwimmingStyles;
import model.observer.SwimmingRaceObserver;
import model.service.ServiceException;
import model.service.SwimmingRaceServices;
import server.repository.interfaces.AdminRepository;
import server.repository.interfaces.RaceRepository;
import server.repository.interfaces.SwimmerRaceRepository;
import server.repository.interfaces.SwimmerRepository;

import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

public class ServerSwimmingRaceServices implements SwimmingRaceServices {

    private AdminRepository adminRepository;
    private RaceRepository raceRepository;
    private SwimmerRepository swimmerRepository;
    private SwimmerRaceRepository swimmerRaceRepository;
    private Map<String, SwimmingRaceObserver> loggedClients;

    public ServerSwimmingRaceServices() {
        loggedClients = new ConcurrentHashMap<>();
    }

    public void setAdminRepository(AdminRepository adminRepository) {
        this.adminRepository = adminRepository;
    }

    public void setRaceRepository(RaceRepository raceRepository) {
        this.raceRepository = raceRepository;
    }

    public void setSwimmerRepository(SwimmerRepository swimmerRepository) {
        this.swimmerRepository = swimmerRepository;
    }

    public void setSwimmerRaceRepository(SwimmerRaceRepository swimmerRaceRepository) {
        this.swimmerRaceRepository = swimmerRaceRepository;
    }

    @Override
    public synchronized void login(String username, String password, SwimmingRaceObserver client) throws NoSuchAlgorithmException, ServiceException {
        Admin admin = adminRepository.findByUsernameAndPassword(username, PasswordHashingUtils.MD5Hashing(password));
        if (admin != null) {
            if (loggedClients.get(admin.getUsername()) != null) {
                throw new ServiceException("User already logged in!");
            }
            loggedClients.put(username, client);
        }
        else {
            throw new ServiceException("Incorrect username or password!");
        }
    }

    @Override
    public synchronized void logout(String username) throws ServiceException {
        if (loggedClients.remove(username) == null) {
            throw new ServiceException("User is not logged in!");
        }
    }

    public synchronized List<RaceDTO> findAllRacesDetails() {
        List<RaceDTO> raceDTOS = new ArrayList<>();
        for (Race race: raceRepository.findAllRaces()) {
            raceDTOS.add(new RaceDTO(race.getDistance(), race.getStyle(), swimmerRaceRepository.getNumberOfSwimmersForRace(race)));
        }
        return raceDTOS;
    }

    public synchronized List<SwimmerDTO> findAllSwimmersDetailsForRace(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle) {
        List<SwimmerDTO> swimmerDTOS = new ArrayList<>();
        Race race = raceRepository.findRaceByDistanceAndStyle(swimmingDistance, swimmingStyle);
        for (Swimmer swimmer: swimmerRaceRepository.findAllSwimmersForRace(race)) {
            List<Race> racesForSwimmer = swimmerRaceRepository.findAllRacesForSwimmer(swimmer);
            List<RaceDetailsDTO> raceDetailsDTOS = racesForSwimmer.stream()
                            .map(x -> new RaceDetailsDTO(x.getDistance(), x.getStyle()))
                                    .toList();
            swimmerDTOS.add(new SwimmerDTO(swimmer, raceDetailsDTOS));
        }
        return swimmerDTOS;
    }

    public synchronized void addSwimmer(String firstName, String lastName, Integer age, List<RaceDetailsDTO> raceDetailsDTOS) {
        Swimmer swimmer = new Swimmer(firstName, lastName, age);
        Integer swimmerID = swimmerRepository.add(swimmer);
        swimmer.setID(swimmerID);

        for (var raceDetailDTO : raceDetailsDTOS) {
            Race race = raceRepository.findRaceByDistanceAndStyle(raceDetailDTO.getSwimmingDistance(), raceDetailDTO.getSwimmingStyle());
            SwimmerRace swimmerRace = new SwimmerRace(swimmer, race);
            swimmerRaceRepository.add(swimmerRace);
        }

        notifyAddedSwimmer();
    }

    private void notifyAddedSwimmer() {
        for (var client: loggedClients.values()) {
            client.raceUpdated();
        }
    }
}
