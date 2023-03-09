using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace System.gm
{
    public class DateFunc
    {
        public static void GetDifference(DateTime date1, DateTime date2, out int year, out int month, out int day)
        {
            year = 0;
            month = 0;
            day = 0;

            int y1 = date1.Year;
            int m1 = date1.Month;
            int d1 = date1.Day;

            int y2 = date2.Year;
            int m2 = date2.Month;
            int d2 = date2.Day;

            if (d1 < d2)
            {
                d1 = d1 + DateTime.DaysInMonth(y1, m1);
                m1 = m1 - 1;
            }

            day = d1 - d2;

            while (m1 < m2)
            {
                y1 = y1 - 1;
                m1 = m1 + 12;
            }

            month = m1 - m2;

            year = y1 - y2;
        }

        public static string GetMonthShortName(int month)
        {
            if (month > 0 && month < 13)
            {
                return GetMonthFullName(month).Substring(0, 3);
            }

            return "";
        }

        public static string GetMonthFullName(int month)
        {
            switch (month)
            {
                case 1: return "January";
                case 2: return "February";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "August";
                case 9: return "September";
                case 10: return "October";
                case 11: return "November";
                case 12: return "December";
                default: return "";
            }
        }

        //enum Phase { Years, Months, Days, Done }

        public static DataTable GetHourTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            for (int i = 1; i < 13; i++)
            {
                dt.Rows.Add(i.ToString());
            }
            return dt;
        }

        public static DataTable GetMinuteTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            for (int i = 0; i < 60; i++)
            {
                dt.Rows.Add(i.ToString().PadLeft(2, '0'));
            }
            return dt;
        }

        public static DataTable GetMonthTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("name");

            dt.Rows.Add(0, "---");

            for (int i = 1; i < 13; i++)
            {
                dt.Rows.Add(i, GetMonthFullName(i));
            }

            return dt;
        }

        public static DataTable GetAMPMTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Rows.Add("AM");
            dt.Rows.Add("PM");
            return dt;
        }
    }
}