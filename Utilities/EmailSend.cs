using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ContactUsApplication.Helpers;
using ContactUsApplication.Constants;

namespace ContactUsApplication.Utilities
{
    public class EmailSend : IEmailSend
    {
        private readonly EmailConstants _emailConstants;
        private IOptions<OutlookParams> _outlookParams;
        public EmailSend(IOptions<OutlookParams> outlookPramas)
        {
            _emailConstants = new EmailConstants();
            _outlookParams = outlookPramas;
            
        }

        public async Task<bool> SendEmail(string name, string enquirersEmail, string? notes, IFormFile? file)
        {
            // Setting up SMPT client
            var client = new SmtpClient(_emailConstants.EmailSMTPServer, _emailConstants.EmailSMTPPort)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_emailConstants.EmailFrom, _outlookParams.Value.Password)
            };
            
            try
            {
                // Setting email details
                var mailContent = new MailMessage(
                                from: _emailConstants.EmailFrom,
                                to: _emailConstants.EmailTo,
                                subject: _emailConstants.EmailSubject,
                                body: _emailConstants.createEmailBodyContent(name, enquirersEmail, notes)
                                );
                if (file != null)
                {
                    Attachment attachment = new Attachment(file.OpenReadStream(), file.FileName);
                    mailContent.Attachments.Add(attachment);
                }                
                
                // Sending Email
                await client.SendMailAsync(mailContent);
                return true;
            }
            catch (Exception ex)
            {
                return false;              
            }
        }
    }
}
