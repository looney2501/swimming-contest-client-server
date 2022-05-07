package services;

import domain.dtos.RaceDTO;
import domain.dtos.RaceDetailsDTO;
import domain.dtos.SwimmerDTO;
import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;
import observer.SwimmingRaceObserver;

import java.security.NoSuchAlgorithmException;
import java.util.List;

public interface SwimmingRaceServices {
    void login(String username, String password, SwimmingRaceObserver client) throws NoSuchAlgorithmException, ServicesException;
    void logout(String username) throws ServicesException;
    List<RaceDTO> findAllRacesDetails();
    List<SwimmerDTO> findAllSwimmersDetailsForRace(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle);
    void addSwimmer(String firstName, String lastName, Integer age, List<RaceDetailsDTO> raceDetailsDTOs);
}
