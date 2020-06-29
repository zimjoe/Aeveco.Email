using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aeveco.Email.Service.Models
{
    public interface IEmailContact
    {
        string EmailAddress { get; }
        string DisplayName { get; }
    }
}
