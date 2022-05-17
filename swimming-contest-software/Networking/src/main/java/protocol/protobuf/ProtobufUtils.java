package protocol.protobuf;

import domain.dtos.AdminDTO;
import domain.dtos.RaceDTO;
import domain.dtos.RaceDetailsDTO;
import domain.dtos.SwimmerDTO;
import domain.entities.Swimmer;
import domain.enums.SwimmingDistance;
import domain.enums.SwimmingStyle;

import java.util.List;

public final class ProtobufUtils {

    public static SwimmingContestProtobuf.AdminDTO adminDTOToProtobuf(AdminDTO adminDTO) {
        return SwimmingContestProtobuf.AdminDTO.newBuilder().setUsername(adminDTO.getUsername()).setPassword(adminDTO.getPassword()).build();
    }

    public static SwimmingContestProtobuf.SwimmingDistance swimmingDistanceToProtobuf(SwimmingDistance swimmingDistance) {
        return switch (swimmingDistance) {
            case _50m -> SwimmingContestProtobuf.SwimmingDistance._50m;
            case _200m -> SwimmingContestProtobuf.SwimmingDistance._200m;
            case _800m -> SwimmingContestProtobuf.SwimmingDistance._800m;
            case _1500m -> SwimmingContestProtobuf.SwimmingDistance._1500m;
        };
    }

    public static SwimmingDistance swimmingDistanceFromProtobuf(SwimmingContestProtobuf.SwimmingDistance swimmingDistance) {
        return switch (swimmingDistance) {
            case _50m -> SwimmingDistance._50m;
            case _200m -> SwimmingDistance._200m;
            case _800m -> SwimmingDistance._800m;
            case _1500m -> SwimmingDistance._1500m;
            default -> null;
        };
    }

    public static SwimmingContestProtobuf.SwimmingStyle swimmingStyleToProtobuf(SwimmingStyle swimmingStyle) {
        return switch (swimmingStyle) {
            case Mixed -> SwimmingContestProtobuf.SwimmingStyle.Mixed;
            case Butterfly -> SwimmingContestProtobuf.SwimmingStyle.Butterfly;
            case Freestyle -> SwimmingContestProtobuf.SwimmingStyle.Freestyle;
            case Backstroke -> SwimmingContestProtobuf.SwimmingStyle.Backstroke;
        };
    }

    public static SwimmingStyle swimmingStyleFromProtobuf(SwimmingContestProtobuf.SwimmingStyle swimmingStyle) {
        return switch (swimmingStyle) {
            case Mixed -> SwimmingStyle.Mixed;
            case Butterfly -> SwimmingStyle.Butterfly;
            case Freestyle -> SwimmingStyle.Freestyle;
            case Backstroke -> SwimmingStyle.Backstroke;
            default -> null;
        };
    }

    public static RaceDetailsDTO raceDetailsDTOFromProtobuf(SwimmingContestProtobuf.RaceDetailsDTO raceDetailsDTOProtobuf) {
        SwimmingDistance swimmingDistance = swimmingDistanceFromProtobuf(raceDetailsDTOProtobuf.getSwimmingDistance());
        SwimmingStyle swimmingStyle = swimmingStyleFromProtobuf(raceDetailsDTOProtobuf.getSwimmingStyle());
        return new RaceDetailsDTO(swimmingDistance, swimmingStyle);
    }

    public static SwimmingContestProtobuf.RaceDetailsDTO raceDetailsDTOToProtobuf(RaceDetailsDTO raceDetailsDTO) {
        SwimmingContestProtobuf.SwimmingDistance swimmingDistance = swimmingDistanceToProtobuf(raceDetailsDTO.getSwimmingDistance());
        SwimmingContestProtobuf.SwimmingStyle swimmingStyle = swimmingStyleToProtobuf(raceDetailsDTO.getSwimmingStyle());
        return SwimmingContestProtobuf.RaceDetailsDTO.newBuilder().setSwimmingDistance(swimmingDistance).setSwimmingStyle(swimmingStyle).build();
    }

    public static RaceDTO raceDTOFromProtobuf(SwimmingContestProtobuf.RaceDTO raceDTOProtobuf) {
        SwimmingDistance swimmingDistance = swimmingDistanceFromProtobuf(raceDTOProtobuf.getSwimmingDistance());
        SwimmingStyle swimmingStyle = swimmingStyleFromProtobuf(raceDTOProtobuf.getSwimmingStyle());
        int noSwimmers = raceDTOProtobuf.getNoSwimmers();
        return new RaceDTO(swimmingDistance, swimmingStyle, noSwimmers);
    }

