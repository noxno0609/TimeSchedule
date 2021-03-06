﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLCommon;
using System.Data;
using SQLTS;

namespace SQLTS
{
    public partial class PeriodEventDAO
    {
        public static int Insert(PeriodEventDTO dto, MySqlConnection conn)
        {
            #region Insert Period Event
            conn.Open();

            string sql = String.Format(@"INSERT INTO PeriodEvent (Note, TimeStart, TimeEnd, DaySelect, DateStart, DateEnd, Name, Color)
                                        VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",

            dto.Note,
            SQLFormat.addDateTime(dto.TimeStart),
            SQLFormat.addDateTime(dto.TimeEnd),
            dto.DaySelect,
            SQLFormat.addDateTime(dto.DateStart),
            SQLFormat.addDateTime(dto.DateEnd),
            dto.Name,
            dto.Color);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            int newId = Convert.ToInt32(cmd.LastInsertedId);

            conn.Close();
            #endregion

            #region Insert Time Event Depend On Period
            //
            List<DayOfWeek> list = SQLFormat.formatDaySelect(dto.DaySelect);
            for (DateTime date = dto.DateStart; date <= dto.DateEnd; date = date.AddDays(1))
            {
                if (list.Contains(date.DayOfWeek))
                {
                    TimeEventDTO dtotemp = new TimeEventDTO();
                    dtotemp.PE_ID = newId;
                    dtotemp.Note = dto.Note;
                    dtotemp.TimeStart = dto.TimeStart;
                    dtotemp.TimeEnd = dto.TimeEnd;
                    dtotemp.DaySelect = date;
                    dtotemp.Color = dto.Color;
                    TimeEventDAO.Insert(dtotemp, conn);
                }
            }
            #endregion

            return newId;
        }
    }
}