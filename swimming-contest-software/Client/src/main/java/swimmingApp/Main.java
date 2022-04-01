package swimmingApp;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import swimmingApp.controller.Controller;
import swimmingApp.controller.LoginController;
import swimmingApp.controller.MainController;
import swimmingApp.controller.RaceViewController;
import swimmingApp.domain.enums.SwimmingDistances;
import swimmingApp.domain.enums.SwimmingStyles;
import swimmingApp.repository.dbRepository.AdminDBRepository;
import swimmingApp.repository.dbRepository.RaceDBRepository;
import swimmingApp.repository.dbRepository.SwimmerDBRepository;
import swimmingApp.repository.dbRepository.SwimmerRaceDBRepository;
import swimmingApp.service.Service;

import java.io.FileReader;
import java.io.IOException;
import java.net.URL;
import java.util.Properties;

public class Main extends Application {

    private static Service service;
    private static Stage primaryStage;
    private static FXMLLoader fxmlLoader;
    private static Controller currentController;

    @Override
    public void start(Stage primaryStage) throws Exception {
        service = loadService();
        Main.primaryStage = primaryStage;
        initView();
    }

    private Service loadService() {
        Properties properties = loadProperties();
        AdminDBRepository adminDBRepository = new AdminDBRepository(properties);
        RaceDBRepository raceDBRepository = new RaceDBRepository(properties);
        SwimmerDBRepository swimmerDBRepository = new SwimmerDBRepository(properties);
        SwimmerRaceDBRepository swimmerRaceDBRepository = new SwimmerRaceDBRepository(properties, swimmerDBRepository, raceDBRepository);
        Service service = new Service();
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
        URL path = Main.class.getResource("/swimmingApp/fxml/login-view.fxml");
        fxmlLoader = new FXMLLoader(path);
        fxmlLoader.setController(currentController);
        Parent root = fxmlLoader.load();
        primaryStage.setScene(new Scene(root));
    }

    public static void changeSceneToMainView() throws IOException {
        currentController = new MainController();
        currentController.setService(service);
        URL path = Main.class.getResource("/swimmingApp/fxml/main-view.fxml");
        fxmlLoader = new FXMLLoader(path);
        fxmlLoader.setController(currentController);
        Parent root = fxmlLoader.load();
        primaryStage.setScene(new Scene(root));
    }

    public static void changeSceneToRaceView(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle) throws IOException {
        currentController = new RaceViewController(swimmingDistance, swimmingStyle);
        currentController.setService(service);
        URL path = Main.class.getResource("/swimmingApp/fxml/race-view.fxml");
        fxmlLoader = new FXMLLoader(path);
        fxmlLoader.setController(currentController);
        Parent root = fxmlLoader.load();
        primaryStage.setScene(new Scene(root));
    }
}
