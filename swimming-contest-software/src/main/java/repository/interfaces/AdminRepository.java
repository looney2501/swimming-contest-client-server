package repository.interfaces;

import domain.Admin;

public interface AdminRepository extends Repository<Integer, Admin> {
    Admin findByUsernameAndPassword(String username, String password);
}
