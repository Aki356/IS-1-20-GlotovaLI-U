using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Connect_task3
{
    static class Auth
    {
        //Статичное поле, которое хранит значение статуса авторизации
        public static bool auth = false;
        //Статичное поле, которое хранит значения ID пользователя
        public static string order_id = null;
        //Статичное поле, которое хранит значения ФИО пользователя
        public static string order_client = null;
        public static string order_status = null;
        public static string order_time = null;
        public static string order_employ = null;
        public static string order_totalCount = null;
        public static string order_dt = null;
    }
    public class Connectd
    {
        public static MySqlConnection Conn()
        {
            string connStr = "server=chuc.caseum.ru;port=33333;user=st_1_20_8;database=is_1_20_st8_KURS;password=43660467;";
            //Переменная соединения
            MySqlConnection conn = new MySqlConnection(connStr);
            return conn;
        }


    }
    static class Program
    {
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
