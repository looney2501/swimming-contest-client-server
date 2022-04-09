using System.Net.Sockets;
using System.Threading;

namespace Server.Network;

public abstract class AbstractConcurrentServer: AbstractServer
{
    protected AbstractConcurrentServer(string host, int port) : base(host, port)
    {
    }

    public override void ProcessRequest(TcpClient client)
    {
        Thread t = createWorker(client);
        t.Start();
    }

    protected abstract Thread createWorker(TcpClient client);
}