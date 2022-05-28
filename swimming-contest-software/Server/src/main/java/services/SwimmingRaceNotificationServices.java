package services;

import notification.Notification;
import notification.NotificationType;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.jms.core.JmsOperations;

public class SwimmingRaceNotificationServices implements NotificationServices {

    private final JmsOperations jmsOperations;
    private final static Logger logger = LogManager.getLogger();

    public SwimmingRaceNotificationServices(JmsOperations jmsOperations) {
        this.jmsOperations = jmsOperations;
    }

    @Override
    public void racesUpdated() {
        logger.traceEntry("Sending races updated notification to ActiveMQ...");
        Notification notification = new Notification(NotificationType.RACES_UPDATED);
        jmsOperations.convertAndSend(notification);
        logger.traceEntry("Notification sent: " + notification);
    }
}
