
using System;
using System.ComponentModel;
using System.Net;
using System.Windows;


namespace A05_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private const string kNewGameProtocol = "NEWGAME:";
        private const string kKillServerProtocol = "SHUTDOWN:";
        private string guid;
        private int minNum;
        private int maxNum;

        public MainWindow()
        {
            InitializeComponent();
        }

        /*
        * METHOD : Button_Click
        * DESCRIPTION : This method will validate the IP and Port number the user has chosen
        * to connect to the server. It will create a new game after the validation has been 
        * passed. 
        */
        public void Button_Click (object sender, RoutedEventArgs e)
        {
            

            if (IPAddress.TryParse(IPBox.Text, out _)) 
            {
                string ipText = IPBox.Text;

                if (int.TryParse(PortBox.Text, out _))
                {

                    NewGame();
                    ConnectButton.IsEnabled = false;

                }
                else
                {
                    outputBox.AppendText("Port needs to be an integer.\n");
                    outputBox.ScrollToEnd();
                }
            }
            else
            {
                outputBox.AppendText("Invalid IP\n");
                outputBox.ScrollToEnd();
            }

        }
        /*
        * METHOD : DisconnectButton_Click
        * DESCRIPTION : This method will disconnect from the server and prompt the user for confirmation
        * if they'd like to disconnect or not. 
        */
        public void DisconnectButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you would like to end this game?", "Disconnect", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    Communication.ConnectClient("127.0.0.1", 13000, kKillServerProtocol + guid);
                    ConnectButton.IsEnabled = true;
                    break;
                case MessageBoxResult.No:
                    break;

            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        /*
        * METHOD : GuessButton_Click
        * DESCRIPTION : This method will allow the user to make guesses and will send the guess to the
        * server. It will validate the users guess to make sure it's an integer and a valid integer. If 
        * not, the user will receive a notification to select a proper number.
        */
        private void GuessButton_Click(object sender, RoutedEventArgs e)
        {
            var ipText = IPBox.Text;
            var portText = PortBox.Text;
            var parsedPort = Convert.ToInt32(portText);// Need validation for this later to make sure it's an int
            var guessingNum = guessBox.Text;

            // Validates that the number is within range. 
            if (int.TryParse(guessingNum, out _) == false || Convert.ToInt32(guessingNum) < minNum || Convert.ToInt32(guessingNum) > maxNum)
            {
                MessageBoxResult result = MessageBox.Show("Please enter a number within the proper range.", "Invalid Number", MessageBoxButton.OK);
                return;
            }

            var serverResponse = Communication.ConnectClient(ipText, parsedPort, guid + ":" + guessingNum);

            if (serverResponse == "fail")
            {
                outputBox.AppendText("Server has lost connection, please try again.");
                outputBox.ScrollToEnd();
            }
            else
            {
                var parsedArray = serverResponse.Split(':');
                var winOrNot = parsedArray[1];
                var numPair = parsedArray[2];
                var parsedNum = numPair.Split(',');
                minNum = Convert.ToInt32(parsedNum[0]);
                maxNum = Convert.ToInt32(parsedNum[1]);


                if (winOrNot == "win")
                {
                    MessageBoxResult result = MessageBox.Show("You won! Would you like to play again?", "WINNER!", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes: //TODO: Start new game 
                            NewGame();
                            break;
                        case MessageBoxResult.No: //TODO: Close server 
                            Communication.ConnectClient(ipText, Convert.ToInt32(portText), kKillServerProtocol);
                            outputBox.AppendText("\nGoodbye");
                            outputBox.ScrollToEnd();
                            break;
                    }
                }
                else
                {
                    outputBox.AppendText("\nPlease guess a number between " + minNum + " " + "and " + maxNum + ".\n");
                    outputBox.ScrollToEnd();
                }

            }

        }

        /*
        * METHOD : NewGame()
        * DESCRIPTION : This method will allow the user to start a new game. It will connect to the server
        * and parse the response back from the server to display a message with a min and max number. 
        */
        public void NewGame()
        {
            string ipText = IPBox.Text;
            int parsedPort = Convert.ToInt32(PortBox.Text);
            var serverResponse = Communication.ConnectClient(ipText, parsedPort, kNewGameProtocol);
    
            var parsedArray = serverResponse.Split(':');
            var numPair = parsedArray[2];
            guid = parsedArray[0];
            var usersName = NameBox.Text;

            var parsedNum = numPair.Split(',');
            minNum = Convert.ToInt32(parsedNum[0]);
            maxNum = Convert.ToInt32(parsedNum[1]);

            outputBox.AppendText("\nHello, " + usersName + " please guess a number between " + minNum + " " + "and " + maxNum + ".\n");
            outputBox.ScrollToEnd();
        }

   

    }



}
