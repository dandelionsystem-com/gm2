using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Globalization;

namespace System.gm
{
    public class IniWriter
    {
        public static void WriteIniFile(string filePath, string title, object anyClassObject)
        {
            string iniContent = GenerateIniContent(title, anyClassObject);
            File.WriteAllText(filePath, iniContent);
        }

        public static string GenerateIniContent(string title, object anyClassObject)
        {
            var fields = anyClassObject.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            StringBuilder sb = new StringBuilder();

            if (title != null && title.Length > 0)
                sb.AppendFormat("[{0}]", title);

            foreach (var fieldInfo in fields)
            {
                if (fieldInfo.Name.StartsWith("<"))
                    continue;

                if (fieldInfo.Name.StartsWith("_"))
                    continue;

                object obValue = fieldInfo.GetValue(anyClassObject);

                sb.AppendLine();

                if (obValue.GetType() == typeof(Boolean))
                    sb.Append(fieldInfo.Name + "=" + Convert.ToInt32(obValue));
                else if (obValue.GetType() == typeof(DateTime))
                    sb.Append(fieldInfo.Name + "=" + ((DateTime)obValue).ToString("dd-MM-yyyy"));
                else
                    sb.Append(fieldInfo.Name + "=" + obValue);
            }
            return sb.ToString();
        }

        public static string GenerateIniContent(string title, Dictionary<string,string> dic)
        {
            StringBuilder sb = new StringBuilder();

            if (title != null && title.Length > 0)
                sb.AppendFormat("[{0}]", title);

            foreach(var kv in dic)
            {
                sb.AppendLine();
                sb.Append(kv.Key);
                sb.Append("=");
                sb.Append(kv.Value);
            }

            return sb.ToString();
        }

        public static string GenerateIniContentPublicField(string title, object anyClassObject)
        {
            var fields = anyClassObject.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            var properties = anyClassObject.GetType().GetProperties();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("[" + title + "]");
            foreach (var fieldInfo in fields)
            {
                if (fieldInfo.Name.StartsWith("<"))
                    continue;

                if (fieldInfo.Name.StartsWith("_"))
                    continue;

                object obValue = fieldInfo.GetValue(anyClassObject);

                if (obValue.GetType() == typeof(Boolean))
                    sb.AppendLine(fieldInfo.Name + "=" + Convert.ToInt32(obValue));
                else if (obValue.GetType() == typeof(DateTime))
                    sb.AppendLine(fieldInfo.Name + "=" + ((DateTime)obValue).ToString("dd-MM-yyyy"));
                else
                    sb.AppendLine(fieldInfo.Name + "=" + obValue);
            }
            foreach (var fieldInfo in properties)
            {
                if (fieldInfo.Name.StartsWith("<"))
                    continue;

                if (fieldInfo.Name.StartsWith("_"))
                    continue;

                object obValue = fieldInfo.GetValue(anyClassObject);

                if (obValue.GetType() == typeof(Boolean))
                    sb.AppendLine(fieldInfo.Name + "=" + Convert.ToInt32(obValue));
                else if (obValue.GetType() == typeof(DateTime))
                    sb.AppendLine(fieldInfo.Name + "=" + ((DateTime)obValue).ToString("dd-MM-yyyy"));
                else
                    sb.AppendLine(fieldInfo.Name + "=" + obValue);
            }
            return sb.ToString();
        }

        public static T ReadIniFile<T>(string filePath)
        {
            string iniContent = "";
            try
            {
                iniContent = File.ReadAllText(filePath);
            }
            catch { }
            return ReadIniContent<T>(iniContent);
        }

        public static T ReadIniContent<T>(string iniContent)
        {
            var ob = Activator.CreateInstance<T>();

            string[] lines = iniContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (string s in lines)
            {
                string a = s.Trim();

                if (a.Length == 0)
                    continue;

                if (s.StartsWith(";") ||
                    s.StartsWith("#") ||
                    s.StartsWith("["))
                {
                    continue;
                }

                if (!s.Contains("="))
                {
                    continue;
                }

                int splitPoint = s.IndexOf('=');

                string b = s.Substring(0, splitPoint);
                string c = s.Substring(splitPoint + 1);

                dic[b] = c;
            }

            var fields = ob.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (var fieldInfo in fields)
            {
                if (fieldInfo.Name.StartsWith("<"))
                    continue;

                if (fieldInfo.Name.StartsWith("_"))
                    continue;

                if (dic.ContainsKey(fieldInfo.Name))
                {
                    fieldInfo.SetValue(ob, GetValue(dic[fieldInfo.Name], fieldInfo.FieldType));
                }
            }

            return (T)ob;
        }

        static object GetValue(string s, Type targetType)
        {
            if (targetType == null)
            {
                return null;
            }
            else if (targetType == typeof(String))
            {
                return s + "";
            }
            else if (targetType == typeof(DateTime))
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
            else if (targetType == typeof(bool))
            {
                return dp.BoolParse(s);
            }
            else if (targetType == typeof(short))
            {
                short i = 0;
                short.TryParse(s + "", out i);
                return i;
            }
            else if (targetType == typeof(int))
            {
                int i = 0;
                int.TryParse(s + "", out i);
                return i;
            }
            else if (targetType == typeof(long))
            {
                long i = 0;
                long.TryParse(s + "", out i);
                return i;
            }
            else if (targetType == typeof(ushort))
            {
                ushort i = 0;
                ushort.TryParse(s + "", out i);
                return i;
            }
            else if (targetType == typeof(uint))
            {
                uint i = 0;
                uint.TryParse(s + "", out i);
                return i;
            }
            else if (targetType == typeof(ulong))
            {
                ulong i = 0;
                ulong.TryParse(s + "", out i);
                return i;
            }
            else if (targetType == typeof(double))
            {
                double i = 0;
                double.TryParse(s + "", out i);
                return i;
            }
            else if (targetType == typeof(decimal))
            {
                decimal i = 0m;
                decimal.TryParse(s + "", out i);
                return i;
            }
            else if (targetType == typeof(float))
            {
                float i = 0;
                float.TryParse(s + "", out i);
                return i;
            }
            else if (targetType == typeof(byte))
            {
                byte i = 0;
                byte.TryParse(s + "", out i);
                return i;
            }
            else if (targetType == typeof(sbyte))
            {
                sbyte i = 0;
                sbyte.TryParse(s + "", out i);
                return i;
            }

            throw new Exception("Data Type is not supported in this INI Writer parsing. Data Type=" + targetType);
        }

        public static void WriteIniFile(string filepath, string title, Dictionary<string, string> dic)
        {
            StringBuilder sb = new StringBuilder();
            if (title != null && title.Length > 0)
            {
                sb.AppendFormat("[{0}]", title);
            }

            foreach(var kv in dic)
            {
                sb.AppendLine();
                sb.Append(kv.Key);
                sb.Append("=");
                sb.Append(kv.Value);
            }

            File.WriteAllText(filepath, sb.ToString());
        }

        public static Dictionary<string,string> ReadIniContent(string iniContent)
        {
            string[] ia = iniContent.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach(var s in ia)
            {
                if (s != null && s.Length>0)
                {
                    string[] sa = s.Split('=');
                    if (sa.Length < 2)
                        continue;

                    string data = "";
                    string key = sa[0];
                    for(int i =1;i<sa.Length;i++)
                    {
                        if (data.Length > 0)
                            data += "=";
                        data += sa[i];
                    }
                    dic[key] = data;
                }
            }

            return dic;
        }
    }
}
