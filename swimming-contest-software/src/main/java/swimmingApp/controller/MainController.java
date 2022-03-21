package swimmingApp.controller;

import javafx.beans.property.SimpleStringProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.scene.control.Alert;
import javafx.scene.control.ComboBox;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import swimmingApp.Main;
import swimmingApp.domain.SwimmingDistances;
import swimmingApp.domain.SwimmingStyles;
import swimmingApp.domain.dtos.RaceDTO;

import java.io.IOException;
import java.util.Arrays;


public class MainController extends Controller {

    @FXML
    private ComboBox<SwimmingDistances> distanceComboBox;
    @FXML
    private ComboBox<SwimmingStyles> styleComboBox;
    @FXML
    private TableView<RaceDTO> racesTableView;
    @FXML
    private TableColumn<RaceDTO, String> raceDistanceColumn;
    @FXML
    private TableColumn<RaceDTO, String> raceStyleColumn;
    @FXML
    private TableColumn<RaceDTO, String> raceSwimmersNoColumn;
    @FXML
    private final ObservableList<RaceDTO> raceDTOsModel = FXCollections.observableArrayList();
    @FXML
    private final ObservableList<SwimmingDistances> distances = FXCollections.observableArrayList();
    @FXML
    private final ObservableList<SwimmingStyles> styles = FXCollections.observableArrayList();

    @FXML
    public void logoutButtonAction() throws IOException {
        Main.changeSceneToLogin();
    }

    @FXML
    public void raceSearchAction() throws IOException {
        SwimmingDistances swimmingDistance = distanceComboBox.getValue();
        SwimmingStyles swimmingStyle = styleComboBox.getValue();
        if (swimmingDistance == null || swimmingStyle == null) {
            MessageAlert.showErrorMessage(null, "Valori invalide!");
        }
        else {
            Main.changeSceneToRaceView(swimmingDistance, swimmingStyle);
        }
    }

    public void initialize() {
        initializeModels();
        initializeRacesTableView();
        initializeComboBoxes();
    }

    private void initializeRacesTableView() {
        racesTableView.setItems(raceDTOsModel);
        raceDistanceColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getRace().getDistance().toString()));
        raceStyleColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getRace().getStyle().toString()));
        raceSwimmersNoColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getNoSwimmers().toString()));
    }

    private void initializeModels() {
        raceDTOsModel.setAll(service.findAllRacesDetails());
        distances.setAll(Arrays.asList(SwimmingDistances._50, SwimmingDistances._200, SwimmingDistances._800, SwimmingDistances._1500));
        styles.setAll(Arrays.asList(SwimmingStyles._FREESTYLE, SwimmingStyles._BACKSTROKE, SwimmingStyles._BUTTERFLY, SwimmingStyles._MIXED));
    }

    private void initializeComboBoxes() {
        distanceComboBox.setItems(distances);
        styleComboBox.setItems(styles);
    }
}
