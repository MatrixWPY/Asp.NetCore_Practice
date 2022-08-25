using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using WebApi.Commands.Instance;
using WebApi.Commands.Interface;
using WebApi.Middlewares;
using WebApi.Services.Instance;
using WebApi.Services.Interface;

namespace WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddJsonOptions(opt =>
                    {
                        //�����w�]JsonNamingPolicy.CamelCase
                        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                        //������r���s�X
                        opt.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                    });

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                // ����ModelStateInvalidFilter => �ϥΦۭqValidFilter:ValidRequest
                opt.SuppressModelStateInvalidFilter = true;
            });

            #region ���UCommand
            services.AddTransient<IContactInfoCommand, ContactInfoCommand>();
            #endregion

            #region ���UService
            services.AddTransient<IContactInfoService, ContactInfoService>();
            #endregion

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

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            #region �O���ǤJ�Ѽ�
            app.UseLogRequestMiddleware();
            #endregion

            #region �O���ǥX�Ѽ�
            app.UseLogResponseMiddleware();
            #endregion

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
