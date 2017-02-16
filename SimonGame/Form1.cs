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
        public static List<string> pattern = new List<string>();

        public Form1()
        {
            //immediately opens main screen
            InitializeComponent();
            MainScreen ms = new MainScreen();
            this.Controls.Add(ms);
        }
    }
}
