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
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }
        // up down 1 
        public int GetNumber1()
        {
            return (int)numericUpDown1.Value;
        }

        public void SetNumber1(int number)
        {
            numericUpDown1.Value = number;
        }

        // up down 2 
        public int GetNumber2()
        {
            return (int)numericUpDown2.Value;
        }

        public void SetNumber2(int number)
        {
            numericUpDown2.Value = number;
        }

        // up down 3 
        public int GetNumber3()
        {
            return (int)numericUpDown3.Value;
        }

        public void SetNumber3(int number)
        {
            numericUpDown3.Value = number;
        }
    }
}
