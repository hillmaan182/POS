using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceHelper
{
    public interface IHttpExtensionRepository 
    {
        Task<string> PostAsync(dynamic model, string uri, string apiKey);
    }
}
