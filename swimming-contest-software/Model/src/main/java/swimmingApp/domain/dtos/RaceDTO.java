package swimmingApp.domain.dtos;

import swimmingApp.domain.enums.SwimmingDistances;
import swimmingApp.domain.enums.SwimmingStyles;

public class RaceDTO {
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
