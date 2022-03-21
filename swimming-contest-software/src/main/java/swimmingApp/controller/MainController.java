package swimmingApp.controller;

import javafx.beans.property.SimpleStringProperty;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.util.Callback;
import swimmingApp.Main;
import swimmingApp.domain.dtos.RaceDTO;

import java.io.IOException;


public class MainController extends Controller {

    @FXML
    private Button logoutButton;
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
    public void logoutButtonAction() throws IOException {
        Main.changeSceneToLogin();
    }

    public void initialize() {
        initializeRacesModel();
        initializeRacesTableView();
    }

    private void initializeRacesTableView() {
        racesTableView.setItems(raceDTOsModel);
        raceNumberColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getRace().getID().toString()));
        raceDistanceColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getRace().getDistance().toString()));
        raceStyleColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getRace().getStyle().toString()));
        raceSwimmersNoColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getNoSwimmers().toString()));
    }

    private void initializeRacesModel() {
        raceDTOsModel.setAll(service.findAllRacesDetails());
    }
}
