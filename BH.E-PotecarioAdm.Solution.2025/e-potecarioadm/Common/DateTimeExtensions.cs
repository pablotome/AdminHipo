using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BH.EPotecario.Adm.Componentes
{
    public static class DateTimeExtensions
    {
        public static int Days360(this DateTime? date, DateTime? initialDate)
        {
            return Days360(date.Value, initialDate.Value);
        }

        public static int Days360(this DateTime date, DateTime initialDate)
        {
            var dateA = initialDate;

            var dateB = date;

            var dayA = dateA.Day;

            var dayB = dateB.Day;

            if (lastDayOfFebruary(dateA) && lastDayOfFebruary(dateB))
                dayB = 30;

            if (dayA == 31 && lastDayOfFebruary(dateA))
                dayA = 30;

            if (dayA == 30 && dayB == 31)
                dayB = 30;

            int days = (dateB.Year - dateA.Year) * 360 +
                ((dateB.Month + 1) - (dateA.Month + 1)) * 30 + (dayB - dayA);

            return days;

        }

        private static bool lastDayOfFebruary(DateTime date)
        {
            int lastDay = DateTime.DaysInMonth(date.Year, 2);

            return date.Day == lastDay;
        }
    }
}