package domain;

/**
 * Available swimming distances for races.
 */
public enum SwimmingDistances {
    _50(50),
    _200(200),
    _800(800),
    _1500(1500);

    private final Integer distance;

    SwimmingDistances(Integer distance) {
        this.distance = distance;
    }

    public Integer getDistance() {
        return distance;
    }
}
