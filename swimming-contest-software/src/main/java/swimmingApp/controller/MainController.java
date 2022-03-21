package swimmingApp.controller;

import javafx.beans.property.SimpleStringProperty;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.ComboBox;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.util.Callback;
import swimmingApp.Main;
import swimmingApp.domain.SwimmingDistances;
import swimmingApp.domain.SwimmingStyles;
import swimmingApp.domain.dtos.RaceDTO;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;


public class MainController extends Controller {

    @FXML
    private Button logoutButton;
    @FXML
    private Button searchSwimmersButton;
    @FXML
    private ComboBox<SwimmingDistances> distanceComboBox;
    @FXML
    private ComboBox<SwimmingStyles> styleComboBox;
    @FXML
    private TableView<RaceDTO> racesTableView;
    @FXML
    TableColumn<RaceDTO, String> raceNumberColumn;
    @FXML
    TableColumn<RaceDTO, String> raceDistanceColumn;
    @FXML
    TableColumn<RaceDTO, String> raceStyleColumn;
    @FXML
    TableColumn<RaceDTO, String> raceSwimmersNoColumn;
    @FXML
    ObservableList<RaceDTO> raceDTOsModel = FXCollections.observableArrayList();
    @FXML
    ObservableList<SwimmingDistances> distances = FXCollections.observableArrayList();
    @FXML
    ObservableList<SwimmingStyles> styles = FXCollections.observableArrayList();

    @FXML
    public void logoutButtonAction() throws IOException {
        Main.changeSceneToLogin();
    }

    public void initialize() {
        initializeModels();
        initializeRacesTableView();
        initializeComboBoxes();
    }

    private void initializeRacesTableView() {
        racesTableView.setItems(raceDTOsModel);
        raceNumberColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getRace().getID().toString()));
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
