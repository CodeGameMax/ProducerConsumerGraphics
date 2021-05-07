using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;

namespace ProducerConsumerGraphics
{
    class Rings
    {
        static public Graphics MainGraphics;
        public Thread t;
        public bool stop, check;
        static public bool stopAll = false;
        private int speed,d;
        public Point position;
        public Size RingsSize;
        public Color color;
        private static Random r = new Random();
        static public Size panelsize = new Size(1, 1);


        public Rings()
        {
            color = Color.FromArgb(50, r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
            RingsSize = new Size(50, 50);
            speed = 40;
            d = 2;
            position = new Point(panelsize.Width / 2,
                                    panelsize.Height / 2);
        }

        public void Paint(Graphics g)
        {
            Brush brush = new SolidBrush(color);
            Rectangle rec = new Rectangle(position, RingsSize);
            g.FillEllipse(brush, rec);
        }

        private void Move()
        {
            RingsSize = new Size(RingsSize.Width + d*4,
                                            RingsSize.Height + d*4);
            position = new Point(panelsize.Width / 2 - RingsSize.Height / 2,
                                    panelsize.Height / 2 - RingsSize.Width / 2);
            check = RingsSize.Height >panelsize.Height+400;
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
