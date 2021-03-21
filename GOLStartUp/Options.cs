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
        public int GetNumberSeconds()
        {
            return (int)numericUpDown1.Value;
        }

        public int GetNumberWidth()
        {
            return (int)numericUpDown2.Value;
        }

        public int GetNumberHeight()
        {
            return (int)numericUpDown3.Value;
        }

        public void SetNumberSeconds(int num1)
        {
            numericUpDown1.Value = num1;
        }

        public void SetNumberWidth(int num2)
        {
            numericUpDown2.Value = num2;
        }

        public void SetNumberHeight(int num3)
        {
            numericUpDown3.Value = num3;
        }
    }
}
