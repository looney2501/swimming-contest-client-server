﻿using System;
using System.Data;
using System.Data.SqlClient;
using MPP_lab_project.Domain;
using MPP_lab_project.Repository.DBRepository;
using MPP_lab_project.Repository.Interfaces;
using MPP_lab_project.Utils;
using NUnit.Framework;

namespace Tests.tests.Repository.DBRepository
{
    [TestFixture]
    public class SwimmerDBRepositoryTests
    {
        [Test]
        public void Add()
        {
            var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project_test.db");

            ISwimmerRepository swimmerRepository = new SwimmerDBRepository(properties);
            swimmerRepository.Add(new Swimmer("Gigi", "Ursu", 43));

            IDbConnection connection = DbUtils.GetConnection(properties);

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select * from Swimmers;";
                using (IDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        Assert.AreEqual("Gigi", dataReader.GetString(1));
                        Assert.AreEqual("Ursu", dataReader.GetString(2));
                        Assert.AreEqual(43, dataReader.GetInt32(3));
                    }
                }
            }

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select count(*) from Swimmers;";
                using (IDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        Assert.AreEqual(1, dataReader.GetInt32(0));
                    }
                }
            }

            swimmerRepository.Add(new Swimmer("aaa", "aaa", 21));
            
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "select count(*) from Swimmers;";
                using (IDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        Assert.AreEqual(2, dataReader.GetInt32(0));
                    }
                }
            }
            
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "delete from Swimmers;";
                command.ExecuteNonQuery();
            }
        }

        [Test]
        public void FindById()
        {
            var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project_test.db");

            ISwimmerRepository swimmerRepository = new SwimmerDBRepository(properties);
            int id = swimmerRepository.Add(new Swimmer("Gigi", "Ursu", 43));
            
            Assert.AreEqual(new Swimmer(id, "Gigi", "Ursu", 43), swimmerRepository.FindById(id));

            IDbConnection connection = DbUtils.GetConnection(properties);
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = "delete from Swimmers;";
                command.ExecuteNonQuery();
            }
        }
    }
}