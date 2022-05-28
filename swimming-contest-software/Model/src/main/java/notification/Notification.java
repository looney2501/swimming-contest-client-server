package notification;

public class Notification {
    private final NotificationType notificationType;

    public Notification(NotificationType notificationType) {
        this.notificationType = notificationType;
    }

    public NotificationType getNotificationType() {
        return notificationType;
    }

    @Override
    public String toString() {
        return "Notification{" +
                "notificationType=" + notificationType +
                '}';
    }
}
