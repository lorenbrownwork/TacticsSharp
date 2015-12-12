using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;

namespace TacticsSharp
{
    public struct Tile
    {
        public Character character;
        public int height;
        public bool stepable;
        public bool aSpawnable; //Can place a character here at the start of a battle (Team A)
        public bool bSpawnable; //Can place a character here at the start of a battle (Team B)
    }

    class Board : Form
    {
        public int xSize, ySize;
        public Tile[,] gameBoard;

        public Board() : base()
        {
            //Board Setup
            xSize = 12;
            ySize = 12;
            gameBoard = new Tile[xSize, ySize];
            createBoard();

            //Graphics Setup
            this.TopMost = true;
            this.DoubleBuffered = true;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.Purple;
            this.TransparencyKey = Color.Purple;
        }

        public void createBoard()
        {
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    gameBoard[i, j].height = 0;
                    gameBoard[i, j].stepable = true;
                    gameBoard[i, j].aSpawnable = true;
                    gameBoard[i, j].bSpawnable = true;
                }
            }
            gameBoard[0, 0].height = 3;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen whitePen = new Pen(Color.White, 10);
            drawBoard(e, 100, 100, 5);
            this.Invalidate(); //cause repaint
        }

        private void drawBoard(PaintEventArgs e, int xOff, int yOff, int size)
        {
            for (int i = 0; i < size; i++)
            {
                drawLayer(e, 200, 200 - (i * 10), 5, i);
            }
            
        }

        private void drawLayer(PaintEventArgs e, int xOff, int yOff, int size, int layer)
        {

            for (int i = 0; i < size; i++)
            {
                drawRow(e, xOff - (i * 50), yOff + (i * 25), 5, layer, i);
            }
        }

        private void drawRow(PaintEventArgs e, int xOff, int yOff, int size, int layer, int yTile)
        {
            int x = 50 * size;
            int y = 0;

            for (int i = 0; i < size; i++)
            {
                if (gameBoard[i, yTile].height >= layer)
                {
                    drawTile(e, x + xOff, y + yOff);
                }
                x += 50;
                y += 25;
            }
        }

        private void drawTile(PaintEventArgs e, int x, int y)
        {
            
            Pen whitePen = new Pen(Color.White, 1);
            Pen bluePen = new Pen(Color.LightBlue, 1);
 
            Point point1 = new Point(x-50, y);
            Point point2 = new Point(x, y+25);
            Point point3 = new Point(x+50, y);
            Point point4 = new Point(x, y-25);
            Point point5 = new Point(x - 50, y +10);
            Point point6 = new Point(x, y + 35);
            Point point7 = new Point(x + 50, y + 10);

            Point[] top = { point1, point2, point3, point4 };
            Point[] left = { point1, point2, point6, point5 };
            Point[] right = { point2, point3, point7, point6 };

            e.Graphics.FillPolygon(Brushes.White, top);
            e.Graphics.FillPolygon(Brushes.LightBlue, left);
            e.Graphics.FillPolygon(Brushes.LightBlue, right);
            e.Graphics.DrawPolygon(whitePen, left);
            e.Graphics.DrawPolygon(whitePen, right);
            e.Graphics.DrawPolygon(bluePen, top);

        }
    }
}
        /*private Tile[,] gameBoard;
        private int xSize, ySize;
        
        public Board ()
        {
            //drawTile();

            xSize = 12;
            ySize = 12;
        
            gameBoard = new Tile[xSize, ySize];

            //Create teh default (lame) board;
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    gameBoard[i, j].height = 0;
                    gameBoard[i, j].stepable = true;
                    gameBoard[i, j].aSpawnable = true;
                    gameBoard[i, j].bSpawnable = true;
                }
            }
        }

        //Return true if character was moved
        public bool move(Tile curr, Tile next)
        {
            if (next.character == null) //Space is empty
            {
                next.character = curr.character;
                curr.character = null; //allows the object to cleaned up by garbage collection
                return true;
            }
            return false;
        }

        

        public void drawTile()
        {
            Task mytask = Task.Run(() =>
            {
                Form1 form = new Form1();
                form.DrawIt();
                form.ShowDialog();
            });

        }
    }
}
*/