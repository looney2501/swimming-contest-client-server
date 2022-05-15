package services;

import domain.entities.Race;
import org.springframework.http.HttpStatus;
import org.springframework.http.RequestEntity;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import repository.RaceRepository;

import java.util.List;
import java.util.Objects;

@RestController
@RequestMapping("swimming-contest/races")
public class RaceController {
    private final RaceRepository raceRepository;

    public RaceController(RaceRepository raceRepository) {
        this.raceRepository = raceRepository;
    }

    @GetMapping("/test")
    public String test(@RequestParam(value = "name", defaultValue = "Hello") String name) {
        return name.toUpperCase();
    }

    @GetMapping
    public ResponseEntity<List<Race>> getAll() {
        return new ResponseEntity<>(raceRepository.findAllRaces(), HttpStatus.OK);
    }

    @GetMapping("/{id}")
    public ResponseEntity<?> getByID(@PathVariable Integer id) {
        Race race = raceRepository.findById(id);
        if (race == null) {
            return new ResponseEntity<>("Race not found!", HttpStatus.NOT_FOUND);
        }
        else {
            return new ResponseEntity<>(race, HttpStatus.OK);
        }
    }

    @PostMapping
    public ResponseEntity<?> addRace(RequestEntity<Race> raceRequestEntity) {
        Integer addedRaceID = raceRepository.add(raceRequestEntity.getBody());
        Race addedRace = raceRepository.findById(addedRaceID);
        return new ResponseEntity<>(addedRace, HttpStatus.CREATED);
    }

    @PutMapping("/{id}")
    public ResponseEntity<?> modifyRace(@PathVariable Integer id,  RequestEntity<Race> raceRequestEntity) {
        Race race = raceRequestEntity.getBody();
        if (!Objects.equals(race.getID(), id)) {
            return new ResponseEntity<>("ID not matching!", HttpStatus.BAD_REQUEST);
        }
        race.setID(id);
        Race oldRace = raceRepository.update(race, id);
        if (oldRace == null) {
            return new ResponseEntity<>("Race not found!", HttpStatus.NOT_FOUND);
        }
        else {
            return new ResponseEntity<>(race, HttpStatus.OK);
        }
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<?> deleteRace(@PathVariable Integer id) {
        Race deletedRace = raceRepository.delete(id);
        if (deletedRace == null) {
            return new ResponseEntity<>("Race not found!", HttpStatus.NOT_FOUND);
        }
        else {
            return new ResponseEntity<>(deletedRace, HttpStatus.OK);
        }
    }
}
