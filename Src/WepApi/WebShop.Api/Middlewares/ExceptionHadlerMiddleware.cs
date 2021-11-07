using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WepShop.Application.Behaviours;
using WepShop.Application.Exceptions;
using WepShop.Application.Wrappers;

namespace WebShop.Api.Middlewares
{
    public class ExceptionHadlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHadlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            Response response;

            switch (ex)
            {
                case  ValidationException validationException :
                    code = HttpStatusCode.BadRequest;
                    response = new Response(nameof(ValidationException), validationException.Failures);
                    result = JsonConvert.SerializeObject(response);
                    break;
                case NotFoundException notFoundException :
                    code = HttpStatusCode.NotFound;
                    response = new Response(nameof(NotFoundException), new Dictionary<string, string[]>()
                    {
                        {"",new string[]{notFoundException.Message}}
                    });
                    result = JsonConvert.SerializeObject(response);
                    break;
                case BadRequestException badRequestException :
                    code = HttpStatusCode.BadRequest;
                    response = new Response(nameof(BadRequestException), new Dictionary<string, string[]>()
                    {
                        { "", new string[]{ badRequestException.Message }}
                    });
                    result = JsonConvert.SerializeObject(response);
                    break;
                default:
                    code = HttpStatusCode.BadRequest;
                    response = new Response(nameof(Exception), new Dictionary<string, string[]>()
                    {
                        {"" ,new string[]{ ex.Message }}
                    });

                    result = JsonConvert.SerializeObject(response);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonConvert.SerializeObject(new { error = ex.Message });
            }

            return context.Response.WriteAsync(result);
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExeptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHadlerMiddleware>();
        }
    }
}