using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using Google.Protobuf;
using log4net;
using Model.Observer;
using Model.Protocol.Protobuf;
using Model.Services;
using RaceDTO = Model.Domain.DTOs.RaceDTO;
using SwimmerDTO = Model.Domain.DTOs.SwimmerDTO;

namespace Server.Network;

public class SwimmingRacesClientWorkerProtobuf : ISwimmingRaceObserver
{
    private static readonly ILog Logger = LogManager.GetLogger("Worker");
    private readonly TcpClient _clientSocket;
    private volatile bool _connected;
    private readonly ISwimmingRaceServices _services;
    private readonly NetworkStream _stream;

    public SwimmingRacesClientWorkerProtobuf(ISwimmingRaceServices services, TcpClient clientSocket)
    {
        _services = services;
        _clientSocket = clientSocket;
        try
        {
            _stream = clientSocket.GetStream();
            _connected = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    public void RacesUpdated()
    {
        try
        {
            SendResponse(ProtoUtils.CreateRacesUpdatedResponse());
            Logger.Info("RacesUpdateResponse sent to client: " + _clientSocket);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    public void Run()
    {
        while (_connected)
        {
            try
            {
                var request = Request.Parser.ParseDelimitedFrom(_stream);
                var response = HandleRequest(request);
                if (response != null)
                {
                    SendResponse(response);
                    Logger.Info("Response sent.");
                }
                else
                {
                    Logger.Info("Unknown request type, cannot be handled!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            try
            {
                Thread.Sleep(1000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        try
        {
            _stream.Close();
            _clientSocket.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    private Response HandleRequest(Request request)
    {
        if (request.RequestTypeCase == Request.RequestTypeOneofCase.LoginRequest)
        {
            Logger.Info("HandleRequest(request = LoginRequest)");
            var adminDTO = ProtoUtils.AdminDTOFromProtobuf(request.LoginRequest.AdminDTO);
            try
            {
                lock (_services)
                {
                    _services.Login(adminDTO.Username, adminDTO.Password, this);
                }

                Logger.Info("Result: response = OkResponse");
                return ProtoUtils.CreateOkResponse();
            }
            catch (ServicesException ex)
            {
                _connected = false;
                Logger.Info("Result: response = ErrorResponse");
                return ProtoUtils.CreateErrorResponse(ex.Message);
            }
        }

        if (request.RequestTypeCase == Request.RequestTypeOneofCase.LogoutRequest)
        {
            Logger.Info("HandleRequest(request = LogoutRequest)");
            var adminUsername = request.LogoutRequest.Username;
            try
            {
                lock (_services)
                {
                    _services.Logout(adminUsername);
                }

                _connected = false;
                Logger.Info("Result: response = LogoutRequest");
                return ProtoUtils.CreateOkResponse();
            }
            catch (ServicesException ex)
            {
                Logger.Info("Result: response = ErrorResponse");
                return ProtoUtils.CreateErrorResponse(ex.Message);
            }
        }

        if (request.RequestTypeCase == Request.RequestTypeOneofCase.FindAllRacesDetailsRequest)
        {
            Logger.Info("HandleRequest(request = FindAllRacesDetailsRequest)");
            List<RaceDTO> allRacesDetails;
            lock (_services)
            {
                allRacesDetails = _services.FindAllRacesDetails();
            }

            Logger.Info("Result: response = FindAllRacesDetailsResponse");
            return ProtoUtils.CreateFindAllRacesDetailsReponse(allRacesDetails);
        }

        if (request.RequestTypeCase == Request.RequestTypeOneofCase.FindAllSwimmersDetailsForRaceRequest)
        {
            Logger.Info("HandleRequest(request = FindAllSwimmersDetailsForRaceRequest)");
            var raceDetailsDTO =
                ProtoUtils.RaceDetailsDTOFromProtobuf(request.FindAllSwimmersDetailsForRaceRequest.RaceDetailsDTO);
            List<SwimmerDTO> allSwimmersDetailsForRace;
            lock (_services)
            {
                allSwimmersDetailsForRace = _services.FindAllSwimmersDetailsForRace(raceDetailsDTO.SwimmingDistance,
                    raceDetailsDTO.SwimmingStyle);
            }

            Logger.Info("Result: response = FindAllSwimmersDeailsForRaceResponse");
            return ProtoUtils.CreateFindAllSwimmersDetailsForRaceResponse(allSwimmersDetailsForRace);
        }

        if (request.RequestTypeCase == Request.RequestTypeOneofCase.AddSwimmerRequest)
        {
            Logger.Info("HandleRequest(request = AddSwimmerRequest)");
            var swimmerDTO = ProtoUtils.SwimmerDTOFromProtobuf(request.AddSwimmerRequest.SwimmerDTO);
            lock (_services)
            {
                _services.AddSwimmer(swimmerDTO.FirstName, swimmerDTO.LastName, swimmerDTO.Age,
                    swimmerDTO.RaceDetailsDTOs);
            }

            Logger.Info("Result: response = OkResponse");
            return ProtoUtils.CreateOkResponse();
        }

        return null;
    }

    private void SendResponse(Response response)
    {
        lock (_stream)
        {
            response.WriteDelimitedTo(_stream);
            _stream.Flush();
        }
    }
}