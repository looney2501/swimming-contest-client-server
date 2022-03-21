package swimmingApp;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import swimmingApp.controller.LoginController;
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

    @Override
    public void start(Stage primaryStage) throws Exception {
        service = loadService();
        this.primaryStage = primaryStage;
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
            //TODO nu gaseste calea
            properties.load(new FileReader("D:\\Proiecte\\Java\\MPP-lab-project\\swimming-contest-software\\bd.config"));
        } catch (IOException e) {
            e.printStackTrace();
        }
        return properties;
    }

    public static void main(String[] args) {
        launch();
    }

    private static void initView() throws IOException {
        primaryStage.setResizable(false);
        changeSceneToLogin();
        primaryStage.show();
    }

    public static void changeSceneToLogin() throws IOException {
        LoginController currentController = new LoginController();
        currentController.setService(service);
        URL path = Main.class.getResource("fxml/login-view.fxml");
        fxmlLoader = new FXMLLoader(path);
        fxmlLoader.setController(currentController);
        Parent root = fxmlLoader.load();
        primaryStage.setScene(new Scene(root));
    }
}
