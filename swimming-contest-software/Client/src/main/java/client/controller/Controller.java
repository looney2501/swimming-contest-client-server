package client.controller;

import model.domain.entities.Admin;
import server.service.ServerSwimmingRaceServices;

public abstract class Controller {
    protected ServerSwimmingRaceServices service;
    protected Admin loggedAdmin;

    public void setService(ServerSwimmingRaceServices service) {
        this.service = service;
    }

    public void setLoggedAdmin(Admin loggedAdmin) {
        this.loggedAdmin = loggedAdmin;
    }
}
