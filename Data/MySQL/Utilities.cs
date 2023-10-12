using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Globalization;
using System.Text.RegularExpressions;
using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json.Linq;

namespace N_Ter_Task_Custom.Data.MySQL
{
    public static class Utilities
    {
        public static bool IsValidEmail(string email)
        {
            //try
            //{
            //    var emailAddress = new MailAddress(email);
            //}
            //catch
            //{
            //    return false;
            //}

            //return true;

            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (email != null) return regex.IsMatch(email);
            else return false;
        }
        public static bool IsValidPhoneNumber(string phone)
        {
            Regex regex = new Regex(@"^\+\d{1,15}$");

            if (phone != null) return regex.IsMatch(phone);
            else return false;
        }
        public static bool IsValidInteger(dynamic value)
        {
            if (value is int intValue && intValue >= 0)
            {
                return true;
            }

            return false;
        }
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
