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

        private bool isHost = false;
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
                results.Items.Add("Listening for a client...");
                Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                ProtocolType.Tcp);
                IPEndPoint iep = new IPEndPoint(IPAddress.Any, 9050);
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
                Thread receiver = new Thread(new ThreadStart(ReceiveData));
                receiver.Start();
                // bir kere listen'a bastıktan sonra tuşu deaktive edebilmek için
                ListenButton.Enabled = false;
                ConnectButton.Enabled = false;
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
                ListenButton.Enabled = false;
                ConnectButton.Enabled = false;
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
                        //
                    }
                    else if (stringData.StartsWith("Attack Location: "))
                    {
                        string attackLoc = stringData.Substring(stringData.IndexOf("Attack Location: "));
                        MessageBox.Show(attackLoc);
                        // Flag attackLocation = Flag()
                    }
                    results.Items.Add(stringData);
                }
                stringData = "bye";
                byte[] message = Encoding.ASCII.GetBytes(stringData);
                client.Send(message);
                client.Close();
                results.Items.Add("Connection stopped");
                ListenButton.Enabled = true;
                ConnectButton.Enabled = true;
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
            TurnLabel.Text = "Game hasn't started yet!";

            Control.CheckForIllegalCrossThreadCalls = false;
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

        private void Form1_Load(object sender, EventArgs e)
        {

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

        private void AttackButton_TextChanged(object sender, EventArgs e)
        {
            // bu sayede attack text kısmına bir şey yazıldığında attack tuşu aktive olur
            AttackButton.Enabled = true;
        }


        //private void AttackText_TextChanged(object sender, EventArgs e)
        //{
        //    AttackButton.Enabled = true;
        //}

        public void GetHitToPosition(int x, int y)
        {
            Flag hitArea = new Flag(x, y);
            int startPointXForArea = hitArea.flagAreaCoordinates.GetLength(0);
            int startPointYForArea = hitArea.flagAreaCoordinates.GetLength(1);
            for (int i = 0; i < startPointXForArea; i++)
            {
                for (int j = 0; j < startPointYForArea; j++)
                {
                    mapBitMap.SetPixel(i, j, Color.Black);
                }
            }


        }
    }
}
