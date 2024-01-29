using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Middleware
{
    public class GlobalExceptionHandler
    {
        public RequestDelegate _nextMiddleware { get; }
        public ILogger<GlobalExceptionHandler> _logger { get; }

        public GlobalExceptionHandler(RequestDelegate requestDelegate, ILogger<GlobalExceptionHandler> logger)
        {
            _nextMiddleware = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _nextMiddleware(httpContext);
            }
            catch (Exception ex)
            {
       

                _logger.LogError(ex.Message);



                string LogPath = @"D:\Techpro\Git FileExplorer\Log File";
                string FileName = "ErrorLog_" + DateTime.Now.ToString("dd/MM/yyyy").Replace("/", "_");
                string LogFullPath = Path.Combine(LogPath, FileName);

                FileStream stream = new FileStream(LogFullPath, FileMode.Append);
                StreamWriter writer = new StreamWriter(stream);
                string FileContent = "---------------------------------------Error Information---------------------------------------" + Environment.NewLine;
                FileContent += "DateTime: " + DateTime.Now.ToString() + Environment.NewLine;
                FileContent += "Error Message: " + ex.Message + Environment.NewLine;
                FileContent += "Error Source: " + ex.StackTrace + Environment.NewLine;
                FileContent += "Deep Information: " + ex.InnerException + Environment.NewLine;
                FileContent += "Deep Information: " + ex.InnerException + Environment.NewLine;

                writer.WriteLine(FileContent);
                writer.Close();

                // Redirect to an error page
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsync("An unexpected error occurred. Please try again later after some time :)");


            }
        }

    }
}
