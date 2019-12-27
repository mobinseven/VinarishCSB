using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace VinarishCsb.Shared
{
    public static class Utility
    {
        private static PersianCalendar pc = new PersianCalendar();

        public static string ConvertToFormattedPersianCalendar(DateTime dt)
        {
            StringBuilder sb = new StringBuilder();
            return sb.AppendFormat("{0}:{1} | {2}/{3}/{4}",
                pc.GetHour(dt), pc.GetMinute(dt), pc.GetDayOfMonth(dt), pc.GetMonth(dt), pc.GetYear(dt)).ToString();
        }
    }
}