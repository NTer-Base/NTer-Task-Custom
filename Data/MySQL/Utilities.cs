using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;

namespace N_Ter_Task_Custom.Data.MySQL
{
    public static class Utilities
    {
        public static bool IsValidEmail(string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }
        public static bool IsValidPhoneNumber(string phone) { return false; }
        public static bool IsValidInteger(dynamic number) { return false; }
        public static bool IsValidDateRange(string date) { return false; }
    }
}
