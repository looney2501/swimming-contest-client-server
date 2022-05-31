package main;

import org.springframework.context.support.ClassPathXmlApplicationContext;

public class ApplicationContextWrapper {
    private static ClassPathXmlApplicationContext instance = null;

    private ApplicationContextWrapper() {}

    public static ClassPathXmlApplicationContext getInstance() {
        if (instance == null) {
            instance = new ClassPathXmlApplicationContext("spring-client.xml");
        }
        return instance;
    }
}
