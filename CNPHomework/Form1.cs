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

using System.Diagnostics;

namespace CNPHomework
{
    public partial class Form1 : Form
    {
        private static Socket client;
        private static byte[] data = new byte[1024];

        private static int maxFlagNumberPerPlayer = 5;
        public int currentFlagNumber = 0;
        private Flag[] flags = new Flag[maxFlagNumberPerPlayer];

        private bool isYourTurn = false;
        private bool AttackPhase = false;
        private bool SewFlagPhase = true;

        Socket newsock;
        Thread receiver;
        IPEndPoint iep;
        private void SewFlag(int x, int y)
        {
            // eğer yerleştirilen bayrak sayısı, maksimum izin verilen bayrak sayısından az ise
            // bayrak dikmeye izin veren if sorgusu
            if (currentFlagNumber < maxFlagNumberPerPlayer)
            {
                Flag newFlag = new Flag(x, y);
                
                currentFlagNumber += 1;
                flags[currentFlagNumber - 1] = newFlag;
                results.Items.Add(String.Format("New Flag has been sewed. It's coordinates are: X={0}, Y={1}", x, y));
                ListBoxOfSewedFlags.Items.Add(newFlag.ToString());
                FlagsLeftToSewTextBox.Text = (maxFlagNumberPerPlayer - currentFlagNumber).ToString();

                // eğer dikilen bayrak sayısı, maksimum izin verilen bayrak sayısına ulaşırsa
                // ready tuşunu aktive etmeyi sağlayan if sorgusu
                if ((currentFlagNumber == maxFlagNumberPerPlayer) && isYourTurn)
                {
                    ReadyButton.Enabled = true;
                }
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
                newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp);
                iep = new IPEndPoint(IPAddress.Any, 9050);
                newsock.Bind(iep);
                newsock.Listen(5);
                newsock.BeginAccept(new AsyncCallback(AcceptConn), newsock);
                
            }
            catch (Exception ex)
            {
                results.Items.Add("Error on listen on click button");
                MessageBox.Show(ex.ToString());
            }
            
        }

        void SkipTurn()
        {
            try
            {
                byte[] message = Encoding.ASCII.GetBytes("Your Turn!");
                client.BeginSend(message, 0, message.Length, 0,
                new AsyncCallback(SendData), client);
            }
            catch (Exception)
            {
                results.Items.Add("Error on skipping the turn");
            }
        }
        void AcceptConn(IAsyncResult iar)
        {
            try
            {
                Socket oldserver = (Socket)iar.AsyncState;
                client = oldserver.EndAccept(iar);
                results.Items.Add("Connection from: " + client.RemoteEndPoint.ToString());
                receiver = new Thread(new ThreadStart(ReceiveData));
                receiver.Start();
                // to deactive and active the appropriate buttons after connection has been accepted
                ListenButton.Enabled = false;
                ConnectButton.Enabled = false;
                DisconnectButton.Enabled = true;
                isYourTurn = true;

                TurnLabel.Text = "Your Turn!";
            }
            catch (Exception)
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

                // to deactive and active the appropriate buttons after connection has been accepted
                ListenButton.Enabled = false;
                ConnectButton.Enabled = false;
                DisconnectButton.Enabled = true;
                isYourTurn = false;
                TurnLabel.Text = "Enemy Turn";
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
            catch (Exception)
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
                    // bize bye gelirse bağlantı koptu demektir
                    if (stringData == "bye")
                        break;
                    // Your Turn! gelirse sıra bize geçti demektir
                    else if (stringData == "Your Turn!")
                    {
                        TurnLabel.Text = "Your Turn!";
                        isYourTurn = true;
                        // bu oyuncuyunun sırası gelecek şekilde ayarlamalıyım
                        if (SewFlagPhase = true && AttackPhase == false)
                        {
                            if(currentFlagNumber == maxFlagNumberPerPlayer)
                            {
                                ReadyButton.Enabled = true;
                            }
                        }
                        else if (AttackPhase == true && SewFlagPhase == false)
                        {
                            AttackText.Enabled = true;
                            AttackText.Text = "Select Location to Attack!";

                        }
                        
                    }
                    else if (stringData.StartsWith("Attack Location: "))
                    {
                        try
                        {
                            string attackLoc = stringData.Split(':')[1].Substring(1);
                            
                            int x = int.Parse(attackLoc.Split(',')[0].Split('=')[1]);
                            int y = int.Parse(attackLoc.Split(',')[1].Split('=')[1]);

                            MessageBox.Show(attackLoc + " and X: " + x + " , " + y );
                            GetHitToPosition(x, y);

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show(ex.ToString());
                        }

                    }
                    results.Items.Add(stringData);
                }


