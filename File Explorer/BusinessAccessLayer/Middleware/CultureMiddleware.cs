    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using System.Globalization;
    using System.Threading.Tasks;

namespace BusinessAccessLayer.Middleware
{

    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var cultureCookie = context.Request.Cookies["culture"];
            if (!string.IsNullOrEmpty(cultureCookie))
            {
                var culture = new CultureInfo(cultureCookie);
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }

                await _next(context);
        }
    }

    public static class CultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseCultureMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CultureMiddleware>();
        }
    }

}
