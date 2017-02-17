using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimonGame
{
    public partial class Form1 : Form
    {   
        //defines globally accesible list for the pattern order. 0 corrsesponds to green, 1 to red, 2 to blue, and 3 to yellow.
        public static List<int> pattern = new List<int>();

        public Form1()
        {
            //immediately opens main control screen
            InitializeComponent();
            MainScreen ms = new MainScreen();
            this.Controls.Add(ms);
        }
    }
}
