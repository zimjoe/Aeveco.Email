using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aeveco.Email.Service.Models.Email
{
    public class HappyEmailMessage: EmailMessage, IEmailMessage
    {
        public HappyEmailMessage(IOptions<SendGridOptions> sendGridOptions, IWebHostEnvironment env) : base(sendGridOptions, env)
        {

           
        }
        /// <summary>
        /// This can be sniffed like Razor does (convention over configuration) and then overridden for other occasions
        /// </summary>
        public override string TemplateName { 
            get {
                return "HappyEmail";
            } 
        }

        public override string Subject { get {
                return "Happy Email Message";
            } 
        }

        public override string Message { get; }

        public override string HtmlMessage { get; }
    }
}
