﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLCommon;

namespace SQLTS
{
    public partial class TimeEventDAO
    {
        public static void Delete(TimeEventDTO dto, MySqlConnection conn)
        {
            conn.Open();
            string sql = "DELETE FROM timeevent WHERE TE_ID = " + dto.ID;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
