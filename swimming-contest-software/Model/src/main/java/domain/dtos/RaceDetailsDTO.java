package domain.dtos;

import domain.enums.SwimmingDistances;
import domain.enums.SwimmingStyles;

import java.io.Serializable;

public class RaceDetailsDTO implements Serializable {
    SwimmingDistances swimmingDistance;
    SwimmingStyles swimmingStyle;

    public RaceDetailsDTO(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle) {
        this.swimmingDistance = swimmingDistance;
        this.swimmingStyle = swimmingStyle;
    }

    public SwimmingDistances getSwimmingDistance() {
        return swimmingDistance;
    }

    public SwimmingStyles getSwimmingStyle() {
        return swimmingStyle;
    }

    @Override
    public String toString() {
        return swimmingDistance + ", " + swimmingStyle + "; ";
    }
}
