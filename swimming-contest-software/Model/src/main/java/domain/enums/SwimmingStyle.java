package domain.enums;

/**
 * Available swimming styles for races.
 */
public enum SwimmingStyle {
    Mixed,
    Freestyle,
    Butterfly,
    Backstroke;

    public static SwimmingStyle styleFromInteger(Integer value) {
        return switch (value) {
            case 1 -> Mixed;
            case 2 -> Freestyle;
            case 3 -> Butterfly;
            case 4 -> Backstroke;
            default -> null;
        };
    }

    public static Integer integerFromStyle(SwimmingStyle swimmingDistance) {
        return switch (swimmingDistance) {
            case Mixed -> 1;
            case Freestyle -> 2;
            case Butterfly -> 3;
            case Backstroke -> 4;
        };
    }
}
