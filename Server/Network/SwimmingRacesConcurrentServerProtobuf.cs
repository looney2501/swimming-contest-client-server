using System.Net.Sockets;
using System.Threading;
using Model.Services;

namespace Server.Network;

public class SwimmingRacesConcurrentServerProtobuf : AbstractConcurrentServer
{
    private readonly ISwimmingRaceServices _services;

    public SwimmingRacesConcurrentServerProtobuf(string host, int port, ISwimmingRaceServices services) : base(host,
        port)
    {
        Logger.Info("Server name: SwimmingRacesServer; Server type: concurrent; Port: " + port);
        _services = services;
    }

    protected override Thread createWorker(TcpClient client)
    {
        Logger.Info("Creating worker to handle client...");
        var clientWorker = new SwimmingRacesClientWorkerProtobuf(_services, client);
        return new Thread(clientWorker.Run);
    }
}