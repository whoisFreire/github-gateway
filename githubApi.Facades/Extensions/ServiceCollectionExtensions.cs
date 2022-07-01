using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using githubApi.Facades.Interfaces;
using githubApi.Facades.Strategies.ExceptionHandlingStrategies;
using githubApi.Models;
using githubApi.Models.UI;
using githubApi.Services;
using githubApi.Services.Interfaces;
using githubApi.Services.RestEase;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestEase;

using Serilog;
using Serilog.Exceptions;

namespace githubApi.Facades.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        private const string APPLICATION_KEY = "Application";
        private const string SETTINGS_SECTION = "Settings";

        /// <summary>
        /// Registers project's specific services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSingletons(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection(SETTINGS_SECTION).Get<ApiSettings>();
           

            // Dependency injection
            services.AddSingleton(settings)
                    .AddSingleton(settings.BlipBotSettings)
                    .AddSingleton<IJwtService, JwtService>()
                    .AddSingleton<IGithubReposFacade, GithubReposFacade>()
                    .AddSingleton<IJwtAuthenticationFacade, JwtAuthenticationFacade>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            var key = Encoding.ASCII.GetBytes(settings.JwtSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddSingleton(provider =>
            {
                var logger = provider.GetService<ILogger>();
                return new Dictionary<Type, ExceptionHandlingStrategy>
                {
                    { typeof(ApiException), new ApiExceptionHandlingStrategy(logger) },
                    { typeof(NotImplementedException), new NotImplementedExceptionHandlingStrategy(logger) }
                };
            });
            // SERILOG settings
            services.AddSingleton<ILogger>(new LoggerConfiguration()
                     .ReadFrom.Configuration(configuration)
                     .Enrich.WithMachineName()
                     .Enrich.WithProperty(APPLICATION_KEY, Constants.PROJECT_NAME)
                     .Enrich.WithExceptionDetails()
                     .CreateLogger());

            services.AddRestEaseClients(settings);
        }

        private static void AddRestEaseClients(this IServiceCollection services, ApiSettings settings)
        {
            var githubApiUrl = new Uri(settings.GithubApiSettings.ApiUrl);

            var githubClient = RestClient.For<IGithubClient>(new HttpClient()
            {
                BaseAddress = githubApiUrl
            });

            services.AddSingleton(githubClient);
        }
    }
}
