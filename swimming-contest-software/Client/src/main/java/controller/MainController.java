package controller;

import domain.dtos.SwimmerDTO;
import javafx.beans.property.SimpleStringProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.*;
import javafx.stage.Stage;
import javafx.util.converter.IntegerStringConverter;
import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;
import domain.dtos.RaceDTO;
import domain.dtos.RaceDetailsDTO;
import observer.SwimmingRaceObserver;
import services.ServicesException;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.function.UnaryOperator;


public class MainController extends Controller implements SwimmingRaceObserver {

    @FXML
    private ComboBox<SwimmingDistance> distanceComboBox;
    @FXML
    private ComboBox<SwimmingStyle> styleComboBox;
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
    private final ObservableList<SwimmingDistance> distances = FXCollections.observableArrayList();
    @FXML
    private final ObservableList<SwimmingStyle> styles = FXCollections.observableArrayList();
    @FXML
    private TextField firstNameTextField;
    @FXML
    private TextField lastNameTextField;
    @FXML
    private TextField ageTextField;
    @FXML
    private Stage loginStage;
    @FXML
    private TableView<SwimmerDTO> raceSwimmersTableView;
    @FXML
    private TableColumn<SwimmerDTO, String> firstNameColumn;
    @FXML
    private TableColumn<SwimmerDTO, String> lastNameColumn;
    @FXML
    private TableColumn<SwimmerDTO, String> ageColumn;
    @FXML
    private TableColumn<SwimmerDTO, String> racesEnrolledColumn;
    @FXML
    private final ObservableList<SwimmerDTO> swimmerDTOsModel = FXCollections.observableArrayList();
    private String loggedUsername;
    private SwimmingDistance swimmingDistance;
    private SwimmingStyle swimmingStyle;

    @FXML
    public void logoutButtonAction(ActionEvent actionEvent) {
        try {
            service.logout(loggedUsername);
            stage.close();
            loginStage.show();
        } catch (ServicesException e) {
            MessageAlert.showErrorMessage(null, e.getMessage());
        }
    }

    @FXML
    public void raceSearchAction(ActionEvent actionEvent) {
        swimmingDistance = distanceComboBox.getValue();
        swimmingStyle = styleComboBox.getValue();
        swimmerDTOsModel.setAll(service.findAllSwimmersDetailsForRace(this.swimmingDistance, this.swimmingStyle));
    }

    @FXML
    public void addSwimmerAction(ActionEvent actionEvent) {
        String firstName = firstNameTextField.getText();
        String lastName = lastNameTextField.getText();
        String age = ageTextField.getText();
        ObservableList<RaceDTO> selectedIndices = racesTableView.getSelectionModel().getSelectedItems();
        if (firstName.isEmpty()) {
            MessageAlert.showErrorMessage(null, "Introduceti prenume!");
        }
        else if (lastName.isEmpty()) {
            MessageAlert.showErrorMessage(null, "Introduceti nume!");
        }
        else if (age.isEmpty()) {
            MessageAlert.showErrorMessage(null, "Introduceti varsta!");
        }
        else if (selectedIndices.size() < 1) {
            MessageAlert.showErrorMessage(null, "Selectati cel putin o cursa!");
        }
        else {
            List<RaceDetailsDTO> raceDetailsDTOs = selectedIndices.stream()
                            .map(raceDTO -> new RaceDetailsDTO(raceDTO.getDistance(), raceDTO.getStyle()))
                            .toList();
            service.addSwimmer(firstName, lastName, Integer.parseInt(age), raceDetailsDTOs);
            firstNameTextField.clear();
            lastNameTextField.clear();
            ageTextField.clear();
        }
    }

    public void initialize() {
        updateModels();
        initializeRacesTableView();
        initializeComboBoxes();
        UnaryOperator<TextFormatter.Change> integerFilter = change -> {
            String newText = change.getControlNewText();
            if (newText.matches("-?([1-9][0-9]*)?")) {
                return change;
            }
            return null;
        };

        ageTextField.setTextFormatter(
                new TextFormatter<>(new IntegerStringConverter(), 0, integerFilter));
        ageTextField.clear();
    }

    private void initializeRacesTableView() {
        racesTableView.setItems(raceDTOsModel);
        racesTableView.getSelectionModel().setSelectionMode(SelectionMode.MULTIPLE);
        raceDistanceColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getDistance().toString()));
        raceStyleColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getStyle().toString()));
        raceSwimmersNoColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getNoSwimmers().toString()));
        raceSwimmersTableView.setItems(swimmerDTOsModel);
        firstNameColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getSwimmer().getFirstName()));
        lastNameColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getSwimmer().getLastName()));
        ageColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getSwimmer().getAge().toString()));
        racesEnrolledColumn.setCellValueFactory(param -> new SimpleStringProperty(param.getValue().getRaces()));
    }

    private void updateModels() {
        raceDTOsModel.setAll(service.findAllRacesDetails());
        if (swimmingStyle != null && swimmingDistance != null) {
            swimmerDTOsModel.setAll(service.findAllSwimmersDetailsForRace(this.swimmingDistance, this.swimmingStyle));
        }
        else {
            swimmerDTOsModel.setAll(new ArrayList<>());
        }
    }

    private void initializeComboBoxes() {
        distances.setAll(Arrays.asList(SwimmingDistance._50m, SwimmingDistance._200m, SwimmingDistance._800m, SwimmingDistance._1500m));
        distanceComboBox.setItems(distances);
        styles.setAll(Arrays.asList(SwimmingStyle.Freestyle, SwimmingStyle.Backstroke, SwimmingStyle.Butterfly, SwimmingStyle.Mixed));
        styleComboBox.setItems(styles);
        swimmingDistance = null;
        swimmingStyle = null;
    }

    public void setLoggedUsername(String loggedUsername) {
        this.loggedUsername = loggedUsername;
    }

    public void setLoginStage(Stage loginStage) {
        this.loginStage = loginStage;
    }

    @Override
    public void racesUpdated() {
        updateModels();
    }
}
