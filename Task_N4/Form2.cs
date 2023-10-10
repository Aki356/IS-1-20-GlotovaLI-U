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
using ConnectDB;


namespace Task_N4
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private MySqlDataAdapter MyDA = new MySqlDataAdapter();
        //Объявление BindingSource, основная его задача, это обеспечить унифицированный доступ к источнику данных.
        private BindingSource bSource = new BindingSource();
        //DataSet - расположенное в оперативной памяти представление данных, обеспечивающее согласованную реляционную программную 
        //модель независимо от источника данных.DataSet представляет полный набор данных, включая таблицы, содержащие, упорядочивающие 
        //и ограничивающие данные, а также связи между таблицами.
        private DataSet ds = new DataSet();
        //Представляет одну таблицу данных в памяти.
        private DataTable table = new DataTable();
        //Переменная для ID записи в БД, выбранной в гриде. Пока она не содердит значения, лучше его инициализировать с 0
        //что бы в БД не отправлялся null
        string id_selected_rows = "0";


        //Метод получения ID выделенной строки, для последующего вызова его в нужных методах
        public void GetSelectedIDString()
        {
            try
            {
                //Переменная для индекс выбранной строки в гриде
                string index_selected_rows;
                //Индекс выбранной строки
                index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
                //ID конкретной записи в Базе данных, на основании индекса строки
                id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
                //Указываем ID выделенной строки в метке
                
            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }
        MySqlConnection conn = Class1.Connectd.Conn();
        
        public void DB()
        {
            try
            {
                string sql = $"SELECT * FROM t_datatime";
                conn.Open();
                // объект для выполнения SQL-запроса
                MyDA.SelectCommand = new MySqlCommand(sql, conn);
                //Заполняем таблицу записями из БД
                MyDA.Fill(table);
                //Указываем, что источником данных в bindingsource является заполненная выше таблица
                bSource.DataSource = table;
                //Указываем, что источником данных ДатаГрида является bindingsource 
                dataGridView1.DataSource = bSource;
                //Закрываем соединение
                conn.Close();
                
               
            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            conn.Open();
            int rowIndex = e.RowIndex;
            int conIndex = e.ColumnIndex;
            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            if (conIndex == 1)
            {
                string sql = $"SELECT photoUrl FROM t_datatime WHERE id ={row.Cells[conIndex - 1].Value.ToString()};";
                MySqlCommand command = new MySqlCommand(sql, conn);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pictureBox1.ImageLocation = reader[0].ToString();// вставляем ссылку на картинку
                }


            }
            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DB();
            //Видимость полей в гриде
            
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[3].Visible = false;

            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 15;
            dataGridView1.Columns[1].FillWeight = 40;
            
            //Режим для полей "Только для чтения"
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            
            //Растягивание полей грида
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
