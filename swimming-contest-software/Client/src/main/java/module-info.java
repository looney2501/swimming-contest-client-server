module swimming.contest.software.Client.main {
    requires javafx.controls;
    requires javafx.fxml;
    requires java.desktop;
    requires swimming.contest.software.Model.main;
    requires swimming.contest.software.Server.main;

    opens swimmingApp to javafx.fxml;
    opens swimmingApp.controller to javafx.fxml;

    exports swimmingApp;
}