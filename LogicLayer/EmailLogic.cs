using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace Ecommerce.LogicLayer
{
    public class EmailLogic
    {
        public void Index(string To, string Subject, string Message)
        {


            var SenderEmail = new MailAddress("andilemadlalasa@gmail.com", "Andiles merch store");
            var recieverEmail = new MailAddress(To, "Reciever");

            var Password = $"Captivated144";
            var sub = Subject;
            var body = Message;

            //var smtp = new SmtpClient

            SmtpClient client = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                //Credentials = new NetworkCredential(SenderEmail.Address, Password)

                Credentials = new NetworkCredential("andilemadlalasa@gmail.com", "Captivated144")

            };


            MailMessage mailMessage = new MailMessage();
            mailMessage.From = SenderEmail;
            mailMessage.To.Add(recieverEmail);
            mailMessage.Subject = Subject;
            mailMessage.Body = body;

            client.Send(mailMessage);


            //using (var mess = new MailMessage(SenderEmail, recieverEmail)
            //{
            //    Subject = Subject,
            //    Body = body
            //}
            //    )

            //{
            //    smtp.Send(mess);
            //}


        }



    }
}
