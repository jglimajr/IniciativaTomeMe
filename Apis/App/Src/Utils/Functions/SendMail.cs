using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace InteliSystem.Utils.Functions
{
    public class SendMail
    {
        // private const string PassWord = "uV*CM9mV";
        private const string EMailPadrao = "donotreply@InteliSystem.app";
        // private const string SmtpHost = "smtp-relay.gmail.com";
        // private const int SmtpPort = 587;
        private SmtpClient _smtpclient;

        public SendMail()
        {
            To = new List<string>();
            Cc = new List<string>();
            _smtpclient = new SmtpClient()
            {
                Host = "email-smtp.us-east-1.amazonaws.com",
                Port = 587,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("AKIAR23KYMADQYM57U5T", "BMDK4lOe0s3XDlx/agjXffrkrKPayf3KkXL3qYuYH3Yj"),
            };
        }

        public string From { get; set; } = EMailPadrao;
        //public string To { get; set; }
        public IList<string> To { get; set; }
        //public string Cc{ get; set; }
        public IList<string> Cc { get; set; }
        public string Subject { get; set; } = "";
        public string Body { get; set; }
        // public string FromPassWord { get; set; } = PassWord;
        // public string Host { get; set; } = SmtpHost;
        // public int Port { get; set; } = SmtpPort;

        public bool Send()
        {
            MailMessage email = new MailMessage();
            // SmtpClient client = new SmtpClient();
            try
            {
                email.From = new MailAddress(From);
                To.ToList().ForEach(t =>
                {
                    email.To.Add(new MailAddress(t));
                });

                if (Cc.Count > 0)
                {
                    Cc.ToList().ForEach(c =>
                    {
                        email.CC.Add(new MailAddress(c));
                    });
                }
                email.Subject = Subject;
                email.IsBodyHtml = true;
                email.Body = Body;

                // client.Port = Port;
                // client.Host = SmtpHost;
                // client.EnableSsl = true;
                // client.UseDefaultCredentials = false;
                // client.Credentials = new NetworkCredential(From, FromPassWord);
                // client.DeliveryMethod = SmtpDeliveryMethod.Network;
                this._smtpclient.Send(email);

                return true;
            }
            catch (System.Exception e)
            {
                throw new System.Exception($"Error to Send E-Mail:{e.Message}", e);
            }
            finally
            {
                this._smtpclient.Dispose();
                email.Dispose();
            }
        }
    }
}