using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aeveco.Email.Service.Models
{
    public class SendGridOptions
    {
        public const string SendGrid = "SendGrid";

        public string SendGridId { get; set; }
        public string SendGridKey { get; set; }
        
    }
}
