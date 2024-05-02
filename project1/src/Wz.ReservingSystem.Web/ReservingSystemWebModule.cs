using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Wz.ReservingSystem.EntityFrameworkCore;
using Wz.ReservingSystem.Localization;
using Wz.ReservingSystem.MultiTenancy;
using Wz.ReservingSystem.Web.Menus;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.Security.Claims;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.OpenIddict;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using Wz.ReservingSystem.CustomUserAppservice;
using Wz.ReservingSystem.CustomUserIAppservice;
using Wz.ReservingSystem.ReservingOperationAppservice;
using Wz.ReservingSystem.ReservingOperationIAppservice;

namespace Wz.ReservingSystem.Web;

[DependsOn(
    typeof(ReservingSystemHttpApiModule),
    typeof(ReservingSystemApplicationModule),
    typeof(ReservingSystemEntityFrameworkCoreModule),
    typeof(AbpAutofacModule),
    typeof(AbpIdentityWebModule),
    typeof(AbpSettingManagementWebModule),
    typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpTenantManagementWebModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule)
    )]
public class ReservingSystemWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.Services.AddTransient<CustomUserIAppservices, CustomUserAppservices>();
        context.Services.AddTransient<ReservingOperationIAppservices, ReservingOperationAppservices>();
        
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(ReservingSystemResource),
                typeof(ReservingSystemDomainModule).Assembly,
                typeof(ReservingSystemDomainSharedModule).Assembly,
                typeof(ReservingSystemApplicationModule).Assembly,
                typeof(ReservingSystemApplicationContractsModule).Assembly,
                typeof(ReservingSystemWebModule).Assembly
            );
        });
        
        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("ReservingSystem");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });

        if (!hostingEnvironment.IsDevelopment())
        {
            PreConfigure<AbpOpenIddictAspNetCoreOptions>(options =>
            {
                options.AddDevelopmentEncryptionAndSigningCertificate = false;
            });

            PreConfigure<OpenIddictServerBuilder>(serverBuilder =>
            {
                serverBuilder.AddProductionEncryptionAndSigningCertificate("openiddict.pfx", "00000000-0000-0000-0000-000000000000");
            });
        }
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        ConfigureAuthentication(context);
        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureNavigationServices();
        ConfigureAutoApiControllers();
        ConfigureCors(context);
        ConfigureSwaggerServices(context.Services);
        // ConfigureJwtAuthentication(context,configuration);
    }

//     private void ConfigureJwtAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
// {
//     context.Services.AddAuthentication(options =>
//         {
//             options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//             options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//         })
//         .AddJwtBearer(options =>
//         {
//             options.TokenValidationParameters =
//                 new TokenValidationParameters()
//                 {
//                     // 是否开启签名认证
//                     ValidateIssuerSigningKey = true,
//                     ValidateIssuer = true,
//                     ValidateAudience = true,
//                     ValidateLifetime = true,
//                     //ClockSkew = TimeSpan.Zero,
//                     ValidIssuer = configuration["Jwt:Issuer"],
//                     ValidAudience = configuration["Jwt:Audience"],
//                     IssuerSigningKey =
//                         new SymmetricSecurityKey(
//                             Encoding.ASCII.GetBytes(configuration["Jwt:SecurityKey"]))
//                 };
//
//             options.Events = new JwtBearerEvents
//             {
//                 OnMessageReceived = currentContext =>
//                 {
//                     var path = currentContext.HttpContext.Request.Path;
//                     if (path.StartsWithSegments("/login"))
//                     {
//                         return Task.CompletedTask;
//                     }
//
//                     var accessToken = string.Empty;
//                     if (currentContext.HttpContext.Request.Headers.ContainsKey("Authorization"))
//                     {
//                         accessToken = currentContext.HttpContext.Request.Headers["Authorization"];
//                         if (!string.IsNullOrWhiteSpace(accessToken))
//                         {
//                             accessToken = accessToken.Split(" ").LastOrDefault();
//                         }
//                     }
//
//                     if (accessToken.IsNullOrWhiteSpace())
//                     {
//                         accessToken = currentContext.Request.Query["access_token"].FirstOrDefault();
//                     }
//
//                     if (accessToken.IsNullOrWhiteSpace())
//                     {
//                         accessToken = currentContext.Request.Cookies["Wz.ReservingSystem.HttpApi"];
//                     }
//
//                     currentContext.Token = accessToken;
//                     currentContext.Request.Headers.Remove("Authorization");
//                     currentContext.Request.Headers.Add("Authorization", $"Bearer {accessToken}");
//
//                     return Task.CompletedTask;
//                 }
//             };
//         });
// }
    private void ConfigureCors(ServiceConfigurationContext context)
    {
        context.Services.AddCors(options =>
        {
            options.AddPolicy(name: "AllowSpecificOrigin",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context)
    {
        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
        context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ReservingSystemWebModule>();
        });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<ReservingSystemDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Wz.ReservingSystem.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<ReservingSystemDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Wz.ReservingSystem.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<ReservingSystemApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Wz.ReservingSystem.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<ReservingSystemApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Wz.ReservingSystem.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<ReservingSystemWebModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureNavigationServices()
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new ReservingSystemMenuContributor());
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(ReservingSystemApplicationModule).Assembly);
        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "ReservingSystem API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseCors("AllowSpecificOrigin");
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();

        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "ReservingSystem API");
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
