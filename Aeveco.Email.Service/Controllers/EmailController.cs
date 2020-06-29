using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aeveco.Email.Service.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Aeveco.Email.Service.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        public EmailController(IOptions<SendGridOptions> optionsAccessor, IWebHostEnvironment env)
        {
            options = optionsAccessor;
            environment = env;
        }
        private readonly IWebHostEnvironment environment;

        private readonly IOptions<SendGridOptions> options; //set in the startup from the appsettings

        // GET: /<EmailController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            // truly being lazy just to call this.  I care more about sending the email than the semantics of this GET ;-)
            var emailSender = new Models.Email.HappyEmailMessage(options, environment);

            var recipients = new List<Models.IEmailContact>();
            recipients.Add(new Customer
            {
                FirstName = "Joe",
                LastName = "Cummins",
                EmailAddress = "zimjoe@gmail.com"
            });

            var sender = new Company
            {
                CompanyName = "Acme Inc.",
                EmailAddress = "Boss@acme.com"
            };

            emailSender.SendEmailAsync(recipients, sender, "subject", "Message");
            return new string[] { "value1", "value2" };
        }

        // GET /<EmailController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST /<EmailController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT /<EmailController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE /<EmailController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
