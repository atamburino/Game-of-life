using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GOLStartUp
{
    // CELL COUNT FOR APP SET TO 30 BY 30 FOR UNIVERSE AND SCRATCHPAD 
    public partial class Form1 : Form
    {
        // The universe array
        bool[,] universe = new bool[30, 30];
        bool[,] scratchPad = new bool[30, 30];

        // Drawing colors
        Color gridColor = Color.Black;
        Color cellColor = Color.Gray;

        // The Timer class
        Timer timer = new Timer();

        // Generation count
        int generations = 0;

        // Seed 
        int seed = 0;

        public Form1()
        {
            InitializeComponent();

            // Setup the timer
            timer.Interval = 100; // milliseconds
            timer.Tick += Timer_Tick;
            timer.Enabled = false; // start timer running
        }

        // Calculate the next generation of cells
        private void NextGeneration()
        {
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int count = CountNeighborsToroidal(x, y);
                    scratchPad[x, y] = false;

                    // Apply the rules 
                    if (count < 2 && universe[x, y] == true)
                    {
                        scratchPad[x, y] = false;
                    }
                    else if (count > 3 && universe[x, y] == true)
                    {
                        scratchPad[x, y] = false;
                    }
                    else if (universe[x, y] == true && count == 2 || count == 3)
                    {
                        scratchPad[x, y] = true;
                    }

                    if (universe[x, y] == false && count == 3)
                    {
                        scratchPad[x, y] = true;
                    }
                    // Turn on/off in the scratchPad

                }
            }
            // Increment generation count
            generations++;

            // Copy everything from scratchPad to the universe 
            // Swap them...
            bool[,] temp = universe;
            universe = scratchPad;
            scratchPad = temp;


            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + generations.ToString();
            graphicsPanel1.Invalidate();
        }

        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            NextGeneration();
        }
        // FORM PANEL BASE CODE 
        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)

        {
            // FLOATS !!!
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(gridColor, 1);

            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(cellColor);

            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // RectangleF used for floats
                    // A rectangle to represent each cell in pixels
                    Rectangle cellRect = Rectangle.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive
                    if (universe[x, y] == true)
                    {
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    // Outline the cell with a pen
                    e.Graphics.DrawRectangle(gridPen, cellRect.X, cellRect.Y, cellRect.Width, cellRect.Height);


                }
            }

            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
        }
        // CODE FOR TRACKING CLICKING ON THE APP FOR UNIVERSE 
        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                int cellWidth = graphicsPanel1.ClientSize.Width / universe.GetLength(0);
                int cellHeight = graphicsPanel1.ClientSize.Height / universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = e.X / cellWidth;
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = e.Y / cellHeight;

                // Toggle the cell's state
                universe[x, y] = !universe[x, y];

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private int CountNeighborsToroidal(int x, int y)
        {
            int count = 0;
            int xLen = universe.GetLength(0);
            int yLen = universe.GetLength(1);

            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    int xCheck = x + xOffset;
                    int yCheck = y + yOffset;

                    // if xOffset and yOffset are both equal to 0 then continue
                    if (xOffset == 0 && yOffset == 0)
                    {
                        continue;
                    }
                    // if xCheck is less than 0 then set to xLen - 1
                    if (xCheck < 0)
                    {
                        xCheck = xLen - 1;
                    }
                    // if yCheck is less than 0 then set to yLen - 1
                    if (yCheck < 0)
                    {
                        yCheck = yLen - 1;
                    }
                    // if xCheck is greater than or equal too xLen then set to 0
                    if (xCheck >= xLen)
                    {
                        xCheck = 0;
                    }
                    // if yCheck is greater than or equal too yLen then set to 0
                    if (yCheck >= yLen)
                    {
                        yCheck = 0;
                    }


                    if (universe[xCheck, yCheck] == true)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        //////////////////////////////////// CLICK EVENTS ////////////////////////// CLICK EVENTS //////////////////////////// CLICK EVENTS //////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////// Everything Below Powers The Click Events ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        ///// Tool Strip Main Drop Down ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // EXIT MENU STRIP 
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        // START MENU STRIP
        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }
        // NEW UNIVERSE MENU 
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    universe[x, y] = false;
                }
            }
            graphicsPanel1.Invalidate();
        }


        ///// TOOL STRIP BUTTONS //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // START BUTTON 
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }
        // STOP BUTTON 
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }
        // NEW UNIVERSE MENU BUTTON 
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            NextGeneration();
            graphicsPanel1.Invalidate();
        }


        ///// MODEL MENU /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // seed
        private void seedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelDialog dlg = new ModelDialog();
            dlg.SetNumber(seed);
            if (DialogResult.OK == dlg.ShowDialog())
            {
                seed = dlg.GetNumber();
                Random rand = new Random(seed);

                for (int y = 0; y < universe.GetLength(1); y++)
                {
                    for (int x = 0; x < universe.GetLength(0); x++)
                    {
                        int seed = rand.Next(0, 3);
                        if (seed == 0)
                        {
                            universe[x, y] = true;
                        }
                        else
                        {
                            universe[x, y] = false;
                        }
                    }
                }
            }
            graphicsPanel1.Invalidate();
        }
        // time
        private void timeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    int seed = rand.Next(0, 3);
                    if (seed == 0)
                    {
                        universe[x, y] = true;
                    }
                    else
                    {
                        universe[x, y] = false;
                    }
                    graphicsPanel1.Invalidate();
                }
            }
        }
        //Settings//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        // Options 
        private void opttionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options dwg = new Options();
            
            if (dwg.ShowDialog(this) == DialogResult.OK)
            {
                
                
            }
            else
            {
                
            }
            dwg.Dispose();
        }
        // Forbackground Color
        private void backColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (colorDialogBack.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.FormBackground = colorDialogBack.Color;
                this.BackColor = colorDialogBack.Color;
            }
        }
        // Background color 
        private void backColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        // Cell Color
        private void cellColorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        // Grid Color
        private void gridColorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        // MISS CLICK DEAD ARRAY
       private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
       {

        }
    }
}