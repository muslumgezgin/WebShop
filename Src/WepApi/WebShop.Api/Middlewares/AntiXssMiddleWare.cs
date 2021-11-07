using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ganss.XSS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using WepShop.Application.Behaviours;
using WepShop.Application.Exceptions;

namespace WebShop.Api.Middlewares
{
    public class AntiXssMiddleWare
    {
        private readonly RequestDelegate _next;

        public AntiXssMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            // enable buffering so that the request can be read by the model binders next
            httpContext.Request.EnableBuffering();
            
            // leaveOpen: true to leave the stream open after disposing,
            // so it can be read by the model binders
            using (var streamReader = new StreamReader
                (httpContext.Request.Body,Encoding.UTF8,leaveOpen:true))
            {
                var raw = await streamReader.ReadToEndAsync();
                var sanitiser = new HtmlSanitizer();
                var sanitised = sanitiser.Sanitize(raw);

                if (raw != sanitised)
                {
                    throw new BadRequestException("Xss injection detected from middleware");
                }

            }

            httpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            await _next.Invoke(httpContext);
        }
    }

    public static class AntiXssMiddlewareExtensions
    {
        public static IApplicationBuilder UseAntiXssMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AntiXssMiddleWare>();
        }
    }
}