using log4net.Config;
using Server.Repository;
using Server.Repository.DBRepository;
using Server.Utils;
using Server.Services;

namespace Server;

public class StartServer
{
    // static Services.Services LoadServices()
    // {
    //     var properties = DbUtils.GetDBPropertiesByName("mpp_lab_project.db");
    //     IAdminRepository adminRepository = new AdminDBRepository(properties);
    //     IRaceRepository raceRepository = new RaceDBRepository(properties);
    //     ISwimmerRepository swimmerRepository = new SwimmerDBRepository(properties);
    //     ISwimmerRaceRepository swimmerRaceRepository =
    //         new SwimmerRaceDBRepository(swimmerRepository, raceRepository, properties);
    //     Services.Services service = new Services.Services(adminRepository, swimmerRepository, raceRepository,
    //         swimmerRaceRepository);
    //     return service;
    // }
    
    public static void Main(string[] args)
    {
        
    }
}