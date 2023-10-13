using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Connect_task2
{
    public partial class Form1 : Form
    {
        class Connctd
        {
            public static MySqlConnection GetConnctn()
            {
                string connStr = "server=chuc.caseum.ru;port=33333;user=uchebka;database=uchebka;password=uchebka;";
                //Переменная соединения
                MySqlConnection conn = new MySqlConnection(connStr);
                return conn;
            }
        } 
        MySqlConnection conn = Connctd.GetConnctn();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    label1.Visible = true;
                    label1.Text = "Соединение подключено.";
                }
                conn.Close();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка.");
            }
            
            
            
        }
    }
}
