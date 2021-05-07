using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProducerConsumerGraphics
{
    public partial class Form1 : Form
    {
        private CommonData data;
        private List<Producer> prods;
        private Consumer cons;
        private Animations anim;
        public Form1()
        {
            InitializeComponent();
            Rings.MainGraphics = panel1.CreateGraphics();
            data = new CommonData(panel2.CreateGraphics(),panel3.CreateGraphics(), panel4.CreateGraphics(), panel5.CreateGraphics());
            cons = new Consumer(data, panel1.CreateGraphics(), panel1.CreateGraphics().VisibleClipBounds.Size.ToSize());
            prods = new List<Producer>(3)
            {
                new Producer(data, 0),
                new Producer(data, 1),
                new Producer(data, 2)
            };
            Circle.panelsize = panel1.CreateGraphics().VisibleClipBounds.Size.ToSize();
            Animations.panelsize = panel1.CreateGraphics().VisibleClipBounds.Size.ToSize();
            Rings.panelsize = panel1.CreateGraphics().VisibleClipBounds.Size.ToSize(); 
            anim = new Animations(panel1.CreateGraphics(),panel2.CreateGraphics(),data);
            anim.Start();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            cons.Start();
            foreach (var p in prods)
            {
                p.Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var p in prods)
            {
                p.Stop();
            }
            cons.Stop();
            Animations.StopAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Ok");
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
