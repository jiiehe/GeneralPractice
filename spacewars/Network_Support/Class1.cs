using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWars
{
    public class SocketState
    {
        public Socket workSocket = null;
        public const int BufferSize = 4096;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
        public String errorMessage = "";
        public bool errorOccured = false;
        public long uid = -1;
        public Action<SocketState> callMe;
    }
    public static class Networking
    {
        public const int GAME_PORT = 11000;
        public const int HTTP_PORT = 80;
        public static Socket ConnectToServer(Action<SocketState> call_me,String host_name)
        {
            System.Diagnostics.Debug.WriteLine("connecting  to " + host_name);

            // Connect to a remote device.
            try
            {

                // Establish the remote endpoint for the socket.
                IPHostEntry ipHostInfo;
                IPAddress ipAddress = IPAddress.None;

                // Determine if the server address is a URL or an IP
                try
                {
                    ipHostInfo = Dns.GetHostEntry(host_name);
                    bool foundIPV4 = false;
                    foreach (IPAddress addr in ipHostInfo.AddressList)
                        if (addr.AddressFamily != AddressFamily.InterNetworkV6)
                        {
                            foundIPV4 = true;
                            ipAddress = addr;
                            break;
                        }
                    // Didn't find any IPV4 addresses
                    if (!foundIPV4)
                    {
                        System.Diagnostics.Debug.WriteLine("Invalid addres: " + host_name);
                        return null;
                    }
                }
                catch (Exception)
                {
                    // see if host name is actually an ipaddress, i.e., 155.99.123.456
                    System.Diagnostics.Debug.WriteLine("using IP");
                    ipAddress = IPAddress.Parse(host_name);
                }

                // Create a TCP/IP socket.
                Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
                socket.NoDelay = true;

                SocketState theServer = new SocketState();
                theServer.callMe = call_me;
                theServer.workSocket = socket;

                IAsyncResult result = theServer.workSocket.BeginConnect(ipAddress, Networking.GAME_PORT, ConnectedToServer, theServer);

                // Timeout after 3 seconds. Client should check for a closed socket
                bool success = result.AsyncWaitHandle.WaitOne(3000, true);
                if (!success)
                {
                    socket.Close();
                    return socket;
                }

                return theServer.workSocket;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Unable to connect to server. Error occured: " + e);
                return null;
            }
        }


        public static void ConnectedToServer(IAsyncResult ar)
        {
            SocketState state = (SocketState)ar.AsyncState;
            try
            {
                state.workSocket.EndConnect(ar);
                state.workSocket.NoDelay = true;
                state.callMe(state);
               
            }
            catch(Exception e)
            {
                state.errorMessage = e.ToString();
                state.errorOccured = true;
                state.callMe(state);
            }

        }
      
        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                SocketState state = (SocketState)ar.AsyncState;
                Socket socket = state.workSocket;
                int bytesRead = socket.EndReceive(ar);
                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.UTF8.GetString(state.buffer, 0, bytesRead));
                    try
                    {
                        state.callMe(state);
                    }
                    catch (Exception except)
                    {
                        Console.WriteLine("error is " + except);
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine("EXCEPTION: " + e.ToString());
            }
            
            
        }
        public static void RequestMoreData(SocketState state)
        {
            
            state.workSocket.BeginReceive(state.buffer, 0, SocketState.BufferSize, 0, ReceiveCallback, state);
        }



        public static bool Send(Socket socket, String data)
        {
            try
            {
                int offset = 0;
                byte[] byteData = Encoding.UTF8.GetBytes(data); // Convert the string data to byte data using ASCII encoding.
                socket.BeginSend(byteData, offset, byteData.Length, SocketFlags.None, SendCallback, socket); // Begin sending the data to the remote device.
                return true;
            }
            catch (Exception e) // FIXME get the correct socket closed exception
            {
              
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception except)
                {
                    System.Diagnostics.Debug.WriteLine("Socket was already closed: " + except);
                }
                return false;
            }
        }
        public static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket socket = (Socket)ar.AsyncState;
                if (socket.Connected)
                {
                    socket.EndSend(ar);
                }

            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
