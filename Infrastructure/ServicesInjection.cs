using System.Reflection;
using System.Text;
using Amazon.Runtime;
using Amazon.S3;
using Application.Contracts;
using Infrastructure.EmailSender;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Scrutor;

namespace Infrastructure;

public static class ServicesInjection
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailSender, MailgunService>();
        
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString, opt =>
            {
                opt.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
            });
        });

        services.Scan(selector => selector
            .FromAssemblies(Assembly.LoadFrom(typeof(ServicesInjection).Assembly.Location))
            .AddClasses()
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime()
        );
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!))
                };
            }
        );
        
        // S3 Settings
        var awsOptions = new AmazonS3Config
        {
            ServiceURL = configuration["AWS:ServiceUrl"]
        };
        
        var awsCredentials = new BasicAWSCredentials(
            configuration["AWS:AccessKey"],
            configuration["AWS:SecretKey"]
        );
        var s3Client = new AmazonS3Client(awsCredentials, awsOptions);
        services.AddSingleton<IAmazonS3>(s3Client);
        services.AddHostedService<BackgroundService>();

    }
}