using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace blockgame
{
    public partial class blockUI : Form
    {
        private Timer ticker;
        private int ticks;

        private Point center { get { return new Point(ClientRectangle.Size.Width / 2, ClientRectangle.Size.Height / 2); } }
        private double windowRatio = 5.0 / 8.0;

        private Stage stage;
        public static Image block { get; } = Image.FromFile("assets\\images\\block.png");

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

            stage = new Stage(gridSize);
        }

        private void onPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            draw(g);
        }

        private int renderResolution; // draw everything as if this was the window height (of a 8:5 ratio screen), then scale down appropriately
        private void draw(Graphics g)
        {
            // distribute space out
            renderResolution = Height;
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
            int h;
            if (img.Width > img.Height)
                h = img.Height;
            else
                h = img.Width;

            Size gridBounds = new Size((int)(h * 0.9), (int)(h * 0.9));
            img = stage.drawGrid(img.Size, gridBounds, new Point(img.Width / 2, img.Height / 2), ColorTranslator.FromHtml("#232323"), (int)(0.0075 * renderResolution));
            Point corner = new Point()
            {
                X = img.Width / 2 - gridBounds.Width / 2,
                Y = img.Height / 2 - gridBounds.Height / 2
            };
            int interval = gridBounds.Width / gridSize.Width;
            int margin = (int)(0.0075 * renderResolution);

            Graphics g = Graphics.FromImage(img);
            g.DrawImage(new BlockGroup().draw(interval - margin, interval), corner);

            return img;
        }

        private Bitmap assembleInteractBar(Bitmap img)
        {
            return img;
        }

        private void tick(object sender, EventArgs e)
        {
            ticks++;
            lblTest.Text = PointToClient(MousePosition).ToString() + " " + ClientRectangle.Size.ToString();
            Refresh();
        }
    }
}
