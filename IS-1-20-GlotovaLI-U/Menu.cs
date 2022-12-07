using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Acessori;
using Connect_task2;
using Connect_task3;

namespace IS_1_20_GlotovaLI_U
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Accessories accssrs = new Accessories();
            accssrs.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 connct_n2 = new Form1();
            connct_n2.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConnectN3 conn_n3 = new ConnectN3();
            conn_n3.ShowDialog();
        }
    }
}
