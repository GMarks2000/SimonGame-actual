using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using System.Drawing.Drawing2D;

namespace SimonGame
{
    public partial class GameScreen : UserControl
    {
        public GameScreen()
        {
            InitializeComponent();
        }
        //tracks whether it is the player's turn
        bool isPlayerTurn = false;

        //tracks player guess number
        int guess = 0;

        //function for the computer's turn
        private void ComputerTurn()
        {   
            //randomly decides which color to add
            Random rand = new Random();
            int colorIndicator = rand.Next(0, 4);

            //adds colors to array as appropriate
            if (colorIndicator == 0) { Form1.pattern.Add("green"); }
            else if (colorIndicator == 1) { Form1.pattern.Add("red"); }
            else if (colorIndicator == 2) { Form1.pattern.Add("blue"); }
            else { Form1.pattern.Add("yellow"); }

            for (int i = 0; i < Form1.pattern.Count(); i++)
            {
                //calls method to light appropriate button and pauses for 1 s
                handleButton(Form1.pattern[i]);
                
                //clears colors
                handleButton("null");
            }
            //resets guess number and resumes player turn
            isPlayerTurn = true;
            guess = 0;
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            //clears pattern, refreshes screen, and starts computer's turn
            Form1.pattern.Clear();
            Refresh();
            Thread.Sleep(1000);
            ComputerTurn();
        }

        //function to determine which button to light and light that button as well as play a specific sound
        private void handleButton(string color)
        {
            //starts by resetting all button colors
            greenButton.BackColor = Color.Green;
            redButton.BackColor = Color.Red;
            blueButton.BackColor = Color.MediumBlue;
            yellowButton.BackColor = Color.Yellow;

            //brightens button color as appropriate
            if (color == "green") { greenButton.BackColor = Color.LightGreen; }
            if (color == "red") { redButton.BackColor = Color.FromArgb(255, 100, 100); }
            if (color == "blue") { blueButton.BackColor = Color.LightBlue; }
            if (color == "yellow") { yellowButton.BackColor = Color.LightYellow; }

            //Defines soundplayer to be used
            SoundPlayer player = null;

            //loads color-dependant sound as appropriate
            if (color == "green") { player = new SoundPlayer(Properties.Resources.green); }
            if (color == "red") { player = new SoundPlayer(Properties.Resources.red); }
            if (color == "blue") { player = new SoundPlayer(Properties.Resources.blue); }
            if (color == "yellow") { player = new SoundPlayer(Properties.Resources.yellow); }

            //plays sound if the null command (used to simply clear the buttons) was not used
            if (color != "null") { player.Play(); }

            Refresh();
            Thread.Sleep(300);
        }

        //function to handle player turn
        private void handleClick(string color)
        {   
            if (isPlayerTurn == true)
            {   
                //if the button press is correct, lights button up. Otherwise, ends game.
                if (Form1.pattern[guess] == color)
                {
                    isPlayerTurn = false;
                    handleButton(color);

                    handleButton("null");
                    isPlayerTurn = true;

                    //increments guess number
                    guess++;

                    //starts a new computer turn if the user has guessed through the entire pattern.
                    if (guess == Form1.pattern.Count())
                    {   
                        isPlayerTurn = false;
                        Thread.Sleep(500);
                        ComputerTurn();
                    }
                }
                //if the user presse the incorrect button, ends game.
                else
                {
                    gameOver();
                }
            }
        } 

        //method to end game and go to game over screen
        private void gameOver()
        {
            SoundPlayer player = new SoundPlayer(Properties.Resources.mistake);
            player.Play();

            Thread.Sleep(1000);

            //closes screen and makes new game over screen appear
            Form f = this.FindForm();
            f.Controls.Remove(this);

            GameOverScreen gos = new GameOverScreen();
            f.Controls.Add(gos);
        }

        //calls handleClick method with appropriate color when a button is clicked
        private void greenButton_Click(object sender, EventArgs e)
        {
            handleClick("green");
        }
        private void redButton_Click(object sender, EventArgs e)
        {
            handleClick("red");
        }
        private void yellowButton_Click(object sender, EventArgs e)
        {
            handleClick("yellow");
        }
        private void blueButton_Click(object sender, EventArgs e)
        {
            handleClick("blue"); 
        }
    }
}
