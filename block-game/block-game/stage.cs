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
        public int width { get; private set; }
        public int height { get; private set; }
        public Bitmap cache; // stores the grid image so that it does not have to be redrawn every frame

        public Stage(Size s)
        {
            width = s.Width;
            height = s.Height;
        }

        public Bitmap drawGrid(Size drawArea, Size bounds, Color borderColor, int margin, Point anchorPoint)
        {
            if (cache == null)
            {
                Bitmap img = new Bitmap(drawArea.Width, drawArea.Height);
                Graphics g = Graphics.FromImage(img);
                Pen pen = new Pen(borderColor);
                pen.Width = 3;
                pen.EndCap = LineCap.Round;
                pen.StartCap = LineCap.Round;
                int xInterval = bounds.Width / width; // the distance between lines in the grid
                int yInterval = bounds.Height / height;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Point cellAnchor = new Point(anchorPoint.X + i * xInterval, anchorPoint.Y + j * yInterval);
                        g.DrawImage(BlockGroup.getBlock(7, xInterval - margin), cellAnchor);
                    }
                }
                cache = img;
                return img;
            }
            else return cache;
        }

        public Bitmap drawGrid(Size drawArea, Size bounds, Point center, Color borderColor, int margin)
        {
            return drawGrid(drawArea, bounds, borderColor, margin, new Point { X = center.X - bounds.Width / 2, Y = center.Y - bounds.Height / 2 });

        }
    }
}
