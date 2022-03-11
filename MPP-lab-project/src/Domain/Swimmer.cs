using System;
using System.Collections.Generic;

namespace MPP_lab_project.Domain;

[Serializable()]
public class Swimmer: Identifiable<Int32>
{
    public int ID { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public Int32 Age { get; set; }

    public Swimmer(int id, string firstName, string lastName, int age)
    {
        ID = id;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }

    public Swimmer(string firstName, string lastName, int age)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
    }
}