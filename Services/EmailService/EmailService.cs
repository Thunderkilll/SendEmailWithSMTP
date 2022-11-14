using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace SimpleEmailApp.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        /// <summary>
        /// Class constractor with IConfiguration interface as aprams
        /// to use the appsettings we need to initialize the IConfiguration here!
        /// </summary>
        /// <param name="config"></param>
        public EmailService(IConfiguration config) {
            this._config = config;
        }   
        /// <summary>
        /// Send an email using dep injection $
        /// we call the appSettings file to get email host 
        /// sender's password and username
        /// this is a test version
        /// </summary>
        /// <param name="emailDto"></param>
        public void SendEmail(EmailDto emailDto)
        {
            var email = new MimeMessage();
            //sender
            email.From.Add(MailboxAddress.Parse(emailDto.Sender));
            //reciever
            email.To.Add(MailboxAddress.Parse(emailDto.Reciever));
            //subject of the email
            email.Subject = emailDto.Subject;
            //body of the email type html
            email.Body = new TextPart(TextFormat.Html) { Text = emailDto.Body };

            //using smtp service from MailKit
            using var smtp = new SmtpClient();
            //connect to mail server
            //smtp.gmail.email for gmail
            //smtp.office365.email for office 365
            smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            //Authentification to mail server
            smtp.Authenticate(_config.GetSection("EmailUserName").Value, _config.GetSection("EmailPassword").Value);

            //send email
            smtp.Send(email);

            smtp.Disconnect(true);
        }
    }
}
