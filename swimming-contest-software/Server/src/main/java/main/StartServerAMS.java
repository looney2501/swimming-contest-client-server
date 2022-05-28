package main;

import network.AbstractServer;
import network.SwimmingRaceConcurrentServerAMS;
import org.springframework.context.support.ClassPathXmlApplicationContext;

public class StartServerAMS {
    public static void main(String[] args) {
        ClassPathXmlApplicationContext context = new ClassPathXmlApplicationContext("spring-server.xml");
        AbstractServer server = context.getBean("serverAMS", SwimmingRaceConcurrentServerAMS.class);
        server.start();
    }
}
