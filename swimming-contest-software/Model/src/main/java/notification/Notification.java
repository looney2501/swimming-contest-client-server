package notification;

public class Notification {
    private NotificationType notificationType;

    public Notification() {}

    public Notification(NotificationType notificationType) {
        this.notificationType = notificationType;
    }

    public NotificationType getNotificationType() {
        return notificationType;
    }

    public void setNotificationType(NotificationType notificationType) {
        this.notificationType = notificationType;
    }

    @Override
    public String toString() {
        return "Notification{" +
                "notificationType=" + notificationType +
                '}';
    }
}
