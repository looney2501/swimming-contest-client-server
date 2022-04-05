package model.protocol;

import model.domain.dtos.SwimmerDTO;

import java.util.List;

public class FindAllSwimmersDetailsForRaceResponse implements Response {
    private final List<SwimmerDTO> allSwimmersDetailsForRace;

    public FindAllSwimmersDetailsForRaceResponse(List<SwimmerDTO> allSwimmersDetailsForRace) {
        this.allSwimmersDetailsForRace = allSwimmersDetailsForRace;
    }

    public List<SwimmerDTO> getAllSwimmersDetailsForRace() {
        return allSwimmersDetailsForRace;
    }
}
