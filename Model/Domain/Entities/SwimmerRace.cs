using System;

namespace Model.Domain.Entities;

[Serializable]
public class SwimmerRace
{
    public SwimmerRace(Swimmer swimmer, Race race)
    {
        Swimmer = swimmer;
        Race = race;
    }

    public int ID { get; set; }
    public Swimmer Swimmer { get; }
    public Race Race { get; }

    public override string ToString()
    {
        return "SwimmerRace{" +
               "ID=" + ID +
               ", swimmer=" + Swimmer +
               ", race=" + Race +
               '}';
    }
}