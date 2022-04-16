package domain.dtos;

import domain.entities.Swimmer;

import java.io.Serializable;
import java.util.List;

public class SwimmerDTO implements Serializable {
    private final Swimmer swimmer;
    private final List<RaceDetailsDTO> raceDetailsDTOs;

    public SwimmerDTO(Swimmer swimmer, List<RaceDetailsDTO> races) {
        this.swimmer = swimmer;
        this.raceDetailsDTOs = races;
    }

    public Swimmer getSwimmer() {
        return swimmer;
    }

    public List<RaceDetailsDTO> getRaceDetailsDTOs() {
        return raceDetailsDTOs;
    }

    public String getRaces() {
        return raceDetailsDTOs.stream()
                .map(RaceDetailsDTO::toString)
                .reduce("", (subtotal, element) -> subtotal + element);
    }

    @Override
    public String toString() {
        return "SwimmerDTO{" +
                "swimmer=" + swimmer +
                ", raceDetailsDTOS=" + raceDetailsDTOs +
                '}';
    }
}
