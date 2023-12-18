using System.Security.Cryptography.Xml;
using System.Text;

namespace Lektion
{
    enum Direction
    {
        North, East, South, West
    }

    public struct Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Position(int x, int y)
        {
            X = x; 
            Y = y;
        }
    }

    public partial class Form1 : Form
    {
        TableLayoutPanel tpanel = new TableLayoutPanel();
        Panel panel;
        Panel[,] panelArray = new Panel[10, 10];
        Position oldPosition = new Position(0,0);
        Position newPosition;
        Position enemyOldPosition;
        Position enemyNewPosition;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int posX = 30;
            int posY = 30;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    panel = new Panel();
                    panel.Size = new Size(30, 30);
                    panel.Location = new Point(posX, posY);
                    if (i == 0 && j == 0) panel.BackColor = Color.Green;
                    else panel.BackColor = Color.Gray;
                    tpanel.Controls.Add(panel, i, j);
                    posX += 30;
                    panelArray[i, j] = panel;
                }
                posX = 30;
                posY += 30;
            }
            Controls.Add(tpanel);
            tpanel.Dock = DockStyle.Fill;
            Random r = new Random();
            enemyOldPosition = new Position(r.Next(7, panelArray.GetLength(0) - 1), r.Next(7, panelArray.GetLength(1) - 1));
            panelArray[enemyOldPosition.X, enemyOldPosition.Y].BackColor = Color.Red;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) moveSquare(Direction.North);
            if (e.KeyCode == Keys.Right) moveSquare(Direction.East);
            if (e.KeyCode == Keys.Down) moveSquare(Direction.South);
            if (e.KeyCode == Keys.Left) moveSquare(Direction.West);
        }

        private void moveSquare (Direction d)
        {
            newPosition = oldPosition;
            switch (d)
            {
                case Direction.North:
                    if (oldPosition.Y != 0)
                    {
                        newPosition.Y -= 1;
                        break;
                    } else return;
                case Direction.East:
                    if (oldPosition.X < panelArray.GetLength(0) - 1)
                    {
                        newPosition.X += 1;
                        break;
                    } else return;
                case Direction.South:
                    if (oldPosition.Y < panelArray.GetLength(1) - 1)
                    {
                        newPosition.Y += 1;
                        break;
                    }
                    else return;
                case Direction.West:
                    if (oldPosition.X != 0)
                    {
                        newPosition.X -= 1;
                        break;
                    }
                    else return;
            }
            if (newPosition.X == enemyOldPosition.X && newPosition.Y == enemyOldPosition.Y)
            {
                MessageBox.Show("Du förlorade", "THE END", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
            
            panelArray[oldPosition.X, oldPosition.Y].BackColor = Color.Gray;
            panelArray[newPosition.X, newPosition.Y].BackColor = Color.Green;
            oldPosition = newPosition;
            while (!moveSquareEnemy()) ;
        }

        private bool moveSquareEnemy()
        {
            Random r = new Random();
            Direction d = (Direction)r.Next(4);
            enemyNewPosition = enemyOldPosition;
            switch (d) 
            {
                case Direction.North:
                    if (enemyOldPosition.Y != 0)
                    {
                        enemyNewPosition.Y -= 1;
                        break;
                    } else return false;
                case Direction.East:
                    if (enemyOldPosition.X < panelArray.GetLength(0) - 1)
                    {
                        enemyNewPosition.X += 1;
                        break;
                    }
                    else return false;
                case Direction.South:
                    if (enemyOldPosition.Y < panelArray.GetLength(1) - 1)
                    {
                        enemyNewPosition.Y += 1;
                        break;
                    }
                    else return false;
                case Direction.West:
                    if (enemyOldPosition.X != 0)
                    {
                        enemyNewPosition.X -= 1;
                        break;
                    }
                    else return false;
            }
            if (enemyNewPosition.X == oldPosition.X && enemyNewPosition.Y == oldPosition.Y)
            {
                MessageBox.Show("Du förlorade", "THE END", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }

            panelArray[enemyOldPosition.X, enemyOldPosition.Y].BackColor = Color.Gray;
            panelArray[enemyNewPosition.X, enemyNewPosition.Y].BackColor = Color.Red;
            enemyOldPosition = enemyNewPosition;

            return true;
        }

    }
}