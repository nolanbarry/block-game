using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace blockgame
{
    public class Stage
    {
        public int dimension { get; private set; } // number of columns
        public Bitmap cache; // stores the grid image so that it does not have to be redrawn every frame
        public List<Shape> shapes;

        public Stage(int dimension)
        {
            this.dimension = dimension;
            shapes = new List<Shape>();
            for(int i = 0; i < 4; i++)
            {
                shapes.Add(new Shape(new Point(Shape.r.Next(7), Shape.r.Next(7))));
            }
        }

        #region rendering methods/overloads
        public Bitmap assembleGridTo(Bitmap img, Size gridBounds, int margin)
        {
            Point corner = new Point()
            {
                X = img.Width / 2 - gridBounds.Width / 2,
                Y = img.Height / 2 - gridBounds.Height / 2
            };
            int interval = gridBounds.Width / dimension;

            img = drawGrid(img.Size, gridBounds, new Point(img.Width / 2, img.Height / 2), margin);

            Graphics g = Graphics.FromImage(img);
            foreach (Shape s in shapes)
            {
                int x = corner.X + (s.groupAnchor.X * interval);
                int y = corner.Y + (s.groupAnchor.Y * interval);
                g.DrawImage(s.draw(interval - margin, interval), x, y);
            }
            return img;
        }
        public Bitmap drawGrid(Size drawArea, Size bounds, int margin, Point anchorPoint)
        {
            if (cache == null)
            {
                Bitmap img = new Bitmap(drawArea.Width, drawArea.Height);
                Graphics g = Graphics.FromImage(img);
                int xInterval = bounds.Width / dimension; // the distance between lines in the grid
                int yInterval = bounds.Height / dimension;

                for (int i = 0; i < dimension; i++)
                {
                    for (int j = 0; j < dimension; j++)
                    {
                        Point cellAnchor = new Point(anchorPoint.X + i * xInterval, anchorPoint.Y + j * yInterval);
                        g.DrawImage(Shape.getBlock(7, xInterval - margin), cellAnchor);
                    }
                }
                cache = img;
                return img;
            }
            else return cache;
        }

        public Bitmap drawGrid(Size drawArea, Size bounds, Point center, int margin)
        {
            return drawGrid(drawArea, bounds, margin, new Point { X = center.X - bounds.Width / 2, Y = center.Y - bounds.Height / 2 });

        }
        #endregion
    }
}
