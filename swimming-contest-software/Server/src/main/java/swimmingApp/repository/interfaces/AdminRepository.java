package swimmingApp.repository.interfaces;

import swimmingApp.domain.entities.Admin;

public interface AdminRepository extends Repository<Integer, Admin> {
    Admin findByUsernameAndPassword(String username, String password);
}
