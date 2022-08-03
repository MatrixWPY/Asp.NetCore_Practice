using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using WebApi.Commands.Instance;
using WebApi.Commands.Interface;
using WebApi.Services.Instance;
using WebApi.Services.Interface;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region ���UCommand
            services.AddTransient<IContactInfoCommand, ContactInfoCommand>();
            #endregion

            #region ���UService
            services.AddTransient<IContactInfoService, ContactInfoService>();
            #endregion

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                // ����ModelStateInvalidFilter => �ϥΦۭqValidFilter:ValidRequest
                opt.SuppressModelStateInvalidFilter = true;
            });

            #region ���USwagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    // name: ���� SwaggerDocument �� URL ��m�C
                    name: "v1",
                    // info: �O�Ω� SwaggerDocument ������T�����(���e�D����)�C
                    info: new OpenApiInfo
                    {
                        Title = "WebApi",
                        Version = "1.0.0",
                        Description = "This is ASP.NET Core API Sample.",
                        Contact = new OpenApiContact
                        {
                            Name = "Matrix",
                            Email = string.Empty
                        }
                    }
                );

                // XML �ɮ�: �����Ѽ���
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "WebApi.xml");
                c.IncludeXmlComments(xmlPath);
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region �ϥ�SwaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    // url: �ݰt�X SwaggerDoc �� name�C "/swagger/{SwaggerDoc name}/swagger.json"
                    url: "/swagger/v1/swagger.json",
                    // name: �Ω� Swagger UI �k�W����ܤ��P������ SwaggerDocument ��ܦW�٨ϥΡC
                    name: "API Document V1"
                );
            });
            #endregion
        }
    }
}
