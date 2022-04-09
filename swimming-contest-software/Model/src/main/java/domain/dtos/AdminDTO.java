package domain.dtos;

import java.io.Serializable;

public class AdminDTO implements Serializable {

    private final String username;
    private final String password;

    public AdminDTO(String username, String password) {
        this.username = username;
        this.password = password;
    }

    public String getUsername() {
        return username;
    }

    public String getPassword() {
        return password;
    }
}
