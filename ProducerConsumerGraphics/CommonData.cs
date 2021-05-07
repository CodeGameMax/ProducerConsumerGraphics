using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Threading.Tasks;

namespace ProducerConsumerGraphics
{
    class CommonData
    {
        private Graphics Maingraphics, Maingraphics1, Maingraphics2, Maingraphics3;
        public static readonly int ProducerCount = 3;
        public Queue<Circle>[] values =
        {
            new Queue<Circle>(),
            new Queue<Circle>(),
            new Queue<Circle>()
        };
        public CommonData(Graphics g1,Graphics g2,Graphics g3, Graphics g4)
        {
            Maingraphics = g1;
            Maingraphics1 = g2;
            Maingraphics2 = g3;
            Maingraphics3 = g4;
        }

        public bool[] Cheking = new bool[3] { true,true,true};

        public Circle[] result;
        public List<Rings> boomm = new List<Rings>();
        public void AddCircle(int index, Circle cir)
        {
            index = Math.Abs(index) % ProducerCount;
            Monitor.Enter(values);
            try
            {              
                while(values[index].Count > 2)
                {
                    Monitor.Wait(values);
                }
                values[index].Enqueue(cir);
                ShowCircle(values[index], index);
                Monitor.PulseAll(values);

            }
            finally
            {
                Monitor.Exit(values);
            }
        }
        public Circle[] ConsumeValues()
        {
            result = new Circle[ProducerCount];
            Monitor.Enter(values);
            try
            {
                Maingraphics.Clear(Color.White);
                for (int i = 0; i < 3; i++)
                {
                    while (values[i].Count == 0 || !values[i].Peek().check)
                    {
                        Monitor.Wait(values,5);
                    }
                    result[i] = values[i].Dequeue();
                    ShowCircle(values[i], i);
                    result[i].CircleSize = new Size(50, 50);
                    result[i].position = new Point(0 + i*50,0);
                    result[i].Paint(Maingraphics);
                    Monitor.PulseAll(values);
                }             
            }
            finally
            {
                Monitor.Exit(values);
            }
            boomm.Add(new Rings());
            Thread.Sleep(30);
            return result;
        }

        private void ShowCircle(Queue<Circle> showingCirc, int ind)
        {
            if (ind == 0)
            {
                Queue<Circle> circless = new Queue<Circle>();
                circless = showingCirc;
                List<Circle> circl1 = new List<Circle>();
                List<Circle> circl2 = new List<Circle>();
                Maingraphics1.Clear(Color.White);
                while(circless.Count != 0)
                {
                    Circle cr = new Circle();
                    cr.color = circless.Peek().color;
                    circl1.Add(circless.Dequeue());
                    circl2.Add(cr);
                }
                for (int i = 0; i < circl1.Count; i++)
                {
                    circless.Enqueue(circl1[i]);
                }
                for (int i = 0; i < circl2.Count; i++)
                {
                    circl2[i].CircleSize = new Size(50, 50);
                    circl2[i].position = new Point(150 - i * 50, 0);
                    circl2[i].Paint(Maingraphics1);
                }
            }
            else if(ind == 1)
            {
                Queue<Circle> circless1 = new Queue<Circle>();
                circless1 = showingCirc;
                List<Circle> circl3 = new List<Circle>();
                List<Circle> circl4 = new List<Circle>();
                Maingraphics3.Clear(Color.White);
                while (circless1.Count != 0)
                {
                    Circle cr = new Circle();
                    cr.color = circless1.Peek().color;
                    circl3.Add(circless1.Dequeue());
                    circl4.Add(cr);
                }
                for (int i = 0; i < circl3.Count; i++)
                {
                    circless1.Enqueue(circl3[i]);
                }
                for (int i = 0; i < circl4.Count; i++)
                {
                    circl4[i].CircleSize = new Size(50, 50);
                    circl4[i].position = new Point(0, 150 - i*50);
                    circl4[i].Paint(Maingraphics3);
                }
            }
            else if (ind == 2)
            {
                Queue<Circle> circless2 = new Queue<Circle>();
                circless2 = showingCirc;
                List<Circle> circl5 = new List<Circle>();
                List<Circle> circl6 = new List<Circle>();
                Maingraphics2.Clear(Color.White);
                while (circless2.Count != 0)
                {
                    Circle cr = new Circle();
                    cr.color = circless2.Peek().color;
                    circl5.Add(circless2.Dequeue());
                    circl6.Add(cr);
                }
                for (int i = 0; i < circl5.Count; i++)
                {
                    circless2.Enqueue(circl5[i]);
                }
                for (int i = 0; i < circl6.Count; i++)
                {
                    circl6[i].CircleSize = new Size(50, 50);
                    circl6[i].position = new Point(0 + i * 50, 0);
                    circl6[i].Paint(Maingraphics2);
                }
            }
        }
    }
}
