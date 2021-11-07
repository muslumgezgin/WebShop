using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace WebShop.Api.Middlewares
{
    public class AntiXss2Middleware
    {
        private readonly RequestDelegate _next;
        private ErrorResponse _error;
        private readonly int statusCode = (int)HttpStatusCode.BadRequest;

        public AntiXss2Middleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }


        public async Task Invoke(HttpContext context)
        {
            if (!string.IsNullOrWhiteSpace(context.Request.Path.Value))
            {
                var url = context.Request.Path.Value;
                if (CrossSiteScriptingValidation.IsDangerousString(url, out _))
                {
                    await ResponseWithAnError(context).ConfigureAwait(false);
                    return;
                }
            }

            var orginalBody = context.Request.Body;
            try
            {
                var content = await ReadRequestBody(context);

                if (CrossSiteScriptingValidation.IsDangerousString(content, out _))
                {
                    await ResponseWithAnError(context).ConfigureAwait(false);
                    return;
                }

                await _next(context).ConfigureAwait(false);
            }
            finally
            {
                context.Request.Body = orginalBody;
            }
        }

        private static async Task<string> ReadRequestBody(HttpContext context)
        {
            var buffer = new MemoryStream();
            await context.Request.Body.CopyToAsync(buffer);
            context.Request.Body = buffer;
            buffer.Position = 0;

            var encoding = Encoding.UTF8;

            var requestContent = await new StreamReader(buffer, encoding).ReadToEndAsync();
            context.Request.Body.Position = 0;
            return requestContent;
        }

        private async Task ResponseWithAnError(HttpContext context)
        {
            context.Response.Clear();
            context.Response.Headers.AddHeaders();
            context.Response.ContentType = "application/json; charset=utf-8";
            context.Response.StatusCode = statusCode;

            _error ??= new ErrorResponse()
            {
                ErrorCode = 500,
                Description = "Error from AntiXss2Middleware"
            };

            await context.Response.WriteAsync(_error.ToJSON());
        }
    }

    public static class AntiXss2MiddlewareExtension
    {
        public static IApplicationBuilder UseAntiXss2Middleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AntiXss2Middleware>();
        }
    }

    /// <summary>
    /// Imported from System.Wep.CrossSiteScriptingValidation Class
    /// </summary>
    public static class CrossSiteScriptingValidation
    {
        private static readonly char[] StartingChars = { '<', '&' };

        #region Public methods

        public static bool IsDangerousString(string s, out int matchIndex)
        {
            matchIndex = 0;

            for (int i = 0; ;)
            {
                var n = s.IndexOfAny(StartingChars, i);

                // If it's the last char, it's safe 
                if (n < 0) return false;

                // If it's the last char, it's safe 
                if (n == s.Length - 1) return false;

                matchIndex = n;

                switch (s[n])
                {
                    case  '<':
                        if (IsAtoZ(s[n + 1]) || s[n + 1] == '!' || s[n + 1] == '/' || s[n + 1] == '?')
                            return true;
                        break;
                    case  '&':
                        if (s[n + 1] == '#') return true;
                        break;
                }

                i = n + 1;
            }
        }

        #endregion

        #region Private methods

        private static bool IsAtoZ(char c)
        {
            return c is >= 'a' and <= 'z'or >= 'A' and <= 'Z';
        }

        #endregion

        public static void AddHeaders(this IHeaderDictionary headers)
        {
            if (headers["P#P"].IsNullOrEmpty())
            {
                headers.Add("P3P", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
            }
        }

        public static bool  IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        public static string ToJSON(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }

    }

    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string Description { get; set; }
    }
}