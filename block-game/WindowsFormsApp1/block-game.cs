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
        private double windowRatio = 5.0 / 8.0;

        /// <summary>
        /// Number of squares in x and y axis
        /// </summary>
        private Size gridSize = new Size(10, 10);

        public blockUI()
        {
            InitializeComponent();
            this.BackColor = ColorTranslator.FromHtml("#101010");
            this.Size = new Size((int) (800 * windowRatio), 800);

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
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            draw(g);
        }

        private void draw(Graphics g)
        {
            // distribute space out
            int renderResolution = 2000; // draw everything as if this was the window height (of a 8:5 ratio screen), then scale down appropriately
            double[] spaceAllocation = new double[] { 0.15, 0.6, 0.25 };
            int infoBarHeight = (int)(spaceAllocation[0] * renderResolution);
            int gridHeight = (int)(spaceAllocation[1] * renderResolution);
            int interactBarHeight = (int)(spaceAllocation[2] * renderResolution);

            // creating images to compartmentalize different areas of the form. 
            int w = (int)(renderResolution * windowRatio);
            Bitmap imgInfoBar = assembleInfoBar(new Bitmap(w, infoBarHeight));
            Bitmap imgGrid = assembleGrid(new Bitmap(w, gridHeight));
            Bitmap imgInteractBar = assembleInteractBar(new Bitmap(w, interactBarHeight));

            // draw everything to the form
            Point p = new Point(0, 0);
            Size s = new Size(ClientRectangle.Width, (int)(ClientRectangle.Height * spaceAllocation[0]));
            Rectangle r = new Rectangle(p, s);
            g.DrawImage(imgInfoBar, r);

            p.Y = (int)(ClientRectangle.Height * spaceAllocation[0]) + 1;
            s.Height = (int)(ClientRectangle.Height * spaceAllocation[1]);
            r = new Rectangle(p, s);
            g.DrawImage(imgGrid, r);

            p.Y = (int)(ClientRectangle.Height * (spaceAllocation[0] + spaceAllocation[1])) + 1;
            s.Height = (int)(ClientRectangle.Height * spaceAllocation[2]);
            r = new Rectangle(p, s);
            g.DrawImage(imgInteractBar, r);

        }

        private Bitmap assembleInfoBar(Bitmap img)
        {
            Graphics g = Graphics.FromImage(img);
            return img;
        }

        private Bitmap assembleGrid(Bitmap img)
        {
            Graphics g = Graphics.FromImage(img);
            int h;
            if (img.Width > img.Height)
                h = img.Height;
            else
                h = img.Width;

            Size gridBounds = new Size((int)(h * 0.9), (int)(h * 0.9));
            drawGrid(g, gridBounds, new Point(img.Width / 2, img.Height / 2), ColorTranslator.FromHtml("#232323"));
            return img;
        }

        private Bitmap assembleInteractBar(Bitmap img)
        {
            return img;
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
