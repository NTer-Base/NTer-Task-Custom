using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Globalization;

namespace N_Ter_Task_Custom.Data.MySQL
{
    public static class Utilities
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                return false;
            }

            return true;
        }
        public static bool IsValidPhoneNumber(string phone) { return false; }
        public static bool IsValidInteger(dynamic number) { return false; }
        public static bool IsValidDateRange(string firstDateStr, string secondDateStr)
        {
            DateTime firstDate;
            DateTime secondDate;

            if (DateTime.TryParseExact(firstDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out firstDate) &&
                DateTime.TryParseExact(secondDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out secondDate))
            {
                return secondDate > firstDate;
            }

            return false;
        }
        public static bool IsPassportValid(string dateStr)
        {
            DateTime date;

            if (DateTime.TryParseExact(dateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                DateTime today = DateTime.Today;
                TimeSpan gap = date - today;

                return gap.TotalDays > 7 * 30;
            }

            return false;
        }

    }
}
