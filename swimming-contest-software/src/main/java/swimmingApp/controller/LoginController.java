package swimmingApp.controller;

import javafx.fxml.FXML;
import javafx.scene.control.Alert;
import javafx.scene.control.TextField;
import swimmingApp.utils.PasswordHashingUtils;

import java.security.NoSuchAlgorithmException;


public class LoginController extends Controller {

    @FXML
    private TextField usernameTextField;

    @FXML
    private TextField passwordTextField;

    @FXML
    public void loginButtonAction() throws NoSuchAlgorithmException {
        String username = usernameTextField.getText();
        String password = passwordTextField.getText();
        if (username.isEmpty()) {
            MessageAlert.showErrorMessage(null, "Introduceti un nume de utilizator!");
        }
        else if (password.isEmpty()) {
            MessageAlert.showErrorMessage(null, "Introduceti o parola!");
        }
        else if (service.isExistingUser(username, password)){
            MessageAlert.showMessage(null, Alert.AlertType.CONFIRMATION, "ok", "ok");
        }
        else {
            MessageAlert.showErrorMessage(null, "Username sau parola sunt gresite!");
        }
    }
}
