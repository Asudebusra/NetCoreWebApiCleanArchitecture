using App.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace CleanApp.API.ExceptionHandler
{
    public class CriticalExceptionHandler(ILogger<CriticalExceptionHandler> logger) : IExceptionHandler
    {
        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            if (exception is CriticalException)
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
