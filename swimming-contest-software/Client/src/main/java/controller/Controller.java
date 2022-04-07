package controller;

import services.SwimmingRaceServicesServer;

public abstract class Controller {
    protected SwimmingRaceServicesServer service;
    protected String loggedAdminUsername;

    public void setService(SwimmingRaceServicesServer service) {
        this.service = service;
    }

    public void setLoggedAdminUsername(String loggedAdminUsername) {
        this.loggedAdminUsername = loggedAdminUsername;
    }
}
