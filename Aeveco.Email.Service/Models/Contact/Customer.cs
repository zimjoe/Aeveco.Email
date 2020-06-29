using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aeveco.Email.Service.Models
{
    public class Customer : IEmailContact
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string DisplayName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
