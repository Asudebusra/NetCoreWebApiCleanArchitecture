using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.ExceptionHandler
{
    public class CriticalExceptionHandler(ILogger<CriticalExceptionHandler> logger) : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            if(exception is CriticalException)
            {
                //logger.LogCritical("");log yazdırmaya gerek yok dedi
                Console.WriteLine("hata ile ilgili sms gönderildi");
            }


            return ValueTask.FromResult(false);
            //return true; --> ben geriye bir hata döncem dto olarak 
            //return false; ---> bir sonraki hataya aktarmak istiyorum eğer varsa 
        }
    }
}
