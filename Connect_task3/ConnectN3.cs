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

namespace Connect_task3
{
    public partial class ConnectN3 : Form
    {
        public ConnectN3()
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

        //Метод обновления DataGreed
        public void reload_list()
        {
            //Чистим виртуальную таблицу
            table.Clear();
            //Вызываем метод получения записей, который вновь заполнит таблицу
            DB();
        }

        //Метод получения ID выделенной строки, для последующего вызова его в нужных методах
        public void GetSelectedIDString()
        {
            //Переменная для индекс выбранной строки в гриде
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            //Указываем ID выделенной строки в метке
            toolStripLabel1.Text = id_selected_rows;
        }
        //Выделение всей строки по ЛКМ
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Магические строки
            dataGridView1.CurrentCell = dataGridView1[e.ColumnIndex, e.RowIndex];
            dataGridView1.CurrentRow.Selected = true;
            //Метод получения ID выделенной строки в глобальную переменную
            GetSelectedIDString();
        }
        
        public void getCellValue()
        {
            string index_selected_rows;
            //Индекс выбранной строки
            index_selected_rows = dataGridView1.SelectedCells[0].RowIndex.ToString();
            //ID конкретной записи в Базе данных, на основании индекса строки
            id_selected_rows = dataGridView1.Rows[Convert.ToInt32(index_selected_rows)].Cells[0].Value.ToString();
            string sql = $"SELECT " +
                $"MainOrder.id_Order, " +
                $"Clients.firstname_Client, " +
                $"Status.title_Status, " +
                $"MainOrder.time_Order, " +
                $"Employ.lastname_Employ, " +
                $"MainOrder.totalCount_Order, " +
                $"MainOrder.dt_Order " +
                $"FROM Clients " +
                $"INNER JOIN (Employ " +
                $"INNER JOIN (Status " +
                $"INNER JOIN MainOrder ON Status.id_Status = MainOrder.status_Order) " +
                $"ON Employ.id_Employ = MainOrder.employ_Order) " +
                $"ON Clients.id_Client = MainOrder.client_Order WHERE MainOrder.id_Order = {id_selected_rows}";
            conn.Open();
            MySqlCommand command = new MySqlCommand(sql, conn);
            MySqlDataReader reader = command.ExecuteReader();
            // читаем результат
            while (reader.Read())
            {
                // элементы массива [] - это значения столбцов из запроса SELECT
                Auth.order_id = reader[0].ToString();
                Auth.order_client = reader[1].ToString();
                Auth.order_status = reader[2].ToString();
                Auth.order_time = reader[3].ToString();
                Auth.order_employ = reader[4].ToString();
                Auth.order_totalCount = reader[5].ToString();
                Auth.order_dt = reader[6].ToString();
            }
            reader.Close();
            MessageBox.Show(string.Format("ID: {0}", reader["id_Order"]));
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                MessageBox.Show("ID: " + dataGridView1.CurrentRow.Cells["Код заказа"].Value.ToString()+ "Клиент: " + dataGridView1.CurrentRow.Cells["Клиент"].Value.ToString() + "Статус заказа: " + dataGridView1.CurrentRow.Cells["Статус заказа"].Value.ToString());
            }
            conn.Close();
        }


        MySqlConnection conn = Connectd.Conn();
        
        public void DB()
        {
            
            // запрос
            string sql = $"SELECT " +
                $"MainOrder.id_Order AS 'Код заказа', " +
                $"Clients.firstname_Client AS 'Клиент', " +
                $"Status.title_Status AS 'Статус заказа', " +
                $"MainOrder.time_Order AS 'Время заказа', " +
                $"Employ.lastname_Employ AS 'Работник', " +
                $"MainOrder.totalCount_Order AS 'Цена', " +
                $"MainOrder.dt_Order AS 'Дата заказа'" +
                $"FROM Clients " +
                $"INNER JOIN (Employ " +
                $"INNER JOIN (Status " +
                $"INNER JOIN MainOrder ON Status.id_Status = MainOrder.status_Order) " +
                $"ON Employ.id_Employ = MainOrder.employ_Order) " +
                $"ON Clients.id_Client = MainOrder.client_Order";
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
            //Отражаем количество записей в ДатаГриде
            int count_rows = dataGridView1.RowCount - 1;
            toolStripLabel2.Text = (count_rows).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void ConnectN3_Load(object sender, EventArgs e)
        {
            
            DB();
            //Видимость полей в гриде
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[2].Visible = true;
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[4].Visible = true;
            dataGridView1.Columns[5].Visible = true;
            dataGridView1.Columns[6].Visible = true;

            //Ширина полей
            dataGridView1.Columns[0].FillWeight = 15;
            dataGridView1.Columns[1].FillWeight = 40;
            dataGridView1.Columns[2].FillWeight = 15;
            dataGridView1.Columns[3].FillWeight = 30;
            dataGridView1.Columns[4].FillWeight = 40;
            dataGridView1.Columns[5].FillWeight = 15;
            dataGridView1.Columns[6].FillWeight = 30;
            //Режим для полей "Только для чтения"
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = true;
            //Растягивание полей грида
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //Убираем заголовки строк
            dataGridView1.RowHeadersVisible = false;
            //Показываем заголовки столбцов
            dataGridView1.ColumnHeadersVisible = true;
        }

        
    }
}
