using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Reflection.Metadata.Ecma335;

namespace SimpleEmailApp.Controllers
{

  
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            this._emailService = emailService;
        }



        /// <summary>
        /// Send email in this function
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SendEmail(string body)
        {
            var email = new MimeMessage();
            //sender
            email.From.Add(MailboxAddress.Parse("benton.lowe45@ethereal.email"));
            //reciever
            email.To.Add(MailboxAddress.Parse("benton.lowe45@ethereal.email"));
            //subject of the email
            email.Subject = "Test Email Subject";
            //body of the email type html
            email.Body = new TextPart(TextFormat.Html){ Text = body  };

            //using smtp service from MailKit
            using var smtp = new SmtpClient();
            //connect to mail server
            //smtp.gmail.email for gmail
            //smtp.office365.email for office 365
            smtp.Connect("smtp.ethereal.email" , 587 , SecureSocketOptions.StartTls);
            //Authentification to mail server
            smtp.Authenticate("benton.lowe45@ethereal.email", "b6sxeScf7fHbmSZM6X");
            
            //send email
            smtp.Send(email);

            smtp.Disconnect(true);

            return Ok();
        }

        /// <summary>
        /// Send email in this function
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("SendMeMail")]
        public IActionResult SendMeMail(EmailDto email)
        {
             _emailService.SendEmail(email);

            return Ok();
        }
    }
}
