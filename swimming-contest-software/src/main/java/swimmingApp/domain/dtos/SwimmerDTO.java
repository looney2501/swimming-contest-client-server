package swimmingApp.domain.dtos;

import swimmingApp.domain.Race;
import swimmingApp.domain.Swimmer;

import java.util.List;

public class SwimmerDTO {
    private Swimmer swimmer;
    private List<RaceDetailsDTO> raceDetailsDTOS;

    public SwimmerDTO(Swimmer swimmer, List<RaceDetailsDTO> races) {
        this.swimmer = swimmer;
        this.raceDetailsDTOS = races;
    }

    public Swimmer getSwimmer() {
        return swimmer;
    }

    public List<RaceDetailsDTO> getRaceDetailsDTOS() {
        return raceDetailsDTOS;
    }
}
