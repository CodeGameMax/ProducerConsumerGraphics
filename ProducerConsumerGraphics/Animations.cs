using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ProducerConsumerGraphics
{
    class Animations
    {
        private Thread t,t2;
        static public bool stop;
        private Graphics MainGraphics, BackGraphics;
        private BufferedGraphics bg;
        static public Size panelsize = new Size(1, 1);
        CommonData Data;
        public Animations(Graphics g,Graphics g1 ,CommonData data)
        {
            MainGraphics = g;
            BackGraphics = g1;
            MainGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            bg = BufferedGraphicsManager.Current.Allocate(g, Rectangle.Round(g.VisibleClipBounds));
            Data = data;     
        }
        public void Start()
        {
            stop = false;
            if (t == null || !t.IsAlive)
            {
                t = new Thread(new ThreadStart(Animate));
                t.Start();
            }
        }

        private void Animate()
        {
            while (!stop)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (Data.values[i].Count != 0)
                    {
                        if (Data.values[i].Peek().t == null &&  Data.result[i] == null)
                            Data.values[i].Peek().Start();
                    }
                }
                for (int i = 0; i < Data.boomm.Count; i++)
                {
                    if(Data.boomm[i].t == null)
                        Data.boomm[i].Start();
                }
                bg.Graphics.Clear(Color.White);
                for (int i = 0; i < Data.boomm.Count; i++)
                {
                    if (!Data.boomm[i].check)
                        Data.boomm[i].Paint(bg.Graphics);
                }
                for (int i = 0; i < 3; i++)
                {
                    if (Data.values[i].Count != 0 )
                    {
                        Data.values[i].Peek().Paint(bg.Graphics);
                    }
                }
                bg.Render(MainGraphics);
            }
        }
       static public void StopAll()
       {
            stop = true;
            Circle.stopAll = true;
       }

        
        public void StartBoom()
        {
            if (t2== null || !t.IsAlive)
            {
                t2 = new Thread(new ThreadStart(Booming));
                t2.Start();
            }

        }
        

        private void Booming()
        {
            lock (bg.Graphics)
            {
                Circle[] crr = new Circle[3];
                crr = Data.result;
                Circle cr = new Circle();

                for (int i = 0; i < 3; i++)
                {
                    cr.Red = cr.Red + crr[i].Red;
                    cr.Blue = cr.Blue + crr[i].Blue;
                    cr.Green = cr.Green + crr[i].Green;
                }
                cr.color = Color.FromArgb(60, cr.Red, cr.Blue, cr.Green);

                for (int i = 0; i < 50; i++)
                {
                    bg.Graphics.Clear(Color.White);
                    cr.CircleSize = new Size(cr.CircleSize.Width + 50,
                                            cr.CircleSize.Height + 50);
                    cr.position = new Point(panelsize.Width / 2 - cr.CircleSize.Height / 2,
                                            panelsize.Height / 2 - cr.CircleSize.Width / 2);

                    cr.Paint(bg.Graphics);
                    bg.Render(MainGraphics);
                    Thread.Sleep(10);
                }
                Circle cr1 = new Circle();
                cr1.color = Color.White;
                for (int i = 0; i < 50; i++)
                {
                    cr1.CircleSize = new Size(cr1.CircleSize.Width + 50,
                                            cr1.CircleSize.Height + 50);
                    cr1.position = new Point(panelsize.Width / 2 - cr1.CircleSize.Height / 2,
                                            panelsize.Height / 2 - cr1.CircleSize.Width / 2);
                    cr1.Paint(bg.Graphics);
                    bg.Render(MainGraphics);
                    Thread.Sleep(10);
                }
            }
               
            
        }

    }
}
