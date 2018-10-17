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
        private Point center { get { return new Point(ClientRectangle.Size.Width / 2, ClientRectangle.Size.Height / 2); } }

        private Size gridSize = new Size(10, 10);
        public blockUI()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#101010");
            this.Size = new Size(500, 800);

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
            draw(g);
        }

        private void draw(Graphics g)
        {
            // distribute space out
            double[] spaceAllocation = new double[] { 0.2, 0.6, 0.2 };
            int infoBarHeight = (int)(spaceAllocation[0] * ClientRectangle.Height);
            int gridHeight = (int)(spaceAllocation[1] * ClientRectangle.Height);
            int interactBarHeight = (int)(spaceAllocation[2] * ClientRectangle.Height);

            // creating images to compartmentalize different areas of the form. 
            int w = ClientRectangle.Width;
            Bitmap imgInfoBar = assembleInfoBar(new Bitmap(w, infoBarHeight));
            Bitmap imgGrid = assemgleGrid(new Bitmap(w, gridHeight));
            Bitmap imgInteractBar = assembleInteractBar(new Bitmap(w, interactBarHeight));

            // info bar
            
            // grid
            Size gridBounds = new Size((int) (ClientRectangle.Width * 0.9), (int) (ClientRectangle.Width * 0.9));
            drawGrid(g, gridBounds, center, ColorTranslator.FromHtml("#232323"));
        }

        private Bitmap assembleInfoBar(Bitmap img)
        {
            return null;
        }

        private Bitmap assembleGrid(Bitmap img)
        {
            return null;
        }

        private Bitmap assembleInteractBar(Bitmap img)
        {
            return null;
        }

        private void drawGrid(Graphics g, Size gridSize, Size bounds, Point anchorPoint, Color borderColor)
        {
            Pen pen = new Pen(borderColor);
            pen.Width = 5;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            double xInterval = (double)bounds.Width / gridSize.Width; // the distance between lines in the grid
            double yInterval = (double)bounds.Height / gridSize.Height;

            // draws horizontal lines first
            for (int i = 0; i < gridSize.Height + 1; i++)
            {
                int y = anchorPoint.Y + (int)(yInterval * i);
                Point p1 = new Point(anchorPoint.X, y);
                Point p2 = new Point(anchorPoint.X + bounds.Width, y);

                g.DrawLine(pen, p1, p2);
            }

            // then vertical lines
            // an additional line is added so that the grid has a right and bottom border
            for (int i = 0; i < gridSize.Width + 1; i++)
            {
                int x = anchorPoint.X + (int)(xInterval * i);
                Point p1 = new Point(x, anchorPoint.Y);
                Point p2 = new Point(x, anchorPoint.Y + bounds.Height);

                g.DrawLine(pen, p1, p2);
            }
        }
        private void drawGrid(Graphics g, Size bounds, Point center, Color borderColor)
        {
            drawGrid(g, gridSize, bounds, new Point { X = center.X - bounds.Width / 2, Y = center.Y - bounds.Height / 2 }, borderColor);

        }

        private void tick(object sender, EventArgs e)
        {
            ticks++;
            lblTest.Text = PointToClient(MousePosition).ToString() + " " + ClientRectangle.Size.ToString();
            Refresh();
        }
    }
}
