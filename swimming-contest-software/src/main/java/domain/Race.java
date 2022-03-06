package domain;

import java.io.Serializable;
import java.util.Objects;

public class Race implements Identifiable<Integer>, Serializable {

    private Integer ID;
    private SwimmingDistances distance;
    private SwimmingStyles style;
    private Integer swimmersNumber;

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Race race = (Race) o;
        return Objects.equals(ID, race.ID) && distance == race.distance && style == race.style && Objects.equals(swimmersNumber, race.swimmersNumber);
    }

    @Override
    public int hashCode() {
        return Objects.hash(ID, distance, style, swimmersNumber);
    }

    public SwimmingDistances getDistance() {
        return distance;
    }

    public void setDistance(SwimmingDistances distance) {
        this.distance = distance;
    }

    public SwimmingStyles getStyle() {
        return style;
    }

    public void setStyle(SwimmingStyles style) {
        this.style = style;
    }

    public Integer getSwimmersNumber() {
        return swimmersNumber;
    }

    public void setSwimmersNumber(Integer swimmersNumber) {
        this.swimmersNumber = swimmersNumber;
    }

    @Override
    public Integer getID() {
        return null;
    }

    @Override
    public void setID(Integer id) {

    }
}
