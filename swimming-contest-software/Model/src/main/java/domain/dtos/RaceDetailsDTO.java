package domain.dtos;

import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;

import java.io.Serializable;

public class RaceDetailsDTO implements Serializable {
    SwimmingDistance swimmingDistance;
    SwimmingStyle swimmingStyle;

    public RaceDetailsDTO(SwimmingDistance swimmingDistance, SwimmingStyle swimmingStyle) {
        this.swimmingDistance = swimmingDistance;
        this.swimmingStyle = swimmingStyle;
    }

    public SwimmingDistance getSwimmingDistance() {
        return swimmingDistance;
    }

    public SwimmingStyle getSwimmingStyle() {
        return swimmingStyle;
    }

    @Override
    public String toString() {
        return swimmingDistance + ", " + swimmingStyle + "; ";
    }
}
