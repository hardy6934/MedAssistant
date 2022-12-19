using MedAssistant.Core.Abstractions;
using System.Net;
using System.Net.Mail;

namespace MedAssistant.Buisness.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmailForRecoveryPassword(string email, string path)
        {
            try
            {
                MailMessage msg = new MailMessage();
                SmtpClient client = new SmtpClient();
                msg.Subject = $"Восстановление пароля для пользователя {email}";
                msg.Body = path + "для восстановления пароля перейдите по данной ссылке";
                msg.From = new MailAddress("mailer.aleksandar@yandex.com");
                msg.To.Add(email);
                msg.IsBodyHtml = true;
                client.Host = "smtp.yandex.com";
                NetworkCredential basicauthenticationinfo = new NetworkCredential("mailer.aleksandar@yandex.com", "124832qwe");
                client.UseDefaultCredentials = false;
                client.Port = int.Parse("587");
                client.EnableSsl = true; 
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;                
                client.Send(msg); 
                client.Dispose();
             }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
 

