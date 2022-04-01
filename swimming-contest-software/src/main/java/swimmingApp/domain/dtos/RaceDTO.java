package swimmingApp.domain.dtos;

import swimmingApp.domain.enums.SwimmingDistances;
import swimmingApp.domain.enums.SwimmingStyles;

public class RaceDTO {
    private SwimmingDistances swimmingDistance;
    private SwimmingStyles swimmingStyle;
    private Integer noSwimmers;

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
