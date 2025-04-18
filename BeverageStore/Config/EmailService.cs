using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BeverageStore.Config
{
    public class EmailService
    {
        private const string GOOGLE_EMAIL = "linhpn.work@gmail.com";
        private const string GOOGLE_PASSWORD = "paxz dstq gimh srly";
        public bool SendOtpEmail(string toEmail, string otp)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(GOOGLE_EMAIL); // Email người gửi
                mail.To.Add(toEmail); // Email người nhận
                mail.Subject = "Your OTP Code";
                mail.Body = $"Your OTP code is: {otp}. It is valid for 5 minutes.";

                // Cấu hình SmtpClient
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
                //{
                //    Port = 587, // Cổng SMTP (TLS)
                //    Credentials = new NetworkCredential(GOOGLE_EMAIL, GOOGLE_PASSWORD), // Thông tin đăng nhập
                //    EnableSsl = true, // Bật SSL/TLS
                //};
                smtpClient.Host = "smtp.gmail.com"; // Địa chỉ máy chủ SMTP
                smtpClient.Port = 587; // Cổng SMTP (TLS)
                smtpClient.Credentials = new NetworkCredential(GOOGLE_EMAIL, GOOGLE_PASSWORD); // Thông tin đăng nhập
                smtpClient.EnableSsl = true; // Bật SSL/TLS

                // Gửi email
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false;
            }
        }
    }
}
