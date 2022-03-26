using MPP_lab_project.Repository.Interfaces;

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
}