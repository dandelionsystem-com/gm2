using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.gm
{
    public class Address
    {
        public enum AddressMode
        {
            Line,
            Paragraph
        }

        public static string GetFullAddress(string adr1, string adr2, string adr3, string postcode, string city, string state, string country, AddressMode mode)
        {
            string separator = "";
            if (mode == AddressMode.Line)
                separator = ", ";
            else if (mode == AddressMode.Paragraph)
                separator = "\r\n";

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(adr1);

            if ((adr2 + "").Length > 0)
            {
                if (sb.Length > 0 && adr2.Length > 0)
                    sb.AppendFormat(separator);
                sb.AppendFormat(adr2);
            }

            if ((adr3 + "").Length > 0)
            {
                if (sb.Length > 0 && adr3.Length > 0)
                    sb.AppendFormat(separator);
                sb.AppendFormat(adr3);
            }

            if ((postcode + "").Length > 0)
            {
                if (sb.Length > 0 && postcode.Length > 0)
                    sb.AppendFormat(separator);
                sb.AppendFormat(postcode);
            }

            if ((city + "").Length > 0)
            {
                if (postcode.Length > 0 && city.Length > 0)
                    sb.AppendFormat(" " + city);
                else if (sb.Length > 0 && city.Length > 0)
                {
                    sb.AppendFormat(separator);
                    sb.AppendFormat(city);
                }
            }

            if ((state + "").Length > 0)
            {
                if (sb.Length > 0 && state.Length > 0)
                    sb.AppendFormat(separator);
                sb.AppendFormat(state);
            }

            if ((country + "").Length > 0)
            {
                if (sb.Length > 0 && country.Length > 0)
                    sb.AppendFormat(separator);
                sb.AppendFormat(country);
            }

            return sb.ToString();
        }
    }
}
