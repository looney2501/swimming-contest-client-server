package controller;

import javafx.fxml.FXML;
import javafx.scene.control.TextField;
import main.Main;
import service.ServiceException;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;


public class LoginController extends Controller {

    @FXML
    private TextField usernameTextField;
    @FXML
    private TextField passwordField;

    @FXML
    public void loginButtonAction() throws NoSuchAlgorithmException, IOException {
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
                this.service.login(username, password);
                Main.changeSceneToMainView(loggedAdminUsername);
            } catch (ServiceException e) {
                MessageAlert.showErrorMessage(null, e.getMessage());
            }
        }
    }
}
