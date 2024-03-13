using System.Net.Mail;
using System.Net;
using N.Extensions;
using System.Web;

namespace N.Core.Email
{
    public class EmailProvider
    {
        public static bool SendEmail(string body, string subject, string address)
        {
            try
            {
                SmtpClient server = new SmtpClient();
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(AppSettings.Mail.From, AppSettings.Mail.Alias);
                    mail.To.Add(address);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                    using (SmtpClient smtp = new SmtpClient(AppSettings.Mail.Host, AppSettings.Mail.Port))
                    {
                        smtp.Credentials = new NetworkCredential(AppSettings.Mail.From, AppSettings.Mail.Password);
                        smtp.EnableSsl = AppSettings.Mail.EnableSsl;
                        smtp.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SendMailCofirmEmail(string address, string token, string baseUri)
        {
            var body = GetMailBody("Register.html");
            var url = $"{baseUri}confirmEmail?email={HttpUtility.UrlEncode(address)}&token={HttpUtility.UrlEncode(token)}";
            body = body.Replace("[[url]]", url);
            return SendEmail(body, "Confirm email", address);
        }


        public static bool SendMailResetPassword(string address, string password, string baseUri)
        {
            var body = GetMailBody("ResetPassword.html");
            body = body.Replace("[[password]]", password);
            return SendEmail(body, "Reset password", address);
        }

        private static string GetMailBody(string fileName)
        {
            return File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), $"Core/Email/Templete/{fileName}"));
        }
    }
}
