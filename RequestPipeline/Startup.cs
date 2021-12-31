using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RequestPipeline.Extensions;
using RequestPipeline.Middlewares;

namespace RequestPipeline
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //đây là middleware
                app.UseDeveloperExceptionPage();
            }
            //đây là middleware và trả dữ liệu cho middle next
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("<div>Hello word from the middleware 1</div>");
                //hàm này là gọi tới middleware tiếp theo
                await next.Invoke();
                await context.Response.WriteAsync("<div>Returning Hello word from the middleware 1</div>");
            });

            //đây là middleware và trả dữ liệu cho middle next
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("<div>Hello word from the middleware 2</div>");
                //hàm này là gọi tới middleware tiếp theo
                await next.Invoke();
                await context.Response.WriteAsync("<div>Returning Hello word from the middleware 2</div>");
            });

            app.UseSimpleMiddleware();

            //đây là middleware terminal (middleware cuối cùng và trả response ngược lại)
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("<div>Hello word from the middleware last</div>");
            });
            // result: 
                //Hello word from the middleware 1
                //Hello word from the middleware 2
                //Hello word from the middleware
                //Hello word from the middleware last
                //Bye from Hello word from the middleware
                //Returning Hello word from the middleware 2
                //Returning Hello word from the middleware 1
        }
    }
}
