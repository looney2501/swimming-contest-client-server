package model.protocol;

import model.domain.dtos.SwimmerDTO;

public class AddSwimmerRequest implements Request {

    private final SwimmerDTO swimmerDTO;

    public AddSwimmerRequest(SwimmerDTO swimmerDTO) {
        this.swimmerDTO = swimmerDTO;
    }

    public SwimmerDTO getSwimmerDTO() {
        return swimmerDTO;
    }
}
