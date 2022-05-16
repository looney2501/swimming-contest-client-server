using System.Net;
using System.Net.Sockets;
using log4net;

namespace Server.Network;

public abstract class AbstractServer
{
    protected static readonly ILog Logger = LogManager.GetLogger("Server");
    private readonly string _host;
    private readonly int _port;
    private TcpListener _socket;

    public AbstractServer(string host, int port)
    {
        _host = host;
        _port = port;
    }

    public void Start()
    {
        var ipAddress = IPAddress.Parse(_host);
        var ipEndPoint = new IPEndPoint(ipAddress, _port);
        _socket = new TcpListener(ipEndPoint);
        _socket.Start();
        Logger.Info("Server started successfully!");
        while (true)
        {
            Logger.Info("Waiting for clients...");
            var client = _socket.AcceptTcpClient();
            Logger.Info("Client connected!");
            ProcessRequest(client);
        }
    }

    public abstract void ProcessRequest(TcpClient client);
}