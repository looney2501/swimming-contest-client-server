package services;

import domain.entities.Race;
import org.springframework.http.HttpStatus;
import org.springframework.http.RequestEntity;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import repository.RaceRepository;

import java.util.List;

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
        raceRepository.add(raceRequestEntity.getBody());
        return new ResponseEntity<>(HttpStatus.CREATED);
    }
}
