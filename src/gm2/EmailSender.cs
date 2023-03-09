using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;

namespace System.gm
{
    public class EmailSender
    {
        public static void Send(string emailFrom, string emailTo, string subject, string message, bool isHtmlBody, Attachment attachment, string smtpAddress, int portNumber, string pwd)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                if (attachment != null)
                    mail.Attachments.Add(attachment);

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, pwd);
                    smtp.EnableSsl = false;
                    smtp.Send(mail);
                }
            }
        }

        public static void SendFile(string emailFrom, string emailTo, string subject, string message, bool isHtmlBody, string filePath, string smtpAddress, int portNumber, string pwd)
        {
            Attachment attachment = new Attachment(filePath);
            Send(emailFrom, emailTo, subject, message, isHtmlBody, attachment, smtpAddress, portNumber, pwd);
        }

        public static void SendFileBytes(string emailFrom, string emailTo, string subject, string message, bool isHtmlBody, string fileName, byte[] fileBytes, string smtpAddress, int portNumber, string pwd)
        {
            System.IO.MemoryStream ms = new IO.MemoryStream(fileBytes);
            Attachment attachment = new Attachment(ms, fileName);
            Send(emailFrom, emailTo, subject, message, isHtmlBody, attachment, smtpAddress, portNumber, pwd);
        }
    }
}