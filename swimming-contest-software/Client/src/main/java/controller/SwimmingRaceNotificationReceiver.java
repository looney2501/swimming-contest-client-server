package controller;

import javafx.application.Platform;
import notification.Notification;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;
import org.springframework.jms.core.JmsOperations;
import services.NotificationReceiver;
import services.NotificationSubscriber;

import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;
import java.util.concurrent.TimeUnit;

public class SwimmingRaceNotificationReceiver implements NotificationReceiver {

    private final JmsOperations jmsOperations;
    private boolean running;
    ExecutorService service;
    NotificationSubscriber subscriber;
    private static final Logger logger = LogManager.getLogger();

    public SwimmingRaceNotificationReceiver(JmsOperations operations) {
        jmsOperations = operations;
    }

    @Override
    public void start(NotificationSubscriber subscriber) {
        logger.traceEntry("Starting notification receiver ...");
        running = true;
        this.subscriber = subscriber;
        service = Executors.newSingleThreadExecutor();
        service.submit(this::run);
    }

    private void run() {
        while (running) {
            Notification notification = (Notification) jmsOperations.receiveAndConvert();
            logger.traceEntry("Notificaton received: " + notification);
            subscriber.notificationReceived(notification);
        }
    }

    @Override
    public void stop() {
        running = false;
        try {
            service.awaitTermination(100, TimeUnit.MILLISECONDS);
            service.shutdown();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        logger.traceEntry("Stopped notification receiver");
    }
}
