package controller;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.TextField;
import javafx.stage.Stage;
import main.ApplicationContextWrapper;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import services.ServicesException;
import services.SwimmingRaceServicesAMS;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;

public class LoginControllerAMS extends ControllerAMS {
    @FXML
    private TextField usernameTextField;
    @FXML
    private TextField passwordField;

    public LoginControllerAMS(SwimmingRaceServicesAMS service) {
        super(service);
    }

    @FXML
    public void loginButtonAction(ActionEvent actionEvent) throws NoSuchAlgorithmException, IOException {
        String username = usernameTextField.getText();
        String password = passwordField.getText();
        if (username.isEmpty()) {
            MessageAlert.showErrorMessage(null, "Username cannot be empty!");
        }
        else if (password.isEmpty()) {
            MessageAlert.showErrorMessage(null, "Password cannot be empty!");
        }
        else {
            try {
                ClassPathXmlApplicationContext context = ApplicationContextWrapper.getInstance();
                MainControllerAMS mainController = context.getBean("mainController", MainControllerAMS.class);
                mainController.setLoggedUsername(username);
                mainController.setLoginStage(stage);

                service.login(username, password);

                FXMLLoader fxmlLoader = new FXMLLoader(this.getClass().getResource("../client/fxml/main-view.fxml"));
                fxmlLoader.setController(mainController);
                Parent root = fxmlLoader.load();

                Stage mainStage = new Stage();
                mainStage.setTitle("Swimming races administration");
                mainStage.setScene(new Scene(root));
                mainController.setStage(mainStage);
                mainStage.show();

                resetTextFields();
                stage.hide();
            } catch (ServicesException e) {
                MessageAlert.showErrorMessage(null, e.getMessage());
            }
        }
    }

    private void resetTextFields() {
        usernameTextField.clear();
        passwordField.clear();
    }
}
