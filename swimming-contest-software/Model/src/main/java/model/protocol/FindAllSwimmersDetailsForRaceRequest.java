package model.protocol;

import model.domain.dtos.RaceDetailsDTO;

public class FindAllSwimmersDetailsForRaceRequest implements Request {

    private final RaceDetailsDTO raceDetailsDTO;

    public FindAllSwimmersDetailsForRaceRequest(RaceDetailsDTO raceDetailsDTO) {
        this.raceDetailsDTO = raceDetailsDTO;
    }

    public RaceDetailsDTO getRaceDetailsDTO() {
        return raceDetailsDTO;
    }
}
