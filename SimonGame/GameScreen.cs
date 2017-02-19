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
            
            //initializes soundplayer array (can't be done in declaration)
            soundPlayers.Add(new SoundPlayer(Properties.Resources.green));
            soundPlayers.Add(new SoundPlayer(Properties.Resources.red));
            soundPlayers.Add(new SoundPlayer(Properties.Resources.blue));
            soundPlayers.Add(new SoundPlayer(Properties.Resources.yellow));

            //initializes button array (can't be done in declaration)
            buttons.Add(greenButton);
            buttons.Add(redButton);
            buttons.Add(blueButton);
            buttons.Add(yellowButton);
        }
        //defines lists for buttons, sounds, and colors
        List<Button> buttons = new List<Button>();
        List<SoundPlayer> soundPlayers = new List<SoundPlayer>();
        List<Color> colors = new List<Color>(new Color[] { Color.LightGreen, Color.FromArgb(255, 100, 100), Color.LightBlue, Color.LightYellow, Color.Green, Color.Red, Color.Blue, Color.Yellow });

        //tracks whether it is the player's turn
        bool isPlayerTurn = false;

        //tracks player guess number
        int guess = 0;

        //function for the computer's turn
        private void ComputerTurn()
        {           
            //randomly decides which color to add
            Random rand = new Random();
            Form1.pattern.Add(rand.Next(0, 4));

            //updates round label
            roundLabel.Text = "Round " + Form1.pattern.Count();

            for (int i = 0; i < Form1.pattern.Count(); i++)
            {
                //calls method to light appropriate button and pauses for 1 s
                handleButton(Form1.pattern[i]);
                
                //clears colors
                handleButton(4);
            }

           //resets guess number and resumes player turn
            isPlayerTurn = true;
            guess = 0;
        }

        private void GameScreen_Load(object sender, EventArgs e)
        {
            //defines circular path for button to fill
            GraphicsPath greenPath = new GraphicsPath();
            greenPath.AddEllipse(5, 5, 190, 190);

            //defines the region for the button component
            Region buttonRegion = new Region(greenPath);
            greenButton.Region = buttonRegion;

            //defines matrix to rotate button region
            Matrix transformMatrix = new Matrix();

            //rotates matrix and defines component regions respectively as appropriate
            rotateButtonRegion(transformMatrix, buttonRegion, redButton);
            rotateButtonRegion(transformMatrix, buttonRegion, yellowButton);
            rotateButtonRegion(transformMatrix, buttonRegion, blueButton);

            //creates the region for the blocking central ellipse
            GraphicsPath blockPath = new GraphicsPath();
            blockPath.AddEllipse(5, 5, 170, 170);
            roundLabel.Region = new Region(blockPath);

            //clears pattern, refreshes screen, and starts computer's turn
            Form1.pattern.Clear();
            Refresh();
            Thread.Sleep(1000);
            ComputerTurn();
        }

        //function to determine which button to light and light that button as well as play a specific sound
        private void handleButton(int colorIndex)
        {
            //starts by resetting all button colors. Note that "default" colors are stored at an index four higher than their "brightened" variant for easy alternation.
            for (int i = 0; i < buttons.Count(); i++)
            {
                buttons[i].BackColor = colors[i + 4];
            }

            //only attempts to  access lists if null command (4, used to clear) was not used.
            if (colorIndex != 4) {
                           
                //plays appropriate sound and brightens appropriate color
                buttons[colorIndex].BackColor = colors[colorIndex];
                soundPlayers[colorIndex].Play();
            }
            Refresh();
            Thread.Sleep(300);
        }

        //function to handle player turn
        private void handleClick(int colorIndex)
        {   
            if (isPlayerTurn == true)
            {   
                //if the button press is correct, lights button up. Otherwise, ends game.
                if (Form1.pattern[guess] == colorIndex)
                {   
                    //flashes brightened button on and off
                    isPlayerTurn = false;
                    handleButton(colorIndex);
                    handleButton(4);
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

            //pauses for 1 second before showing game oover screen
            Thread.Sleep(1000);

            //closes screen and makes new game over screen appear
            Form f = this.FindForm();
            f.Controls.Remove(this);

            GameOverScreen gos = new GameOverScreen();
            f.Controls.Add(gos);
        }

        //method to rotate the region of a simon button and redefine  its component region to fit the newly rotated region
        private void rotateButtonRegion(Matrix transformMatrix, Region buttonRegion, Button button)
        {
            transformMatrix.RotateAt(90, new PointF(50, 50));
            buttonRegion.Transform(transformMatrix);
            button.Region = buttonRegion;
        }

        //calls handleClick method with appropriate color when a button is clicked
        private void greenButton_Click(object sender, EventArgs e)
        {
            handleClick(0);
        }
        private void redButton_Click(object sender, EventArgs e)
        {
            handleClick(1);
        }
        private void blueButton_Click(object sender, EventArgs e)
        {
            handleClick(2);
        }
        private void yellowButton_Click(object sender, EventArgs e)
        {
            handleClick(3);
        }
    }
}
