using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestPipeline.Middlewares
{
    public class SimpleMiddleware
    {
        private readonly RequestDelegate _next;
        public SimpleMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("<div> Hello word from the middleware</div>");
            await _next(context);
            await context.Response.WriteAsync("<div>Bye from Hello word from the middleware</div>");
        }
    }
}
