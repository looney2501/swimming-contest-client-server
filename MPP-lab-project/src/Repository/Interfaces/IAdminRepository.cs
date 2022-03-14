using System;
using MPP_lab_project.Domain;

namespace MPP_lab_project.Repository.Interfaces;

public interface IAdminRepository : IRepository<Int32, Admin>
{
    public Admin FindByUsernameAndPassword(String username, String password);
}