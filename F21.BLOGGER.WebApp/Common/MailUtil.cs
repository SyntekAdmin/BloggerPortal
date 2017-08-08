using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace F21.BLOGGER.WebApp.Common
{
    public class MailUtil
    {
        public static bool SendMail(
            string smtpServer,
            int smtpPort,
            bool smtpAuthentication,
            string smtpUserName,
            string smtpPassword,
            string mailFrom,
            string mailTo,
            string subject,
            string body)
        {

            // asp.net 2.0은 ';'을 지원하지 않으므로 이를 ','로 치환한다.
            mailTo = mailTo.Replace(";", ",");

            // MailMessage 개체 설정
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(mailFrom, mailTo);

            // Message 상세 설정
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            // SmtpServer/SmtpClient 설정
            System.Net.Mail.SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = smtpServer;
            if (smtpPort != null)
                smtpClient.Port = (int)smtpPort;

            if (smtpAuthentication)
            {
                if (!String.IsNullOrEmpty(smtpUserName) && !String.IsNullOrEmpty(smtpPassword))
                {
                    smtpClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);
                }
            }
            else
            {
                smtpClient.UseDefaultCredentials = true;
            }

            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
