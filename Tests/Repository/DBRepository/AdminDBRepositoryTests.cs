using Model.Domain.Entities;
using NUnit.Framework;
using Server.Repository;
using Server.Repository.DBRepository;
using Server.Utils;

namespace Tests.Repository.DBRepository
{
    [TestFixture]
    public class AdminDBRepositoryTests
    {
        [Test]
        public void FindByUsernameAndPasswordTest()
        {
            var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project_test.db");
            IAdminRepository adminRepository = new AdminDBRepository(properties);

            var connection = DbUtils.GetConnection(properties);

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "select count(*) from Admins;";

                using (var dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        var noAdmins = dataReader.GetInt32(0);
                        Assert.True(0 == noAdmins);
                    }
                }
            }

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "insert into Admins (username, password) values ('admin', 'admin');";
                command.ExecuteNonQuery();
            }

            var admin = new Admin("admin", "admin");
            Assert.AreEqual(admin, adminRepository.FindByUsernameAndPassword("admin", "admin"));
            // Assert.True(admin.Equals(adminRepository.FindByUsernameAndPassword("admin", "admin")));

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "delete from Admins;";
                command.ExecuteNonQuery();
            }
        }
    }
}