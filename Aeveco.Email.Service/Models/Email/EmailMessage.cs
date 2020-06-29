using Antlr4.StringTemplate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Aeveco.Email.Service.Models
{
    public abstract class EmailMessage: IEmailMessage
    {
        public EmailMessage(IOptions<SendGridOptions> sendGridOptions, IWebHostEnvironment env)
        {
            options = sendGridOptions;
            environment = env;
        }

        private readonly IWebHostEnvironment environment;
        private readonly IOptions<SendGridOptions> options; //set in the startup from the appsettings

        /// <summary>
        /// Force all objects to set their template name
        /// There are other ways to do this.  Extra credit for 
        /// using object name like a Razor template
        /// </summary>
        public abstract string TemplateName { get; }

        string templateFolder;
        public string TemplateFolder
        {
            get
            {
                if (string.IsNullOrWhiteSpace(templateFolder))
                {
                    templateFolder = Path.Combine(environment.ContentRootPath, "EmailTemplates");
                }
                return templateFolder;
            }
        }

        /// <summary>
        /// Everyone getting this email
        /// </summary>
        public IList<Models.IEmailContact> Recipients { get; set; }

        /// <summary>
        /// Contact that is sending this email
        /// </summary>
        public Models.IEmailContact Sender { get; set; }

        public abstract string Subject { get; }

        public string Message { get; set; }

        public string HtmlMessage {
            get {

                return getEmailHTML();
            }
        }

        private string getEmailHTML()
        {
            Template layout = null;
            TemplateGroup group = new TemplateRawGroupDirectory(TemplateFolder, '$', '$');

            layout = group.GetInstanceOf(TemplateName);

            layout.Add("email", this);

            //layout.Add("style", this.Hub.GetStyleSheet());

            return layout.Render();
        }

        public Task SendEmailAsync(List<IEmailContact> recipients, IEmailContact sender, string subject, string message)
        {
            // create HTML and Text versions of the email

            return Send(subject, message, recipients, sender);
        }

        public Task Send(string subject, string message, List<IEmailContact> recipients, IEmailContact sender)
        {
            var client = new SendGridClient(options.Value.SendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(sender.EmailAddress, sender.DisplayName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = HtmlMessage
            };

            foreach (var recipient in recipients)
            {
                msg.AddTo(new EmailAddress(recipient.EmailAddress, recipient.DisplayName));
            }

            Task response = client.SendEmailAsync(msg);
            return response;
        }
    }
}
