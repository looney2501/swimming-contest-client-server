
using System.Data;
using MPP_lab_project.Domain;
using MPP_lab_project.Repository.DBRepository;
using MPP_lab_project.Repository.Interfaces;
using MPP_lab_project.Utils;
using NUnit.Framework;

namespace Tests.tests.Repository.DBRepository
{
    [TestFixture]
    public class AdminDBRepositoryTests
    {
        [Test]
        public void FindByUsernameAndPasswordTest()
        {
            var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project_test.db");
            IAdminRepository adminRepository = new AdminDBRepository(properties);

            IDbConnection connection = DbUtils.GetConnection(properties);

            using (IDbCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "select count(*) from Admins;";

                using (IDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        int noAdmins = dataReader.GetInt32(0);
                        Assert.True(0 == noAdmins);
                    }
                }
                connection.Close();
            }

            using (IDbCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "insert into Admins (username, password) values ('admin', 'admin');";
                command.ExecuteNonQuery();
                connection.Close();
            }
            
            Admin admin = new Admin("admin", "admin");
            Assert.AreEqual(admin, adminRepository.FindByUsernameAndPassword("admin", "admin"));
            // Assert.True(admin.Equals(adminRepository.FindByUsernameAndPassword("admin", "admin")));
            
            using (IDbCommand command = connection.CreateCommand())
            {
                connection.Open();
                command.CommandText = "delete from Admins;";
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}