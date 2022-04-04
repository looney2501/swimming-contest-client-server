package client;

import client.controller.Controller;
import client.controller.LoginController;
import client.controller.MainController;
import client.controller.RaceViewController;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import model.domain.entities.Admin;
import model.domain.enums.SwimmingDistances;
import model.domain.enums.SwimmingStyles;
import server.repository.dbRepository.AdminDBRepository;
import server.repository.dbRepository.RaceDBRepository;
import server.repository.dbRepository.SwimmerDBRepository;
import server.repository.dbRepository.SwimmerRaceDBRepository;
import server.service.ServerSwimmingRaceServices;

import java.io.FileReader;
import java.io.IOException;
import java.net.URL;
import java.util.Properties;

public class Main extends Application {

    private static ServerSwimmingRaceServices service;
    private static Stage primaryStage;
    private static FXMLLoader fxmlLoader;
    private static Controller currentController;

    @Override
    public void start(Stage primaryStage) throws Exception {
        service = loadService();
        Main.primaryStage = primaryStage;
        initView();
    }

    private ServerSwimmingRaceServices loadService() {
        Properties properties = loadProperties();
        AdminDBRepository adminDBRepository = new AdminDBRepository(properties);
        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);
        SwimmerDBRepository swimmerDBRepository = new SwimmerDBRepository(properties);
        SwimmerRaceDBRepository swimmerRaceDBRepository = new SwimmerRaceDBRepository(properties, swimmerDBRepository, raceDBRepository);
        ServerSwimmingRaceServices service = new ServerSwimmingRaceServices();
        service.setAdminRepository(adminDBRepository);
        service.setRaceRepository(raceDBRepository);
        service.setSwimmerRepository(swimmerDBRepository);
        service.setSwimmerRaceRepository(swimmerRaceDBRepository);
        return service;
    }

    private Properties loadProperties() {
        Properties properties = new Properties();
        try {
            properties.load(new FileReader("D:\\Proiecte\\Java\\MPP-lab-project\\swimming-contest-software\\Server\\bd.config"));
        } catch (IOException e) {
            e.printStackTrace();
        }
        return properties;
    }

    public static void main(String[] args) {
        Application.launch();
    }

    private static void initView() throws IOException {
        primaryStage.setResizable(false);
        primaryStage.setTitle("Swimming Contest");
        changeSceneToLogin();
        primaryStage.show();
    }

    public static void changeSceneToLogin() throws IOException {
        currentController = new LoginController();
        currentController.setService(service);
        URL path = Main.class.getResource("/client/fxml/login-view.fxml");
        fxmlLoader = new FXMLLoader(path);
        fxmlLoader.setController(currentController);
        Parent root = fxmlLoader.load();
        primaryStage.setScene(new Scene(root));
    }

    public static void changeSceneToMainView(Admin loggedAdmin) throws IOException {
        currentController = new MainController();
        currentController.setService(service);
        currentController.setLoggedAdmin(loggedAdmin);
        URL path = Main.class.getResource("/client/fxml/main-view.fxml");
        fxmlLoader = new FXMLLoader(path);
        fxmlLoader.setController(currentController);
        Parent root = fxmlLoader.load();
        primaryStage.setScene(new Scene(root));
    }

    public static void changeSceneToRaceView(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle) throws IOException {
        currentController = new RaceViewController(swimmingDistance, swimmingStyle);
        currentController.setService(service);
        URL path = Main.class.getResource("/client/fxml/race-view.fxml");
        fxmlLoader = new FXMLLoader(path);
        fxmlLoader.setController(currentController);
        Parent root = fxmlLoader.load();
        primaryStage.setScene(new Scene(root));
    }
}
