using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IS_1_20_GlotovaLI_U
{
    public partial class Accessories : Form
    {
        abstract class Accessory <T>
        {
            public double price;
            public int yearRelease;
            T artic;
            public Accessory(double price, int yearR, T art)
            {
                this.price = price;
                yearRelease = yearR;
                artic = art;
            }
            public string Display()
            {
                return "Артикул: " + artic + "\n" + price + " руб. " + yearRelease + " год.";
            }
        }
        class Hdd : Accessory<T>
        {
            private double NumRevol { get; set; }
            private string Interfc { get; set; }
            private int Capacity { get; set; }
            public Hdd(double nR, string i, int c, double price, int yearR):base(price, yearR)
            {
                NumRevol = nR;
                Interfc = i;
                Capacity = c;
            }
            public new string Display()
            {
                return "Кол-во оборотов: " + NumRevol + ", интерфейс: " + Interfc + ", объем жесткого диска: " + Capacity + ", цена: " + price + ", год выпуска: " + yearRelease;
            }
        }
        class VideoCard : Accessory
        {
            private double GpuFreq { get; set; }
            private string Manuf { get; set; }
            private int Capacity { get; set; }
            public VideoCard(double gF, string m, int c, double price, int yR):base(price, yR)
            {
                GpuFreq = gF;
                Manuf = m;
                Capacity = c;
            }
            public new string Display()
            {
                return "Тактовая частота: " + GpuFreq + ", производитель: " + Manuf + ", объем памяти: " + Capacity;
            }
        }
        public Accessories()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hdd hdd = new Hdd(Convert.ToDouble(textBox3.Text), textBox4.Text, Convert.ToInt32(textBox5.Text), Convert.ToDouble(textBox1.Text), Convert.ToInt32(textBox2.Text));
            listBox1.Items.Add(hdd.Display());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
