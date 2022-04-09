using System;
using Model.Domain.Entities;

namespace Server.Repository;

public interface IAdminRepository : IRepository<Int32, Admin>
{
    public Admin FindByUsernameAndPassword(String username, String password);
}