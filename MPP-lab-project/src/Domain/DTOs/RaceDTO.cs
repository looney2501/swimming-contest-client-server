namespace MPP_lab_project.Domain.DTOs;

public class RaceDTO
{
    public Race Race { get; }
    public int NoSwimmers { get; }

    public RaceDTO(Race race, int noSwimmers)
    {
        Race = race;
        NoSwimmers = noSwimmers;
    }
}