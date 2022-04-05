package model.service;

import model.domain.dtos.RaceDTO;
import model.domain.dtos.RaceDetailsDTO;
import model.domain.dtos.SwimmerDTO;
import model.domain.enums.SwimmingDistances;
import model.domain.enums.SwimmingStyles;
import model.observer.SwimmingRaceObserver;

import java.security.NoSuchAlgorithmException;
import java.util.List;

public interface SwimmingRaceServices {
    void login(String username, String password, SwimmingRaceObserver client) throws NoSuchAlgorithmException, ServiceException;
    void logout(String username) throws ServiceException;
    List<RaceDTO> findAllRacesDetails();
    List<SwimmerDTO> findAllSwimmersDetailsForRace(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle);
    void addSwimmer(String firstName, String lastName, Integer age, List<RaceDetailsDTO> raceDetailsDTOS);
}
