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
        private static Socket newsock;
        private static Thread receiver;
        private static IPEndPoint iep;

        private static int maxFlagNumberPerPlayer = 5;
        public int currentFlagNumber = 0;
        private Flag[] flags = new Flag[maxFlagNumberPerPlayer];

        private bool isYourTurn = false;
        private bool AttackPhase = false;
        private bool SewFlagPhase = true;

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
                ListenButton.Enabled = false;
                ConnectButton.Enabled = false;
                results.Items.Add("Listening for a client...");
                ConnectButton.Enabled = false;
                newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp);
                iep = new IPEndPoint(IPAddress.Any, 9050);
                newsock.Bind(iep);
                newsock.Listen(5);
                newsock.BeginAccept(new AsyncCallback(AcceptConn), newsock);
                
            }
            catch (Exception)
            {
                results.Items.Add("Error on listen on click button");
                // if error occurs enable back the connect button
                ListenButton.Enabled = true;
                ConnectButton.Enabled = true;
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

                // active the appropriate button after connection has been accepted
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
                IpAdressOfEndPointTextBox.ReadOnly = true;
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

                    // if received string is "bye", it means it's our turn to disconnect and close
                    // so we end the while loop and run the remaning codes outside the while loop
                    if (stringData == "bye")
                        break;

                    // if received string is "Your Turn!", it means it's our turn
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
                        string attackLoc = stringData.Split(':')[1].Substring(1);
                            
                        int x = int.Parse(attackLoc.Split(',')[0].Split('=')[1]);
                        int y = int.Parse(attackLoc.Split(',')[1].Split('=')[1]);

                        stringData = "Attack Received! " + stringData;

                        GetHitToPosition(x, y);
                        
                    }
                    results.Items.Add(stringData);
                }

                if(currentFlagNumber > 0)
                {
                    // if enemy disconnect or if enemy got captured, enemy will send bye message
                    // so in both scenerio, remaining one will win the game
                    results.Items.Add("YOU WON! CONGRATULATIONS");
                }
                Disconnect();
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
            AttackText.Enabled = falseA
            DisconnectButton.Enabled = false;
            TurnLabel.Text = "Game hasn't started yet!";
            IpAdressOfEndPointTextBox.Text = "127.0.0.1";
            localIpAdressTextBox.Text = 
                Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .First(x => x.AddressFamily == System.Net.Sockets
                .AddressFamily.InterNetwork)
                .ToString();

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        void ButtonConnectOnClick(object obj, EventArgs ea)
        {
            results.Items.Add("Connecting...");
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
            ProtocolType.Tcp);
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IpAdressOfEndPointTextBox.Text), 9050);
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

        void SkipTurn()
        {
            try
            {
                byte[] message = Encoding.ASCII.GetBytes("Your Turn!");
                client.BeginSend(message, 0, message.Length, 0,
                new AsyncCallback(SendData), client);
                results.Items.Add("Enemy Turn");
                TurnLabel.Text = "Enemy Turn";
            }
            catch (Exception)
            {
                results.Items.Add("Error on skipping the turn");
            }
        }

        public void GetHitToPosition(int x, int y)
        {
            Flag hitArea = new Flag(x,y);
            foreach (Flag flag in flags)
            {
                // if the flag is NOT already captured
                if(!flag.isCaptured)
                {
                    // if this attack is in one my flags
                    if (flag.isThisAttackInMyArea(hitArea))
                    {
                        // this finds the spesific flag from the listBox of sewed flags and 
                        // changes its name into clearer string
                        int index = ListBoxOfSewedFlags.Items.IndexOf(flag.ToString());
                        var record = ListBoxOfSewedFlags.Items[index] = flag.ToString() + " (CAPTURED)";


                        // decrease one of the flags we have, if it reaches 0, the game will be lost
                        // this way we dont need to use another variable
                        currentFlagNumber--;
                        if(currentFlagNumber <= 0)
                        {
                            LoseGame();         
                        }

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
            Disconnect();
        }

        private void ClearResultsButton_Click(object sender, EventArgs e)
        {
            results.Items.Clear();  
        }

        private void Disconnect()
        {
            // if client is not null
            if(client.Connected)
            {
                string stringData = "bye";
                byte[] message = Encoding.ASCII.GetBytes(stringData);
                client.Send(message);
                client.Close();
            }
            results.Items.Add("Connection stopped");

            // to enable the buttons
            ListenButton.Enabled = true;
            ConnectButton.Enabled = true;
            IpAdressOfEndPointTextBox.ReadOnly = false;

            // to disable the button that can close the connection
            DisconnectButton.Enabled = false;
            AttackButton.Enabled = false;

            // to make the game like it started
            currentFlagNumber = 0;
            isYourTurn = false;
            TurnLabel.Text = "Game hasn't started yet!";



            // close the socket if it's not closed
            if (newsock != null)
            {
                newsock.Close();
            }

        }

        private void LoseGame()
        {
            // the function that will be used on the state of losing the game
            // appropriate message will be written in the result box and disconnect
            results.Items.Add("YOU LOST THE GAME!");
            Disconnect();
        }

    }
}
