package service;

import domain.dtos.RaceDTO;
import domain.dtos.RaceDetailsDTO;
import domain.dtos.SwimmerDTO;
import domain.enums.SwimmingDistances;
import domain.enums.SwimmingStyles;
import observer.SwimmingRaceObserver;
import service.ServiceException;

import java.security.NoSuchAlgorithmException;
import java.util.List;

public interface SwimmingRaceServices {
    void login(String username, String password, SwimmingRaceObserver client) throws NoSuchAlgorithmException, ServiceException;
    void logout(String username) throws ServiceException;
    List<RaceDTO> findAllRacesDetails();
    List<SwimmerDTO> findAllSwimmersDetailsForRace(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle);
    void addSwimmer(String firstName, String lastName, Integer age, List<RaceDetailsDTO> raceDetailsDTOS);
}
