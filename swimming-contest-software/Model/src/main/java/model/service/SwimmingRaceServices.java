package model.service;

import model.domain.dtos.RaceDTO;
import model.domain.dtos.SwimmerDTO;
import model.domain.entities.Admin;
import model.domain.enums.SwimmingDistances;
import model.domain.dtos.RaceDetailsDTO;
import model.domain.enums.SwimmingStyles;

import java.security.NoSuchAlgorithmException;
import java.util.List;

public interface SwimmingRaceServices {
    Admin login(String username, String password) throws NoSuchAlgorithmException, ServiceException;
    void logout(Admin admin);
    List<RaceDTO> findAllRacesDetails();
    List<SwimmerDTO> findAllSwimmersDetailsForRace(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle);
    void addSwimmer(String firstName, String lastName, Integer age, List<RaceDetailsDTO> raceDetailsDTOS);
}
