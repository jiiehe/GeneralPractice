using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpaceWars;
namespace View
{
    public partial class Form1 : Form
    {
        GameController controller = new GameController();
        public Form1()
        {
            InitializeComponent();
            connectButton.Click += ConnectClick;
        }
        private void ConnectClick(object sender,EventArgs e)
        {
            if (serverText.Text == "")
            {
                MessageBox.Show("please enter a server address");
                return;
            }
            if (nameText.Text == "")
            {
                MessageBox.Show("please enter a name");
                return;
            }
            if (nameText.Text.Length > 16)
            {
                MessageBox.Show("name must be less than 16 characters");
                return;
            }
            connectButton.Enabled = false;
            serverText.Enabled = false;
            nameText.Enabled = false;
            controller.SetServerSocket(Networking.ConnectToServer(FirstContact,serverText.Text));
        }
        private void FirstContact(SocketState state)
        {
            if (state.workSocket == null || state.workSocket.Connected == false)
            {
                MessageBox.Show("unable to connect");
                this.Invoke(new MethodInvoker(() =>
                {
                    connectButton.Enabled = true;
                    serverText.Enabled = true;
                    nameText.Enabled = true;
                }));
                return;
            }
            Console.WriteLine("connect from server");

            state.callMe = controller.ReceiveStartupInfo;
            Networking.Send(state.workSocket, nameText.Text + "\n");
            Networking.RequestMoreData(state);
        }

    }
}
