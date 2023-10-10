using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ConnectDB
{
    public class Class1
    {
        public class Connectd
        {
            public static MySqlConnection Conn()
            {
                string connStr = "server=chuc.sdlik.ru;port=33333;user=st_1_20_8;database=is_1_20_st8_KURS;password=43660467;";
                //Переменная соединения
                MySqlConnection conn = new MySqlConnection(connStr);
                return conn;
            }


        }
    }
}
