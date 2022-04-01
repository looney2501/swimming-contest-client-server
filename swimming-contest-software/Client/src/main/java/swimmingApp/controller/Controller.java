package swimmingApp.controller;

import swimmingApp.service.Service;

public abstract class Controller {
    protected Service service;

    public void setService(Service service) {
        this.service = service;
    }
}
