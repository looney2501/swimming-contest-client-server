package swimmingApp.domain.dtos;

import swimmingApp.domain.SwimmingDistances;
import swimmingApp.domain.SwimmingStyles;

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