    public static SwimmerDTO swimmerDTOFromProtobuf(SwimmingContestProtobuf.SwimmerDTO swimmerDTOProtobuf) {
        Swimmer swimmer = swimmerFromProtobuf(swimmerDTOProtobuf.getSwimmer());
        List<RaceDetailsDTO> raceDetailsDTOs = swimmerDTOProtobuf.getRaceDetailsDTOsList().stream()
                .map(ProtobufUtils::raceDetailsDTOFromProtobuf)
                .toList();
        return new SwimmerDTO(swimmer, raceDetailsDTOs);
    }

    public static SwimmingContestProtobuf.SwimmerDTO swimmerDTOToProtobuf(SwimmerDTO swimmerDTO) {
        SwimmingContestProtobuf.Swimmer swimmer = swimmerToProtobuf(swimmerDTO.getSwimmer());
        List<SwimmingContestProtobuf.RaceDetailsDTO> allRaceDetailsDTOs = swimmerDTO.getRaceDetailsDTOs().stream()
                .map(ProtobufUtils::raceDetailsDTOToProtobuf)
                .toList();
        return SwimmingContestProtobuf.SwimmerDTO.newBuilder().setSwimmer(swimmer).addAllRaceDetailsDTOs(allRaceDetailsDTOs).build();
    }

    public static Swimmer swimmerFromProtobuf(SwimmingContestProtobuf.Swimmer swimmerProtobuf) {
        return new Swimmer(swimmerProtobuf.getID(),
                swimmerProtobuf.getFirstName(),
                swimmerProtobuf.getLastName(),
                swimmerProtobuf.getAge());
    }

    public static SwimmingContestProtobuf.Swimmer swimmerToProtobuf(Swimmer swimmer) {

        SwimmingContestProtobuf.Swimmer.Builder swimmerBuilder = SwimmingContestProtobuf.Swimmer.newBuilder().setFirstName(swimmer.getFirstName()).setLastName(swimmer.getLastName()).setAge(swimmer.getAge());
        if (swimmer.getId() != null){
            swimmerBuilder.setID(swimmer.getId());
        }
        return swimmerBuilder.build();
    }

    public static SwimmingContestProtobuf.Request createLoginRequest(AdminDTO adminDTO) {
        SwimmingContestProtobuf.AdminDTO adminDTOProtobuf = adminDTOToProtobuf(adminDTO);
        SwimmingContestProtobuf.LoginRequest loginRequest = SwimmingContestProtobuf.LoginRequest.newBuilder().setAdminDTO(adminDTOProtobuf).build();
        return SwimmingContestProtobuf.Request.newBuilder().setLoginRequest(loginRequest).build();
    }

    public static SwimmingContestProtobuf.Request createLogoutRequest(String username) {
        SwimmingContestProtobuf.LogoutRequest logoutRequest = SwimmingContestProtobuf.LogoutRequest.newBuilder().setUsername(username).build();
        return SwimmingContestProtobuf.Request.newBuilder().setLogoutRequest(logoutRequest).build();
    }

    public static SwimmingContestProtobuf.Request createFindAllRacesDetailsRequest() {
        SwimmingContestProtobuf.FindAllRacesDetailsRequest findAllRacesDetailsRequest = SwimmingContestProtobuf.FindAllRacesDetailsRequest.newBuilder().build();
        return SwimmingContestProtobuf.Request.newBuilder().setFindAllRacesDetailsRequest(findAllRacesDetailsRequest).build();
    }

    public static SwimmingContestProtobuf.Request createFindAllSwimmersDetailsForRaceRequest(RaceDetailsDTO raceDetailsDTO) {
        SwimmingContestProtobuf.RaceDetailsDTO raceDetailsDTOProtobuf = raceDetailsDTOToProtobuf(raceDetailsDTO);
        SwimmingContestProtobuf.FindAllSwimmersDetailsForRaceRequest findAllSwimmersDetailsForRaceRequest = SwimmingContestProtobuf.FindAllSwimmersDetailsForRaceRequest.newBuilder().setRaceDetailsDTO(raceDetailsDTOProtobuf).build();
        return SwimmingContestProtobuf.Request.newBuilder().setFindAllSwimmersDetailsForRaceRequest(findAllSwimmersDetailsForRaceRequest).build();
    }

    public static SwimmingContestProtobuf.Request createAddSwimmerRequest(SwimmerDTO swimmerDTO) {
        SwimmingContestProtobuf.SwimmerDTO swimmerDTOProtobuf = swimmerDTOToProtobuf(swimmerDTO);
        SwimmingContestProtobuf.AddSwimmerRequest addSwimmerRequest = SwimmingContestProtobuf.AddSwimmerRequest.newBuilder().setSwimmerDTO(swimmerDTOProtobuf).build();
        return SwimmingContestProtobuf.Request.newBuilder().setAddSwimmerRequest(addSwimmerRequest).build();
    }
}
