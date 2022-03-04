

using System;
using System.Configuration;

namespace A05_Server
{
    public class GameEngine
    {
        public int Guess { get; set; }            
        public int Answer { get; set; }

        private int min;

        public int Min
        {
            get { return min; }
            set { min = value; }
        }

        public int Max { get; set; }

        /*METHOD : ResetGame
        * DESCRIPTION : Sets all properties to default game state
        */
        public void ResetGame()
        {
            Min = 1;
            Max = int.Parse(ConfigurationManager.AppSettings.Get("Key0"));
            Guess = 0;
            Answer = GenerateAnswer();
        }

        /*METHOD : CheckHi
        * DESCRIPTION : Used as bool to determine game state
        */
        public bool CheckHi()
        {
            return Guess > Answer;
        }

        /*METHOD : CheckRange
        * DESCRIPTION : Used to check if Guess is in range
        */
        public bool CheckRange()
        {
            return Guess < Max && Guess > Min;
        }

        /*METHOD : CheckLo
        * DESCRIPTION : Used as bool to determine game state
        */
        public bool CheckLo()
        {
            return Guess < Answer;
        }

        /*METHOD : WinCondition
        * DESCRIPTION : Used as bool to determine game state
        */
        public bool WinCondition()
        {
            return Guess == Answer;
        }

        /*METHOD : GenerateAnswer
        * DESCRIPTION : Uses random number to get answer for new game
        */
        public int GenerateAnswer()
        {
            Random num = new Random();

            return num.Next(Min, Max);
        }

        /*METHOD : RangeString
        * DESCRIPTION : Helper function to create MIN,MAX protocol
        */
        internal string RangeString()
        {
            return Min.ToString() + "," + Max.ToString();
        }
    }
}