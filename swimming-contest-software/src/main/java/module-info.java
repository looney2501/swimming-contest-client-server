module com.map_toysocialnetwork_gui {
    requires javafx.controls;
    requires javafx.fxml;

    requires org.controlsfx.controls;
    requires com.dlsc.formsfx;
    requires org.kordamp.bootstrapfx.core;
    requires java.sql;
    requires java.desktop;
    requires org.apache.logging.log4j;

    opens swimmingApp to javafx.fxml;
    exports swimmingApp;
}