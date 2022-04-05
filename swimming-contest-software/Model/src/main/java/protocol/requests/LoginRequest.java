package protocol.requests;

import domain.dtos.AdminDTO;

public class LoginRequest implements Request {

    private final AdminDTO admin;

    public LoginRequest(AdminDTO admin) {
        this.admin = admin;
    }

    public AdminDTO getAdmin() {
        return admin;
    }
}
