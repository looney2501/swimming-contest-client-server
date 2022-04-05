package client.controller;

import model.domain.entities.Admin;
import server.service.ServerSwimmingRaceServices;

public abstract class Controller {
    protected ServerSwimmingRaceServices service;
    protected String loggedAdminUsername;

    public void setService(ServerSwimmingRaceServices service) {
        this.service = service;
    }

    public void setLoggedAdminUsername(String loggedAdminUsername) {
        this.loggedAdminUsername = loggedAdminUsername;
    }
}
