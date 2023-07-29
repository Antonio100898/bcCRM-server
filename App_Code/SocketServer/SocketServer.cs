using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;

public class SocketServer
{
    public string Value { get; set; }    
    private static SocketServer _instance;
    Socket serverSocket;
    private static readonly object _lock = new object();
    private SocketServer() { }
    public static SocketServer GetInstance(string value)
    {
        // This conditional is needed to prevent threads stumbling over the
        // lock once the instance is ready.
        if (_instance == null)
        {
            // Now, imagine that the program has just been launched. Since
            // there's no Singleton instance yet, multiple threads can
            // simultaneously pass the previous conditional and reach this
            // point almost at the same time. The first of them will acquire
            // lock and will proceed further, while the rest will wait here.
            lock (_lock)
            {
                // The first thread to acquire the lock, reaches this
                // conditional, goes inside and creates the Singleton
                // instance. Once it leaves the lock block, a thread that
                // might have been waiting for the lock release may then
                // enter this section. But since the Singleton field is
                // already initialized, the thread won't create a new
                // object.
                if (_instance == null)
                {
                    _instance = new SocketServer();
                    _instance.Value = value;
                }
            }
        }
        return _instance;
    }

    public void TrySimple()
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        serverSocket.Bind(new IPEndPoint(IPAddress.Any, 8080));
        serverSocket.Listen(128);
        serverSocket.BeginAccept(null, 0, OnAccept, null);
    }

    private void OnAccept(IAsyncResult result)
    {
        try
        {
            Socket client = null;
            if (serverSocket != null && serverSocket.IsBound)
            {
                client = serverSocket.EndAccept(result);
            }
            if (client != null)
            {
                /* Handshaking and managing ClientSocket */
            }
        }
        catch (SocketException exception)
        {

        }
        finally
        {
            if (serverSocket != null && serverSocket.IsBound)
            {
                serverSocket.BeginAccept(null, 0, OnAccept, null);
            }
        }
    }

}