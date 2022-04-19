using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using log4net;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Model.Domain.Entities;
using Server.Utils;

namespace Server.Repository.DBRepositoryORM;

public class SwimmerDBRepositoryORM : ISwimmerRepository
{
    private static readonly ILog Logger = LogManager.GetLogger("RaceDbRepository");
    IDictionary<String, String> properties;

    public SwimmerDBRepositoryORM(IDictionary<string, string> properties)
    {
        Logger.InfoFormat("Initialising SwimmerDBRepository...");
        this.properties = properties;
    }

    public int Add(Swimmer elem)
    {
        Logger.InfoFormat("Add(swimmer = {0})", elem);

        int id = -1;

        using (DataContext dataContext =
               new DataContext(properties["ConnectionString"]))
        {
            DatabaseFacade facade = new DatabaseFacade(dataContext);
            facade.EnsureCreated();
            dataContext.Swimmers.Add(elem);
            dataContext.SaveChanges();

            id = elem.ID;
        }
        
        Logger.InfoFormat("Result: id = {0}", id);
        return id;
    }

    public void Delete(Swimmer elem)
    {
        throw new System.NotImplementedException();
    }

    public void Update(Swimmer elem, int id)
    {
        throw new System.NotImplementedException();
    }

    public Swimmer FindById(int id)
    {
        Logger.InfoFormat("FindById(id = {0})", id);
        Swimmer swimmer = null;
        
        using (DataContext dataContext =
               new DataContext(properties["ConnectionString"]))
        {
            DatabaseFacade facade = new DatabaseFacade(dataContext);
            facade.EnsureCreated();
            swimmer = dataContext.Swimmers.Find(id);
            dataContext.SaveChanges();
        }

        Logger.InfoFormat("Result: swimmer = {0}", swimmer);
        
        return swimmer;
    }
}