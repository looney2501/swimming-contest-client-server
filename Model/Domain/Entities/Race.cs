using System;
using Model.Domain.Enums;

namespace Model.Domain.Entities;

[Serializable]
public class Race : Identifiable<int>
{
    public Race()
    {
    }

    public Race(int id, SwimmingDistance distance, SwimmingStyle style, int swimmersNumber)
    {
        this.id = id;
        Distance = distance;
        Style = style;
        SwimmersNumber = swimmersNumber;
    }

    public Race(SwimmingDistance distance, SwimmingStyle style, int swimmersNumber)
    {
        Distance = distance;
        Style = style;
        SwimmersNumber = swimmersNumber;
    }

    public SwimmingDistance Distance { get; set; }
    public SwimmingStyle Style { get; set; }
    public int SwimmersNumber { get; set; }
    public int id { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        var other = obj as Race;
        if (other == null) return false;
        return other.Distance == Distance && other.Style == Style && other.SwimmersNumber == SwimmersNumber;
    }

    public bool Equals(Race other)
    {
        if (other == null) return false;
        return other.Distance == Distance && other.Style == Style && other.SwimmersNumber == SwimmersNumber;
    }

    public override string ToString()
    {
        return "Race{" +
               "ID=" + id +
               ", distance=" + Distance +
               ", style=" + Style +
               ", swimmersNumber=" + SwimmersNumber +
               '}';
    }
}