package domain.dtos;

import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;

import java.io.Serializable;

public class RaceDTO implements Serializable {
    private final SwimmingDistance swimmingDistance;
    private final SwimmingStyle swimmingStyle;
    private final Integer noSwimmers;

    public RaceDTO(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle, Integer noSwimmers) {
        this.swimmingDistance = swimmingDistance;
        this.swimmingStyle = swimmingStyle;
        this.noSwimmers = noSwimmers;
    }

    public SwimmingDistance getDistance() {
        return swimmingDistance;
    }

    public SwimmingStyle getStyle() {
        return swimmingStyle;
    }

    public Integer getNoSwimmers() {
        return noSwimmers;
    }
}
