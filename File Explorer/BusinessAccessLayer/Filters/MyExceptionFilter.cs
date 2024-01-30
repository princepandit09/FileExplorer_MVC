using File_Explorer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
//using System.Web.Mvc;

namespace BusinessAccessLayer.Filters
{
    public class MyExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<MyExceptionFilter> _logger;

        public MyExceptionFilter(ILogger<MyExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            
            _logger.LogError(context.Exception, context.Exception.Message);

          
            // Optionally, you can set the result of the action
             context.Result = new StatusCodeResult(500); // Or any other appropriate result
            context.Result = new RedirectToActionResult("Error","Home",null);
            context.ExceptionHandled = true; // Mark the exception as handled
        }
    }
}
