using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Model.Domain.DTOs;
using Model.Domain.Entities;
using Model.Domain.Enums;
using Model.Observer;
using Model.Services;
using Server.Repository;

namespace Server.Services;

public class SwimmingRaceServicesServer : ISwimmingRaceServices
{
    private static readonly ILog Logger = LogManager.GetLogger("Services");
    private readonly IDictionary<string, ISwimmingRaceObserver> _loggedClients;

    public SwimmingRaceServicesServer(IAdminRepository adminRepository, ISwimmerRepository swimmerRepository,
        IRaceRepository raceRepository, ISwimmerRaceRepository swimmerRaceRepository)
    {
        AdminRepository = adminRepository;
        SwimmerRepository = swimmerRepository;
        RaceRepository = raceRepository;
        SwimmerRaceRepository = swimmerRaceRepository;
        _loggedClients = new Dictionary<string, ISwimmingRaceObserver>();
    }

    public IAdminRepository AdminRepository { get; set; }
    public ISwimmerRepository SwimmerRepository { get; set; }
    public IRaceRepository RaceRepository { get; set; }
    public ISwimmerRaceRepository SwimmerRaceRepository { get; set; }

    public void Login(string username, string password, ISwimmingRaceObserver client)
    {
        var admin = AdminRepository.FindByUsernameAndPassword(username, password);
        if (admin != null)
        {
            if (_loggedClients.ContainsKey(admin.Username))
                throw new ServicesException("User already logged in!");
            _loggedClients[admin.Username] = client;
        }
        else
        {
            throw new ServicesException("Incorrect username or password!");
        }
    }

    public void Logout(string username)
    {
        if (_loggedClients.Remove(username) == false) throw new ServicesException("User is not logged in!");
    }

    public List<RaceDTO> FindAllRacesDetails()
    {
        var raceDTOs = new List<RaceDTO>();
        foreach (var race in RaceRepository.FindAllRaces())
            raceDTOs.Add(new RaceDTO(race.Distance, race.Style,
                SwimmerRaceRepository.GetNumberOfSwimmersForRace(race)));

        return raceDTOs;
    }

    public List<SwimmerDTO> FindAllSwimmersDetailsForRace(SwimmingDistance swimmingDistance,
        SwimmingStyle swimmingStyle)
    {
        var swimmerDTos = new List<SwimmerDTO>();
        var race = RaceRepository.FindRaceByDistanceAndStyle(swimmingDistance, swimmingStyle);
        foreach (var swimmer in SwimmerRaceRepository.FindAllSwimmersForRace(race))
        {
            var racesForSwimmer = SwimmerRaceRepository.FindAllRacesForSwimmer(swimmer);
            var raceDetailsDTOS =
                racesForSwimmer.Select(x => new RaceDetailsDTO(x.Distance, x.Style)).ToList();
            swimmerDTos.Add(new SwimmerDTO(swimmer, raceDetailsDTOS));
        }

        return swimmerDTos;
    }

    public void AddSwimmer(string fistName, string lastName, int age, List<RaceDetailsDTO> raceDetailsDTOs)
    {
        var swimmer = new Swimmer(fistName, lastName, age);
        var swimmerID = SwimmerRepository.Add(swimmer);
        swimmer.id = swimmerID;

        foreach (var raceDetailDTO in raceDetailsDTOs)
        {
            var race = RaceRepository.FindRaceByDistanceAndStyle(raceDetailDTO.SwimmingDistance,
                raceDetailDTO.SwimmingStyle);
            var swimmerRace = new SwimmerRace(swimmer, race);
            SwimmerRaceRepository.Add(swimmerRace);
        }

        NotifyAddedSwimmer();
    }

    private void NotifyAddedSwimmer()
    {
        Logger.Info("Sending RaceUpdateResponse to all connected clients...");
        foreach (var client in _loggedClients.Values) Task.Run(() => client.RacesUpdated());
    }
}