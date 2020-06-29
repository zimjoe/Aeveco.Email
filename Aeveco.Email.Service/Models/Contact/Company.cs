using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aeveco.Email.Service.Models
{
    public class Company : IEmailContact
    {
        public string CompanyName { get; set; }

        public string EmailAddress { get; set; }

        public string DisplayName
        {
            get
            {
                return CompanyName;
            }
        }
    }
}
