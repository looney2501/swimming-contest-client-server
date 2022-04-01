package swimmingApp.domain.dtos;

import swimmingApp.domain.entities.Race;

public class RaceDTO {
    private Race race;
    private Integer noSwimmers;

    public RaceDTO(Race race, Integer noSwimmers) {
        this.race = race;
        this.noSwimmers = noSwimmers;
    }

    public Race getRace() {
        return race;
    }

    public Integer getNoSwimmers() {
        return noSwimmers;
    }
}
