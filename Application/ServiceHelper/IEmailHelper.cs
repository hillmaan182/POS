using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceHelper
{
    public interface IEmailHelper
    {
        Task<string> SendEmail(string toEmail, string mailSubject, StringBuilder mailBody,IConfiguration config);

        StringBuilder SetBodyEmailActivateAccount(string link);
    }
}
