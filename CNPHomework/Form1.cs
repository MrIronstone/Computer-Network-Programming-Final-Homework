using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace CNPHomework
{
    public partial class Form1 : Form
    {
        private static Socket client;
        private static byte[] data = new byte[1024];

        private static int maxFlagNumberPerPlayer = 5;
        public int currentFlagNumber = 0;
        private Flag[] flags = new Flag[maxFlagNumberPerPlayer];

        public void SewFlag(int x, int y)
        {
            if (currentFlagNumber < maxFlagNumberPerPlayer)
            {
                Flag newFlag = new Flag(x, y);
                currentFlagNumber += 1;
                flags[currentFlagNumber - 1] = newFlag;
                results.Items.Add(String.Format("New Flag has been sewed.\r It's coordinates are: X={0}, Y={1}", x, y));
            }
        }

        public void TcpChat()
        {

        }
        void ButtonListenOnClick(object obj, EventArgs ea)
        {
            try
            {
                results.Items.Add("Listening for a client...");
                Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
                newsock.Bind(iep);
                newsock.Listen(5);
                newsock.BeginAccept(new AsyncCallback(AcceptConn), newsock);
            }
            catch (Exception)
            {
                results.Items.Add("Error on listen on click button");
            }
            
        }
        void ButtonConnectOnClick(object obj, EventArgs ea)
        {
            try
            {
                results.Items.Add("Connecting...");
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
                client.BeginConnect(iep, new AsyncCallback(Connected), client);
                
            }
            catch (Exception ex)
            {
                results.Items.Add("Error on the connect on click button");
            }

            
        }
        void ButtonSendOnClick(object obj, EventArgs ea)
        {
            try
            {
                byte[] message = Encoding.ASCII.GetBytes(newText.Text);
                newText.Clear();
                client.BeginSend(message, 0, message.Length, 0,
                new AsyncCallback(SendData), client);
            }
            catch (Exception ex)
            {
                results.Items.Add("Error on sending the data on button click");
            }
            
        }
        void AcceptConn(IAsyncResult iar)
        {
            try
            {
                Socket oldserver = (Socket)iar.AsyncState;
                client = oldserver.EndAccept(iar);
                results.Items.Add("Connection from: " + client.RemoteEndPoint.ToString());
                Thread receiver = new Thread(new ThreadStart(ReceiveData));
                receiver.Start();
            }
            catch (Exception ex)
            {

                results.Items.Add("Error accepting the connection");   
            }
            
        }
        void Connected(IAsyncResult iar)
        {
            try
            {
                client.EndConnect(iar);
                results.Items.Add("Connected to: " + client.RemoteEndPoint.ToString());
                Thread receiver = new Thread(new ThreadStart(ReceiveData));
                receiver.Start();
            }
            catch (SocketException)
            {
                results.Items.Add("Error on connected");
            }
        }
        void SendData(IAsyncResult iar)
        {
            try
            {
                Socket remote = (Socket)iar.AsyncState;
                int sent = remote.EndSend(iar);
            }
            catch (Exception ex)
            {

                results.Items.Add("Error on sending the data");
            }
            
        }
        void ReceiveData()
        {
            try
            {
                int recv;
                string stringData;
                while (true)
                {
                    recv = client.Receive(data);
                    stringData = Encoding.ASCII.GetString(data, 0, recv);
                    if (stringData == "bye")
                        break;
                    results.Items.Add(stringData);
                }
                stringData = "bye";
                byte[] message = Encoding.ASCII.GetBytes(stringData);
                client.Send(message);
                client.Close();
                results.Items.Add("Connection stopped");
                return;
            }
            catch (Exception ex)
            {

                results.Items.Add("Error on receiving the data");
            }
            
        }

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            ButtonSendOnClick(sender, e);
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            ButtonConnectOnClick(sender, e);    
        }

        private void ListenButton_Click(object sender, EventArgs e)
        {
            ButtonListenOnClick(sender, e); 
        }

        private void Map_MouseDown(object sender, MouseEventArgs e)
        {
            Image b = Map.Image;
            int x = b.Width * e.X / Map.Width;
            int y = b.Height * e.Y / Map.Height;
            SewFlag(x, y);
        }
    }
}
