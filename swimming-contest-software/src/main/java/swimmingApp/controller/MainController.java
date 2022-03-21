package swimmingApp.controller;

import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import swimmingApp.Main;

import java.io.IOException;


public class MainController extends Controller {
    @FXML
    private Button logoutButton;
    @FXML
    private TableView racesTableView;
    @FXML
    TableColumn raceNumberColumn;
    @FXML
    TableColumn raceDistanceColumn;
    @FXML
    TableColumn raceStyleColumn;
    @FXML
    TableColumn raceSwimmersNoColumn;

    @FXML
    public void logoutButtonAction() throws IOException {
        Main.changeSceneToLogin();
    }
}
