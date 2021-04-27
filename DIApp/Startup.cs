using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DIApp.Services;

namespace DIApp
{
    public class Startup
    {
        private IServiceCollection _services;
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICounter, RandomCounter>();
            services.AddTimeService();
            services.AddScoped<CounterService>();
                _services = services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, TimeService timeService)
        {
            app.UseMiddleware<CounterMiddleware>();
            app.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>��� �������</h1>");
                sb.Append("<table>");
                sb.Append("<tr><th>���</th><th>Lifetime</th><th>����������</th></tr>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                sb.Append($"<H1>������� �����: {timeService?.GetTime()}</H1>");
                context.Response.ContentType = "text/html;charset=utf-8";
                
                await context.Response.WriteAsync(sb.ToString());
            });
        }
        
    }
}
