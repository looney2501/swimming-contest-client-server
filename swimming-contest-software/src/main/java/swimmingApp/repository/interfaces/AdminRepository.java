package swimmingApp.repository.interfaces;

import swimmingApp.domain.Admin;

public interface AdminRepository extends Repository<Integer, Admin> {
    Admin findByUsernameAndPassword(String username, String password);
}
