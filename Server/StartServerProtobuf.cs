using log4net.Config;
using Model.Services;
using Server.Network;
using Server.Repository;
using Server.Repository.DBRepository;
using Server.Repository.DBRepositoryORM;
using Server.Services;
using Server.Utils;

namespace Server;

public class StartServerProtobuf
{
    private static SwimmingRaceServicesServer LoadServices()
    {
        var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project.db");
        IAdminRepository adminRepository = new AdminDBRepository(properties);
        IRaceRepository raceRepository = new RaceDBRepository(properties);
        ISwimmerRepository swimmerRepository = new SwimmerDBRepositoryORM(properties);
        ISwimmerRaceRepository swimmerRaceRepository =
            new SwimmerRaceDBRepository(swimmerRepository, raceRepository, properties);
        var swimmingRaceServiceServer = new SwimmingRaceServicesServer(adminRepository, swimmerRepository,
            raceRepository,
            swimmerRaceRepository);
        return swimmingRaceServiceServer;
    }

    public static void Main(string[] args)
    {
        XmlConfigurator.Configure();
        ISwimmingRaceServices services = LoadServices();
        var server = new SwimmingRacesConcurrentServerProtobuf("127.0.0.1", 55555, services);
        server.Start();
    }
}