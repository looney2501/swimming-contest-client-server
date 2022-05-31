package main;

import controller.LoginControllerAMS;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import java.net.URL;

public class StartClientAMS extends Application {
    @Override
    public void start(Stage primaryStage) throws Exception {
        ClassPathXmlApplicationContext context = ApplicationContextWrapper.getInstance();
        LoginControllerAMS loginController = context.getBean("loginController", LoginControllerAMS.class);
        loginController.setStage(primaryStage);

        URL path = this.getClass().getResource("../client/fxml/login-view.fxml");
        FXMLLoader fxmlLoader = new FXMLLoader(path);
        fxmlLoader.setController(loginController);

        Parent root = fxmlLoader.load();
        primaryStage.setScene(new Scene(root));
        primaryStage.setTitle("Login");
        primaryStage.show();
    }
}
