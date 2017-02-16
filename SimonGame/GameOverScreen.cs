using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimonGame
{
    public partial class GameOverScreen : UserControl
    {
        public GameOverScreen()
        {
            InitializeComponent();

            //displays user's final score
            scoreLabel.Text += Form1.pattern.Count() - 1;
            Refresh();
        }


        private void GameOverScreen_Load(object sender, EventArgs e)
        {
            scoreLabel.Text += Form1.pattern.Count();
            Refresh();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            //closes screen and makes new main screen appear
            Form f = this.FindForm();
            f.Controls.Remove(this);

            MainScreen ms = new MainScreen();
            f.Controls.Add(ms);
        }
    }
}
