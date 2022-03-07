using System;

namespace MPP_lab_project.Domain;

[Serializable()]
public class Race: Identifiable<Int32>
{
    public int ID { get; set; }
    public SwimmingDistances Distance { get; set; }
    public SwimmingStyles Style { get; set; }
    public Int32 SwimmersNumber { get; set; }
    
    
}