using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class blockUI : Form
    {
        private Timer ticker;
        private int ticks;

        private Size gridSize = new Size(10, 10);
        public blockUI()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#101010");
            this.Size = new Size(750, 750);

            // TIMER
            ticker = new Timer();
            ticker.Tick += new EventHandler(tick);
            ticker.Interval = 10;
            ticker.Start();

            // PAINT
            this.Paint += new PaintEventHandler(onPaint);
            DoubleBuffered = true;
        }

        private void onPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            drawGrid(g, gridSize, new Size(600, 600), new Point(ClientRectangle.Size.Width/2, ClientRectangle.Size.Height/2), Color.AntiqueWhite);
        }

        private void drawGrid(Graphics g, Size gridSize, Size bounds, Point center, Color borderColor)
        {
            Pen pen = new Pen(borderColor);
            pen.Width = 10;
            double xInterval = (double) bounds.Width / gridSize.Width; // the distance between lines in the grid
            double yInterval = (double) bounds.Height / gridSize.Height;
            Point anchorPoint = new Point { X = center.X - bounds.Width / 2, Y = center.Y - bounds.Height / 2 }; // top left point

            // draws horizontal lines first
            for(int i = 0; i < gridSize.Height + 1; i++)
            {
                int y = anchorPoint.Y + (int)(yInterval * i);
                Point p1 = new Point(anchorPoint.X, y);
                Point p2 = new Point(anchorPoint.X + bounds.Width, y);

                g.DrawLine(pen, p1, p2);
            }

            // then vertical lines
            // an additional line is added so that the grid has a right and bottom border
            for(int i = 0; i < gridSize.Width + 1; i++)
            {
                int x = anchorPoint.X + (int)(xInterval * i);
                Point p1 = new Point(x, anchorPoint.Y);
                Point p2 = new Point(x, anchorPoint.Y + bounds.Height);

                g.DrawLine(pen, p1, p2);
            }

        }

        private void tick(object sender, EventArgs e)
        {
            ticks++;
            Refresh();
        }
    }
}
