package controller;

import javafx.fxml.FXML;
import javafx.stage.Stage;
import services.SwimmingRaceServicesAMS;

public abstract class ControllerAMS {
    protected final SwimmingRaceServicesAMS service;
    @FXML
    protected Stage stage;

    public ControllerAMS(SwimmingRaceServicesAMS service) {
        this.service = service;
    }

    public void setStage(Stage stage) {
        this.stage = stage;
    }
}
