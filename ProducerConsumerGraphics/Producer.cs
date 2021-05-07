using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Threading.Tasks;

namespace ProducerConsumerGraphics
{
    class Producer
    {
        private CommonData Data
        {
            get;
            set;
        }

        private int index;
        public int Index
        {
            get => index;
            set => index = Math.Abs(value) % CommonData.ProducerCount;
        }

        private Thread t = null;
        private bool stop = false;

        private static Random r = new Random((int)DateTime.Now.Ticks);
        public Producer(CommonData data, int index)
        {
            Data = data;
            Index = index;
           
        }

        public void Start()
        {
            if (t?.IsAlive == true) return;
            stop = false;
            t = new Thread(new ThreadStart(produce));
            t.Start();
        }

        private void produce()
        {           
            try
            {
                while (!stop)
                {
                   // Thread.Sleep(1000 + Index * r.Next(2000));
                    Circle cr = new Circle(Index);
                    Data.AddCircle(Index, cr);
                }
            }
            catch { }
        }

        public void Stop()
        {
            stop = true;
            t.Interrupt();
        }
    }
}
