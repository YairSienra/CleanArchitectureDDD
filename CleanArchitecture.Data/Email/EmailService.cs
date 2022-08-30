using CleanArchitecture.Application.Contracts.Infrastucture;
using CleanArchitecture.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastucture.Email
{
    public class EmailService : IEmailService
    {
        public EmailSettings _emailSettins { get; }
        public ILogger<EmailService> logger { get; }

        public EmailService(IOptions<EmailSettings> emailSettins, ILogger<EmailService> logger)
        {
            _emailSettins = emailSettins.Value;
            logger = logger;
        }

        public async Task<bool> SendEmail(Application.Models.Email email)
        {
            var client = new SendGridClient(_emailSettins.Apikey);
            var subject = email.Asunto;
            var to = new EmailAddress(email.To);
            var body = email.body;


            var from = new EmailAddress()
            {
                Email = _emailSettins.FromAddress,
                Name = _emailSettins.FromName,
            };
            var sendGridMessege = MailHelper.CreateSingleEmail(from, to, subject, body, body);

            var response = await client.SendEmailAsync(sendGridMessege);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted || response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }

            logger.LogError("El email no se pudo enviar");
            return false;

           
        }
    }
}
