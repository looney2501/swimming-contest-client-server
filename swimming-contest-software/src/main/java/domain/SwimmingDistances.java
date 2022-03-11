package domain;

/**
 * Available swimming distances for races.
 */
public enum SwimmingDistances {
    _50,
    _200,
    _800,
    _1500;

    public static SwimmingDistances distanceFromInteger(Integer value) {
        return switch (value) {
            case 1 -> _50;
            case 2 -> _200;
            case 3 -> _800;
            case 4 -> _1500;
            default -> null;
        };
    }

    public static Integer integerFromDistance(SwimmingDistances swimmingDistance) {
        return switch (swimmingDistance) {
            case _50 -> 1;
            case _200 -> 2;
            case _800 -> 3;
            case _1500 -> 4;
        };
    }
}
