using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public class dp
    {
        public static decimal DecimalParse(object ob)
        {
            if (ob == null)

                return 0m;
            if (ob is DBNull)
                return 0m;

            Type t = ob.GetType();

            if (t == typeof(decimal))
            {
                return (decimal)ob;
            }

            if (t == typeof(bool))
            {
                if ((bool)ob == true)
                    return 1m;
                else
                    return 0m;
            }

            decimal a = 0;

            decimal.TryParse(ob + "", out a);

            return a;
        }

        public static bool BoolParse(object ob)
        {
            if (ob == null)
                return false;

            if (ob is DBNull)
                return false;

            Type t = ob.GetType();

            if (t == typeof(bool))
            {
                return (bool)ob;
            }

            if (
                t == typeof(byte) ||
                t == typeof(sbyte) ||
                t == typeof(short) ||
                t == typeof(ushort) ||
                t == typeof(int) ||
                t == typeof(uint) ||
                t == typeof(long) ||
                t == typeof(ulong) ||
                t == typeof(float) ||
                t == typeof(double) ||
                t == typeof(decimal))
            {
                return Convert.ToBoolean(ob);
            }

            if ((ob + "") == "TRUE")
                return true;

            return false;
        }

        public static int IntParse(object ob)
        {
            if (ob == null)
                return 0;

            if (ob is DBNull)
                return 0;

            Type t = ob.GetType();

            if (t == typeof(bool))
            {
                if (((bool)ob) == true)
                    return 1;
                else
                    return 0;
            }

            if (
                t == typeof(byte) ||
                t == typeof(sbyte) ||
                t == typeof(short) ||
                t == typeof(ushort) ||
                t == typeof(int) ||
                t == typeof(uint) ||
                t == typeof(long) ||
                t == typeof(ulong) ||
                t == typeof(float) ||
                t == typeof(double) ||
                t == typeof(decimal))
            {
                return Convert.ToInt32(ob);
            }

            int i = 0;

            int.TryParse(ob + "", out i);

            return i;
        }

        public static DateTime DateParse(object ob)
        {
            if (ob == null)
                return DateTime.MinValue;

            if (ob is System.DBNull)
                return DateTime.MinValue;

            if (ob.GetType() == typeof(DateTime))
            {
                return (DateTime)ob;
            }

            DateTime dtime = DateTime.MinValue;

            DateTime.TryParse(ob + "", out dtime);
            return dtime;
        }

        public static string DateParseMySQLString(string s)
        {
            DateTime d = DateTime.MinValue;
            DateTimeFormatInfo df1 = new DateTimeFormatInfo();
            df1.DateSeparator = "-";
            DateTimeFormatInfo df2 = new DateTimeFormatInfo();
            df2.DateSeparator = "/";
            DateTimeFormatInfo df3 = new DateTimeFormatInfo();
            df3.DateSeparator = "\\";
            DateTimeFormatInfo df4 = new DateTimeFormatInfo();
            df4.DateSeparator = ".";

            if (DateTime.TryParseExact(s, "dd-MM-yyyy", df1, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d.ToString("yyyy-MM-dd 00:00:00");
            }

            if (DateTime.TryParseExact(s, "dd-MM-yyyy", df2, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d.ToString("yyyy-MM-dd 00:00:00");
            }

            if (DateTime.TryParseExact(s, "dd-MM-yyyy", df3, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d.ToString("yyyy-MM-dd 00:00:00");
            }

            if (DateTime.TryParseExact(s, "dd-MM-yyyy", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d.ToString("yyyy-MM-dd 00:00:00");
            }

            return "0001-01-01 00:00:00";
        }

        public static string DateParseToHumanize(object ob)
        {
            DateTime dtime = DateTime.MinValue;
            if (DateTime.TryParse(ob + "", out dtime))
            {
                if (dtime == DateTime.MinValue)
                    return "";
                else
                    return dtime.ToString("dd-MM-yyyy");
            }
            else
                return "";
        }

        /// <summary>
        /// Convert string "dd-MM-yyyy" to DateTime
        /// </summary>
        /// <param name="s">dd-MM-yyyy</param>
        /// <returns>DateTime</returns>
        public static DateTime DateParseExact(string s)
        {
            DateTime d = DateTime.MinValue;
            DateTimeFormatInfo df1 = new DateTimeFormatInfo();
            df1.DateSeparator = "-";
            DateTimeFormatInfo df2 = new DateTimeFormatInfo();
            df2.DateSeparator = "/";
            DateTimeFormatInfo df3 = new DateTimeFormatInfo();
            df3.DateSeparator = "\\";
            DateTimeFormatInfo df4 = new DateTimeFormatInfo();
            df4.DateSeparator = ".";

            if (DateTime.TryParseExact(s, "dd-MM-yyyy", df1, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd/MM/yyyy", df2, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd\\MM\\yyyy", df3, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd.MM.yyyy", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            // =============================================

            if (DateTime.TryParseExact(s, "d-M-yyyy", df1, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd-M-yyyy", df1, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d-MM-yyyy", df1, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d/M/yyyy", df2, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d/MM/yyyy", df2, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd/M/yyyy", df2, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d\\M\\yyyy", df3, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d\\MM\\yyyy", df3, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd\\M\\yyyy", df3, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d.M.yyyy", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "d.MM.yyyy", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd.M.yyyy", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Convert string "yyyy-MM-dd" to DateTime. For HTML input=date
        /// </summary>
        /// <param name="s">yyyy-MM-dd</param>
        /// <returns>DateTime</returns>
        public static DateTime DateParseExactReverse(string s)
        {
            if (s == null)
                return DateTime.MinValue;

            if (s.Length == 0)
            {
                return DateTime.MinValue;
            }
            try
            {
                string[] sa = s.Split(new char[] { '-', '.', '/', '\\', 'T', ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (sa.Length > 2)
                {
                    int year = dp.IntParse(sa[0]);
                    int month = dp.IntParse(sa[1]);
                    int day = dp.IntParse(sa[2]);

                    int hour = 0;
                    int minute = 0;
                    int second = 0;

                    if (sa.Length > 4)
                    {
                        hour = dp.IntParse(sa[3]);
                        minute = dp.IntParse(sa[4]);
                    }

                    if (sa.Length > 5)
                    {
                        second = dp.IntParse(sa[5]);
                    }

                    DateTime date = new DateTime(year, month, day, hour, minute, second);

                    return date;
                }
            }
            catch
            { }
            return DateTime.MinValue;
        }

        public static DateTime DateParseInput(string s)
        {
            return DateParseExactReverse(s);
        }

        public static DateTime DateTimeParseInput(string s)
        {
            try
            {
                if (s == null || s.Length == 0)
                    return DateTime.MinValue;

                string[] da = s.Split(new char[] { '-', 'T', ':' }, StringSplitOptions.RemoveEmptyEntries);

                if (da.Length < 5)
                {
                    return DateTime.MinValue;
                }

                int year = dp.IntParse(da[0]);
                int month = dp.IntParse(da[1]);
                int day = dp.IntParse(da[2]);
                int hour = dp.IntParse(da[3]);
                int minute = dp.IntParse(da[4]);

                DateTime date = new DateTime(year, month, day, hour, minute, 0);

                return date;
            }
            catch { }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Convert string "dd-MM-yyyy hh:mm tt" to DateTime
        /// </summary>
        /// <param name="s">dd-MM-yyyy hh:mm tt</param>
        /// <returns>DateTime</returns>
        public static DateTime DateTimeParseExact(string s)
        {
            DateTime d = DateTime.MinValue;
            DateTimeFormatInfo df1 = new DateTimeFormatInfo();
            df1.DateSeparator = "-";
            df1.TimeSeparator = ":";
            df1.PMDesignator = "pm";
            df1.AMDesignator = "am";

            DateTimeFormatInfo df2 = new DateTimeFormatInfo();
            df2.DateSeparator = "/";
            df2.TimeSeparator = df1.TimeSeparator;
            df2.PMDesignator = df1.PMDesignator;
            df2.AMDesignator = df1.AMDesignator;

            DateTimeFormatInfo df3 = new DateTimeFormatInfo();
            df3.DateSeparator = "\\";
            df3.TimeSeparator = df1.TimeSeparator;
            df3.PMDesignator = df1.PMDesignator;
            df3.AMDesignator = df1.AMDesignator;

            DateTimeFormatInfo df4 = new DateTimeFormatInfo();
            df4.DateSeparator = ".";
            df4.TimeSeparator = df1.TimeSeparator;
            df4.PMDesignator = df1.PMDesignator;
            df4.AMDesignator = df1.AMDesignator;

            if (DateTime.TryParseExact(s, "dd-MM-yyyy hh:mm tt", df1, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd/MM/yyyy hh:mm tt", df2, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd\\MM\\yyyy hh:mm tt", df3, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            if (DateTime.TryParseExact(s, "dd.MM.yyyy hh:mm tt", df4, DateTimeStyles.AllowWhiteSpaces, out d))
            {
                return d;
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// Convert string "yyyy-MM-dd HH:mm" to DateTime. For HTML input=datetime-local
        /// </summary>
        /// <param name="s">yyyy-MM-ddTHH:mm</param>
        /// <returns>DateTime</returns>
        public static DateTime DateTimeLocalParse(string s)
        {
            if (s == null)
                return DateTime.MinValue;

            string[] sa = s.Split(new char[] { 'T', '-', ':' });

            if (sa.Length != 5)
                return DateTime.MinValue;

            int yr = dp.IntParse(sa[0]);
            int month = dp.IntParse(sa[1]);
            int day = dp.IntParse(sa[2]);
            int hour = dp.IntParse(sa[3]);
            int min = dp.IntParse(sa[4]);

            return new DateTime(yr, month, day, hour, min, 0);
        }

        /// <summary>
        /// Return the int's ordinal extension.
        /// </summary>
        /// <param name="value">The number to be converted</param>
        /// <returns>The result</returns>
        public static string ConvertToOrdinal(int num)
        {
            string output = num.ToString();
            if (output.EndsWith("11")) return output + "th";
            if (output.EndsWith("12")) return output + "th";
            if (output.EndsWith("13")) return output + "th";
            if (output.EndsWith("1")) return output + "st";
            if (output.EndsWith("2")) return output + "nd";
            if (output.EndsWith("3")) return output + "rd";
            return output + "th";
        }
    }
}
