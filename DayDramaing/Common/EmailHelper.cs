using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;

namespace DayDramaing.Common
{
    public static class EmailHelper
    {
        public static void SendBookingEmail(string body)
        {
            var to = ConfigurationManager.AppSettings["BOOKING_EMAIL"];
            SendEmail(to, "Booking Form", body);   
        }
        public static void SendContactEmail(string body)
        {
            var to = ConfigurationManager.AppSettings["CONTACT_EMAIL"];
            SendEmail(to, "Contact Form:", body);
        }

        public static void SendEmail(string to, string subject, string body)
        {
            var from = ConfigurationManager.AppSettings["MAILGUN_SMTP_LOGIN"];

            //send msg
            var smtpClient = new SmtpClient();
            var msg = new MailMessage(from, to, subject, body);
            smtpClient.Send(msg);
        }
    }
}