                // disconnect
                stringData = "bye";
                byte[] message = Encoding.ASCII.GetBytes(stringData);
                client.Send(message);
                client.Close();
                results.Items.Add("Connection stopped");
                ListenButton.Enabled = true;
                ConnectButton.Enabled = true;
                DisconnectButton.Enabled = false;
                isYourTurn = false;
                TurnLabel.Text = "Game hasn't started yet!";

                newsock.Shutdown(SocketShutdown.Both);
                receiver.Abort();
                

                AttackButton.Enabled = false;
                currentFlagNumber = 0;
                return;
            }
            catch (Exception)
            {
                results.Items.Add("Error on receiving the data");
            }
            
        }

        public Form1()
        {
            InitializeComponent();

            FlagsLeftToSewTextBox.Text = (maxFlagNumberPerPlayer - currentFlagNumber).ToString();

            ReadyButton.Enabled = false;
            AttackButton.Enabled = false;
            AttackText.Enabled = false;
            DisconnectButton.Enabled = false;
            TurnLabel.Text = "Game hasn't started yet!";

            // Control.CheckForIllegalCrossThreadCalls = false;
        }

        void ButtonConnectOnClick(object obj, EventArgs ea)
        {
            results.Items.Add("Connecting...");
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
            ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            client.BeginConnect(iep, new AsyncCallback(Connected), client);
            
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            ButtonConnectOnClick(sender, e);    
        }

        private void ListenButton_Click(object sender, EventArgs e)
        {
            ButtonListenOnClick(sender, e);
        }

        private void pictureBoxOfMap_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Image b = pictureBoxOfMap.Image;
                int x = b.Width * e.X / pictureBoxOfMap.Width;
                int y = b.Height * e.Y / pictureBoxOfMap.Height;
                if ( SewFlagPhase = true && AttackPhase == false)
                {
                    SewFlag(x, y);
                }
                else if (AttackPhase == true && SewFlagPhase == false && AttackText.Enabled == true)
                {
                    AttackText.Text = String.Format("X={0}, Y={1}", x, y);
                }

            }
            catch (Exception)
            {
                results.Items.Add("Error on map picture click");
            }
        }

        private void ReadyButton_Click(object sender, EventArgs e)
        {
            TurnLabel.Text = "Enemy Turn";
            SewFlagPhase = false;
            AttackPhase = true;
            ReadyButton.Enabled = false;
            SkipTurn();
        }

        private void AttackButton_Click(object sender, EventArgs e)
        {
            AttackButton.Enabled = false;
            SendAttackLocations();
            TurnLabel.Text = "Enemy Turn";
            SkipTurn();
        }

        private void SendAttackLocations()
        {
            try
            {
                string attackLocation = "Attack Location: " + AttackText.Text;
                byte[] message = Encoding.ASCII.GetBytes(attackLocation);
                client.BeginSend(message, 0, message.Length, 0,
                new AsyncCallback(SendData), client);
                results.Items.Add("You attacked to: " + AttackText.Text);
            }
            catch (Exception)
            {
                results.Items.Add("Error on sending the attack locations");
            }
        }

        public void GetHitToPosition(int x, int y)
        {
            Flag hitArea = new Flag(x, y);
            
            // this double for loop iterates over the flag's coordinates
            for (int i = 0; i < hitArea.flagAreaCoordinates.Length; i++)
            {
                for (int j = 0; j < hitArea.flagAreaCoordinates.Length; j++)
                {
                    // this line gets the coordinates info from the coordinate array and draw it to black
                    try
                    {
                        mapBitMap.SetPixel(hitArea.flagAreaCoordinates[i, j].getX(), hitArea.flagAreaCoordinates[i, j].getX(), Color.Black);
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Error while drawing the picture");
                    }
                    
                }
                
            }
        }

        private void AttackText_TextChanged(object sender, EventArgs e)
        {
            if(AttackText.Text != "Select Location to Attack!")
            {
                AttackButton.Enabled = true;
            }
            
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            if(client != null)
            {
                byte[] message = Encoding.ASCII.GetBytes("bye");
                client.Send(message);
                ConnectButton.Enabled = true;
                ListenButton.Enabled = true;
            }
            else
            {
                MessageBox.Show("Client is null");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            byte[] message = Encoding.ASCII.GetBytes("bye");
            client.Send(message);
        }
    }
}
