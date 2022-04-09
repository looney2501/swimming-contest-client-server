﻿using System.Collections.Generic;
using System.Linq;
using Model.Domain.DTOs;
using Model.Domain.Entities;
using Model.Domain.Enums;
using Model.Services;
using Server.Repository;

namespace Server.Services;

public class Services
{
    public IAdminRepository AdminRepository { get; set; }
    public ISwimmerRepository SwimmerRepository { get; set; }
    public IRaceRepository RaceRepository { get; set; }
    public ISwimmerRaceRepository SwimmerRaceRepository { get; set; }

    public Services(IAdminRepository adminRepository, ISwimmerRepository swimmerRepository, IRaceRepository raceRepository, ISwimmerRaceRepository swimmerRaceRepository)
    {
        AdminRepository = adminRepository;
        SwimmerRepository = swimmerRepository;
        RaceRepository = raceRepository;
        SwimmerRaceRepository = swimmerRaceRepository;
    }

    public bool IsExistingUser(string username, string password)
    {
        return AdminRepository.FindByUsernameAndPassword(username, password) != null;
    }

    public void Login(string username, string password)
    {
        Admin admin = AdminRepository.FindByUsernameAndPassword(username, password);
        if (admin != null)
        {
            //ceva
        }
        else
        {
            throw new ServicesException("Incorrect username or password!");
        }
    }

    public void Logout(string username)
    {
        //ceva
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