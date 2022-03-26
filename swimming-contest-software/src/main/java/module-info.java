module com.map_toysocialnetwork_gui {
    requires javafx.controls;
    requires javafx.fxml;
    requires java.sql;
    requires java.desktop;
    requires org.apache.logging.log4j;

    opens swimmingApp to javafx.fxml;
    opens swimmingApp.controller to javafx.fxml;

    exports swimmingApp;
    exports swimmingApp.domain.enums;
    exports swimmingApp.domain.entities;
}