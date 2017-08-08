using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F21.BLOGGER.DataAccess
{
    public static class Common
    {
        public static string LongDate(string s)
        {
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(s, out dt))
            {
                return dt.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
            return s;
        }

        public static string ShortDate(string s)
        {
            DateTime dt = DateTime.MinValue;
            if (DateTime.TryParse(s, out dt))
            {
                return dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            }
            return s;
        }

        public static string MoneyFormat(string s)
        {
            return string.Format("{0:C2}", s); // $12,345.00
        }
    }
}
