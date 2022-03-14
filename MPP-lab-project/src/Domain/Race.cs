using System;
using System.Collections.Generic;

namespace MPP_lab_project.Domain;

[Serializable()]
public class Race: Identifiable<Int32>
{
    public int ID { get; set; }
    public SwimmingDistances Distance { get; set; }
    public SwimmingStyles Style { get; set; }
    public Int32 SwimmersNumber { get; set; }

    public Race(int id, SwimmingDistances distance, SwimmingStyles style, int swimmersNumber)
    {
        ID = id;
        Distance = distance;
        Style = style;
        SwimmersNumber = swimmersNumber;
    }

    public Race(SwimmingDistances distance, SwimmingStyles style, int swimmersNumber)
    {
        Distance = distance;
        Style = style;
        SwimmersNumber = swimmersNumber;
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