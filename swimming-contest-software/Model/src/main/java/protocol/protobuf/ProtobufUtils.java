package protocol.protobuf;

import domain.dtos.AdminDTO;
import domain.dtos.RaceDetailsDTO;
import domain.dtos.SwimmerDTO;
import domain.entities.Swimmer;
import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;

import java.util.List;

public final class ProtobufUtils {

    private static SwimmingContestProtobuf.SwimmingDistance swimmingDistanceToProtobuf(SwimmingDistance swimmingDistance) {
        return switch (swimmingDistance) {
            case _50m -> SwimmingContestProtobuf.SwimmingDistance._50m;
            case _200m -> SwimmingContestProtobuf.SwimmingDistance._200m;
            case _800m -> SwimmingContestProtobuf.SwimmingDistance._800m;
            case _1500m -> SwimmingContestProtobuf.SwimmingDistance._1500m;
        };
    }

    private static SwimmingDistance swimmingDistanceFromProtobuf(SwimmingContestProtobuf.SwimmingDistance swimmingDistance) {
        return switch (swimmingDistance) {
            case _50m -> SwimmingDistance._50m;
            case _200m -> SwimmingDistance._200m;
            case _800m -> SwimmingDistance._800m;
            case _1500m -> SwimmingDistance._1500m;
            default -> null;
        };
    }

    private static SwimmingContestProtobuf.SwimmingStyle swimmingStyleToProtobuf(SwimmingStyle swimmingStyle) {
        return switch (swimmingStyle) {
            case Mixed -> SwimmingContestProtobuf.SwimmingStyle.Mixed;
            case Butterfly -> SwimmingContestProtobuf.SwimmingStyle.Butterfly;
            case Freestyle -> SwimmingContestProtobuf.SwimmingStyle.Freestyle;
            case Backstroke -> SwimmingContestProtobuf.SwimmingStyle.Backstroke;
        };
    }

    private static SwimmingStyle swimmingStyleFromProtobuf(SwimmingContestProtobuf.SwimmingStyle swimmingStyle) {
        return switch (swimmingStyle) {
            case Mixed -> SwimmingStyle.Mixed;
            case Butterfly -> SwimmingStyle.Butterfly;
            case Freestyle -> SwimmingStyle.Freestyle;
            case Backstroke -> SwimmingStyle.Backstroke;
            default -> null;
        };
    }

    private static SwimmingContestProtobuf.RaceDetailsDTO raceDetailsDTOToProtobuf(RaceDetailsDTO raceDetailsDTO) {
        SwimmingContestProtobuf.SwimmingDistance swimmingDistance = swimmingDistanceToProtobuf(raceDetailsDTO.getSwimmingDistance());
        SwimmingContestProtobuf.SwimmingStyle swimmingStyle = swimmingStyleToProtobuf(raceDetailsDTO.getSwimmingStyle());
        return SwimmingContestProtobuf.RaceDetailsDTO.newBuilder().setSwimmingDistance(swimmingDistance).setSwimmingStyle(swimmingStyle).build();
    }

    private static SwimmingContestProtobuf.SwimmerDTO swimmerDTOToProtobuf(SwimmerDTO swimmerDTO) {
        SwimmingContestProtobuf.Swimmer swimmer = swimmerToProtobuf(swimmerDTO.getSwimmer());
        List<SwimmingContestProtobuf.RaceDetailsDTO> allRaceDetailsDTOs = swimmerDTO.getRaceDetailsDTOs().stream()
                .map(ProtobufUtils::raceDetailsDTOToProtobuf)
                .toList();
        return SwimmingContestProtobuf.SwimmerDTO.newBuilder().setSwimmer(swimmer).addAllRaceDetailsDTOs(allRaceDetailsDTOs).build();
    }

    private static SwimmingContestProtobuf.Swimmer swimmerToProtobuf(Swimmer swimmer) {
        return SwimmingContestProtobuf.Swimmer.newBuilder().setID(swimmer.getID()).setFirstName(swimmer.getFirstName()).setLastName(swimmer.getLastName()).setAge(swimmer.getAge()).build();
    }

    public static SwimmingContestProtobuf.LoginRequest createLoginRequest(AdminDTO adminDTO) {
        return null;
    }

    public static SwimmingContestProtobuf.LogoutRequest createLogoutRequest(String username) {
        return null;
    }

    public static SwimmingContestProtobuf.FindAllRacesDetailsRequest createFindAllRacesDetailsRequest() {
        return null;
    }

    public static SwimmingContestProtobuf.FindAllSwimmersDetailsForRaceRequest createFindAllSwimmersDetailsForRaceRequest(RaceDetailsDTO raceDetailsDTO) {
        SwimmingContestProtobuf.RaceDetailsDTO raceDetailsDTOProtobuf = raceDetailsDTOToProtobuf(raceDetailsDTO);
        return SwimmingContestProtobuf.FindAllSwimmersDetailsForRaceRequest.newBuilder().setRaceDetailsDTO(raceDetailsDTOProtobuf).build();
    }

    public static SwimmingContestProtobuf.AddSwimmerRequest createAddSwimmerRequest(SwimmerDTO swimmerDTO) {
        SwimmingContestProtobuf.SwimmerDTO swimmerDTOProtobuf = swimmerDTOToProtobuf(swimmerDTO);
        return SwimmingContestProtobuf.AddSwimmerRequest.newBuilder().setSwimmerDTO(swimmerDTOProtobuf).build();
    }
}
