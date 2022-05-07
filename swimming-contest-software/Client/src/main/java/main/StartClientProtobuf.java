package main;

import controller.LoginController;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import services.SwimmingRaceServices;
import services.SwimmingRaceServicesProxyProtobuf;

import java.net.URL;

public class StartClientProtobuf extends Application {

    private static final int defaultPort = 55555;
    private static final String defaultServer = "localhost";

    @Override
    public void start(Stage primaryStage) throws Exception {
        SwimmingRaceServices services = new SwimmingRaceServicesProxyProtobuf(defaultServer, defaultPort);

        LoginController loginController = new LoginController();
        loginController.setService(services);
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
