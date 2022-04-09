package domain.dtos;

import domain.enums.SwimmingDistances;
import domain.enums.SwimmingStyles;

import java.io.Serializable;

public class RaceDTO implements Serializable {
    private final SwimmingDistances swimmingDistance;
    private final SwimmingStyles swimmingStyle;
    private final Integer noSwimmers;

    public RaceDTO(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle, Integer noSwimmers) {
        this.swimmingDistance = swimmingDistance;
        this.swimmingStyle = swimmingStyle;
        this.noSwimmers = noSwimmers;
    }

    public SwimmingDistances getDistance() {
        return swimmingDistance;
    }

    public SwimmingStyles getStyle() {
        return swimmingStyle;
    }

    public Integer getNoSwimmers() {
        return noSwimmers;
    }
}
