using System.Net;
using System.Net.Sockets;
using log4net;

namespace Server.Network;

public abstract class AbstractServer
{
    private TcpListener _socket;
    private string _host;
    private int _port;
    protected static readonly ILog Logger = LogManager.GetLogger("Server");

    public AbstractServer(string host, int port)
    {
        _host = host;
        _port = port;
    }

    public void Start()
    {
        IPAddress ipAddress = IPAddress.Parse(_host);
        IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, _port);
        _socket = new TcpListener(ipEndPoint);
        _socket.Start();
        Logger.Info("Server started successfully!");
        while (true)
        {
            Logger.Info("Waiting for clients...");
            TcpClient client = _socket.AcceptTcpClient();
            Logger.Info("Client connected!");
            ProcessRequest(client);
        }
    }

    public abstract void ProcessRequest(TcpClient client);
}