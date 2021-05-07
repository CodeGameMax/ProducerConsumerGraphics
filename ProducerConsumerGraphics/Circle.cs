using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

namespace ProducerConsumerGraphics
{
    class Circle
    {
        public Thread t;
        private int red, blue, green;
        public bool stop,check;
        static public bool stopAll = false;
        private int dx, dy, speed;
        private int RGBINDEX;
        public Point position;
        public Size CircleSize;
        public Color color;
        private static Random r = new Random();
        static public Size panelsize = new Size(1, 1);
        public int Red
        {
            get => red;
            set
            {
                if(value <= 255)
                {
                    red = value;
                }
                else if(value > 255 && value<500)
                {
                    red = value - 255;
                }
                else if (value > 500 )
                {
                    red = value - 500;
                }
            }
        }
        public int Blue
        {
            get => blue;
            set
            {
                if (value <= 255)
                {
                    blue = value;
                }
                if (value > 255 && value < 500)
                {
                    blue = value - 255;
                }
                else if (value > 500)
                {
                    blue = value - 500;
                }
            }
        }
        public int Green
        {
            get => green;
            set
            {
                if (value <= 255)
                {
                    green = value;
                }
                if (value > 255 && value < 500)
                {
                    green = value - 255;
                }
                else if (value > 500)
                {
                    green = value - 500;
                }
            }
        }
        public int XPosition
        {
            get => position.X;
            set
            {
                if (value == panelsize.Width/2)
                {
                    position.X = position.X;
                }
                else
                {
                    position.X = value;
                }
            }
        }

        public int YPosition
        {
            get => position.Y;
            set
            {
                if (value == panelsize.Height / 2)
                {
                    position.Y = position.Y;
                }
                else
                {
                    position.Y = value;
                }

            }
        }
        public Circle(int rgb)
        {
            RGBINDEX = rgb;
            if(rgb == 0)
            {
                Red = 255;
                Blue = r.Next(0, 255);
                Green = r.Next(0, 255);
                color = Color.FromArgb(200,Red, Blue, Green);
                position = new Point(0,panelsize.Height/2);
                dx = 3;
                dy = 0;
                speed = 10;

            }
            else if (rgb == 1)
            {
                Red = r.Next(0, 255);
                Blue = 255;
                Green = r.Next(0, 255);
                color = Color.FromArgb(200, Red,Blue, Green);
                position = new Point(panelsize.Width/2, 0);
                dx = 0;
                dy = 3;
                speed = 15;
            }
            else
            {
                Red = r.Next(0, 255);
                Blue = r.Next(0, 255);
                Green = 255;
                color = Color.FromArgb(200, red, blue,Green);
                position = new Point(panelsize.Width, panelsize.Height / 2);
                dx = -3;
                dy = 0;
                speed = 30;
            }
            CircleSize = new Size(50, 50);
        }

        public Circle()
        {
            color = Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
            CircleSize = new Size(50, 50);
            position = new Point(panelsize.Width/2-CircleSize.Height, panelsize.Height / 2 - CircleSize.Width);
        }

        public void Paint(Graphics g)
        {
           
                Brush brush = new SolidBrush(color);
                Rectangle rec = new Rectangle(position, CircleSize);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.FillEllipse(brush, rec);
           

            
        }

        private void Move()
        {
            XPosition += dx;
            YPosition += dy;
            check = (XPosition <= panelsize.Width / 2 + 1 && XPosition >= panelsize.Width / 2 - 1)
                 && (YPosition <= panelsize.Height / 2 + 1 && YPosition >= panelsize.Height / 2 - 1);
        }

        public void Start()
        {
            stop = false;
            t = new Thread(
                new ThreadStart(Run)
                );
            t.Start();
        }

        private void Run()
        {
            while (!stopAll && !stop && !check)
            {
                Move();
                Thread.Sleep(speed);
            }
        }
    }
}
