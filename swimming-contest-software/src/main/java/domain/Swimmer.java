package domain;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;
import java.util.Objects;

public class Swimmer implements Identifiable<Integer>, Serializable {

    private Integer ID;
    private String firstName;
    private String lastName;
    private String age;
    private List<Race> races;

    public Swimmer(Integer ID, String firstName, String lastName, String age) {
        this.ID = ID;
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        races = new ArrayList<>();
    }

    public Swimmer(String firstName, String lastName, String age) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.age = age;
        races = new ArrayList<>();
    }

    @Override
    public Integer getID() {
        return ID;
    }

    @Override
    public void setID(Integer id) {
        ID = id;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getAge() {
        return age;
    }

    public void setAge(String age) {
        this.age = age;
    }

    public List<Race> getRaces() {
        return races;
    }

    public void setRaces(List<Race> races) {
        this.races = races;
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        Swimmer swimmer = (Swimmer) o;
        return Objects.equals(ID, swimmer.ID) && Objects.equals(firstName, swimmer.firstName) && Objects.equals(lastName, swimmer.lastName) && Objects.equals(age, swimmer.age);
    }

    @Override
    public int hashCode() {
        return Objects.hash(ID);
    }
}
