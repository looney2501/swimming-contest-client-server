package client.controller;

import server.service.ServerSwimmingRaceServices;

public abstract class Controller {
    protected ServerSwimmingRaceServices service;

    public void setService(ServerSwimmingRaceServices service) {
        this.service = service;
    }
}
