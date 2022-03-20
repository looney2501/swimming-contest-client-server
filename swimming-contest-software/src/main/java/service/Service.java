package service;

import repository.dbRepository.AdminDBRepository;
import repository.dbRepository.RaceDBRepository;
import repository.dbRepository.SwimmerDBRepository;
import repository.dbRepository.SwimmerRaceDBRepository;

public class Service {
    private AdminDBRepository adminDBRepository;
    private RaceDBRepository raceDBRepository;
    private SwimmerDBRepository swimmerDBRepository;
    private SwimmerRaceDBRepository swimmerRaceDBRepository;

    public void setAdminDBRepository(AdminDBRepository adminDBRepository) {
        this.adminDBRepository = adminDBRepository;
    }

    public void setRaceDBRepository(RaceDBRepository raceDBRepository) {
        this.raceDBRepository = raceDBRepository;
    }

    public void setSwimmerDBRepository(SwimmerDBRepository swimmerDBRepository) {
        this.swimmerDBRepository = swimmerDBRepository;
    }

    public void setSwimmerRaceDBRepository(SwimmerRaceDBRepository swimmerRaceDBRepository) {
        this.swimmerRaceDBRepository = swimmerRaceDBRepository;
    }

    public boolean isExistingUser(String username, String password) {
        return adminDBRepository.findByUsernameAndPassword(username, password) != null;
    }
}
