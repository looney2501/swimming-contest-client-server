package services;

import domain.dtos.RaceDTO;
import domain.dtos.RaceDetailsDTO;
import domain.dtos.SwimmerDTO;
import domain.entities.Admin;
import domain.entities.Race;
import domain.entities.Swimmer;
import domain.entities.SwimmerRace;
import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;
import observer.SwimmingRaceObserver;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import repository.AdminRepository;
import repository.RaceRepository;
import repository.SwimmerRaceRepository;
import repository.SwimmerRepository;

import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class SwimmingRaceServicesServerAMS implements SwimmingRaceServicesAMS {

    private AdminRepository adminRepository;
    private RaceRepository raceRepository;
    private SwimmerRepository swimmerRepository;
    private SwimmerRaceRepository swimmerRaceRepository;
    private NotificationServices notificationServices;
    private final Map<String, Admin> loggedClients = new HashMap<>();
    private static final Logger logger = LogManager.getLogger();

    public SwimmingRaceServicesServerAMS(AdminRepository adminRepository, RaceRepository raceRepository, SwimmerRepository swimmerRepository, SwimmerRaceRepository swimmerRaceRepository, NotificationServices notificationServices) {
        this.adminRepository = adminRepository;
        this.raceRepository = raceRepository;
        this.swimmerRepository = swimmerRepository;
        this.swimmerRaceRepository = swimmerRaceRepository;
        this.notificationServices = notificationServices;
    }

    @Override
    public void login(String username, String password) throws NoSuchAlgorithmException, ServicesException {
        Admin admin = adminRepository.findByUsernameAndPassword(username, password);
        if (admin != null) {
            if (loggedClients.get(admin.getUsername()) != null) {
                throw new ServicesException("User already logged in!");
            }
            else {
                loggedClients.put(username, admin);
            }
        }
        else {
            throw new ServicesException("Incorrect username or password!");
        }
    }

    @Override
    public void logout(String username) throws ServicesException {
        if (loggedClients.remove(username) == null) {
            throw new ServicesException("User is not logged in!");
        }
    }

    @Override
    public List<RaceDTO> findAllRacesDetails() {
        List<RaceDTO> raceDTOS = new ArrayList<>();
        for (Race race: raceRepository.findAllRaces()) {
            raceDTOS.add(new RaceDTO(race.getDistance(), race.getStyle(), swimmerRaceRepository.getNumberOfSwimmersForRace(race)));
        }
        return raceDTOS;
    }

    @Override
    public List<SwimmerDTO> findAllSwimmersDetailsForRace(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle) {
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

    @Override
    public void addSwimmer(String firstName, String lastName, Integer age, List<RaceDetailsDTO> raceDetailsDTOs) {
        Swimmer swimmer = new Swimmer(firstName, lastName, age);
        Integer swimmerID = swimmerRepository.add(swimmer);
        swimmer.setId(swimmerID);

        for (var raceDetailDTO : raceDetailsDTOs) {
            Race race = raceRepository.findRaceByDistanceAndStyle(raceDetailDTO.getSwimmingDistance(), raceDetailDTO.getSwimmingStyle());
            SwimmerRace swimmerRace = new SwimmerRace(swimmer, race);
            swimmerRaceRepository.add(swimmerRace);
        }

        notificationServices.racesUpdated();
    }
}
