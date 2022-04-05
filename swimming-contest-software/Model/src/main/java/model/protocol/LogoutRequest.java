package model.protocol;

public class LogoutRequest implements Request {
    private final String username;

    public LogoutRequest(String username) {
        this.username = username;
    }

    public String getUsername() {
        return username;
    }
}
