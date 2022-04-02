module swimming.contest.software.Client.main {
    requires javafx.controls;
    requires javafx.fxml;
    requires java.desktop;
    requires swimming.contest.software.Model.main;
    requires swimming.contest.software.Server.main;

    opens client to javafx.fxml;
    opens client.controller to javafx.fxml;

    exports client;
}