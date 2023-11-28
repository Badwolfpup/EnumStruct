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
        Panel panel = new Panel();
        Panel[,] panelArray = new Panel[10, 10];
        Direction direction;
        Position oldPosition = new Position(0,0);
        Position newPosition;

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
                    if (oldPosition.Y != 0) newPosition.Y -= 1;
                    break;
                case Direction.East:
                    if (oldPosition.X < panelArray.Length - 1) newPosition.X += 1;
                    break;
                case Direction.South:
                    if (oldPosition.Y < panelArray.Length - 1) newPosition.Y += 1;
                    break;
                case Direction.West:
                    if (oldPosition.X != 0) newPosition.X -= 1;
                    break;
            }
            panelArray[oldPosition.X, oldPosition.Y].BackColor = Color.Gray;
            panelArray[newPosition.X, newPosition.Y].BackColor = Color.Green;
            oldPosition = newPosition;
        }
    }
}