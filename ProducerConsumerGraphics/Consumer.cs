using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Threading.Tasks;

namespace ProducerConsumerGraphics
{
    class Consumer
    {
        private CommonData Data
        {
            get;
            set;
        }

        private Thread t = null;
        private bool stop = false;
        private Graphics MainGraphics;
        private Size panelsize;


        public Consumer(CommonData data,Graphics g, Size ps)
        {
            Data = data;
            MainGraphics = g;
            panelsize = ps;
        }

        public void Start()
        {
            if (t?.IsAlive == true) return;
            stop = false;
            t = new Thread(new ThreadStart(consume));
            t.Start();
        }

        private void consume()
        {
            try
            {
                while (!stop)
                {
                  
                    var values = Data.ConsumeValues();
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
