using System;
using Model.Domain.Enums;

namespace Model.Domain.Entities;

[Serializable]
public class Race: Identifiable<Int32>
{
    public Int32 ID { get; set; }
    public SwimmingDistance Distance { get; set; }
    public SwimmingStyle Style { get; set; }
    public Int32 SwimmersNumber { get; set; }

    public Race(int id, SwimmingDistance distance, SwimmingStyle style, int swimmersNumber)
    {
        ID = id;
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

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        Race other = obj as Race;
        if (other == null)
        {
            return false;
        }
        return other.Distance == Distance && other.Style == Style && other.SwimmersNumber == SwimmersNumber;
    }

    public bool Equals(Race other)
    {
        if (other == null)
        {
            return false;
        }
        return other.Distance == Distance && other.Style == Style && other.SwimmersNumber == SwimmersNumber;
    }

    public override string ToString()
    {
        return "Race{" +
               "ID=" + ID +
               ", distance=" + Distance +
               ", style=" + Style +
               ", swimmersNumber=" + SwimmersNumber +
               '}';
    }
}