using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using log4net;
using Model.Domain.DTOs;
using Model.Observer;
using Model.Protocol.Requests;
using Model.Protocol.Responses;
using Model.Services;

namespace Server.Network;

public class SwimmingRacesClientWorker: ISwimmingRaceObserver
{
    private ISwimmingRaceServices _services;
    private TcpClient _clientSocket;
    private NetworkStream _stream;
    private IFormatter _formatter;
    private volatile bool _connected;
    private static readonly ILog Logger = LogManager.GetLogger("Worker");

    public SwimmingRacesClientWorker(ISwimmingRaceServices services, TcpClient clientSocket)
    {
        _services = services;
        _clientSocket = clientSocket;
        try
        {
            _stream = clientSocket.GetStream();
            _formatter = new BinaryFormatter();
            _connected = true;
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
                object request = _formatter.Deserialize(_stream);
                object response = HandleRequest((IRequest) request);
                if (response != null)
                {
                    SendResponse((IResponse) response);
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


    public void RacesUpdated()
    {
        try
        {
            SendResponse(new RacesUpdatedResponse());
            Logger.Info("RacesUpdateResponse sent to client: " + _clientSocket);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    private IResponse HandleRequest(IRequest request)
    {
        if (request is LoginRequest loginRequest)
        {
            Logger.Info("HandleRequest(request = LoginRequest)");
            var adminDTO = loginRequest.AdminDTO;
            try
            {
                lock (_services)
                {
                    _services.Login(adminDTO.Username, adminDTO.Password, this);
                }
                Logger.Info("Result: response = OkResponse");
                return new OkResponse();
            }
            catch (ServicesException ex)
            {
                _connected = false;
                Logger.Info("Result: response = ErrorResponse");
                return new ErrorResponse(ex.Message);
            }
        }

        if (request is LogoutRequest logoutRequest)
        {
            Logger.Info("HandleRequest(request = LogoutRequest)");
            var adminUsername = logoutRequest.Username;
            try
            {
                lock (_services)
                {
                    _services.Logout(adminUsername);
                }

                _connected = false;
                Logger.Info("Result: response = LogoutRequest");
                return new OkResponse();
            }
            catch (ServicesException ex)
            {
                Logger.Info("Result: response = ErrorResponse");
                return new ErrorResponse(ex.Message);
            }
        }

        if (request is FindAllRacesDetailsRequest findAllRacesDetailsRequest)
        {
            Logger.Info("HandleRequest(request = FindAllRacesDetailsRequest)");
            List<RaceDTO> allRacesDetails;
            lock (_services)
            {
                allRacesDetails = _services.FindAllRacesDetails();
            }
            Logger.Info("Result: response = FindAllRacesDetailsResponse");
            return new FindAllRacesDetailsResponse(allRacesDetails);
        }

        if (request is FindAllSwimmersDetailsForRaceRequest findAllSwimmersDetailsForRaceRequest)
        {
            Logger.Info("HandleRequest(request = FindAllSwimmersDetailsForRaceRequest)");
            var raceDetailsDTO = findAllSwimmersDetailsForRaceRequest.RaceDetailsDTO;
            List<SwimmerDTO> allSwimmersDetailsForRace;
            lock (_services)
            {
                allSwimmersDetailsForRace = _services.FindAllSwimmersDetailsForRace(raceDetailsDTO.SwimmingDistance,
                    raceDetailsDTO.SwimmingStyle);
            }
            Logger.Info("Result: response = FindAllSwimmersDeailsForRaceResponse");
            return new FindAllSwimmersDetailsForRaceResponse(allSwimmersDetailsForRace);
        }

        if (request is AddSwimmerRequest addSwimmerRequest)
        {
            Logger.Info("HandleRequest(request = AddSwimmerRequest)");
            var swimmerDTO = addSwimmerRequest.SwimmerDTO;
            lock (_services)
            {
                _services.AddSwimmer(swimmerDTO.FirstName, swimmerDTO.LastName, swimmerDTO.Age, swimmerDTO.RaceDetailsDTOs);
            }
            Logger.Info("Result: response = OkResponse");
            return new OkResponse();
        }

        return null;
    }

    private void SendResponse(IResponse response)
    {
        lock (_stream)
        {
            _formatter.Serialize(_stream, response);
            _stream.Flush();
        }
    }
}