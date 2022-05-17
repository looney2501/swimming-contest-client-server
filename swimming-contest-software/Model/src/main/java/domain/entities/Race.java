package domain.entities;

import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;

import java.io.Serializable;
import java.util.Objects;

public class Race implements Identifiable<Integer>, Serializable {

    private Integer id;
    private SwimmingDistance distance;
    private SwimmingStyle style;
    private Integer swimmersNumber;

    public Race() {}

    public Race(Integer id, SwimmingDistance distance, SwimmingStyle style, Integer swimmersNumber) {
        this.id = id;
        this.distance = distance;
        this.style = style;
        this.swimmersNumber = swimmersNumber;
    }

    public Race(SwimmingDistance distance, SwimmingStyle style, Integer swimmersNumber) {
        this.id = null;
        this.distance = distance;
        this.style = style;
        this.swimmersNumber = swimmersNumber;
    }

    @Override
    public Integer getId() {
        return id;
    }

    @Override
    public void setId(Integer id) {
        this.id = id;
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
        return Objects.hash(id);
    }

    @Override
    public String toString() {
        return "Race{" +
                "ID=" + id +
                ", distance=" + distance +
                ", style=" + style +
                ", swimmersNumber=" + swimmersNumber +
                '}';
    }
}
