package model.protocol;

import model.domain.dtos.RaceDTO;

import java.util.List;

public class FindAllRacesDetailsResponse implements Response {
    private final List<RaceDTO> allRacesDetails;

    public FindAllRacesDetailsResponse(List<RaceDTO> allRacesDetails) {
        this.allRacesDetails = allRacesDetails;
    }

    public List<RaceDTO> getAllRacesDetails() {
        return allRacesDetails;
    }
}
