using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Roy.Core.AuthHelper;
using Roy.Core.AuthHelper.JWT;
using Roy.Core.AuthHelper.JWT.SecurityDemo.Authentication.JWT.AuthHelper;
using Roy.Core.AuthHelper.OverWrite;
using Roy.Core.IServices;
using Roy.Core.Middware.Filter;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using static Roy.Core.SwaggerHelper.CustomApiVersion;

namespace Roy.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        //JWT密钥
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(SecretKey));
        /// <summary>
        /// 构造函数
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

        private const string ApiName = "Roy.Core";

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddMemoryCache();

            services.AddMvc(options =>
            {
                options.Filters.Add<ValidataModelFilter>();
            });

            #region CORS
            services.AddCors(c =>
            {
                ////一般采用这种方法
                c.AddPolicy("AllowDomain", policy =>
                {
                    policy
                    .WithOrigins("http://localhost:8888", "http://localhost:8080")//支持多个域名端口
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });

            });
            #endregion

            #region Swagger 
            services.AddSwaggerGen(c=> {
                //遍历出全部的版本，做文档信息展示
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new Info
                    {
                        // {ApiName} 定义成全局变量，方便修改
                        Version = version,
                        Title = $"{ApiName} 接口文档",
                        Description = $"{ApiName} HTTP API " + version,
                        //TermsOfService = "None",
                        //Contact = new Contact { Name = "Roy.Core", Email = "www.zengzhongzhong@formssi.com", Url = "" }
                    });
                });

                //WebApi的备注说明
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Roy.Core.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

                var xmlModelPath = Path.Combine(basePath, "Roy.Core.Model.xml");//这个就是Model层的xml文件名
                c.IncludeXmlComments(xmlModelPath);

                #region Token绑定到ConfigureServices
                //添加header验证信息
                //c.OperationFilter<SwaggerHeader>();
                var security = new Dictionary<string, IEnumerable<string>> { { "Bearer", new string[] { } }, };
                c.AddSecurityRequirement(security);
                //方案名称“Roy.Core”可自定义，上下一致即可
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {auth_token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = "header"//jwt默认存放Authorization信息的位置(请求头中)
                });
                #endregion
            });
            #endregion

            #region Token服务注册            

            #region 认证
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });           

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
             {
                 configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                 configureOptions.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
                     ValidateAudience = true,
                     ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = _signingKey,
                     RequireExpirationTime = false,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero
                 };
                 configureOptions.SaveToken = true;
             });

            //services.AddAuthentication("Bearer").AddIdentityServerAuthentication(option=> {
            //    option.Authority = "http://localhost:5000";
            //    option.RequireHttpsMetadata = false;
            //    option.ApiName = "api";
            //});
            #endregion

            #region 授权
             services.AddAuthorization(options =>
             {
                 //options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                 //options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                 //options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));

                 options.AddPolicy("Permssion", policy => policy.Requirements.Add(new PermissionRequirement()));
             });
            services.AddSingleton<IAuthorizationHandler, PermissionHander>();
            #endregion
            #endregion

            #region AautoFac
            //实例化 AutoFac  容器   
            var builder = new ContainerBuilder();
            ////注册要通过反射创建的组件
            //builder.RegisterType<FunctionServices>().As<IModuleService>();
            //var assemblysServices = Assembly.Load("Roy.Core.Services");
            var baseAbPath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
            var servicesDllFile = Path.Combine(baseAbPath, "Roy.Core.Services.dll");//获取注入项目绝对路径
            var assemblysServices = Assembly.LoadFile(servicesDllFile);//直接采用加载文件的方法
            builder.RegisterAssemblyTypes(assemblysServices).AsImplementedInterfaces();

            var repositoryDllFile = Path.Combine(baseAbPath, "Roy.Core.Repository.dll");
            var assemblysRepository = Assembly.LoadFile(repositoryDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();


            //将services填充到Autofac容器生成器中
            builder.Populate(services);

            //使用已进行的组件登记创建新容器
            var ApplicationContainer = builder.Build();
            #endregion

            return new AutofacServiceProvider(ApplicationContainer);//第三方IOC接管 core内置DI容器
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                #region Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                    //c.RoutePrefix = "";//路径配置，设置为空，表示直接访问该文件
                });
                #endregion
            }

            //app.UseMiddleware<JwtTokenAuth>();中间件形式认证
            app.UseAuthentication();

            //添加中间间到应用程序中，允许跨域请求
            app.UseCors("AllowDomain");

            app.UseMvc();
        }
    }
}

