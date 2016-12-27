using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SmartBadmintonTrainingSystem
{
    class singletonDB
    {
        private static singletonDB instance = new singletonDB();
        public MySqlConnection conn;
        string strconn = "Server=220.69.209.170;Database=badminton;Uid=cglab;Pwd=clws";
        private singletonDB()
        {
            conn = new MySqlConnection(strconn);
            conn.Open();
        }

        public static void IsOpen()
        {
            if (!singletonDB.instance.conn.State.ToString().Equals("Open"))
            {
                try
                {
                    singletonDB.instance.conn.Open();
                }
                catch(System.Exception ex){

                }
            }
        }

        public static singletonDB getInstance()
        {
            return instance;
        }
    }
}
