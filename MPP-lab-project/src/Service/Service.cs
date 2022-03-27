using System.Collections.Generic;
using System.Linq;
using MPP_lab_project.Domain;
using MPP_lab_project.Domain.DTOs;
using MPP_lab_project.Repository.Interfaces;
using MPP_lab_project.Utils;

namespace MPP_lab_project.Service;

public class Service
{
    public IAdminRepository AdminRepository { get; set; }
    public ISwimmerRepository SwimmerRepository { get; set; }
    public IRaceRepository RaceRepository { get; set; }
    public ISwimmerRaceRepository SwimmerRaceRepository { get; set; }

    public Service(IAdminRepository adminRepository, ISwimmerRepository swimmerRepository, IRaceRepository raceRepository, ISwimmerRaceRepository swimmerRaceRepository)
    {
        AdminRepository = adminRepository;
        SwimmerRepository = swimmerRepository;
        RaceRepository = raceRepository;
        SwimmerRaceRepository = swimmerRaceRepository;
    }

    public bool IsExistingUser(string username, string password)
    {
        return AdminRepository.FindByUsernameAndPassword(username, PasswordHashingUtils.MD5Hashing(password)) != null;
    }

    public List<RaceDTO> FindAllRacesDetails()
    {
        List<RaceDTO> raceDTOs = new List<RaceDTO>();
        foreach (Race race in RaceRepository.FindAllRaces())
        {
            raceDTOs.Add(new RaceDTO(race.Distance, race.Style, SwimmerRaceRepository.GetNumberOfSwimmersForRace(race)));
        }

        return raceDTOs;
    }
    
    public List<SwimmerDTO> FindAllSwimmersDetailsForRace(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle) {
        List<SwimmerDTO> swimmerDTos = new List<SwimmerDTO>();
        Race race = RaceRepository.FindRaceByDistanceAndStyle(swimmingDistance, swimmingStyle);
        foreach (Swimmer swimmer in SwimmerRaceRepository.FindAllSwimmersForRace(race)) {
            List<Race> racesForSwimmer = SwimmerRaceRepository.FindAllRacesForSwimmer(swimmer);
            List<RaceDetailsDTO> raceDetailsDTOS =
                racesForSwimmer.Select(x => new RaceDetailsDTO(x.Distance, x.Style)).ToList();
            swimmerDTos.Add(new SwimmerDTO(swimmer, raceDetailsDTOS));
        }
        return swimmerDTos;
    }

    public void AddSwimmer(string fistName, string lastName, int age, List<RaceDetailsDTO> raceDetailsDTOs)
    {
        Swimmer swimmer = new Swimmer(fistName, lastName, age);
        int swimmerID = SwimmerRepository.Add(swimmer);
        swimmer.ID = swimmerID;

        foreach (RaceDetailsDTO raceDetailDTO in raceDetailsDTOs)
        {
            Race race = RaceRepository.FindRaceByDistanceAndStyle(raceDetailDTO.SwimmingDistance,
                raceDetailDTO.SwimmingStyle);
            SwimmerRace swimmerRace = new SwimmerRace(swimmer, race);
            SwimmerRaceRepository.Add(swimmerRace);
        }
    }
}