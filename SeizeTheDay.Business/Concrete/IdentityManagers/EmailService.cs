using Microsoft.AspNet.Identity;
using System;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace SeizeTheDay.Business.Concrete.IdentityManagers
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return ConfigSendGridasyncAsync(message);
        }

        private async Task ConfigSendGridasyncAsync(IdentityMessage message)
        {
            MailMessage msg = new MailMessage
            {
                From = new MailAddress("ismaildogaan@gmail.com")
            };
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message.Body, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(message.Body, null, MediaTypeNames.Text.Html));

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("ismaildogaan@gmail.com", "Password");
            smtpClient.Credentials = credentials;
            smtpClient.EnableSsl = true;
            smtpClient.Send(msg);
        }
    }
}
