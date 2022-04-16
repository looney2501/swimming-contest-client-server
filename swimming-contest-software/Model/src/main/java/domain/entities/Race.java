package domain.entities;

import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;

import java.io.Serializable;
import java.util.Objects;

public class Race implements Identifiable<Integer>, Serializable {

    private Integer ID;
    private SwimmingDistance distance;
    private SwimmingStyle style;
    private Integer swimmersNumber;

    public Race(Integer ID, SwimmingDistance distance, SwimmingStyle style, Integer swimmersNumber) {
        this.ID = ID;
        this.distance = distance;
        this.style = style;
        this.swimmersNumber = swimmersNumber;
    }

    public Race(SwimmingDistance distance, SwimmingStyle style, Integer swimmersNumber) {
        this.ID = null;
        this.distance = distance;
        this.style = style;
        this.swimmersNumber = swimmersNumber;
    }

    @Override
    public Integer getID() {
        return ID;
    }

    @Override
    public void setID(Integer id) {
        ID = id;
    }

    public SwimmingDistance getDistance() {
        return distance;
    }

    public void setDistance(SwimmingDistance distance) {
        this.distance = distance;
    }

    public SwimmingStyle getStyle() {
        return style;
    }

    public void setStyle(SwimmingStyle style) {
        this.style = style;
    }

    public Integer getSwimmersNumber() {
        return swimmersNumber;
    }

    public void setSwimmersNumber(Integer swimmersNumber) {
        this.swimmersNumber = swimmersNumber;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Race race = (Race) o;
        return distance == race.distance && style == race.style && Objects.equals(swimmersNumber, race.swimmersNumber);
    }

    @Override
    public int hashCode() {
        return Objects.hash(ID);
    }

    @Override
    public String toString() {
        return "Race{" +
                "ID=" + ID +
                ", distance=" + distance +
                ", style=" + style +
                ", swimmersNumber=" + swimmersNumber +
                '}';
    }
}
