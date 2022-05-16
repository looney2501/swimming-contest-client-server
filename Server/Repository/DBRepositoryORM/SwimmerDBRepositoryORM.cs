using System;
using System.Collections.Generic;
using log4net;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Model.Domain.Entities;

namespace Server.Repository.DBRepositoryORM;

public class SwimmerDBRepositoryORM : ISwimmerRepository
{
    private static readonly ILog Logger = LogManager.GetLogger("RaceDbRepository");
    private readonly IDictionary<string, string> properties;

    public SwimmerDBRepositoryORM(IDictionary<string, string> properties)
    {
        Logger.InfoFormat("Initialising SwimmerDBRepositoryORM...");
        this.properties = properties;
    }

    public int Add(Swimmer elem)
    {
        Logger.InfoFormat("Add(swimmer = {0})", elem);

        var id = -1;

        using (var dataContext =
               new DataContext(properties["ConnectionString"]))
        {
            var facade = new DatabaseFacade(dataContext);
            facade.EnsureCreated();
            dataContext.Swimmers.Add(elem);
            dataContext.SaveChanges();

            id = elem.id;
        }

        Logger.InfoFormat("Result: id = {0}", id);
        return id;
    }

    public void Delete(Swimmer elem)
    {
        throw new NotImplementedException();
    }

    public void Update(Swimmer elem, int id)
    {
        throw new NotImplementedException();
    }

    public Swimmer FindById(int id)
    {
        Logger.InfoFormat("FindById(id = {0})", id);
        Swimmer swimmer = null;

        using (var dataContext =
               new DataContext(properties["ConnectionString"]))
        {
            var facade = new DatabaseFacade(dataContext);
            facade.EnsureCreated();
            swimmer = dataContext.Swimmers.Find(id);
            dataContext.SaveChanges();
        }

        Logger.InfoFormat("Result: swimmer = {0}", swimmer);

        return swimmer;
    }
}