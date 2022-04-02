package model.domain.dtos;

import model.domain.enums.SwimmingDistances;
import model.domain.enums.SwimmingStyles;

public class RaceDetailsDTO {
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
