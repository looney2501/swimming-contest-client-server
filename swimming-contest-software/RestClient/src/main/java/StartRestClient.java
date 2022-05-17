import client.RestClient;
import domain.entities.Race;
import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;

import java.util.Arrays;

public class StartRestClient {

    private static final RestClient restClient = new RestClient();

    public static void getAll() {
        Arrays.stream(restClient.getAll()).forEach(System.out::println);
    }

    public static void getById() {
        System.out.println(restClient.getById("16"));
        System.out.println(restClient.getById("166"));
    }

    public static void create() {
        Race race = new Race(SwimmingDistance._50m, SwimmingStyle.Butterfly, 50);
        System.out.println(restClient.create(race));
        System.out.println(restClient.create(race));
        System.out.println(restClient.create(race));
    }

    public static void update() {
        Race race = new Race(30, SwimmingDistance._1500m, SwimmingStyle.Freestyle, 59);
        restClient.update(race);
        System.out.println(restClient.getById("30"));
        race = new Race(30, SwimmingDistance._1500m, SwimmingStyle.Freestyle, 679);
        restClient.update(race);
        System.out.println(restClient.getById("30"));
        race = new Race(300, SwimmingDistance._1500m, SwimmingStyle.Freestyle, 679);
        restClient.update(race);
    }

    public static void delete() {
        System.out.println(restClient.getById("30"));
        restClient.delete("30");
        System.out.println(restClient.getById("30"));
    }

    public static void main(String[] args) {

//        getById();

        create();
        getAll();
//        getAll();

//        update();

//        delete();
    }
}
