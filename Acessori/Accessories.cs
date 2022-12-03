using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Acessori
{
    public partial class Accessories : Form
    {
        abstract class Accessory<T>
        {
            public double price;
            public int yearRelease;
            public T artic;
            public Accessory(double price, int yearR, T art)
            {
                this.price = price;
                yearRelease = yearR;
                artic = art;
            }
            public string Display()
            {
                return $"Артикул: {artic} " +
                    $"Цена: {price} руб. " +
                    $"{yearRelease} год.";
            }
        }
        class Hdd<T> : Accessory<T>
        {
            private double NumRevol { get; set; }
            private string Interfc { get; set; }
            private int Capacity { get; set; }
            public Hdd(double nR, string i, int c, double price, int yearR, T art) : base(price, yearR, art)
            {
                NumRevol = nR;
                Interfc = i;
                Capacity = c;
            }
            public new string Display()
            {
                return "Кол-во оборотов: " + NumRevol + ", интерфейс: " + Interfc + ", объем жесткого диска: " + Capacity + ", цена: " + price + " руб., год выпуска: " + yearRelease;
            }
        }
        class VideoCard<T> : Accessory<T>
        {
            private double GpuFreq { get; set; }
            private string Manuf { get; set; }
            private int Capacity { get; set; }
            public VideoCard(double gF, string m, int c, double price, int yR, T art) : base(price, yR, art)
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
            try
            {
                Accessory<string> hdd = new Hdd<string>(Convert.ToDouble(textBox3.Text), textBox4.Text, Convert.ToInt32(textBox5.Text), Convert.ToDouble(textBox1.Text), Convert.ToInt32(textBox2.Text), textBox6.Text);
                listBox1.Items.Add(hdd.Display());
                listBox1.HorizontalScrollbar = true;
            }
            catch
            {
                MessageBox.Show("Возникла ошибка! Проверьте введенные данные.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Hdd<string> hdd = new Hdd<string>(Convert.ToDouble(textBox3.Text), textBox4.Text, Convert.ToInt32(textBox5.Text), Convert.ToDouble(textBox1.Text), Convert.ToInt32(textBox2.Text), textBox6.Text);
                listBox1.Items.Add(hdd.Display());
                listBox1.HorizontalScrollbar = true;
            }
            catch
            {
                MessageBox.Show("Возникла ошибка! Проверьте введенные данные.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                VideoCard<string> vidC = new VideoCard<string>(Convert.ToDouble(textBox7.Text), textBox8.Text, Convert.ToInt32(textBox9.Text), Convert.ToDouble(textBox1.Text), Convert.ToInt32(textBox2.Text), textBox6.Text);
                listBox1.Items.Add(vidC.Display());
                listBox1.HorizontalScrollbar = true;
            }
            catch
            {
                MessageBox.Show("Возникла ошибка! Проверьте введенные данные.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }
    }
}
