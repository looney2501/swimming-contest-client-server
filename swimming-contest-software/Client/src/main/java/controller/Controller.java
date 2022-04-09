package controller;

import javafx.fxml.FXML;
import javafx.stage.Stage;
import services.SwimmingRaceServices;

public abstract class Controller {
    protected SwimmingRaceServices service;
    @FXML
    protected Stage stage;

    public void setService(SwimmingRaceServices service) {
        this.service = service;
    }

    public void setStage(Stage stage) {
        this.stage = stage;
    }
}
