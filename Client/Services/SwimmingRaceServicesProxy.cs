using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using log4net;
using Model.Domain.DTOs;
using Model.Domain.Entities;
using Model.Domain.Enums;
using Model.Observer;
using Model.Protocol.Requests;
using Model.Protocol.Responses;
using Model.Services;

namespace Client.Services;

public class SwimmingRaceServicesProxy : ISwimmingRaceServices
{
    private static readonly ILog Logger = LogManager.GetLogger("Proxy");
    private readonly string _host;
    private readonly int _port;
    private readonly Queue<IResponse> _qresponses;
    private ISwimmingRaceObserver _client;
    private volatile bool _finished;
    private IFormatter _formatter;
    private NetworkStream _networkStream;
    private TcpClient _socketClient;
    private EventWaitHandle _waitHandle;

    public SwimmingRaceServicesProxy(string host, int port)
    {
        _host = host;
        _port = port;
        _qresponses = new Queue<IResponse>();
    }

    public void Login(string username, string password, ISwimmingRaceObserver client)
    {
        InitializeConnection();
        var adminDTO = new AdminDTO(username, password);
        Logger.InfoFormat("Sending login request: username: {0}, password: {1}...", username, password);
        SendRequest(new LoginRequest(adminDTO));
        Logger.Info("Waiting for response...");
        var response = ReadResponse();
        if (response is OkResponse)
        {
            Logger.Info("OkResponse received!");
            _client = client;
        }

        if (response is ErrorResponse errorResponse)
        {
            Logger.Info("ErrorResponse received!");
            CloseConnection();
            throw new ServicesException(errorResponse.ErrorMessage);
        }
    }

    public void Logout(string username)
    {
        Logger.InfoFormat("Sending logout request: username: {0}", username);
        SendRequest(new LogoutRequest(username));
        Logger.Info("Waiting for response...");
        var response = ReadResponse();
        if (response is OkResponse)
        {
            Logger.Info("Okresponse received!");
            CloseConnection();
        }

        if (response is ErrorResponse errorResponse)
        {
            Logger.Info("ErrorResponse received!");
            throw new ServerException(errorResponse.ErrorMessage);
        }
    }

    public List<RaceDTO> FindAllRacesDetails()
    {
        Logger.Info("Sendinf FindAllRacesDetailsRequest...");
        SendRequest(new FindAllRacesDetailsRequest());
        Logger.Info("Waiting for response...");
        var response = ReadResponse();
        if (response is FindAllRacesDetailsResponse findAllRacesDetailsResponse)
        {
            Logger.Info("FindAllRacesDetailsResponse received!");
            return findAllRacesDetailsResponse.AllRacesDetails;
        }

        Logger.Info("Wrong response received!");
        return null;
    }

    public List<SwimmerDTO> FindAllSwimmersDetailsForRace(SwimmingDistance swimmingDistance,
        SwimmingStyle swimmingStyle)
    {
        var raceDetailsDto = new RaceDetailsDTO(swimmingDistance, swimmingStyle);
        Logger.InfoFormat("Sending FindAllSwimmersDetailsForRaceRequest: raceDetails: {0}", raceDetailsDto);
        SendRequest(new FindAllSwimmersDetailsForRaceRequest(raceDetailsDto));
        Logger.Info("Waiting for response...");
        var response = ReadResponse();
        if (response is FindAllSwimmersDetailsForRaceResponse findAllSwimmersDetailsForRaceResponse)
        {
            return findAllSwimmersDetailsForRaceResponse.AllSwimmersDetailsForRace;
        }

        Logger.Info("Wrong rasponse received!");
        return null;
    }

    public void AddSwimmer(string firstName, string lastName, int age, List<RaceDetailsDTO> raceDetailsDTOs)
    {
        var swimmerDTO = new SwimmerDTO(new Swimmer(firstName, lastName, age), raceDetailsDTOs);
        Logger.InfoFormat("Sending AddSwimmerRequest: swimmerDetails: {0}", swimmerDTO);
        SendRequest(new AddSwimmerRequest(swimmerDTO));
        Logger.Info("Waiting for response...");
        var response = ReadResponse();
        if (response is OkResponse)
            Logger.Info("OkResponse received!");
        else
            Logger.Info("Wrong response received!");
    }

    private void InitializeConnection()
    {
        try
        {
            Logger.InfoFormat("Connection details: host: {0}, port: {1}", _host, _port);
            _socketClient = new TcpClient(_host, _port);
            Logger.Info("Successfully connected!");
            _networkStream = _socketClient.GetStream();
            _formatter = new BinaryFormatter();
            _finished = false;
            _waitHandle = new AutoResetEvent(false);
            StartReader();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    private void StartReader()
    {
        var t = new Thread(Run);
        t.Start();
    }

    private void HandleUpdate(IUpdateResponse updateResponse)
    {
        if (updateResponse is RacesUpdatedResponse)
        {
            Logger.Info("Updating UI...");
            _client.RacesUpdated();
            Logger.Info("UI updated successfully!");
        }
    }

    private void CloseConnection()
    {
        Logger.Info("Closing connection...");
        _finished = true;
        try
        {
            _networkStream.Close();
            _socketClient.Close();
            _waitHandle.Close();
            _client = null;
            Logger.Info("Connection closed successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    private void SendRequest(IRequest request)
    {
        try
        {
            _formatter.Serialize(_networkStream, request);
            _networkStream.Flush();
            Logger.Info("Request sent successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }
    }

    private IResponse ReadResponse()
    {
        IResponse response = null;
        try
        {
            _waitHandle.WaitOne();
            lock (_qresponses)
            {
                response = _qresponses.Dequeue();
                Logger.Info("Response read successfully!");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.StackTrace);
        }

        return response;
    }

    public void Run()
    {
        while (!_finished)
            try
            {
                var response = _formatter.Deserialize(_networkStream);
                if (response is IUpdateResponse)
                {
                    Logger.Info("UpdateResponse received. Handling response...");
                    HandleUpdate((IUpdateResponse) response);
                }
                else
                {
                    lock (_qresponses)
                    {
                        _qresponses.Enqueue((IResponse) response);
                    }

                    _waitHandle.Set();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
    }
}