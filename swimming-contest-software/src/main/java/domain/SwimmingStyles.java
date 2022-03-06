package domain;

/**
 * Available swimming styles for races.
 */
public enum SwimmingStyles {
    _MIXED("mixed"),
    _FREESTYLE("freestyle"),
    _BUTTERFLY("butterfly"),
    _BACKSTROKE("backstroke");

    private final String style;

    SwimmingStyles(String style) {
        this.style = style;
    }

    public String getStyle() {
        return style;
    }
}
