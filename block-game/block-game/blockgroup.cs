using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;

namespace blockgame
{
    public class Shape
    {
        public static Random r = new Random();

        public Point[] group { get; private set; }
        public int color { get; private set; }
        public int xBounds
        {
            get
            {
                int max = 0;
                foreach(Point p in group)
                {
                    if (p.X > max)
                    {
                        max = p.X;
                    }
                }
                return max + 1;
            }
        }
        public int yBounds
        {
            get
            {
                int max = 0;
                foreach (Point p in group)
                {
                    if (p.Y > max)
                    {
                        max = p.Y;
                    }
                }
                return max + 1;
            }
        }
        public Point groupAnchor;
        private Bitmap me;

        public Shape(Point anchor)
        {
            color = r.Next(colorOptions.Length);
            group = shapes[color];
            groupAnchor = anchor;
        }

        /// <summary>
        /// Returns an image of the assembled shape
        /// </summary>
        /// <param name="squareLength">The width/height of each square</param>
        /// <param name="interval">The distance between squares (different from squareLength to make room for a margin)</param>
        /// <returns></returns>
        public Image draw(int squareLength, int interval)
        {
            if (me == null)
            {
                Bitmap img = new Bitmap(xBounds * interval, (yBounds + 2) * interval);
                Graphics g = Graphics.FromImage(img);
                g.SmoothingMode = SmoothingMode.HighQuality;
                Bitmap block = getBlock(color, squareLength);
                foreach (Point p in group)
                {
                    g.DrawImage(block, new Point(p.X * interval + ((interval - squareLength) / 2) - 2, -2 + p.Y * interval + ((interval - squareLength) / 2)));
                }
                me = img;
                return img;
            } else
            {
                return me;
            }

        }

        static readonly float[][] colorOptions = new float[][] // length must be at least as long as the number of shape types
        {
            new float[] {255, 151, 115}, // #ff9773
            new float[] {186, 218, 85}, // #bada55
            new float[] {4, 118, 181}, // #0576b5
            new float[] {255, 215, 0}, // #ffd700
            new float[] {98, 128, 164}, // #6280a4
            new float[] {254, 17, 122}, // #fe117a
            new float[] {255, 204, 204}, // #ffcccc
            new float[] {30, 30, 30} // gray, for the grid
        };

        public static Bitmap getBlock(int colorNum, int width)
        {
            Bitmap block = imaging.resizeImage(blockUI.block, width, width);
            float[] color = colorOptions[colorNum];
            block = imaging.recolorImage(block, color[0] / 255f, color[1] / 255f, color[2] / 255f);
            return block;
        }

        #region block groups
        public static Point[][] shapes = new Point[][]
        {
            new Point[]
            {
                new Point(1, 0), //
                new Point(0, 1), // - X -
                new Point(1, 1), // X X X
                new Point(2, 1)  //
            },
            new Point[]
            {
                new Point(0, 0), // X
                new Point(0, 1), // X
                new Point(0, 2), // X
                new Point(0, 3)  // X
            },
            new Point[]
            {
                new Point(0, 0), //
                new Point(1, 0), // X X
                new Point(0, 1), // X
            },
            new Point[]
            {
                new Point(0, 0), // X
                new Point(0, 1), // X
                new Point(0, 2), // X
            },
            new Point[]
            {
                new Point(0, 0), //  
                new Point(1, 0), // X X
                new Point(0, 1), // X
                new Point(0, 2)  // X
            },
            new Point[]
            {
                new Point(0, 0), //
                new Point(0, 1), // X X
                new Point(1, 0), // X X
                new Point(1, 1)  //
            },
            new Point[]
            {
                new Point(0, 0), //
                new Point(0, 1), //
                new Point(0, 2), //
                new Point(1, 0), // X X X
                new Point(1, 1), // X X X
                new Point(1, 2), // X X X
                new Point(2, 0), //
                new Point(2, 1), //
                new Point(2, 2)  //
            },
            new Point[]
            {
                new Point(0, 0), // 
                new Point(1, 0), // X X X 
                new Point(2, 0), // X
                new Point(0, 1), // X
                new Point(0, 2)  //
            }
        };
        #endregion
    }
}
