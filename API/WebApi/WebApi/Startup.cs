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
                        //取消預設JsonNamingPolicy.CamelCase
                        opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                        //維持原字元編碼
                        opt.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);
                    });

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                // 停用ModelStateInvalidFilter => 使用自訂ValidFilter:ValidRequest
                opt.SuppressModelStateInvalidFilter = true;
            });

            #region 註冊Command
            services.AddTransient<IContactInfoCommand, ContactInfoCommand>();
            #endregion

            #region 註冊Service
            services.AddTransient<IContactInfoService, ContactInfoService>();
            #endregion

            #region 註冊Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(
                    // name: 攸關 SwaggerDocument 的 URL 位置。
                    name: "v1",
                    // info: 是用於 SwaggerDocument 版本資訊的顯示(內容非必填)。
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

                // XML 檔案: 文件註解標籤
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

            #region 記錄傳入參數
            app.UseLogRequestMiddleware();
            #endregion

            #region 記錄傳出參數
            app.UseLogResponseMiddleware();
            #endregion

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region 使用SwaggerUI
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(
                    // url: 需配合 SwaggerDoc 的 name。 "/swagger/{SwaggerDoc name}/swagger.json"
                    url: "/swagger/v1/swagger.json",
                    // name: 用於 Swagger UI 右上角選擇不同版本的 SwaggerDocument 顯示名稱使用。
                    name: "API Document V1"
                );
            });
            #endregion
        }
    }
}
