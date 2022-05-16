using Model.Domain.Entities;

namespace Server.Repository;

public interface IAdminRepository : IRepository<int, Admin>
{
    public Admin FindByUsernameAndPassword(string username, string password);
}