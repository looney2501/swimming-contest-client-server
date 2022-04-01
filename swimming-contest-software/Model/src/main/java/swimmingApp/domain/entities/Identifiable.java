package swimmingApp.domain.entities;

public interface Identifiable<Tid> {
    Tid getID();
    void setID(Tid id);
}
