package domain;

public class SwimmerRace {
    private final Swimmer swimmer;
    private final Race race;

    public SwimmerRace(Swimmer swimmer, Race race) {
        this.swimmer = swimmer;
        this.race = race;
    }

    public Swimmer getSwimmer() {
        return swimmer;
    }

    public Race getRace() {
        return race;
    }
}
