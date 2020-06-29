using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aeveco.Email.Service.Models
{
    public interface IEmailMessage
    {
        IList<Models.IEmailContact> Recipients { get; set; }

        Models.IEmailContact Sender { get; set; }

        string Subject { get; }

        string Message { get; }

        string HtmlMessage { get; }

        Task SendEmailAsync(List<IEmailContact> recipients, IEmailContact from, string subject, string message);
    }
}
