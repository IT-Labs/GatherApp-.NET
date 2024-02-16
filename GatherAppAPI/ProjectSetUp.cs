using Azure.Storage.Blobs;
using FluentValidation.AspNetCore;
using GatherApp.DataContext;
using GatherApp.Repositories;
using GatherApp.Repositories.Impl;
using GatherApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using GatherApp.API.Middleware;
using Microsoft.AspNetCore.Authorization;
using GatherApp.Contracts.Configuration;
using GatherApp.Contracts.Enums;
using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using GatherApp.API.Filters;

namespace GatherApp.API
{
    public static class ProjectSetUp
    {
        
        public static void SetUpDI(this WebApplicationBuilder builder)
        {
            ConfigDI(builder);
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IEventRepository, EventRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();
            builder.Services.AddScoped<IInvitationRepository, InvitationRepository>();
            builder.Services.AddScoped<IRoleRepository, RoleRepository>();
            builder.Services.AddScoped<IEmailRepository, EmailRepository>();
            builder.Services.AddScoped<ITokenRepository, TokenRepository>();
            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddTransient<IEventService, EventService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IInvitationService, InvitationService>();
            builder.Services.AddTransient<IJwtService, JwtService>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IValidateEmailService, ValidateEmailService>();
            builder.Services.AddTransient<IFileService, FileService>();
            builder.Services.AddTransient<IPasswordService, PasswordService>();
            builder.Services.AddTransient<ICountryService, CountryService>();
            builder.Services.AddTransient<IEmailService, EmailService>();

            builder.Services.AddSingleton<ILoggingService, LoggingService>(x => new LoggingService(builder.Configuration));
            builder.Services.AddScoped<InvitationResponseActionFilter>();
            builder.Services.AddScoped<MyEventResponseActionFilter>();
            builder.Services.AddScoped<OwnEventOrAdminActionFilter>();
        }

        public static void ConfigDI(WebApplicationBuilder builder)
        {
            var emailSettings = builder.Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            var urls = builder.Configuration.GetSection("Urls").Get<Urls>();
            var blobStorageSettings = builder.Configuration.GetSection("BlobStorageSettings").Get<BlobStorageSettings>();
            var certificate = builder.Configuration.GetSection("Certificate").Get<Certificate>();
            var jwt = builder.Configuration.GetSection("JWT").Get<JWT>();
            var keyVault = builder.Configuration.GetSection("AzureKeyVaultSettings").Get<AzureKeyVaultSettings>();

            builder.Services.AddTransient(_ => emailSettings);
            builder.Services.AddTransient(_ => jwt);
            builder.Services.AddTransient(_ => certificate);
            builder.Services.AddTransient(_ => blobStorageSettings);
            builder.Services.AddTransient(_ => urls);
            builder.Services.AddTransient(_ => keyVault);
        }

        public static void SetupCorsPolicy(this WebApplicationBuilder builder)
        {
            var urls = builder.Configuration.GetSection("Urls").Get<Urls>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("corsPolicy", builder => builder
                    .WithOrigins(urls.UiUrl)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true)
                );
            });
        }

        public static void SetUpAPI(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "GatherAppJWT", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            

        }
        public static void AddCustomRoleAuthorizationPolicy(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IAuthorizationHandler, CustomRoleAuthorizationHandler>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(nameof(AuthorizationPolicyEnum.Admin), policy =>
                {
                    policy.Requirements.Add(new CustomRoleAuthorizationRequirement(RoleEnum.Admin.ToString()));
                });
            });
        }

        public static void SetUpAzureBlobStorage(this WebApplicationBuilder builder, IConfiguration configuration) 
        {
            builder.Services.AddScoped(_ =>
            {
                return new BlobServiceClient(configuration.GetSection("BlobStorageSettings")["AzureStorage"]);
            });
        }

        public static void SetUpDB(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddDbContext<GatherAppContext>(
            opt => opt.UseNpgsql(configuration.GetConnectionString("WebApiDatabase")));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public static void SetUpAzureKeyVault(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            bool useAzureKeyVault = Convert.ToBoolean(configuration.GetSection("AzureKeyVaultSettings:UseAzureKeyVault").Value);

            if (useAzureKeyVault)
            {
                string keyVaultName = configuration.GetSection("AzureKeyVaultSettings:KeyVaultName").Value;
                string azureTenantId = configuration.GetSection("AzureKeyVaultSettings:AzureTenantId").Value;
                string azureClientId = configuration.GetSection("AzureKeyVaultSettings:AzureClientId").Value;
                string azureClientSecretId = configuration.GetSection("AzureKeyVaultSettings:AzureClientSecretId").Value;

                var credential = new ClientSecretCredential(azureTenantId, azureClientId, azureClientSecretId);

                builder.Configuration.AddAzureKeyVault(new Uri(keyVaultName), credential, new AzureKeyVaultConfigurationOptions());
            }
        }
    }
}
