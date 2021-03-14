using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOLStartUp
{
    public partial class ModelDialog : Form
    {
        public ModelDialog()
        {
            InitializeComponent();
        }
        public int GetNumber()
        {
            return (int)numericUpDown1.Value;
        }

        public void SetNumber(int number)
        {
            numericUpDown1.Value = number;
        }
        // Failed click function ////////////////////////////////////
        private void label2_Click(object sender, EventArgs e)
        {

        }
        /////////////////////////////////////////////////////////////
        
        // Random 
        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            SetNumber(rand.Next(0, 101));
        }
        // ok
        private void button2_Click(object sender, EventArgs e)
        {

        }
        // Cancel
        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
