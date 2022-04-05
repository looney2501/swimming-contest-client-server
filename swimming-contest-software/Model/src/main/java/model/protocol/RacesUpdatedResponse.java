package model.protocol;

import model.domain.dtos.RaceDTO;

import java.util.List;

public class RacesUpdatedResponse implements UpdateResponse {
    private final List<RaceDTO> allRacesDetails;

    public RacesUpdatedResponse(List<RaceDTO> allRacesDetails) {
        this.allRacesDetails = allRacesDetails;
    }

    public List<RaceDTO> getAllRacesDetails() {
        return allRacesDetails;
    }
}
