using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWars
{
    public class GameController
    {
        private Socket server;

        private int PlayerID;
        private World world;
        public void SetServerSocket(Socket s)
        {
            server = s;
        }
        public void ReceiveStartupInfo(SocketState state)
        {
            StringBuilder sb = state.sb;
            int universeSize = 0;

            try
            {
                char[] separator = new char[] { '\n' };
                String[] objects= sb.ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                if (objects.Length < 2 || (objects.Length == 2 && sb.ToString()[sb.Length - 1] != '\n'))
                {
                    Networking.RequestMoreData(state);
                    return;
                }
                sb.Remove(0, objects[0].Length + 1);
                PlayerID = Int32.Parse(objects[0]);
                state.uid = PlayerID;

                sb.Remove(0, objects[1].Length + 1);
                universeSize = Int32.Parse(objects[1]);
                
            }catch(Exception except)
            {

            }
            world = new World(universeSize);
            state.callMe = RecevieData;
            RecevieData(state);
            
        }
        public void RecevieData(SocketState state)
        {
            StringBuilder sb = state.sb;

        }

    }
}
