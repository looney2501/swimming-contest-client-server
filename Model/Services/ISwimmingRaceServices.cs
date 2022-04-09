using System.Collections.Generic;
using Model.Domain.DTOs;
using Model.Domain.Enums;
using Model.Observer;

namespace Model.Services;

public interface ISwimmingRaceServices
{
    void Login(string username, string password, ISwimmingRaceObserver client);
    void Logout(string username);
    List<RaceDTO> FindAllRacesDetails();
    List<SwimmerDTO> FindAllSwimmersDetailsForRace(SwimmingDistances swimmingDistance, SwimmingStyles swimmingStyle);
    void AddSwimmer(string firstName, string lastName, int age, List<RaceDetailsDTO> raceDetailsDTOs);
}