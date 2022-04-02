package server.repository.interfaces;

import model.domain.entities.Admin;

public interface AdminRepository extends Repository<Integer, Admin> {
    Admin findByUsernameAndPassword(String username, String password);
}
