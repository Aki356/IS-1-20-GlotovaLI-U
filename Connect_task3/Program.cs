using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Connect_task3
{
    static class Program
    {
        class Connectd
        {
            public MySqlConnection Conn()
            {
                string connStr = "server=chuc.caseum.ru;port=33333;user=st_1_20_8;database=uchebka;password=uchebka;";
                //Переменная соединения
                MySqlConnection conn = new MySqlConnection(connStr);
                return conn;
            }
            
        }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConnectN3());
        }
    }
}
