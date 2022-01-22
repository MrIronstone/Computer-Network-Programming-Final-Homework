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
        private bool isWin = false;

        /// <summary>
        /// This function sews flag to given coordinates if it's possible         
        /// </summary>
        /// <param name="x"> X Coordinate </param>
        /// <param name="y"> Y Coordinate </param>
        private void SewFlag(int x, int y)
        {
            // Player only can sew flag if he/she still have flag to sew
            // If currently sewed flag number is lower than allowed one
            if (currentFlagNumber < maxFlagNumberPerPlayer)
            {
                // Create new flag
                Flag newFlag = new Flag(x, y);
                // increase current flag
                currentFlagNumber += 1;
                // and assign it to apğroriate space
                flags[currentFlagNumber - 1] = newFlag;


                // Logs the sewing flags to the ResultsTextBox
                AddToResults(String.Format("New Flag has been sewed. It's coordinates are: X={0}, Y={1}", x, y));

                ListBoxOfSewedFlags.Items.Add(newFlag.ToString());
                FlagsLeftToSewTextBox.Text = (maxFlagNumberPerPlayer - currentFlagNumber).ToString();

                
                // if sewed flag number, in other words current
                // flag number reaches to maximum allowed flag per player,
                // ready button needs to be enabled
                if ((currentFlagNumber == maxFlagNumberPerPlayer) && isYourTurn)
                {
                    ReadyButton.Enabled = true;
                }
            }
        }

        void ButtonListenOnClick(object obj, EventArgs ea)
        {
            try
            {
                ListenButton.Enabled = false;
                ConnectButton.Enabled = false;

                // results.Items.Add("Listening for a client...");
                AddToResults("Listening for a client...");

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
                // results.Items.Add("Error on listen on click button");
                AddToResults("Someone is already listening the port. Try again later");

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

                // results.Items.Add("Connection from: " + client.RemoteEndPoint.ToString());
                AddToResults("Connection from: " + client.RemoteEndPoint.ToString());

                receiver = new Thread(new ThreadStart(ReceiveData));
                receiver.Start();

                // active the appropriate button after connection has been accepted
                DisconnectButton.Enabled = true;
                isYourTurn = true;
                
                if(currentFlagNumber == 5)
                    ReadyButton.Enabled = true;

                TurnLabel.Text = "Your Turn!";
            }
            catch (Exception)
            {

                // results.Items.Add("Error accepting the connection");
                AddToResults("Error accepting the connection");
            }
            
        }

        void Connected(IAsyncResult iar)
        {
            try
            {
                client.EndConnect(iar);

                // results.Items.Add("Connected to: " + client.RemoteEndPoint.ToString());
                AddToResults("Connected to: " + client.RemoteEndPoint.ToString());

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
                // results.Items.Add("Error on connected");
                AddToResults("There is no such a server to connect");
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

                // results.Items.Add("Error on sending the data");
                AddToResults("Error on sending the data");
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
                    if (stringData == "bye" || recv == 0)
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

                    // results.Items.Add(stringData);
                    AddToResults(stringData);
                }

                EnemyDisconnected();
                return;
            }
            catch (Exception)
            {

                // results.Items.Add("Error on receiving the data");
                AddToResults("Error on receiving the data");
            }
            
        }

        public Form1()
        {
            InitializeComponent();

            FlagsLeftToSewTextBox.Text = (maxFlagNumberPerPlayer - currentFlagNumber).ToString();
            // some buttons need to be disabled when started
            ReadyButton.Enabled = false;
            AttackButton.Enabled = false;
            AttackText.Enabled = false;
            DisconnectButton.Enabled = false;
            TurnLabel.Text = "Game hasn't started yet!";

            // to make localhost default
            IpAdressOfEndPointTextBox.Text = "127.0.0.1";

            // to write client's device's local network ip adress to the textbox
            localIpAdressTextBox.Text = 
                Dns.GetHostEntry(Dns.GetHostName())
                .AddressList
                .First(x => x.AddressFamily == System.Net.Sockets
                .AddressFamily.InterNetwork)
                .ToString();

            // this is to be able to use more than one visual studio instance
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        void ButtonConnectOnClick(object obj, EventArgs ea)
        {
            // results.Items.Add("Connecting...");
            AddToResults("Connecting...");

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

        /// <summary>
        /// This function handles the click on the map, 
        /// if it's sew flag phase it sews flags and if 
        /// it's attack phase it select atack position
        /// </summary>
        private void ClickOnMap(object sender, MouseEventArgs e)
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
                AddToResults("Error on map picture click");
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

        /// <summary>
        /// Sends the attack locations to enemy
        /// </summary>
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


        /// <summary>
        /// Skips turn and notifies the the enemy about it
        /// </summary>
        void SkipTurn()
        {
            try
            {
                byte[] message = Encoding.ASCII.GetBytes("Your Turn!");
                client.BeginSend(message, 0, message.Length, 0,
                new AsyncCallback(SendData), client);

                // results.Items.Add("Enemy Turn");
                AddToResults("Enemy Turn");

                TurnLabel.Text = "Enemy Turn";
            }
            catch (Exception)
            {
                // results.Items.Add("Error on skipping the turn");
                AddToResults("Error on skipping the turn");
            }
        }

        /// <summary>
        /// After receiving enemy's attack locations, this function applies it on local
        /// </summary>
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
            AddToResults("You clicked to Disconnect Button");
            Disconnect();
        }

        private void ClearResultsButton_Click(object sender, EventArgs e)
        {
            results.Items.Clear();  
        }


        /// <summary>
        /// Disconnects from the game and make the game fresh like it has just started
        /// </summary>
        private void Disconnect()
        {

            string stringData = "bye";
            byte[] message = Encoding.ASCII.GetBytes(stringData);
            if(client != null)
                client.Send(message);

            AddToResults("Connection stopped");
            AddToResults("YOU LOST THE GAME!");

            ResetGame();

        }


        /// <summary>
        /// After enemy disconnect notification, make the game like it hasn't started yet
        /// </summary>
        private void EnemyDisconnected()
        {
            // results.Items.Add("Connection stopped");
            AddToResults("Enemy lost connection. Connection stopped");

            if (isWin || currentFlagNumber > 0)
            {
                // if enemy disconnect or if enemy got captured, enemy will send bye message
                // so in both scenerio, remaining one will win the game

                // results.Items.Add("YOU WON! CONGRATULATIONS");
                AddToResults("YOU WON! CONGRATULATIONS");
            }

            ResetGame();

            CloseSockets();

        }

        /// <summary>
        /// Closes the sockets if there is open one
        /// </summary>
        private void CloseSockets()
        {
            if (newsock != null)
                newsock.Close();
            if (client != null)
                client.Close();
        }

        /// <summary>
        /// Resets the game like it's just started
        /// </summary>
        private void ResetGame()
        {
            // to enable the buttons
            ReadyButton.Enabled = false;
            ListenButton.Enabled = true;
            ConnectButton.Enabled = true;
            IpAdressOfEndPointTextBox.ReadOnly = false;
            AttackText.Clear();
            AttackText.Enabled = false;

            // to disable the button that can close the connection
            DisconnectButton.Enabled = false;
            AttackButton.Enabled = false;

            // to make the game like it started
            ListBoxOfSewedFlags.Items.Clear();
            FlagsLeftToSewTextBox.Text = (maxFlagNumberPerPlayer - currentFlagNumber).ToString();

            SewFlagPhase = true;
            AttackPhase = false;
            currentFlagNumber = 0;
            isYourTurn = false;
            TurnLabel.Text = "Game hasn't started yet!";
        }

        /// <summary>
        /// Simulates losing the game by disconnecting from game
        /// </summary>
        private void LoseGame()
        {
            // the function that will be used on the state of losing the game
            // appropriate message will be written in the result box and disconnect
            Disconnect();
        }

        /// <summary>
        /// Adds the parameter string into Results TestBox and scrolls down
        /// </summary>
        private void AddToResults(string str)
        {
            int rowCount = results.Items.Count + 1;
            results.Items.Add(rowCount.ToString() + ". " + str);
            results.SelectedIndex = results.Items.Count - 1;
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Disconnect();
            }
            catch (Exception)
            {
                // process ends, nothings is neccessary
            }
        }

        private void ListBoxOfSewedFlags_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            RemoveFlag();
        }

        /// <summary>
        /// This function removes the selected sewed flag from sewed flags.
        /// </summary>
        private void RemoveFlag()
        {
            // Removing flags only available on sewing flags phase
            if (SewFlagPhase == true && AttackPhase == false)
            {
                //  If selected item is not null, if null which means nothing has selected, do nothing
                if (ListBoxOfSewedFlags.SelectedItem != null)
                {
                    // Second if check for security
                    if (ListBoxOfSewedFlags.SelectedItem.ToString().Length != 0)
                    {
                        // After double click, message box pops up. If user selectes yes, this if's block works
                        if (MessageBox.Show("Are you sure you want to remove the flag at " +
                                            ListBoxOfSewedFlags.SelectedItem.ToString() + "?", "Delete"
                                            + ListBoxOfSewedFlags.SelectedItem.ToString(),
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Information)
                            == DialogResult.Yes
                        )
                        {
                            // iosi is abbravation of "index of selected item"
                            int iosi = ListBoxOfSewedFlags.Items.IndexOf(ListBoxOfSewedFlags.SelectedItem);

                            // we remove the selected flag from listbox
                            ListBoxOfSewedFlags.Items.Remove(ListBoxOfSewedFlags.SelectedItem);

                            // we also need to make null the selected flag from array
                            flags[iosi] = null;

                            // this for loop is to syncronize flags with listbox
                            // if there is a gap between flags, this for remove gaps by swiping
                            // the reason why this for works until "flags.Count()-1" is we swipe selected index with above it
                            for (int i = iosi; i < flags.Count() - 1; i++)
                            {
                                flags[i] = flags[i + 1];
                                flags[i + 1] = null;
                            }

                            // we have remove the currently sewed flags counter
                            currentFlagNumber--;

                            // we have to update the label
                            FlagsLeftToSewTextBox.Text = (maxFlagNumberPerPlayer - currentFlagNumber).ToString();

                            // and we have to disable the ready button in case of sending ready message before planting 5 of the flags
                            ReadyButton.Enabled = false;
                        }
                    }
                }

            }
            
        }
    }
}
